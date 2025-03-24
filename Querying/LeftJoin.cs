using System.Linq.Expressions;
using System.Reflection;

namespace GymSync {
	partial class QueryExtensions {
		// The following implementations were taken from a StackOverflow answer: https://stackoverflow.com/a/35414717
		// Since the original code was very unoptimized, changes were made to improve performance and readability
		#region Intermediate Data
		private class KeyValuePairHolder<T1, T2> {
			public T1 Item1 { get; set; }
			public T2 Item2 { get; set; }

			private static MemberInfo? _propertyItem1;
			public static MemberInfo PropertyItem1 => _propertyItem1 ??= typeof(KeyValuePairHolder<T1, T2>).GetProperty(nameof(Item1))!;

			private static MemberInfo? _propertyItem2;
			public static MemberInfo PropertyItem2 => _propertyItem2 ??= typeof(KeyValuePairHolder<T1, T2>).GetProperty(nameof(Item2))!;
		}
		#endregion

		#region Expression Rewriter
		private class ResultSelectorRewriter<TOuter, TInner, TResult> : ExpressionVisitor {
			private readonly Expression<Func<TOuter, TInner, TResult>> resultSelector;
			public Expression<Func<KeyValuePairHolder<TOuter, IEnumerable<TInner>>, TInner, TResult>> CombinedExpression { get; private set; }

			private ParameterExpression OldTOuterParamExpression;
			private ParameterExpression OldTInnerParamExpression;
			private ParameterExpression NewTOuterParamExpression;
			private ParameterExpression NewTInnerParamExpression;

			public ResultSelectorRewriter(Expression<Func<TOuter, TInner, TResult>> resultSelector) {
				this.resultSelector = resultSelector;
				this.OldTOuterParamExpression = resultSelector.Parameters[0];
				this.OldTInnerParamExpression = resultSelector.Parameters[1];

				this.NewTOuterParamExpression = Expression.Parameter(typeof(KeyValuePairHolder<TOuter, IEnumerable<TInner>>));
				this.NewTInnerParamExpression = Expression.Parameter(typeof(TInner));

				var newBody = this.Visit(this.resultSelector.Body);
				var combinedExpression = Expression.Lambda(newBody, [this.NewTOuterParamExpression, this.NewTInnerParamExpression]);
				this.CombinedExpression = (Expression<Func<KeyValuePairHolder<TOuter, IEnumerable<TInner>>, TInner, TResult>>)combinedExpression;
			}

			protected override Expression VisitParameter(ParameterExpression node) {
				if (node == this.OldTInnerParamExpression)
					return this.NewTInnerParamExpression;
				else if (node == this.OldTOuterParamExpression)
					return Expression.PropertyOrField(this.NewTOuterParamExpression, "Item1");
				else
					throw new InvalidOperationException("Did not expect a parameter: " + node);
			}
		}
		#endregion

		private class QueryReflectionMethods {
			#region Caches
			private static class EnumerableReference<TSource> {
				public static readonly MethodInfo DefaultIfEmpty;

				static EnumerableReference() {
					// Hack LINQ Expressions to get the method
					// Type.GetMethod() will not suffice for generic methods, unfortunately
					Expression<Func<IEnumerable<TSource>, IEnumerable<TSource?>>> defaultIfEmpty = source => source.DefaultIfEmpty();
					DefaultIfEmpty = (defaultIfEmpty.Body as MethodCallExpression)?.Method
						?? throw new InvalidOperationException("Could not find the DefaultIfEmpty method in the Enumerable class.");
				}
			}

			private static class QueryableReference<TSource, TCollection, TResult> {
				public static readonly MethodInfo SelectMany;

				static QueryableReference() {
					// Hack LINQ Expressions to get the method
					// Type.GetMethod() will not suffice for generic methods, unfortunately
					Expression<Func<IQueryable<TSource>, Expression<Func<TSource, IEnumerable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>, IEnumerable<TResult>>> selectMany = (source, collectionSelector, resultSelector) => source.SelectMany(collectionSelector, resultSelector);
					SelectMany = (selectMany.Body as MethodCallExpression)?.Method
						?? throw new InvalidOperationException("Could not find the SelectMany method in the Queryable class.");
				}
			}

			private static class QueryableReference<TOuter, TInner, TKey, TResult> {
				public static readonly MethodInfo GroupJoin;
				public static readonly MethodInfo Join;

				static QueryableReference() {
					// Hack LINQ Expressions to get the methods
					// Type.GetMethod() will not suffice for generic methods, unfortunately
					Expression<Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, IEnumerable<TInner>, TResult>>, IQueryable<TResult>>> groupJoin = (outer, inner, outerKeySelector, innerKeySelector, resultSelector) => outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector);
					GroupJoin = (groupJoin.Body as MethodCallExpression)?.Method
						?? throw new InvalidOperationException("Could not find the GroupJoin method in the Queryable class.");

					Expression<Func<IQueryable<TOuter>, IEnumerable<TInner>, Expression<Func<TOuter, TKey>>, Expression<Func<TInner, TKey>>, Expression<Func<TOuter, TInner, TResult>>, IQueryable<TResult>>> join = (outer, inner, outerKeySelector, innerKeySelector, resultSelector) => outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector);
					Join = (join.Body as MethodCallExpression)?.Method
						?? throw new InvalidOperationException("Could not find the Join method in the Queryable class.");
				}
			}

			private static class ExpressionCache<TOuter, TInner> {
				public static Expression? GroupJoinSelector;
				public static Expression? SelectManyCollectionSelector;
			}
			#endregion

			public static IQueryable<TResult> CreateLeftOuterJoin<TOuter, TInner, TKey, TResult>(
				IQueryable<TOuter> outer,
				IQueryable<TInner> inner,
				Expression<Func<TOuter, TKey>> outerKeySelector,
				Expression<Func<TInner, TKey>> innerKeySelector,
				Expression<Func<TOuter, TInner, TResult>> resultSelector
			) {
				ref var selector = ref ExpressionCache<TOuter, TInner>.GroupJoinSelector;
				if (selector is null) {
					var paramOuter = Expression.Parameter(typeof(TOuter));
					var paramInner = Expression.Parameter(typeof(IEnumerable<TInner>));

					selector = Expression.Lambda(
						Expression.MemberInit(
							Expression.New(typeof(KeyValuePairHolder<TOuter, IEnumerable<TInner>>)),
							Expression.Bind(KeyValuePairHolder<TOuter, IEnumerable<TInner>>.PropertyItem1, paramOuter),
							Expression.Bind(KeyValuePairHolder<TOuter, IEnumerable<TInner>>.PropertyItem2, paramInner)
						),
						paramOuter,
						paramInner
					);
				}

				ref var collectionSelector = ref ExpressionCache<TOuter, TInner>.SelectManyCollectionSelector;
				if (collectionSelector is null) {
					var paramGroup = Expression.Parameter(typeof(KeyValuePairHolder<TOuter, IEnumerable<TInner>>));
					collectionSelector = Expression.Lambda(
						Expression.Call(
							null,
							EnumerableReference<TInner>.DefaultIfEmpty,
							Expression.MakeMemberAccess(paramGroup, KeyValuePairHolder<TOuter, IEnumerable<TInner>>.PropertyItem2)
						),
						paramGroup
					);
				}
				
				var groupJoin = QueryableReference<TOuter, TInner, TKey, KeyValuePairHolder<TOuter, IEnumerable<TInner>>>.GroupJoin.Invoke( null, [ outer, inner, outerKeySelector, innerKeySelector, selector ]);

				Expression newResultSelector = new ResultSelectorRewriter<TOuter, TInner, TResult>(resultSelector).CombinedExpression;

				return (IQueryable<TResult>)QueryableReference<KeyValuePairHolder<TOuter, IEnumerable<TInner>>, TInner, TResult>.SelectMany.Invoke(null, [ groupJoin, collectionSelector, newResultSelector ])!;
			}
		}
	}
}
