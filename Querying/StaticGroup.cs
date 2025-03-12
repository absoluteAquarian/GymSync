using System.Collections;

namespace GymSync.Querying {
	public record class StaticGroup<TKey, TElement>(TKey Key, IEnumerable<TElement> Elements) : IGrouping<TKey, TElement> {
		TKey IGrouping<TKey, TElement>.Key => Key;

		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator() => Elements.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();
	}
}
