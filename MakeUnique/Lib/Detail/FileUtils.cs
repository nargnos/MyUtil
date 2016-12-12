using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Detail
{
    public static class FileUtils
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr StrFormatByteSizeW(long qdw, StringBuilder pszBuf, int cchBuf);
        public static void DeleteFile(string file, bool isShowUi, bool isRecycle)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file,
                isShowUi ? Microsoft.VisualBasic.FileIO.UIOption.AllDialogs : Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                isRecycle ? Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin : Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
        }

        // TODO: 是否需要把重复文件换成硬链接的功能？
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool CreateHardLink(string fileName, string existingFileName, IntPtr reserved);
        public static bool CreateHardLink(string fileName, string existingFileName)
        {
            return CreateHardLink(fileName, existingFileName, IntPtr.Zero);
        }

    }
}
