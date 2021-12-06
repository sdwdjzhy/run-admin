namespace RunUI
{
    /// <summary>
    /// 锁的辅助类
    /// </summary>
    public static class LockHelper
    {
        private static readonly Dictionary<string, ObjectClass> LockerDictionary = new();

        private static readonly object ObjLock = new();

        /// <summary>
        /// 回收时间，默认5分钟
        /// </summary>
        public static uint TimeSpan { get; set; } = 5;

        static LockHelper()
        {
            ThreadHelper.SimpleStart(() =>
            {
                while (true)
                {
                    var time = -1 * TimeSpan;
                    lock (ObjLock)
                    {
                        var keys = LockerDictionary.Where(i => i.Value.Dt < DateTime.Now.AddMinutes(time)).Select(i => i.Key).ToArray();

                        foreach (var key in keys) LockerDictionary.Remove(key);
                    }

                    Thread.Sleep(System.TimeSpan.FromSeconds(30));
                }
            });
        }

        /// <summary>
        /// 锁的辅助类
        /// </summary>
        private class ObjectClass
        {
            public DateTime Dt { get; set; }
        }

        /// <summary>
        /// 获取指定信息的锁
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object GetLockObject(string str)
        {
            if (!LockerDictionary.ContainsKey(str))
                lock (ObjLock)
                {
                    if (!LockerDictionary.ContainsKey(str))
                    {
                        var o = new ObjectClass
                        {
                            Dt = DateTime.Now
                        };
                        LockerDictionary.Add(str, o);
                    }
                }

            var obj = LockerDictionary[str];
            obj.Dt = DateTime.Now;
            return obj;
        }
    }
}