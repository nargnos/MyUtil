using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DownloadGithubExe
{
    public class Application
    {
        public Application(XmlDocument xmlContent)
        {
            xml_ = xmlContent;
        }
        private XmlDocument xml_;
        public static string ApplicationExt { get; } = ".application";

        public static Uri GetApplicationUri(Uri host, string appName)
        {
            return new Uri(host, GetFileName(appName));
        }

        public static string GetFileName(string appName)
        {
            return $"{appName}{ApplicationExt}";
        }
        public AppFileInfo GetManifest(IParser parser)
        {
            var files = parser.GetAddrs(xml_).ToList();
            if (!files.Any())
            {
                throw new Exception("解析文件错误");
            }
            if (files.Count > 1)
            {
                throw new Exception("未考虑到有多个manifest的情况");
            }
            return files.First();
        }

    }
}
