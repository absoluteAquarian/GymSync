using System.Linq.Expressions;

namespace GymSync_Interface_v2.Models {
	public interface IQueryKeyable<TSelf, TKey> where TSelf : IQueryKeyable<TSelf, TKey> {
		static abstract Expression<Func<TSelf, TKey>> GetPrimaryKey();
	}
}
