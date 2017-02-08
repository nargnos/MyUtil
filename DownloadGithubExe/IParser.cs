using System;
using System.Collections.Generic;
using System.Xml;

namespace DownloadGithubExe
{
    public interface IParser
    {
        IEnumerable<AppFileInfo> GetAddrs(XmlDocument xml);
        IEnumerable<AppFileInfo> AddAddrPrefix(string prefix, IEnumerable<AppFileInfo> files);
    }
}