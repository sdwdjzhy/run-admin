namespace RunUI
{
    public class AppEnum
    {
        public enum Sex : int
        {
            未知 = 0,
            男,
            女,
        }

        public enum Flag : int
        {
            已删除 = -2,
            冻结 = -1,
            正常 = 0,
        }
    }
}