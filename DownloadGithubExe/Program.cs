using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DownloadGithubExe.DownloadManager;
namespace DownloadGithubExe
{

    class Program
    {
        private static bool YesOrNo(string text)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.Write(text);
                    Console.Write(" ");
                    switch (Console.ReadKey(false).Key)
                    {
                        case ConsoleKey.Y:
                            return true;
                        case ConsoleKey.N:
                            return false;
                        default:
                            continue;
                    }
                }
            }
            finally
            {
                Console.WriteLine(Environment.NewLine);
            }

        }

        private static void PrintNeedFile(string text)
        {
            Console.WriteLine(Environment.NewLine + "需要文件: [ {0} ]", text);
        }

        private static void SetConfig(string text, Func<string> defaultValue, Action<string> set)
        {
            Console.WriteLine("{0} (回车为默认值[ {1} ]): ", text, defaultValue());
            var val = Console.ReadLine();
            if (!string.IsNullOrEmpty(val))
            {
                set(val);
            }
        }
        private static bool NeedDownload()
        {
            return YesOrNo("是否需要程序自动下载?(手动下好需预先放入之前设定的文件夹) Y: 自动 N: 手动 >");
        }
        static void Main(string[] args)
        {
            try
            {
                SetConfig("服务器地址", () => Config.Host.AbsoluteUri, str => Config.Host = new Uri(str));
                SetConfig("软件名", () => Config.AppName, str => Config.AppName = str);
                SetConfig("保存地址", () => Config.Dir, str => Config.Dir = str);

                Environment.CurrentDirectory = Config.Dir;
                ClearDownloadFile(Config.Dir);
                var dm = CreateDownloadManager(Config.Dir);

                var appFileName = Application.GetFileName(Config.AppName);
                var appUri = Config.Host + appFileName;

                WaitDownload(dm, appUri, appFileName);

                var app = new Application(Utils.LoadXml(appFileName));

                var parser = new Parser();
                var manifestInfo = app.GetManifest(parser);

                var manifestUri = Config.Host + manifestInfo.Addr;
                WaitDownload(dm, manifestUri, manifestInfo.Name);

                var manifest = new Manifest(Utils.LoadXml(manifestInfo.Name));

                Console.WriteLine();
                var files = parser.AddAddrPrefix(Config.Host + manifestInfo.Dir, manifest.GetFiles(parser)).ToList();
                var result = DownOtherFiles(dm, files);
                if (result.Any())
                {
                    ClearDownloadFile(Config.Dir);
                    Console.WriteLine();
                    Console.WriteLine("=========这些文件下载失败(保存到download.log)========");
                    var sb = new StringBuilder();
                    sb.AppendLine("下载失败的文件:");
                    foreach (var item in result)
                    {
                        sb.AppendLine(item);
                        Console.WriteLine(item);
                    }
                    File.WriteAllText("download.log", sb.ToString());
                    Console.Read();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        private static void ClearDownloadFile(string path)
        {
            foreach (var item in from file in Directory.EnumerateFiles(path, "*"+DownloadManager.DownloadExt, SearchOption.AllDirectories) select file)
            {
                File.Delete(item);
            }
        }
        private static List<string> DownOtherFiles(DownloadManager dm, List<AppFileInfo> files)
        {
            ConcurrentBag<string> fails = new ConcurrentBag<string>();

            if (YesOrNo("自动下载剩余文件 Y: 自动 N: 手动 >"))
            {
                List<ManualResetEventSlim> wait = new List<ManualResetEventSlim>();
                Utils.PrepareDirectory(Config.Dir, files);

                files.ForEach(async item =>
                {
                    var path = Path.Combine(item.Dir, item.Name);
                    // 检查文件是否存在
                    if (File.Exists(path))
                    {
                        // FIX: 仅靠检查大小是不行的，还要检查版本号等，以防止多余下载
                        // 检查文件信息不符就要下载
                        if (new FileInfo(path).Length == item.Size)
                        {
                            return;
                        }
                        File.Delete(path);
                    }

                    PrintStartDownload(item.Addr);
                    var tmpWait = new ManualResetEventSlim(false);
                    try
                    {
                        wait.Add(tmpWait);
                        DownloadInfo info = await dm.DownloadFile(item.Addr, path);
                        PrintDownloadComplete(info.Uri);
                    }
                    catch (Exception e)
                    {
                        fails.Add(item.Addr);
                    }
                    finally
                    {
                        tmpWait.Set();
                    }
                });
                foreach (var item in wait)
                {
                    item.Wait();
                }
                Console.WriteLine("=======所有下载已停止======");
            }
            else
            {
                if (YesOrNo("自动生成文件夹结构 Y: 自动 N: 手动 >"))
                {
                    Utils.PrepareDirectory(Config.Dir, files);
                }
                var output = string.Join(Environment.NewLine, from item in files select item.Addr);
                Console.WriteLine("存储下载地址的文件名:");
                var save = Console.ReadLine();
                File.WriteAllText(save, output);
                Console.WriteLine("已输出");
            }
            return fails.ToList();
        }

        private static void WaitDownload(DownloadManager dm, string uri, string filename)
        {
            PrintNeedFile(uri);
            if (NeedDownload())
            {
                Task.Run(async () =>
                {
                    PrintStartDownload(uri);
                    var info = await dm.DownloadFile(uri, filename);
                    PrintDownloadComplete(info.Uri);
                }).Wait();
            }
        }

        private static void PrintDownloadComplete(string uri)
        {
            Console.WriteLine("{0}下载完毕 ({2}){0}[ {1} ]{0}", Environment.NewLine, uri, DateTime.Now);
        }

        private static void PrintStartDownload(string uri)
        {
            Console.WriteLine("{0}开始下载 ({2}){0}[ {1} ]{0}", Environment.NewLine, uri, DateTime.Now);
        }

        private static DownloadManager CreateDownloadManager(string dir)
        {
            return new DownloadManager(dir);
        }

    }
}

