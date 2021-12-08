namespace RunUI
{
    public class TreeNodeHelper
    {
        private static void GetTree(List<TreeNodeItem> treeNodes, string parentId, List<TreeNode> nodes)
        {
            List<TreeNodeItem> filterNodes;

            if (parentId.IsNullOrWhiteSpace())
            {
                filterNodes = treeNodes.Where(x => x.ParentId.IsNullOrWhiteSpace()).OrderByDescending(i => i.Weight).ToList();
            }
            else
            {
                filterNodes = treeNodes.Where(x => x.ParentId.HasValue() && x.ParentId.EqualsIgnoreCase(parentId)).OrderByDescending(i => i.Weight).ToList();
            }
            if (filterNodes.Any())
            {
                filterNodes.ForEach(treeNode =>
                {
                    var node = new TreeNode()
                    {
                        Children = new List<TreeNode>(),
                        Id = treeNode.Id,
                        Extra = treeNode.Extra,
                        Name = treeNode.Name,
                        ParentId = parentId,
                        Weight = treeNode.Weight,
                    };

                    GetTree(treeNodes, node.Id, node.Children);
                    if (!node.Children.Any())
                    {
                        node.Children = null;
                    }
                    nodes.Add(node);
                });
            }
        }

        private static void GetTree<T>(List<TreeNodeItem<T>> treeNodes, T? parentId, List<TreeNode<T>> nodes) where T : struct
        {
            List<TreeNodeItem<T>> filterNodes;

            filterNodes = treeNodes.Where(x => x.ParentId.Equals(parentId)).OrderByDescending(i => i.Weight).ToList();

            if (filterNodes.Any())
            {
                filterNodes.ForEach(treeNode =>
                {
                    var node = new TreeNode<T>()
                    {
                        Children = new List<TreeNode<T>>(),
                        Id = treeNode.Id,
                        Extra = treeNode.Extra,
                        Name = treeNode.Name,
                        ParentId = parentId,
                        Weight = treeNode.Weight,
                    };

                    GetTree(treeNodes, node.Id, node.Children);
                    if (!node.Children.Any())
                    {
                        node.Children = null;
                    }
                    nodes.Add(node);
                });
            }
        }

        public static List<TreeNode> Make(List<TreeNodeItem> treeNodes, string rootParentId)
        {
            List<TreeNode> nodes = new();
            GetTree(treeNodes, rootParentId, nodes);
            return nodes;
        }

        public static List<TreeNode<T>> Make<T>(List<TreeNodeItem<T>> treeNodes, T? rootParentId) where T : struct
        {
            List<TreeNode<T>> nodes = new();
            GetTree(treeNodes, rootParentId, nodes);
            return nodes;
        }
    }
}