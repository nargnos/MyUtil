using ForeachFileLib.Util;

namespace FindDuplicate
{
    class FileNameDuplicate : FindDuplicateBase<string>
    {
        public FileNameDuplicate()
        {
            Name = "查找文件名重复文件";
            Ver = 0;
        }
        
        public override bool Equals(string x, string y)
        {
            return string.Equals(x, y);
        }

        public override int GetHashCode(string obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

        protected override string GetGroupData(string path)
        {
            return Util.GetFileName(path);
        }

        protected override string ConvertGroupName(string key)
        {
            return $"文件名: {key}";
        }
    }
}
