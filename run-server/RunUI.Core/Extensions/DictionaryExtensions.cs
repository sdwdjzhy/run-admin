using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace RunUI
{
    /// <summary>
    /// DictionaryExtensions
    /// </summary>
    public static class DictionaryExtensions
    {

        /// <summary>
        /// Returns a new instance of System.Collections.Generic.SortedDictionary&lt;TKey,TValue&gt;
        /// that is based on the specified Dictionary&lt;TKey,TValue&gt; and uses the default
        /// System.Collections.Generic.IComparer&lt;T&gt; implementation for the key type
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static SortedDictionary<TKey, TValue?> Sort<TKey, TValue>(this Dictionary<TKey, TValue?> dict)
            where TKey : notnull => new(dict);

        /// <summary>
        /// Returns a new instance of System.Collections.Generic.SortedDictionary&lt;TKey,TValue&gt;
        /// that is based on the specified Dictionary&lt;TKey,TValue&gt; and uses the specified
        /// System.Collections.Generic.IComparer&lt;T&gt; to compare keys
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static SortedDictionary<TKey, TValue> Sort<TKey, TValue>(this Dictionary<TKey, TValue> dict, IComparer<TKey> comparer)
            where TKey : notnull => new(dict, comparer);
    }
}