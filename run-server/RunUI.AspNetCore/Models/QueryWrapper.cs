
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{

    public class QueryGenertator<T> where T : class, new()
    {
        Logger Logger = LogManager.GetCurrentClassLogger();

        private void SetProp(WhereHelper<T> whereHelper, string propertyName, string value, Type structType, string type)
        {
            if (value.IsNullOrWhiteSpace()) return;
            try
            {
                object v = Convert.ChangeType(value, structType);
                switch (type)
                {
                    case ">":
                        whereHelper.GreaterThan(propertyName, v);
                        break;

                    case "<":
                        whereHelper.LessThan(propertyName, v);
                        break;

                    case ">=":
                        whereHelper.GreaterThanOrEqual(propertyName, v);
                        break;

                    case "<=":
                        whereHelper.LessThanOrEqual(propertyName, v);
                        break;

                    case "==":
                        whereHelper.Equal(propertyName, v);
                        break;

                    case "!=":
                        whereHelper.NotEqual(propertyName, v);
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = $"对于属性【{propertyName}】, 值【{value}】不能转换为[{structType.Name}]";

                Logger.Error(ex, msg);
            }
        }
        public Expression<Func<T, bool>> GetExpression(Stream stream)
        {
            JsonSerializer serializer = new();
            using (StreamReader sr = new(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var obj = serializer.Deserialize(reader) as JObject;

                JArray where = obj["where"] as JArray;

                WhereHelper<T> whereHelper = new();
                var modelType = typeof(T);
                var props = modelType.GetProperties();
                where.ForEach(x =>
                {
                    var item = JObject.FromObject(x);
                    var column = item["column"].ToString();
                    if (column.IsNullOrWhiteSpace())
                    {
                        throw new ArgumentNullException("column");
                    }

                    var p = props.FirstOrDefault(t => t.Name == column);
                    if (p == null) return;
                    var propertyName = p.Name;
                    var value = item["value"] as JValue;
                    if (p.PropertyType == typeof(string))
                    {
                        if (value == null) return;
                        var s = value.Value.ToStringExt();
                        if (s.HasValue())
                        {
                            if (s.StartsWith("^") || s.EndsWith("$"))
                            {
                                if (s.StartsWith("^") && s.EndsWith("$"))
                                {
                                    s = s[1..^1];
                                    whereHelper.Equal(propertyName, s);
                                }
                                else if (s.StartsWith("^"))
                                {
                                    s = s[1..];
                                    whereHelper.StartsWith(propertyName, s);
                                }
                                else if (s.EndsWith("$"))
                                {
                                    s = s[..^1];
                                    whereHelper.EndsWith(propertyName, s);
                                }
                            }
                            else if (s.StartsWith("=="))
                            {
                                s = s[2..];
                                whereHelper.Equal(propertyName, s);
                            }
                            else if (s.StartsWith("!="))
                            {
                                s = s[2..];
                                whereHelper.NotEqual(propertyName, s);
                            }
                            else
                            {
                                whereHelper.Contains(propertyName, s);
                            }
                        }
                    }
                    if (p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?))
                    {
                        if (value == null) return;
                        var b = value.Value as bool?;
                        if (b != null)
                        {
                            whereHelper.Equal(propertyName, b.Value);
                        }
                    }
                    else if (p.PropertyType == typeof(Guid) || p.PropertyType == typeof(Guid?))
                    {
                        if (value == null) return;
                        if (value.Value is Guid g && g != Guid.Empty)
                            whereHelper.Equal(propertyName, g);
                    }
                    else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                    {
                        if (value != null && value.Value is DateTime g && g != DateTime.MinValue)
                            whereHelper.Equal(propertyName, g);
                        if (item["min"] is JValue val && val.Value is DateTime minDt && minDt != DateTime.MinValue)
                            whereHelper.GreaterThanOrEqual(propertyName, minDt);
                        if (item["max"] is JValue val2 && val2.Value is DateTime maxDt && maxDt != DateTime.MinValue)
                            whereHelper.LessThanOrEqual(propertyName, maxDt);
                    }
                    else if (p.PropertyType.IsEnum || (p.PropertyType.IsNullableType() && p.PropertyType.GenericTypeArguments[0].IsEnum))
                    {
                        //枚举
                        if (value != null && value.Value != null && value.Value is int @enum)
                            whereHelper.Equal(propertyName, @enum);
                        if (item["arr"] != null && item["arr"].Type == JTokenType.Array)
                        {
                            var list = item["arr"].ToString().JsonDeserialize<List<int>>();
                            if (list.Any())
                            {
                                whereHelper.ArrayContains(propertyName, list);
                            }
                        }
                    }
                    else if (p.PropertyType.IsNumberType() || (p.PropertyType.IsNullableType() && p.PropertyType.GenericTypeArguments[0].IsNumberType()))
                    {
                        var structType = p.PropertyType;
                        if (structType.IsNullableType())
                        {
                            structType = structType.GetGenericArguments()[0];
                        }
                        if (value.Value != null && value.Value is string str && str.HasValue())
                        {
                            var s = str;

                            if (s.ContainsIgnoreCase("~") && !s.Trim().EqualsIgnoreCase("~"))
                            {
                                var ss = s.Split("~").Take(2).ToList();
                                if (ss.Any())
                                {
                                    SetProp(whereHelper, propertyName, ss[0], structType, ">=");
                                    SetProp(whereHelper, propertyName, ss[1], structType, "<=");
                                }
                            }
                            else if (s.StartsWith(">="))
                            {
                                var n = s[2..];
                                SetProp(whereHelper, propertyName, n, structType, ">=");
                            }
                            else if (s.StartsWith("<="))
                            {
                                var n = s[2..];
                                SetProp(whereHelper, propertyName, n, structType, "<=");
                            }
                            else if (s.StartsWith(">"))
                            {
                                var n = s[1..];
                                SetProp(whereHelper, propertyName, n, structType, ">");
                            }
                            else if (s.StartsWith("<"))
                            {
                                var n = s[1..];
                                SetProp(whereHelper, propertyName, n, structType, "<");
                            }
                            else if (s.StartsWith("=="))
                            {
                                var n = s[2..];
                                SetProp(whereHelper, propertyName, n, structType, "==");
                            }
                            else if (s.StartsWith("!="))
                            {
                                var n = s[2..];
                                SetProp(whereHelper, propertyName, n, structType, "!=");
                            }
                            else if (s.Contains('，') || s.Contains(','))
                            {
                                var ss = s.Split逗号();
                                if (ss.Any())
                                {
                                    var list = ss.Select(x => x.ToDoubleOrNull()).Where(i => i.HasValue).Select(x => x.Value).ToList();
                                    whereHelper.ArrayContains(propertyName, list);
                                }
                            }
                        }
                    }
                });

                return whereHelper.GetExpression();
            }


        }
    }
}
