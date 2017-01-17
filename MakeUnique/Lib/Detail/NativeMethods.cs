using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MakeUnique.Lib.Detail
{
    public static class NativeMethods
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr StrFormatByteSizeW(long qdw, StringBuilder pszBuf, int cchBuf);
        
        // TODO: 是否需要把重复文件换成硬链接的功能？
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool CreateHardLink(string fileName, string existingFileName, IntPtr reserved);
        public static bool CreateHardLink(string fileName, string existingFileName)
        {
            return CreateHardLink(fileName, existingFileName, IntPtr.Zero);
        }

    }
}
