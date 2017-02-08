using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DownloadGithubExe
{
    public class Parser : IParser
    {
        private IEnumerable<XmlNode> FindNode(XmlNodeList nodes, string name)
        {
            return from XmlNode node in nodes
                   where node.LocalName == name
                   select node;
        }

        public IEnumerable<AppFileInfo> GetAddrs(XmlDocument xml)
        {
            // FIX: 这部分用xpath搜索
            // 
            var codebase = from assembly in FindNode(xml.ChildNodes, "assembly")
                           from dependency in FindNode(assembly.ChildNodes, "dependency")
                           from dependentAssembly in FindNode(dependency.ChildNodes, "dependentAssembly")
                           where dependentAssembly.Attributes["dependencyType"].Value == "install"
                           select new AppFileInfo(GetAttribute(dependentAssembly, "codebase"), Convert.ToInt64(GetAttribute(dependentAssembly, "size")));
            var files = from assembly in FindNode(xml.ChildNodes, "assembly")
                        from file in FindNode(assembly.ChildNodes, "file")
                        let path = GetAttribute(file, "name")
                        where !string.IsNullOrEmpty(path)
                        select new AppFileInfo(path, Convert.ToInt64(GetAttribute(file, "size")));
            return (from item in codebase.Concat(files)
                   orderby item.Dir, item.Size
                   select item).Distinct();
        }

        private static string GetAttribute(XmlNode node, string name)
        {
            return node.Attributes[name].Value;
        }
        public IEnumerable<AppFileInfo> AddAddrPrefix(string prefix, IEnumerable<AppFileInfo> files)
        {
            return from item in files
                   select AddAddrPrefix(prefix, item);
        }
        private static AppFileInfo AddAddrPrefix(string prefix, AppFileInfo file)
        {
            file.Addr = Utils.CombineUrl(prefix, file.Addr);
            return file;
        }
    }
}
