using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadGithubExe
{
    public class DownloadManager
    {
        public DownloadManager(string savePath)
        {
            SavePath = savePath;
        }
        private WebClient CreateWeb()
        {
            WebClient web = new WebClient();
            web.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            return web;
        }

        private static string GetTmpPath(string path)
        {
            return path + DownloadExt;
        }
        public async Task<DownloadInfo> DownloadFile(string uri, string fileName)
        {
            var path = Path.Combine(SavePath, fileName);
            var tmpPath = GetTmpPath(path);
            var result = new DownloadInfo(this, uri, path, tmpPath);
            await CreateWeb().DownloadFileTaskAsync(new Uri(uri), tmpPath);

            return result;
        }

        public const string DownloadExt = ".download";
        // 存储位置
        public string SavePath { get; private set; }

        public class DownloadInfo
        {
            public ManualResetEventSlim WaitHandle { get; private set; }
            public DownloadManager Manager { get; private set; }
            public string Uri { get; private set; }
            public string TmpPath { get; private set; }
            public string Path { get; private set; }
            public DownloadInfo(DownloadManager manager, string uri, string path, string tmp)
            {
                Uri = uri;
                TmpPath = tmp;
                Path = path;
                Manager = manager;
                WaitHandle = new ManualResetEventSlim(false);
            }
        }

    }
}
