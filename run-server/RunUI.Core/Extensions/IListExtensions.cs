using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RunUI
{
    /// <summary>
    /// IList扩展类
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// 根据T，创建对映的DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable CreateTable<T>()
        {
            var entityType = typeof(T);
            var table = new DataTable(entityType.Name);
            var properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                var c = new DataColumn(prop.Name)
                {
                    AllowDBNull = false
                };

                if (prop.PropertyType.IsNullableType())
                {
                    c.AllowDBNull = true;
                    c.DataType = prop.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    c.DataType = prop.PropertyType;
                    if (!IsContainsRequiredAttr(prop)) c.AllowDBNull = true;
                }

                table.Columns.Add(c);
            }

            return table;
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">源</param>
        /// <param name="current">插入元素</param>
        /// <param name="target">目标元素</param>
        /// <param name="moveType">插入类型（prev，next）</param>
        public static void Insert<T>(this IList<T> source, T current, T target, string moveType) where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (current == null)
                throw new ArgumentNullException(nameof(current));
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrWhiteSpace(moveType))
                throw new ArgumentNullException(nameof(moveType));
            source.Remove(current);
            var targetIndex = source.IndexOf(target);
            if (targetIndex == -1)
                throw new ArgumentOutOfRangeException(nameof(target), "目标元素在数据源中不存在");
            var splitIndex = 0;
            if (moveType == "prev")
                splitIndex = targetIndex;
            else if (moveType == "next")
                splitIndex = targetIndex + 1;
            source.Insert(splitIndex, current);
        }

        /// <summary>
        /// 将current插入到target之后
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="current">待插入元素</param>
        /// <param name="target">目标元素</param>
        public static void InsertAfter<T>(this IList<T> source, T current, T target) where T : class
        {
            source.Insert(current, target, "next");
        }

        /// <summary>
        /// 将current插入到target之前
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="current">待插入元素</param>
        /// <param name="target">目标元素</param>
        public static void InsertBefore<T>(this IList<T> source, T current, T target) where T : class
        {
            source.Insert(current, target, "prev");
        }

        /// <summary>
        /// 是否包含RequiredAttribute
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool IsContainsRequiredAttr(PropertyDescriptor prop)
        {
            var attrs = prop.Attributes;
            foreach (var item in attrs)
                if (item is RequiredAttribute)
                    return true;
            return false;
        }

        /// <summary>
        /// IList to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataRow[] ToDataRows<T>(this IList<T> list)
        {
            var table = CreateTable<T>();
            var entityType = typeof(T);
            var properties = TypeDescriptor.GetProperties(entityType);
            var dataRowList = new List<DataRow>();
            foreach (var item in list)
            {
                var row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    if (prop.GetValue(item) == null)
                        row[prop.Name] = DBNull.Value;
                    else
                        row[prop.Name] = prop.GetValue(item);

                dataRowList.Add(row);
            }

            return dataRowList.ToArray();
        }

        /// <summary>
        /// IList to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            var table = CreateTable<T>();
            var entityType = typeof(T);
            var properties = TypeDescriptor.GetProperties(entityType);

            foreach (var item in list)
            {
                var row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                    if (prop.GetValue(item) == null)
                        row[prop.Name] = DBNull.Value;
                    else
                        row[prop.Name] = prop.GetValue(item);

                table.Rows.Add(row);
            }

            return table;
        }
    }
}