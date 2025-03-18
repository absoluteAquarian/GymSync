using System.Linq.Expressions;

namespace GymSync_Interface_v2.Models {
	public interface ICrossReference<TSelf> : IQueryKeyable<TSelf, int> where TSelf : ICrossReference<TSelf> {
		static abstract Expression<Func<TSelf, int>> GetForeignKey();
	}

	public interface ICrossReferencePrimary<T> where T : IQueryKeyable<T, int> { }

	public interface ICrossReferenceForeign<T> where T : IQueryKeyable<T, int> { }
}
