namespace RunUI
{
    /// <summary>
    /// 通用比较
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
        where V : notnull
    {
        private readonly Func<T, V> keySelector;

        /// <summary>
        /// </summary>
        /// <param name="keySelector"></param>
        public CommonEqualityComparer(Func<T, V> keySelector)
        {
            this.keySelector = keySelector;
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
        }
    }

    /// <summary>
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector) where V : notnull
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        /// <summary>
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this IEnumerable<string> collection, string entity)
        {
            return collection.Any(i => i.EqualsIgnoreCase(entity));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Exists<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Any(predicate);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> list, T t)
        {
            for (var i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).Equals(t))
                    return i;
            return -1;
        }

        /// <summary>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int IndexOfIgnoreCase(this IEnumerable<string> list, string t)
        {
            for (var i = 0; i < list.Count(); i++)
                if (list.ElementAt(i).EqualsIgnoreCase(t))
                    return i;
            return -1;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> list, string str)
        {
            return string.Join(str, list);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="char"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> list, char @char)
        {
            return string.Join(@char.ToString(), list);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int LastIndexOf<T>(this IEnumerable<T> list, T t)
        {
            for (var i = list.Count() - 1; i >= 0; i--)
                if (list.ElementAt(i).Equals(t))
                    return i;
            return -1;
        }

        /// <summary>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int LastOfIgnoreCase(this IEnumerable<string> list, string t)
        {
            for (var i = list.Count() - 1; i >= 0; i--)
                if (list.ElementAt(i).EqualsIgnoreCase(t))
                    return i;
            return -1;
        }

        //public static bool IsEmpty(this System.Collections.IEnumerable collection)
        //{
        //    foreach (var v in collection)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public static bool IsEmpty<T>(this IEnumerable<T> collection)
        //{
        //    return collection.Count() == 0;
        //}

        //public static bool IsNull<T>(this IEnumerable<T> collection)
        //{
        //    return collection == null;
        //}
        //public static bool IsNull(this IEnumerable collection)
        //{
        //    return collection == null;
        //}
        //public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        //{
        //    return collection.IsNull() || collection.IsEmpty();
        //}
        //public static bool IsNullOrEmpty(this IEnumerable collection)
        //{
        //    return collection.IsNull() || collection.IsEmpty();
        //}
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}