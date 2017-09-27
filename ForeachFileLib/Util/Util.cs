using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FileIO = Microsoft.VisualBasic.FileIO;

namespace ForeachFileLib.Util
{
    public static class Util
    {
        public static long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }

        public static string SizeToFileSize(long size)
        {
            var sb = new StringBuilder(32);
            NativeMethods.StrFormatByteSizeW(size, sb, sb.MaxCapacity);
            return sb.ToString();
        }

        public static string GetFileName(string path)
        {
            try
            {
                return Path.GetFileName(path);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static void RecycleFile(string file, bool isShowUi, bool isRecycle)
        {
            FileIO.FileSystem.DeleteFile(file,
                isShowUi ? FileIO.UIOption.AllDialogs : FileIO.UIOption.OnlyErrorDialogs,
                isRecycle ? FileIO.RecycleOption.SendToRecycleBin : FileIO.RecycleOption.DeletePermanently);
        }
        public static void RecycleFile(string path)
        {
            try
            {
                RecycleFile(path, false, true);
            }
            catch
            {
            }
        }
        public static void OpenFolderAndSelectFile(string fileFullName)
        {
            if (!IsPathExist(fileFullName))
            {
                return;
            }
            Process.Start("Explorer.exe", $"/e,/select,{fileFullName}");
        }
        public static void OpenFolderAndSelectFile(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                OpenFolderAndSelectFile(path);
            }
        }
        public static void RecycleFile(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                RecycleFile(path);
            }
        }
        public static bool IsPathExist(string path)
        {
            return (Directory.Exists(path) || File.Exists(path));
        }
        public static void Run(string path)
        {
            if (IsPathExist(path))
            {
                Process.Start(path);
            }
        }
        public static void Run(IEnumerable<string> paths)
        {
            foreach (var item in paths)
            {
                Run(item);
            }
        }
        public static void CopyToClipboard(IEnumerable<string> text)
        {
            CopyToClipboard(string.Join(Environment.NewLine, text));
        }
        public static void CopyToClipboard(IEnumerable<IGrouping<string, string>> text)
        {
            var sb = new StringBuilder();
            foreach (var grp in text)
            {
                sb.AppendLine(grp.Key);
                foreach (var item in grp)
                {
                    sb.AppendFormat("\t{0}" + Environment.NewLine, item);
                }
                sb.AppendLine();
            }

            CopyToClipboard(sb.ToString());
        }

        public static void CopyToClipboard(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            try
            {
                Clipboard.SetDataObject(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
