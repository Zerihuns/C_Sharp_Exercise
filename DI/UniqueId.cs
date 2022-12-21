using System.Collections.Generic;

namespace DI
{
    /// <summary>
    /// Unique Id provider. For thread safe implementations you can use <see cref="System.Collections.Concurrent.ConcurrentDictionary{TKey, TValue}"/>
    /// </summary>
    static class UniqueId
    {
        static readonly Dictionary<string, IdGenerator> _generators = new();

        public static int Next(string catalog)
        {
            if (!_generators.TryGetValue(catalog, out var generator))
            {
                generator = new IdGenerator();
                _generators.Add(catalog, generator);
            }
            return generator.Next();
        }

        public static int Next<T>() => Next(typeof(T).FullName);
    }
}
