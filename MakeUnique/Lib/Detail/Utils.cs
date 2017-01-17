using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MakeUnique.Lib.Detail
{
    public class Utils
    {
        public static void DeleteFile(string file, bool isShowUi, bool isRecycle)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file,
                isShowUi ? Microsoft.VisualBasic.FileIO.UIOption.AllDialogs : Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                isRecycle ? Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin : Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
        }
        public static async Task RecycleFile(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    DeleteFile(path, false, true);
                }
                catch
                {

                }
            });
        }
        public static long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }
        public static byte[] GetMD5(string path)
        {
            using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var md5 = new MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(file);
            }
        }
    }
}
