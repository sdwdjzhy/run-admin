namespace RunUI
{
    public sealed class TreeNodeItem<T> where T : struct
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public T? ParentId { get; set; }
        public long Weight { get; set; }

        public Dictionary<string, object> Extra { get; set; }

        public TreeNodeItem()
        { }

        public TreeNodeItem(T id, string name, T? parentId, long weight)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            Weight = weight;
        }
    }

    public sealed class TreeNodeItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public long Weight { get; set; }

        public Dictionary<string, object> Extra { get; set; }

        public TreeNodeItem()
        { }

        public TreeNodeItem(string id, string name, string parentId, long weight)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            Weight = weight;
        }
    }
}