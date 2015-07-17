using System.Collections.Generic;

namespace NStructurizr.Core
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            TValue value;
            return source.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}