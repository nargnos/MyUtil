using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DownloadGithubExe
{
    public static class Utils
    {
        public static XmlDocument LoadXml(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("错误路径");
            }
            var text = File.ReadAllText(path);
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("无内容");
            }
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);
            return xml;
        }
        public static void PrepareDirectory(string path, IEnumerable<string> dir)
        {
            foreach (var item in dir.Where(i => !string.IsNullOrEmpty(i)).Distinct())
            {
                Directory.CreateDirectory(Path.Combine(path, item));
            }
        }
        public static void PrepareDirectory(string path, IEnumerable<AppFileInfo> files)
        {
            PrepareDirectory(path, from item in files select item.Dir);
        }
        public static string CombineUrl(string x, string y)
        {
            x = ReplaceSlash(x);
            y = ReplaceSlash(y);
            var isXEndWithSlash = x.EndsWith("/");
            var isYStartWithSlash = y.StartsWith("/");

            if (!isXEndWithSlash && !isYStartWithSlash)
            {
                return ReplaceSlash($"{x}/{y}");
            }
            if (isXEndWithSlash && isYStartWithSlash)
            {
                return ReplaceSlash(x + y.Substring(1));
            }
            return ReplaceSlash(x + y);
        }

        private static string ReplaceSlash(string str)
        {
            return str.Replace('\\', '/');
        }
    }
}
