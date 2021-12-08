using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RunUI
{
    [JsonConverter(typeof(TreeNodeConverter))]
    public sealed class TreeNode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public long Weight { get; set; }

        public List<TreeNode> Children { get; set; }
        public Dictionary<string, object> Extra { get; set; }
    }

    [JsonConverter(typeof(TreeNodeGenericConverter))]
    public sealed class TreeNode<T> where T : struct
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public T? ParentId { get; set; }
        public long Weight { get; set; }

        public List<TreeNode<T>> Children { get; set; }
        public Dictionary<string, object> Extra { get; set; }

        public void JsonSerializer(Utf8JsonWriter writer, TreeNode<T> value, JsonSerializerOptions options)
        {

            writer.WriteStartObject();
            var namingPolicy = options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase;
            writer.WritePropertyName(namingPolicy.ConvertName(nameof(Id)));
            writer.WriteRawValue(value.Id.ToString());
            writer.WriteString(namingPolicy.ConvertName(nameof(Name)), value.Name);
            writer.WritePropertyName(namingPolicy.ConvertName(nameof(ParentId)));
            if (value.ParentId == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                var str = value.ParentId.ToString();

                writer.WriteRawValue(str);
            }
            writer.WriteNumber(namingPolicy.ConvertName(nameof(Weight)), value.Weight);

            if (value.Extra != null && value.Extra.Any())
            {
                value.Extra.ForEach(x =>
                {
                    writer.WriteString(namingPolicy.ConvertName(x.Key), x.Value.ToString());
                });
            }
            if (value.Children != null && value.Children.Any())
            {
                writer.WritePropertyName(namingPolicy.ConvertName(nameof(Children)));
                writer.WriteStartArray();
                foreach (var child in value.Children)
                {
                    JsonSerializer(writer, child, options);
                }
                writer.WriteEndArray();

            }
            writer.WriteEndObject();
        }
    }


    /// <summary>
    /// </summary>

    public class TreeNodeConverter : JsonConverter<TreeNode>
    {
        public override TreeNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TreeNode value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                return;
            }
            writer.WriteStartObject();
            var namingPolicy = options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase;

            writer.WriteString(namingPolicy.ConvertName(nameof(value.Id)), value.Id);
            writer.WriteString(namingPolicy.ConvertName(nameof(value.Name)), value.Name);
            if (value.ParentId.IsNullOrWhiteSpace())
            {
                writer.WriteNull(namingPolicy.ConvertName(nameof(value.ParentId)));
            }
            else
            {
                writer.WriteString(namingPolicy.ConvertName(nameof(value.ParentId)), value.ParentId);
            }
            writer.WriteNumber(namingPolicy.ConvertName(nameof(value.Weight)), value.Weight);

            if (value.Extra != null && value.Extra.Any())
            {
                value.Extra.ForEach(x =>
                {
                    writer.WriteString(namingPolicy.ConvertName(x.Key), x.Value.ToString());
                });
            }
            if (value.Children != null && value.Children.Any())
            {
                writer.WritePropertyName(namingPolicy.ConvertName(nameof(value.Children)));
                writer.WriteStartArray();
                foreach (var child in value.Children)
                {
                    Write(writer, child, options);
                }
                writer.WriteEndArray();

            }
            writer.WriteEndObject();
        }
    }

    /// <summary>
    /// </summary>

    public class TreeNodeGenericConverter : JsonConverterFactory
    {

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.HasImplementedRawGeneric(typeof(TreeNode<>));

        }
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(TreeNodeConverter<>).MakeGenericType(typeToConvert), BindingFlags.Instance | BindingFlags.Public, null, new object[0], null);
        }



        internal class TreeNodeConverter<T> : JsonConverter<T>
        {

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    return;
                }
                var m = typeof(T).GetMethod("JsonSerializer");
                m.Invoke(value, new object[] { writer, value, options });
            }
        }
    }
}