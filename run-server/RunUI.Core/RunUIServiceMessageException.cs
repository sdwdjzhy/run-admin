namespace RunUI
{
    public class RunUIServiceMessageException : Exception
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="msg"></param>
        public RunUIServiceMessageException(string msg) : base(msg)
        {
        }

        public override string ToString()
        {
            return Message;
        }
    }
}