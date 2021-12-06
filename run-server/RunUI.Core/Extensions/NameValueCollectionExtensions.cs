using System.Collections.Specialized;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this NameValueCollection collection)
        {
            var dict = new Dictionary<string, object>();

            if (collection != null)
                foreach (var key in collection.AllKeys)
                    dict.Add(key, collection[key]);

            return dict;
        }
    }
}