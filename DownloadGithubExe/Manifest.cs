using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DownloadGithubExe
{
    public class Manifest
    {
        private const string DeployExt = ".deploy";
        public Manifest(XmlDocument xmlContent)
        {
            xml_ = xmlContent;
        }
        private XmlDocument xml_;
        // 相对地址，文件夹，文件名，大小
        public IEnumerable<AppFileInfo> GetFiles(IParser parser)
        {
            return from item in parser.GetAddrs(xml_)
                   select AddExt(item);
        }
        private static AppFileInfo AddExt(AppFileInfo obj)
        {
            obj.Addr += DeployExt;
            return obj;
        }
    }
}
