using System.Linq.Expressions;

namespace GymSync.Querying {
	public interface IQueryKeyable<TSelf, TKey> where TSelf : IQueryKeyable<TSelf, TKey> {
		static abstract Expression<Func<TSelf, TKey>> GetPrimaryKey();
	}
}
