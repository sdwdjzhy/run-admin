using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// 线程
    /// </summary>
    public static class ThreadHelper
    {
        /// <summary>
        /// 开始线程
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static Thread SimpleStart(Action action, Action<Exception> exception = null)
        {
            var thread = new Thread(() =>
            {
                var sb = new StringBuilder();
                try
                {
                    action?.Invoke();
                }
                catch (Exception ex)
                {
                    exception?.Invoke(ex);
                }
            })
            {
                IsBackground = true
            };
            thread.Start();
            return thread;
        }

        /// <summary>
        /// 睡眠
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        public static void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="isBackground"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static Thread Start(string name,
            Func<string, string> action,
            bool isBackground = true,
            Func<string, string> before = null,
            Func<string, string> after = null,
            Func<string, Exception, string> exception = null
        )
        {
            var thread = new Thread(() =>
            {
                var sb = new StringBuilder();
                try
                {
                    if (before != null) sb.AppendLine($"线程：{name} before: {before(name)}");
                    if (action != null) sb.AppendLine($"线程：{name} action: {action(name)}");
                }
                catch (Exception ex)
                {
                    if (exception != null) sb.AppendLine($"线程：{name} exception: {exception(name, ex)}");
                }
                finally
                {
                    after?.Invoke(sb.ToString());
                }
            })
            {
                IsBackground = isBackground
            };
            thread.Start();
            return thread;
        }

        /// <summary>
        /// 通过线程池开启线程
        /// <para>等待的话，使用 返回值 ManualResetEvent.WaitOne()</para>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ManualResetEvent ThreadPoolStart(Action action)
        {
            var mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(o =>
            {
                action?.Invoke();
                mre.Set();
            });
            return mre;
        }

        /// <summary>
        /// 通过线程池开启线程
        /// <para>等待的话，使用 返回值 ManualResetEvent.WaitOne()</para>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ManualResetEvent ThreadPoolStart<T>(Action<T> action, T state)
        {
            var mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(o =>
            {
                action?.Invoke((T)o);
                mre.Set();
            }, state);
            return mre;
        }
    }
}
