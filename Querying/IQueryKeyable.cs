using System.Linq.Expressions;

namespace GymSync.Models {
	public interface IQueryKeyable<TSelf, TKey> where TSelf : IQueryKeyable<TSelf, TKey> {
		static abstract Expression<Func<TSelf, TKey>> GetPrimaryKey();
	}
}
