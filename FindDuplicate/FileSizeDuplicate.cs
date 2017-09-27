using ForeachFileLib.Util;

namespace FindDuplicate
{
    class FileSizeDuplicate : FindDuplicateBase<long>
    {
        public FileSizeDuplicate()
        {
            Name = "查找文件大小重复文件";
            Ver = 0;
        }

        public override bool Equals(long x, long y)
        {
            return x == y;
        }

        public override int GetHashCode(long obj)
        {
            return obj.GetHashCode();
        }

        protected override string ConvertGroupName(long key)
        {
            return $"文件大小: {Util.SizeToFileSize(key)} ({key} Bytes)";
        }

        protected override long GetGroupData(string path)
        {
            return Util.GetFileSize(path);
        }        
       
    }
}
