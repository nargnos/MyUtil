
using ForeachFileLib;
using ForeachFileLib.Presenter;
using ForeachFileLib.Util;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ForeachFileLib.Addon;

namespace ForeachFile
{
    // TODO: 相关界面功能要拆分到其它类里
    // 现在用分组显示时不可用虚拟模式，可能需要编写新控件来处理
    public partial class UI : Form
    {
        public ICore Core
        {
            get; private set;
        }

        private ITaskPresenter taskPst_;
        private IPathPresenter pathPst_;


        public SynchronizationContext UIContext { get; private set; }
        public UI(ICore core)
        {
            Core = core;
            InitializeComponent();
            HideProgressBar();
            SetContext();
        }


        private void SetContext()
        {
            UIContext = SynchronizationContext.Current;
        }

        private void Init()
        {
            LoadPresenter();

            UIContextRun(InitAddons);

            // 下拉菜单
            toolStripDropDownButton_file.DropDown = contextMenuStrip_path;

            // 路径列表事件绑定
            pathPst_.PathListCountChanged += OnPathListCountChanged;
            // 插件处理事件绑定
            taskPst_.TaskBegin += (sender, e) => UIContextRun(() => OnTaskBegin(sender, e));
            taskPst_.TaskEnd += (sender, e) => UIContextRun(() => OnTaskEnd(sender, e));
            // 双击事件绑定
            listView_result.DoubleClick += ListView_result_DoubleClick;
        }

        private void ListView_result_DoubleClick(object sender, EventArgs e)
        {
            runDefaultCmd_?.Invoke();
        }

        private void OnTaskEnd(object sender, TaskEndEventArgs e)
        {
            if (e.IsSucceeded)
            {
                OutputResult(e.Result);
                HideProgressBar();
                OutputStatistics();
                Register(e.Result);
                SetContextMenu(e.Result, e.Commands, e.DefaultCommand);
                SetDropdownMenu(e.Result, e.Commands);
            }
            else
            {
                HideProgressBar();
                MessageBox.Show(e.Exception.Message);
                SetStatusText();
            }
            UnlockUI();
        }

        private Action runDefaultCmd_;

        private void SetDefaultCommand(IResult result, Command defaultCommand)
        {
            if (defaultCommand == null || result == null)
            {
                runDefaultCmd_ = null;
                return;
            }
            runDefaultCmd_ = () => OnRunCommand(() =>
              {
                  if (listView_result.SelectedItems.Count > 0)
                  {
                      return listView_result.SelectedItems.Cast<ListViewItem>().Take(1);
                  }
                  return null;
              }, defaultCommand, result);
        }

        // 这个主要是用来响应命令的，命令修改数据，然后数据回调事件
        private void Register(IResult result)
        {
            result.ValueRemoved += Result_ValueRemoved;
            result.ValueAdded += Result_ValueAdded;
            result.KeyRemoved += Result_KeyRemoved;
        }

        private void Result_KeyRemoved(object sender, KeyRemovedEventArgs e)
        {
            var grps = FindGroups(e.Keys, listView_result.Groups.Cast<ListViewGroup>());
            UIContextRun(() =>
            {
                listView_result.BeginUpdate();
                foreach (var grp in grps)
                {
                    foreach (var item in grp.Items.Cast<ListViewItem>().ToList())
                    {
                        item.Remove();
                    }
                    listView_result.Groups.Remove(grp);
                }
                listView_result.EndUpdate();
            });
        }
        private void UIContextRun(Action cb)
        {
            UIContext.Post(ign => cb?.Invoke(), null);
        }
        private void Result_ValueAdded(object sender, ValueAddedEventArgs e)
        {
            // TODO: 懒得写
            throw new NotImplementedException();
        }
        // FIX: 分开执行删除会使界面闪，这里可以用事件队列一起处理
        private void Result_ValueRemoved(object sender, ValueRemovedEventArgs e)
        {
            // 因为分组的关系，同key的只有一个元素
            var items = (from ListViewGroup item in listView_result.Groups
                         where item.Header == e.Key
                         select item.Items.Cast<ListViewItem>()).FirstOrDefault();
            var removeItems = FindItems(e.Values, items);

            UIContextRun(() =>
            {
                listView_result.BeginUpdate();

                foreach (var item in removeItems)
                {
                    item.Remove();
                }
                listView_result.EndUpdate();

            });

        }
        private static List<ListViewGroup> FindGroups(IEnumerable<string> vals, IEnumerable<ListViewGroup> grps)
        {
            var set = new HashSet<string>(vals);

            var ret = (from item in grps where set.Contains(item.Header) select item).ToList();
            return ret;
        }
        private static List<ListViewItem> FindItems(IEnumerable<string> vals, IEnumerable<ListViewItem> items)
        {
            var set = new HashSet<string>(vals);

            var ret = (from item in items where set.Contains(item.Text) select item).ToList();
            return ret;
        }
        private void SetDropdownMenu(IResult ret, IReadOnlyDictionary<string, Command> commands)
        {
            SetMenu(ret, commands, commandToolStripDropDownButton.DropDownItems, () =>
                      listView_result.CheckedItems.Cast<ListViewItem>());
        }
        private void SetMenu(IResult ret, IReadOnlyDictionary<string, Command> commands,
            ToolStripItemCollection menu,
            Func<IEnumerable<ListViewItem>> itemSelector)
        {
            menu.Clear();
            if (commands == null)
            {
                return;
            }
            foreach (var item in commands)
            {
                var cmd = item;
                menu.Add(item.Key).Click +=
                    (sender, e) => OnRunCommand(itemSelector,
                    cmd.Value, ret);
            }
        }

        private void SetContextMenu(IResult ret, IReadOnlyDictionary<string, Command> commands, Command defaultCommand)
        {
            SetMenu(ret, commands, addonCmdToolStripMenuItem.DropDownItems, () =>
                      listView_result.SelectedItems.Cast<ListViewItem>());
            SetDefaultCommand(ret, defaultCommand);
            if (!commands?.ContainsKey(defaultCommand.Name) ?? true)
            {
                return;
            }
            // 如果没找到表示程序出错，并会引发异常
            var item = (from ToolStripMenuItem i in addonCmdToolStripMenuItem.DropDownItems
                        where i.Text == defaultCommand.Name
                        select i
                       ).ToList();
            Debug.Assert(item.Count == 1);
            item.First().Text = item.First().Text + " ●";

        }
        private void OnRunCommand(Func<IEnumerable<ListViewItem>> getItems, Command cmd, IResult ret)
        {
            if (cmd == null || ret == null || getItems == null)
            {
                return;
            }
            IEnumerable<ListViewItem> items = getItems?.Invoke();

            if (items == null || items.Count() == 0)
            {
                return;
            }

            var selects = (from ListViewItem s in items
                           group s.Text by s.Group.Header).ToList();
            Task.Run(() =>
            {
                if (cmd == null)
                {
                    return;
                }
                selects.ForEach(s =>
                {
                    cmd.Run(ret, s.Key, s);
                });
            });
        }

        private void OutputStatistics()
        {
            var grps = listView_result.Groups.Count;
            var items = listView_result.Items.Count;
            SetStatusText(string.Format(Properties.Resources.Statistics, grps, items));
        }

        private void OutputResult(IResult result)
        {
            if (result == null)
            {
                return;
            }
            listView_result.BeginUpdate();
            foreach (var item in result.Data)
            {
                var grp = new ListViewGroup(item.Key);

                var tmpItems = (from path in item.Value
                                select new ListViewItem(path, grp)).ToArray();

                listView_result.Groups.Add(grp);
                listView_result.Items.AddRange(tmpItems);
            }
            listView_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView_result.EndUpdate();
        }

        private void UnlockUI()
        {
            splitContainer.Enabled = true;
        }

        private void OnTaskBegin(object sender, TaskBeginEventArgs e)
        {
            SetCancellationTokenSource(e.Cancellation);
            LockUI();
            ShowProgressBar(ProgressBarStyle.Marquee);
            SetStatusText(Properties.Resources.Processing);
            ClearResult();

        }


        private void ClearResult()
        {
            listView_result.Items.Clear();
            listView_result.Groups.Clear();
        }

        private void LockUI()
        {
            splitContainer.Enabled = false;
        }

        private void ShowProgressBar(ProgressBarStyle style, int max = 100)
        {
            toolStripProgressBar.Visible = true;
            toolStripProgressBar.Style = style;
            toolStripProgressBar.Maximum = max;
        }
        private void StepProgressBar()
        {
            toolStripProgressBar.PerformStep();
        }
        private void HideProgressBar()
        {
            toolStripProgressBar.Visible = false;
        }
        private void SetStatusText(string str = "")
        {
            toolStripStatusLabel.Text = str;
        }


        private CancellationTokenSource cancellation_;
        private void SetCancellationTokenSource(CancellationTokenSource s)
        {
            if (cancellation_ != null)
            {
                try
                {
                    cancellation_.Cancel();
                }
                catch
                {
                }
            }
            cancellation_ = s;
        }

        // 载入必须实现的Presenter
        private void LoadPresenter()
        {
            if (!Core.TryFindPresenter(out taskPst_))
            {
                throw new ApplicationException("ITaskPresenter not impl");
            }
            if (!Core.TryFindPresenter(out pathPst_))
            {
                throw new ApplicationException("IPathPresenter not impl");
            }
        }

        private void InitAddons()
        {

            var cb = new EventHandler((sender, e) =>
            {
                if (sender is ToolStripItem)
                {
                    var addonName = ((ToolStripItem)sender).Text;
                    taskPst_.Do(addonName);
                }
            });
            foreach (var item in taskPst_.Addons)
            {
                toolStripDropDownButton_plugin.DropDownItems.Add(item).Click += cb;
            }
        }

        private void OnPathListCountChanged(object sender, PathPresenterEventArgs e)
        {
            listView_path.VirtualListSize = e.PathCount;
            listView_path.SelectedIndices.Clear();
            listView_path.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog_path.ShowDialog() == DialogResult.OK)
            {
                pathPst_.AddPath(openFileDialog_path.FileNames);
            }
        }


        private async void UI_Load(object sender, EventArgs e)
        {
            await Task.Run(() => Init());
        }

        private void listView_path_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(pathPst_.GetPath(e.ItemIndex));
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pathPst_.ClearPath();
        }

        private void UI_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void UI_DragDrop(object sender, DragEventArgs e)
        {
            pathPst_.AddPath((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedCount = listView_path.SelectedIndices.Count;
            if (selectedCount == 0)
            {
                return;
            }
            if (selectedCount == pathPst_.Count)
            {
                pathPst_.ClearPath();
            }
            else
            {
                var selected = from int idx
                         in listView_path.SelectedIndices
                               select pathPst_.GetPath(idx);
                pathPst_.RemovePath(selected.ToArray());
            }
        }

        private void incSubDirToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Core.IsIncSubDir = incSubDirToolStripMenuItem.Checked;
        }

        private void toolStripTextBox_Filter_TextChanged(object sender, EventArgs e)
        {
            Core.FileFilter = toolStripTextBox_Filter.Text;
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForeachResultItems(item => item.Checked = true);
        }

        private void ForeachResultItems(Action<ListViewItem> cb)
        {
            foreach (ListViewItem item in listView_result.Items)
            {
                cb(item);
            }
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ForeachResultItems(item => item.Checked = false);
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForeachResultItems(item => item.Checked = !item.Checked);
        }

        private void allButFirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var grp in from ListViewGroup i in listView_result.Groups
                                where i.Items.Count > 0
                                select i)
            {
                grp.Items[0].Checked = false;
                foreach (var item in grp.Items.Cast<ListViewItem>().Skip(1))
                {
                    item.Checked = true;
                }
            }
        }

        private void toolStripDropDownButton_search_ButtonClick(object sender, EventArgs e)
        {
            var text = toolStripTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            var useRegex = regexToolStripMenuItem.Checked;
            var view = listView_result;

            view.SelectedItems.Clear();

            Predicate<ListViewItem> pred = null;
            if (useRegex)
            {
                pred = item =>
                {
                    try
                    {
                        return Regex.IsMatch(item.Text, text);
                    }
                    catch
                    {
                        return false;
                    }
                };
            }
            else
            {
                pred = item => item.Text.Contains(text);
            }

            var selects = from ListViewItem item in view.Items where pred(item) select item;

            var ret = selects.ToList();

            if (ret.Any())
            {
                view.BeginUpdate();
                foreach (var item in ret)
                {
                    item.Selected = true;
                }
                ret.First().EnsureVisible();
                view.EndUpdate();
            }
            view.Select();

        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripDropDownButton_search_ButtonClick(sender, e);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.CopyToClipboard(GetSelectedItems());
        }

        private void copyGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grps = from ListViewItem item
                       in listView_result.SelectedItems
                       group item.Text by item.Group.Header;

            Util.CopyToClipboard(grps);
        }

        private IEnumerable<string> GetSelectedItems()
        {
            return from ListViewItem item in listView_result.SelectedItems select item.Text;
        }

        private void UI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cancellation_?.Cancel();
                SetStatusText("正在终止...");
            }
        }

        private void clearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearResult();
        }

        private void regexFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.UseRegex = regexFilterToolStripMenuItem.Checked;
        }
    }
}
