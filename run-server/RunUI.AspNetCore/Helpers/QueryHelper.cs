
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

    public class QueryHelper<T> where T : class, new()
    {
        Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly Stream stream;

        private JObject obj = null;
        public QueryHelper(Stream stream)
        {
            this.stream = stream;
        }

        private async Task InitAsync()
        {
            if (obj == null)
            {
                using StreamReader sr = new(stream);
                var str = await sr.ReadToEndAsync();
                obj = str.DeserializeObject() as JObject;
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj), "无效的body");
            }
        }

        private object GetValue(JValue value, Type structType, string propertyName)
        {

            if (value == null || value.Type == JTokenType.Null || value.Value == null) return null;

            try
            {
                return Convert.ChangeType(value.Value, structType);
            }
            catch (Exception ex)
            {
                var msg = $"对于属性【{propertyName}】, 值【{value}】不能转换为[{structType.Name}]";

                Logger.Error(ex, msg);
                throw new ArgumentException(msg);
            }
        }
        private void SetProp(WhereHelper<T> whereHelper, PropertyInfo property, string value, Type structType, string type)
        {
            if (value.IsNullOrWhiteSpace()) return;
            try
            {
                object v = Convert.ChangeType(value, structType);
                switch (type)
                {
                    case ">":
                        whereHelper.GreaterThan(property, v);
                        break;

                    case "<":
                        whereHelper.LessThan(property, v);
                        break;

                    case ">=":
                        whereHelper.GreaterThanOrEqual(property, v);
                        break;

                    case "<=":
                        whereHelper.LessThanOrEqual(property, v);
                        break;

                    case "==":
                        whereHelper.Equal(property, v);
                        break;

                    case "!=":
                        whereHelper.NotEqual(property, v);
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = $"对于属性【{property.Name}】, 值【{value}】不能转换为[{structType.Name}]";

                Logger.Error(ex, msg);
            }
        }
        public async Task<Expression<Func<T, bool>>> GetExpression()
        {
            await InitAsync();
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

                var p = props.FirstOrDefault(t => t.Name.EqualsIgnoreCase(column));
                if (p == null) return;
                var propertyName = p.Name;
                var value = item["value"] as JValue;
                if (p.PropertyType == typeof(string))
                {
                    if (value == null || value.Type == JTokenType.Null) return;
                    var s = value.Value.ToStringExt();
                    if (s.HasValue())
                    {
                        if (s.StartsWith("^") || s.EndsWith("$"))
                        {
                            if (s.StartsWith("^") && s.EndsWith("$"))
                            {
                                s = s[1..^1];
                                whereHelper.Equal(p, s);
                            }
                            else if (s.StartsWith("^"))
                            {
                                s = s[1..];
                                whereHelper.StartsWith(p, s);
                            }
                            else if (s.EndsWith("$"))
                            {
                                s = s[..^1];
                                whereHelper.EndsWith(p, s);
                            }
                        }
                        else if (s.StartsWith("=="))
                        {
                            s = s[2..];
                            whereHelper.Equal(p, s);
                        }
                        else if (s.StartsWith("!="))
                        {
                            s = s[2..];
                            whereHelper.NotEqual(p, s);
                        }
                        else
                        {
                            whereHelper.Contains(p, s);
                        }
                    }
                }
                if (p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?))
                {
                    if (value == null || value.Type == JTokenType.Null) return;
                    var b = value.Value as bool?;
                    if (b != null)
                    {
                        whereHelper.Equal(p, b.Value);
                    }
                }
                else if (p.PropertyType == typeof(Guid) || p.PropertyType == typeof(Guid?))
                {
                    if (value == null || value.Type == JTokenType.Null) return;
                    if (value.Value is Guid g && g != Guid.Empty)
                        whereHelper.Equal(p, g);
                }
                else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                {
                    if (value != null && value.Type != JTokenType.Null && value.Value is DateTime g && g != DateTime.MinValue)
                        whereHelper.Equal(p, g);
                    if (item["min"] is JValue val && val.Value is DateTime minDt && minDt != DateTime.MinValue)
                        whereHelper.GreaterThanOrEqual(p, minDt);
                    if (item["max"] is JValue val2 && val2.Value is DateTime maxDt && maxDt != DateTime.MinValue)
                        whereHelper.LessThanOrEqual(p, maxDt);
                }
                else if (p.PropertyType.IsEnum || (p.PropertyType.IsNullableType() && p.PropertyType.GenericTypeArguments[0].IsEnum))
                {

                    var structType = p.PropertyType;
                    if (structType.IsNullableType())
                    {
                        structType = structType.GetGenericArguments()[0];
                    }
                    //枚举 
                    if (value != null && value.Type != JTokenType.Null && value.Value != null && value.Value is long @enum)
                        whereHelper.Equal(p, Enum.ToObject(structType, @enum));
                    if (item["arr"] != null && item["arr"].Type == JTokenType.Array)
                    {
                        var arrayType = typeof(List<>);
                        arrayType = arrayType.MakeGenericType(p.PropertyType);
                        var obj = System.Text.Json.JsonSerializer.Deserialize(item["arr"].ToString(), arrayType);

                        whereHelper.ArrayContains(p, obj, arrayType);
                    }
                }
                else if (p.PropertyType.IsNumberType() || (p.PropertyType.IsNullableType() && p.PropertyType.GenericTypeArguments[0].IsNumberType()))
                {
                    var structType = p.PropertyType;
                    if (structType.IsNullableType())
                    {
                        structType = structType.GetGenericArguments()[0];
                    }
                    if (value != null && value.Type != JTokenType.Null && value.Value != null && value.Value is string str && str.HasValue())
                    {
                        var s = str;

                        if (s.ContainsIgnoreCase("~") && !s.Trim().EqualsIgnoreCase("~"))
                        {
                            var ss = s.Split("~").Take(2).ToList();
                            if (ss.Any())
                            {
                                SetProp(whereHelper, p, ss[0], structType, ">=");
                                SetProp(whereHelper, p, ss[1], structType, "<=");
                            }
                        }
                        else if (s.StartsWith(">="))
                        {
                            var n = s[2..];
                            SetProp(whereHelper, p, n, structType, ">=");
                        }
                        else if (s.StartsWith("<="))
                        {
                            var n = s[2..];
                            SetProp(whereHelper, p, n, structType, "<=");
                        }
                        else if (s.StartsWith(">"))
                        {
                            var n = s[1..];
                            SetProp(whereHelper, p, n, structType, ">");
                        }
                        else if (s.StartsWith("<"))
                        {
                            var n = s[1..];
                            SetProp(whereHelper, p, n, structType, "<");
                        }
                        else if (s.StartsWith("=="))
                        {
                            var n = s[2..];
                            SetProp(whereHelper, p, n, structType, "==");
                        }
                        else if (s.StartsWith("!="))
                        {
                            var n = s[2..];
                            SetProp(whereHelper, p, n, structType, "!=");
                        }
                        else if (s.Contains('，') || s.Contains(','))
                        {
                            var ss = s.Split逗号();
                            if (ss.Any())
                            {
                                var list = ss.Select(x => x.ToDoubleOrNull()).Where(i => i.HasValue).Select(x => x.Value).ToList();
                                whereHelper.ArrayContains(p, list);
                            }
                        }
                        else  
                        {
                            var o = GetValue(value, structType, p.Name);
                            if (o != null)
                            {
                                whereHelper.Equal(p, o);
                            }
                        }
                    }

                    if (item["min"] is JValue val)
                    {
                        var o = GetValue(val, structType, propertyName);

                        whereHelper.GreaterThanOrEqual(p, o);
                    }
                    if (item["max"] is JValue val2)
                    {
                        var o = GetValue(val2, structType, propertyName);
                        whereHelper.LessThanOrEqual(p, o);
                    }
                }
            });

            return whereHelper.GetExpression();
        }

        public async Task<(int, int)> GetPageInfo()
        {
            await InitAsync();
            long page = 1;
            long size = 20;
            if (obj["page"] is JValue pageValue && pageValue.Value is long p)
            {
                page = p > 0 ? p : 1;
            }
            if (obj["size"] is JValue sizeValue && sizeValue.Value is int s)
            {
                size = s > 0 ? s : 20;
            }

            return ((int)page, (int)size);
        }
    }
}
