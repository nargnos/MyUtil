using MakeUnique.Lib;
using MakeUnique.Lib.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeUniquePluginFileInfo
{
    // 引用了不适合的项目而且代码结构不好，只作为文件方式的插件测试所以闲了再调整
    public class ReadMD5 : IPlugin
    {
        public string Name
        {
            get
            {
                return "输出文件信息";
            }
        }

        class Ui : Form
        {
            [Flags]
            public enum ShowInfo
            {
                None,
                Md5,
                Size
            }
            TableLayoutPanel layout;
            FlowLayoutPanel checkboxLayout;
            FlowLayoutPanel buttonLayout;
            public Ui()
            {
                SuspendLayout();
                Text = "选择输出";
                FormBorderStyle = FormBorderStyle.Fixed3D;
                ShowInTaskbar = false;
                MinimizeBox = false;
                Width = 400;
                Height = (int)(Width * 0.618);
                MaximizeBox = false;
                StartPosition = FormStartPosition.CenterScreen;
                layout = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };

                checkboxLayout = new FlowLayoutPanel() { Dock = DockStyle.Fill };
                buttonLayout = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };

                var okBtn = new Button() { DialogResult = DialogResult.OK, Text = "确定" };
                var cancleBtn = new Button() { DialogResult = DialogResult.No, Text = "取消" };

                buttonLayout.Controls.Add(cancleBtn);
                buttonLayout.Controls.Add(okBtn);

                var enumType = typeof(ShowInfo);
                var checkBox = from enumVal in Enum.GetValues(enumType).Cast<ShowInfo>()
                               where enumVal != ShowInfo.None
                               select new CheckBox() { Text = Enum.GetName(enumType, enumVal) };
                checkboxLayout.Controls.AddRange(checkBox.ToArray());
                // 界面不整齐

                layout.Controls.Add(checkboxLayout, 0, 0);
                layout.Controls.Add(buttonLayout, 0, 1);
                
                Controls.Add(layout);
                ResumeLayout();
            }
            public new ShowInfo ShowDialog()
            {
                if (base.ShowDialog() != DialogResult.OK)
                {
                    return ShowInfo.None;
                }
                var results = from ctl in checkboxLayout.Controls.Cast<Control>()
                              where ctl is CheckBox && ((CheckBox)ctl).Checked
                              select (ShowInfo)Enum.Parse(typeof(ShowInfo), ctl.Text);
                ShowInfo result = ShowInfo.None;
                foreach (var item in results)
                {
                    result |= item;
                }
                return result;
            }
        }
        Lazy<Ui> ui = new Lazy<Ui>(() => new Ui(), true);

        ParallelQuery<IGrouping<string, string>> IPlugin.Do(HashSet<string> inputFiles)
        {
            var showOption = ui.Value.ShowDialog();
            if (showOption == Ui.ShowInfo.None)
            {
                return null;
            }
            return from path in inputFiles.AsParallel()
                   let infos = GetFileInfo(showOption, path)
                   from info in infos
                   group info by path;

        }
        private IEnumerable<string> GetFileInfo(Ui.ShowInfo option, string path)
        {
            List<string> result = new List<string>();
            if (HasFlag(Ui.ShowInfo.Md5, option))
            {
                result.Add($"MD5: {BitConverter.ToString(Utils.GetMD5(path)).Replace("-", "")}");
            }
            if (HasFlag(Ui.ShowInfo.Size, option))
            {
                StringBuilder sb = new StringBuilder(32);
                NativeMethods.StrFormatByteSizeW(Utils.GetFileSize(path), sb, sb.Capacity);
                result.Add($"Size: {sb.ToString()}");
            }
            return result;
        }

        private bool HasFlag(Ui.ShowInfo flag, Ui.ShowInfo val)
        {
            return (flag & val) == flag;
        }
    }
}
