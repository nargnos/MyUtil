using System.IO;
using System.Security.Cryptography;

namespace ForeachFileLib.Util
{
    public static class FileMD5
    {
        public static byte[] GetMD5(string path)
        {
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(file);
            }
        }        
    }
}
