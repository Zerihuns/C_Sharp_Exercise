namespace DI
{
    /// <summary>
    /// Basic Id generator. This service is not thread safe.
    /// For thread safe implementations you can use locks or <see cref="System.Threading.Interlocked.Increment(ref int)"/>
    /// </summary>
    class IdGenerator
    {
        public IdGenerator(int start = 0)
        {
            LastId = start;
        }

        public int LastId { get; private set; }
        public int Next() => ++LastId;
    }
}
