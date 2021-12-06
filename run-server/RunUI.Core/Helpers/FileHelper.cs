using System.Text;

namespace RunUI
{
    /// <summary>
    /// 文件和目录的辅助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="isOverWrite"></param>
        public static void Copy(string source, string dest, bool isOverWrite = false)
        {
            File.Copy(source, dest, isOverWrite);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateDirectory(string path)
        {
            if (path.IsNullOrWhiteSpace()) throw new ArgumentException("错误的路径名");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return true;
        }

        /// <summary>
        /// 根据文件名，创建文件的目录
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool CreateFileDirectory(string filename)
        {
            if (filename.IsNullOrWhiteSpace()) throw new ArgumentException("错误的文件名");
            var dirpath = Path.GetDirectoryName(filename);

            lock (LockHelper.GetLockObject("CreateFileDirectory-" + dirpath))
            {
                if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            }

            return true;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename"></param>
        public static void Delete(string filename)
        {
            try
            {
                File.Delete(filename);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 是否存在文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool Exists(string filename)
        {
            if (filename.HasValue()) return File.Exists(filename);
            return false;
        }

        /// <summary>
        /// 是否存在文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool ExistsDirectory(string filename)
        {
            if (filename.HasValue()) return Directory.Exists(filename);
            return false;
        }

        /// <summary>
        /// 返回指定路径字符串的目录信息。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetDirectoryName(string filename)
        {
            return Path.GetDirectoryName(filename);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetExtension(string filename)
        {
            return Path.GetExtension(filename);
        }

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFileName(string filename)
        {
            return Path.GetFileName(filename);
        }

        /// <summary>
        /// 返回不具有扩展名的指定路径字符串的文件名。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string filename)
        {
            return Path.GetFileNameWithoutExtension(filename);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="isOverWrite"></param>
        public static void Move(string source, string dest, bool isOverWrite = false)
        {
            CreateFileDirectory(dest);

            if (isOverWrite && File.Exists(dest)) File.Delete(dest);
            File.Move(source, dest);
        }

        #region AppendAllLines

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void AppendAllLines(string path, IEnumerable<string> contents)
        {
            AppendAllText(path, string.Join(Environment.CommandLine, contents.ToArray()));
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            File.AppendAllLines(path, contents, encoding);
        }

        #endregion AppendAllLines

        #region AppendAllText

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void AppendAllText(string path, string contents)
        {
            AppendAllText(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static void AppendAllText(string path, string contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            File.AppendAllText(path, contents, encoding);
        }

        #endregion AppendAllText

        #region WriteAllBytes

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="bytes">要写入文件的字节数组。</param>
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            CreateFileDirectory(path);
            File.WriteAllBytes(path, bytes);
        }

        #endregion WriteAllBytes

        #region WriteAllLines

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void WriteAllLines(string path, IEnumerable<string> contents)
        {
            WriteAllLines(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void WriteAllLines(string path, string[] contents)
        {
            WriteAllLines(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            File.WriteAllLines(path, contents, encoding);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            File.WriteAllLines(path, contents, encoding);
        }

        #endregion WriteAllLines

        #region WriteAllText

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void WriteAllText(string path, string contents)
        {
            WriteAllText(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static void WriteAllText(string path, string contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            File.WriteAllText(path, contents, encoding);
        }

        #endregion WriteAllText

        #region ReadAllLines

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// 打开一个文件，使用指定的编码读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> ReadAllLinesWithNoLock(string path)
        {
            return ReadAllTextWithNolock(path).ToLines();
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<List<string>> ReadAllLinesWithNoLockAsync(string path)
        {
            var text = "";
            using (var f = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                text = await f.ReadToEndAsync();
            }

            return text.ToLines();
        }

        #endregion ReadAllLines

        #region ReadAllBytes

        /// <summary>
        /// 打开一个二进制文件，将文件的内容读入一个字节数组，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        #endregion ReadAllBytes

        #region ReadAllText

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// 打开一个文件，使用指定的编码读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadAllTextWithNolock(string path)
        {
            using (var f = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return f.ReadToEnd();
            }
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string> ReadAllTextWithNolockAsync(string path)
        {
            using (var f = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return await f.ReadToEndAsync();
            }
        }

        #endregion ReadAllText

        /// <summary>
        /// 返回安全的文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string SafeFileName(this string filename)
        {
            if (filename.IsNullOrWhiteSpace()) return "";

            var chars = Path.GetInvalidFileNameChars();

            return chars.Aggregate(filename, (current, item) => current.Replace(item.ToString(), ""));
        }

        /// <summary>
        /// 返回安全的路径名
        /// </summary>
        /// <param name="pathname"></param>
        /// <returns></returns>
        public static string SafePathName(string pathname)
        {
            if (pathname.IsNullOrWhiteSpace()) return "";

            var chars = Path.GetInvalidPathChars();
            foreach (var item in chars) pathname = pathname.Replace(item.ToString(), "");
            return pathname;
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static async Task AppendAllLinesAsync(string path, IEnumerable<string> contents)
        {
            await AppendAllLinesAsync(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static async Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            await File.AppendAllLinesAsync(path, contents, encoding);
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string> ReadAllTextAsync(string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        /// <summary>
        /// 打开一个文件，使用指定的编码读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> ReadAllTextAsync(string path, Encoding encoding)
        {
            return await File.ReadAllTextAsync(path, encoding);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="bytes">要写入文件的字节数组。</param>
        public static async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            CreateFileDirectory(path);
            await File.WriteAllBytesAsync(path, bytes);
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReadAllBytesAsync(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }

        /// <summary>
        /// 打开一个文本文件，读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<string[]> ReadAllLinesAsync(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static async Task AppendAllTextAsync(string path, string contents)
        {
            await AppendAllTextAsync(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static async Task AppendAllTextAsync(string path, string contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            await File.AppendAllTextAsync(path, contents, encoding);
        }

        /// <summary>
        /// 打开一个文件，使用指定的编码读取文件的所有行，然后关闭该文件。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding)
        {
            return await File.ReadAllLinesAsync(path, encoding);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static async Task WriteAllTextAsync(string path, string contents)
        {
            await WriteAllTextAsync(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static async Task WriteAllTextAsync(string path, string contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            await File.WriteAllTextAsync(path, contents, encoding);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static async Task WriteAllLinesAsync(string path, IEnumerable<string> contents)
        {
            await WriteAllLinesAsync(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static async Task WriteAllLinesAsync(string path, string[] contents)
        {
            await WriteAllLinesAsync(path, contents, Encoding.UTF8);
        }

        /// <summary>
        /// 创建一个新文件，在其中写入指定的字符串，然后关闭文件。 如果目标文件已存在，则覆盖该文件。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">应用于字符串的编码。</param>
        public static async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding)
        {
            CreateFileDirectory(path);
            await File.WriteAllLinesAsync(path, contents, encoding);
        }
    }
}