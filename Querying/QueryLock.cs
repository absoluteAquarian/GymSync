namespace GymSync.Querying {
	public struct QueryLock : IDisposable {
		private int _state;
		private const int AVAILABLE = 0, LOCKED = 1;

		public QueryLock Acquire() {
			while (Interlocked.Exchange(ref _state, LOCKED) == LOCKED)
				Thread.Yield();

			return this;
		}

		public void Dispose() {
			Interlocked.Exchange(ref _state, AVAILABLE);
		}
	}
}
