using MakeUnique.Lib;
using MakeUnique.Lib.Detail;
using MakeUnique.Lib.Plugin;
using MakeUnique.Lib.Util;
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

namespace MakeUnique
{
    // 并不是什么有用的类库，有时候用到这些功能而网上下载的不太满意就弄个
    // 这个界面是用来测试的有点乱
    [Export(typeof(GUI))]
    public partial class GUI : Form
    {
        private IPathManager pathManager_;
        private IPluginManager pluginManager_;

        private CancellationTokenSource cancel_ = null;
        private Action do_ = null;
        [ImportingConstructor]
        public GUI(IPathManager pathManager, IPluginManager pluginManager)
        {
            pathManager_ = pathManager;
            pluginManager_ = pluginManager;
            InitializeComponent();
            InitializePluginMenu();
            HideProgressBar();
        }

        

        private void InitializePluginMenu()
        {
            var menu = new List<Tuple<string, EventHandler>>();
            foreach (var item in from p in pluginManager_.Plugins select p.Value)
            {
                menu.Add(new Tuple<string, EventHandler>(item.Name, GetPluginButtonClickHandler(item)));
            }
            SuspendLayout();
            foreach (var item in menu)
            {
                toolStripDropDownButton_plugin.DropDownItems.Add(item.Item1, null, item.Item2);
            }
            ResumeLayout();
        }

        private EventHandler GetPluginButtonClickHandler(IPlugin plugin)
        {
            return async (sender, obj) =>
            {
                try
                {
                    await Begin();
                    var token = cancel_.Token;
                    var files = await GetFiles(token);
                    var result = await plugin.MakeGroup(files, token);
                    await DisplayResult(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    // 设置结果列表的右键菜单的执行方式
                    SetDoFunc(plugin);
                    End();
                }
            };
        }

        private void SetDoFunc(IPlugin plugin)
        {
            var cmdName = plugin.CommandName;
            if (string.IsNullOrEmpty(cmdName))
            {
                doToolStripMenuItem.Visible = false;
                do_ = null;
            }
            else
            {
                doToolStripMenuItem.Visible = true;
                doToolStripMenuItem.Text = cmdName;
                do_ = async () =>
                {
                    LockUI();
                    SetStatusText("处理中");
                    var files = (from ListViewItem item in listView_result.Items
                                 where item.Checked
                                 select item.Text).ToList();
                    var result = await plugin.Do(files);
                    if (result)
                    {
                        BeginUpdateResultView();
                        // 清掉处理过的文件列表
                        await ForEachResult(item => item.Checked, item => item.Remove());
                        EndUpdateResultView();
                    }
                    SetStatusText(string.Empty);
                    UnlockUI();
                };
            }
        }


        private async Task<HashSet<string>> GetFiles(CancellationToken token)
        {
            return await pathManager_.GetFiles(Pattern,
                IsIncludeSub ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly,
                token);
        }

        #region 输入列表控制
        private void OnPathViewRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(pathManager_.ElementAt(e.ItemIndex));
        }

        private void OnPathDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private void OnPathDragDrop(object sender, DragEventArgs e)
        {
            AddPath((string[])e.Data.GetData(DataFormats.FileDrop));
        }
        private void AddPath(params string[] path)
        {
            bool needUpdate = false;
            foreach (var item in path)
            {
                if (pathManager_.Add(item))
                {
                    needUpdate = true;
                }
            }
            if (needUpdate)
            {
                UpdatePathListSize();
            }
        }
        private void ClearPath()
        {
            pathManager_.Clear();
            UpdatePathListSize();
        }
        private void UpdatePathListSize()
        {
            listView_path.VirtualListSize = pathManager_.Count;
            listView_path.SelectedIndices.Clear();
        }

        private void OnAddPathButtonClick(object sender, EventArgs e)
        {
            if (openFileDialog_path.ShowDialog() == DialogResult.OK)
            {
                AddPath(openFileDialog_path.FileNames);
            }
        }

        private void OnRemovePathButtonClick(object sender, EventArgs e)
        {
            var selectedCount = listView_path.SelectedIndices.Count;
            if (selectedCount == 0)
            {
                return;
            }
            if (selectedCount == pathManager_.Count)
            {
                ClearPath();
            }
            else
            {

                var selectedPaths = (from int i in listView_path.SelectedIndices
                                     select pathManager_.ElementAt(i)).ToList();
                foreach (var item in selectedPaths)
                {
                    pathManager_.Remove(item);
                }
            }
            UpdatePathListSize();
        }

        private void OnClearPathButtonClick(object sender, EventArgs e)
        {
            ClearPath();
        }

        #endregion

        #region 基本界面控制
        private bool IsIncludeSub { get { return toolStripButton_IncludeSub.Checked; } }
        private string Pattern
        {
            get
            {
                var result = toolStripTextBox_Filter.Text.Trim();
                return string.IsNullOrEmpty(result) ? "*" : result;
            }
        }
        private async Task Begin()
        {
            cancel_ = new CancellationTokenSource();
            LockUI();
            await ClearResult();
            SetStatusText("按Esc终止");
            ShowProgressBar(ProgressBarStyle.Marquee);
        }

        private void LockUI()
        {
            splitContainer.Enabled = false;
        }

        private async Task ClearResult()
        {
            var iar = BeginInvoke(new Action(() =>
            {
                BeginUpdateResultView();
                listView_result.Items.Clear();
                listView_result.Groups.Clear();
                EndUpdateResultView();
            }));
            await WaitAsyncResult(iar);
        }
        private void End()
        {
            HideProgressBar();
            cancel_ = null;
            UnlockUI();
            SetStatusText($"组: {listView_result.Groups.Count} 总: {listView_result.Items.Count}");
            listView_path.Focus();
        }

        private void UnlockUI()
        {
            splitContainer.Enabled = true;
        }

        private void OnGUIKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (cancel_ != null)
                {
                    if (cancel_.IsCancellationRequested)
                    {
                        return;
                    }
                    cancel_.Cancel();
                    if (cancel_.IsCancellationRequested)
                    {
                        SetStatusText("正在取消...");
                    }
                }
            }
        }
        private void SetStatusText(string str)
        {
            toolStripStatusLabel.Text = str;
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
        #endregion
        private async Task WaitAsyncResult(IAsyncResult iar)
        {
            await Task.Run(() => iar.AsyncWaitHandle.WaitOne());
        }
        #region 结果列表控制
        private async Task DisplayResult(IDictionary<string, IEnumerable<string>> result)
        {
            if (!result.Any())
            {
                return;
            }
            var count = result.Aggregate(0, (val, item) => val + item.Value.Count());
            ShowProgressBar(ProgressBarStyle.Continuous, count);

            await ResultViewDo(() => DoDisplayResult(result));
        }

        private void DoDisplayResult(IDictionary<string, IEnumerable<string>> result)
        {
            BeginUpdateResultView();

            foreach (var item in result)
            {
                var grp = new ListViewGroup(item.Key);
                var tmpResult = from path in item.Value
                                select new ListViewItem(path, grp);
                listView_result.Groups.Add(grp);
                foreach (var resItem in tmpResult)
                {
                    listView_result.Items.Add(resItem);
                    StepProgressBar();
                }
            }
            EndUpdateResultView();
        }
        #endregion


        private void OnResultItemDoubleClick(object sender, EventArgs e)
        {
            if (listView_result.SelectedItems?.Count == 1)
            {
                var path = listView_result.SelectedItems[0].Text;
                if (Utils.IsPathExist(path))
                {
                    Process.Start(path);
                }
            }
        }

        private async void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_result.SelectedItems.Count == 0)
            {
                return;
            }

            var text = string.Empty;
            await ResultViewDo(() => text = string.Join(Environment.NewLine, from ListViewItem item
                                                                             in listView_result.SelectedItems
                                                                             select item.Text));

            CopyToClipboard(text);

        }

        private static void CopyToClipboard(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            try
            {
                Clipboard.SetDataObject(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void copyGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_result.SelectedItems.Count == 0)
            {
                return;
            }
            var sb = new StringBuilder();
            await ResultViewDo(() =>
            {
                HashSet<string> groupNames = new HashSet<string>();
                foreach (ListViewItem item in listView_result.SelectedItems)
                {
                    var grpName = item.Group.Header;
                    if (groupNames.Add(grpName))
                    {
                        if (sb.Length != 0)
                        {
                            sb.AppendLine();
                        }
                        sb.AppendLine(grpName);
                    }
                    sb.Append('\t');
                    sb.AppendLine(item.Text);
                }
            });
            CopyToClipboard(sb.ToString());
        }

        private async void toolStripButton_clearResult_Click(object sender, EventArgs e)
        {
            await ClearResult();
            SetStatusText(string.Empty);
        }

        private async void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            await SetAllResultCheck(true, str => true);
            EndUpdateResultView();
        }

        private async void clearCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            await ClearCheck();
            EndUpdateResultView();
        }

        private async Task ClearCheck()
        {
            await SetAllResultCheck(false, str => true);
        }

        private async Task SetAllResultCheck(bool val, Predicate<string> pred)
        {
            await ForEachResult(item => pred(item.Text), item => item.Checked = val);
        }

        private IEnumerable<ListViewItem> SelectResults(Predicate<ListViewItem> pred)
        {
            return listView_result.Items.Cast<ListViewItem>().Where(i => pred(i));
        }

        private async void toolStripButton_search_Click(object sender, EventArgs e)
        {
            var text = toolStripTextBox_filterText.Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            BeginUpdateResultView();
            ClearSelect();
            var useRegex = toolStripButton_regex.Checked;

            Predicate<ListViewItem> pred = null;
            if (useRegex)
            {
                pred = item => Regex.IsMatch(item.Text, text);
            }
            else
            {
                pred = item => item.Text.Contains(text);
            }
            await ForEachResult(pred, item => item.Selected = true);
            if (listView_result.SelectedItems.Count > 0)
            {
                listView_result.SelectedItems[0].EnsureVisible();
            }
            EndUpdateResultView();
            listView_result.Select();
        }

        private void ClearSelect()
        {
            listView_result.SelectedItems.Clear();
        }

        private async Task ForEachResult(Predicate<ListViewItem> pred, Action<ListViewItem> doSth)
        {
            await ResultViewDo(() =>
            {
                foreach (var item in SelectResults(pred))
                {
                    doSth(item);
                }
            });
        }

        private async void toolStripButton_selectCheck_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            ClearSelect();
            await ForEachResult(item => item.Checked, item => item.Selected = true);
            EndUpdateResultView();
        }

        private async void checkAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            await ClearCheck();
            await ResultViewDo(() =>
            {
                foreach (ListViewGroup grp in listView_result.Groups)
                {
                    foreach (var item in grp.Items.Cast<ListViewItem>().Skip(1))
                    {
                        item.Checked = true;
                    }
                }
            });
            EndUpdateResultView();
        }

        private async Task ResultViewDo(Action doSth)
        {
            await WaitAsyncResult(listView_result.BeginInvoke(doSth));
        }

        private void listView_result_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                e.Item.BackColor = SystemColors.Highlight;
                e.Item.ForeColor = SystemColors.HighlightText;
            }
            else
            {
                e.Item.BackColor = SystemColors.Window;
                e.Item.ForeColor = SystemColors.WindowText;
            }
        }

        private void doToolStripMenuItem_Click(object sender, EventArgs e)
        {
            do_?.Invoke();
        }

        private async void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            await ForEachResult(item => true, item => item.Selected = true);
            EndUpdateResultView();
        }

        private void EndUpdateResultView()
        {
            listView_result.EndUpdate();
        }

        private void BeginUpdateResultView()
        {
            listView_result.BeginUpdate();
        }

        private async void invertSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            await ForEachResult(item => true, item => item.Selected = !item.Selected);
            EndUpdateResultView();
        }

        private async void invertCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginUpdateResultView();
            await ForEachResult(item => true, item => item.Checked = !item.Checked);
            EndUpdateResultView();
        }

        private void clearSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSelect();
        }
    }
}
