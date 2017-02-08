using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadGithubExe
{
    public static class Config
    {
        public static Uri Host { get; set; } = new Uri(@"https://github-windows.s3.amazonaws.com/");
        public static string AppName { get; set; } = "GitHub";
        public static string Dir { get; set; } = Environment.CurrentDirectory;
    }
}
