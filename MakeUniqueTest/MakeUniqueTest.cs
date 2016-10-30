using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using MakeUnique.Lib;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using System.Linq;
using MakeUnique.Lib.Detail;

namespace MakeUniqueTest
{
    [TestClass]
    public class MakeUniqueTest
    {
        [TestMethod]
        public void TestGetDuplicateFileByName()
        {
            DuplicateFileFinder finder = new DuplicateFileFinder();
            finder.Add(@"..\..\TestFile");

            var reader = new FileNameReader();

            GetFiles(reader, finder);

            var fileNames = new List<string>()
                {
                    @"F:\FakeFile",
                    @"F:\FakeDir\FakeFile",
                    @"F:\Fake",
                    @"F:\FakeDir\FakeDir\Fake",
                    @"F:\FakeXXX",
                    @"F:\FakeDir\FakeYYY"
                };
            using (ShimsContext.Create())
            {
                MakeUnique.Lib.Fakes.ShimDuplicateFileFinder.AllInstances.GetAllFilesStringSearchOption =
                    (obj, p, o) =>
                    {
                        return fileNames.AsParallel();
                    };
                DuplicateFileFinder fakeFinder = new DuplicateFileFinder();
                GetFiles(reader, fakeFinder);
            }
        }

        [TestMethod]
        public void TestGetDuplicateFileByMd5()
        {
            DuplicateFileFinder finder = new DuplicateFileFinder();
            finder.Add(@"..\..\TestFile");

            var reader = new FileMd5Reader();

            GetFiles(reader, finder);

            var fileNames = new Dictionary<string, string>()
                {
                    {@"F:\FakeFile", "FakeMd5-123456" },
                    {@"F:\FakeDir\FakeFile", "FakeMd5-654321" },
                    {@"F:\Fake", "FakeMd5-13579" },
                    {@"F:\FakeDir\FakeDir\Fake", "FakeMd5-23333" },
                    {@"F:\FakeXXX", "FakeMd5-123456"},
                    {@"F:\FakeDir\FakeYYY", "FakeMd5-654321"}
                };
            using (ShimsContext.Create())
            {
                MakeUnique.Lib.Fakes.ShimFileMd5Reader.AllInstances.GetMD5String =
                    (obj, path) =>
                    {
                        return fileNames[path];
                    };
                // 修改文件大小都相等
                MakeUnique.Lib.Fakes.ShimFileSizeReader.AllInstances.GetInfoString = (obj, path) => "1234";

                MakeUnique.Lib.Fakes.ShimDuplicateFileFinder.AllInstances.GetAllFilesStringSearchOption =
                    (obj, p, o) =>
                    {
                        return fileNames.Keys.AsParallel();
                    };
                DuplicateFileFinder fakeFinder = new DuplicateFileFinder();
                GetFiles(reader, fakeFinder);
            }
        }
        [TestMethod]
        public void TestGetDuplicateFileBySize()
        {
            DuplicateFileFinder finder = new DuplicateFileFinder();
            finder.Add(@"..\..\TestFile");
            var reader = new FileSizeReader();
            GetFiles(reader, finder);
            using (ShimsContext.Create())
            {
                var fileNames = new Dictionary<string, string>()
                {
                    {@"F:\FakeFile", "123" },
                    {@"F:\FakeDir\FakeFile", "456" },
                    {@"F:\Fake", "123" },
                    {@"F:\FakeDir\FakeDir\Fake", "456" },
                    {@"F:\FakeXXX", "789"},
                    {@"F:\FakeDir\FakeYYY", "987"}
                };
                MakeUnique.Lib.Fakes.ShimFileSizeReader.AllInstances.GetInfoString =
                    (obj, path) =>
                    {
                        return fileNames[path];
                    };
                MakeUnique.Lib.Fakes.ShimDuplicateFileFinder.AllInstances.GetAllFilesStringSearchOption =
                    (obj, p, o) =>
                    {
                        return fileNames.Keys.AsParallel();
                    };
                DuplicateFileFinder fakeFinder = new DuplicateFileFinder();
                GetFiles(reader, fakeFinder);
            }
        }

        [TestMethod]
        public void LinkTest()
        {
            string path = @"..\..\TestFile\";
            string target = Path.Combine(path, "LinkTest.txt");
            string hlFile = Path.Combine(path, "HardLink.txt");
            bool r = FileUtils.CreateHardLink(hlFile, target);
            Assert.IsTrue(r);
            File.Delete(hlFile);
        }


        private static void GetFiles(IFileInfoReader reader, DuplicateFileFinder finder)
        {
            foreach (var item in finder.GetDuplicateFiles("*", SearchOption.AllDirectories, reader))
            {
                Debug.WriteLine($"{reader.ConvertGroupKey(item.Key)}");
                int count = 0;
                foreach (var val in item)
                {
                    ++count;
                    Debug.WriteLine(val);
                }
                Assert.IsTrue(count > 0);
                Debug.WriteLine(Environment.NewLine);
            }
        }
    }
}
