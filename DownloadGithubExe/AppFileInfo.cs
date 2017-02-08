using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadGithubExe
{
    public class AppFileInfo
    {
        public AppFileInfo(string addr,long size)
        {
            Addr = addr.Replace('\\', '/');
            Size = size;
            Dir = Path.GetDirectoryName(addr);
            Name = Path.GetFileName(addr);
        }
        public string Addr { get; set; }
        public string Dir { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
