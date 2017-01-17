using MakeUnique.Lib;
using MakeUnique.Lib.Detail;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeUnique
{
    // 并不是什么有用的类库，有时候用到这些功能而网上下载的不太满意就弄个
    // 这个界面是用来测试的有点乱
    public partial class GUI : Form
    {
        [Import(typeof(IPluginUtil))]
        private IPluginUtil pluginUtil_;
       
        public GUI()
        {
            CatalogHelper.ComposeParts(this);
            InitializeComponent();
            InitPluginButtons();
            ClearResultView();
        }
        private void InitPluginButtons()
        {
            toolStrip_Dir.SuspendLayout();
            pluginButtons.DropDownItems.Clear();
            foreach (var item in pluginUtil_.GetPlugins())
            {
                pluginButtons.DropDownItems.Add(item.Name).Click +=
                    async (sender, e) => await OnPluginClick(item);
            }
            toolStrip_Dir.ResumeLayout();
        }
        private bool IsIncludeSub { get { return toolStripButton_IncludeSub.Checked; } }
        private string Pattern
        {
            get
            {
                var result = toolStripTextBox_Filter.Text.Trim();
                return string.IsNullOrEmpty(result) ? "*" : result;
            }
        }



        private async Task OnPluginClick(IPlugin plugin)
        {
            try
            {
                BeginFind();
                var result = pluginUtil_.Do(plugin, Pattern, IsIncludeSub);
                if (result != null)
                {
                    await Output(result);
                }
            }
            finally
            {
                EndFind();
            }
        }

        public async Task Output(ParallelQuery<IGrouping<string, string>> resultQuery)
        {
            ConcurrentDictionary<string, ConcurrentBag<string>> result = new ConcurrentDictionary<string, ConcurrentBag<string>>();

            await Task.Run(() =>
            {
                try
                {
                    var files = resultQuery.WithCancellation(cancel_.Token);
                    files.ForAll((item) =>
                        result.AddOrUpdate(
                            item.Key,
                            (str) => new ConcurrentBag<string>(item),
                            (str, bag) =>
                            {
                                foreach (var path in item)
                                {
                                    bag.Add(path);
                                }
                                return bag;
                            })
                    );
                }
                catch (OperationCanceledException)
                {
                    Invoke(new Action(() => MessageBox.Show(this, "查找过程由用户取消")));
                }
                catch (Exception exc)
                {
                    Invoke(new Action(() => MessageBox.Show(this, exc.InnerException.Message, exc.Message)));
                }
            });
            await AddGroup(result);
        }

        private void LockDirList()
        {
            splitContainer1.Panel1.Enabled = false;
        }
        private void LockResultList()
        {
            splitContainer1.Panel2.Enabled = false;
        }
        private void UnLockDirList()
        {
            splitContainer1.Panel1.Enabled = true;
        }
        private void UnLockResultList()
        {
            splitContainer1.Panel2.Enabled = true;
        }
        private void LockFindButton()
        {
            pluginButtons.Enabled = false;
        }
        private void UnLockFindButton()
        {
            pluginButtons.Enabled = true;
        }
        private void ShowProgess()
        {
            toolStripProgressBar.Visible = true;
        }
        private void HideProgess()
        {
            toolStripProgressBar.Visible = false;
        }
        private void StepProgess()
        {
            toolStripProgressBar.PerformStep();
        }

        private void SetMaxProgress(int val)
        {
            toolStripProgressBar.Maximum = val;
        }

        private void ClearProgress()
        {
            toolStripProgressBar.Value = 0;
        }

        private CancellationTokenSource cancel_;
        private void OnAddDirButtonClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Add(folderBrowserDialog.SelectedPath);
            }
        }

        private void OnAddFileButtonClick(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Add(openFileDialog.FileNames);
            }
        }
        private static bool IsPathExist(string path)
        {
            return (Directory.Exists(path) || File.Exists(path));
        }
        private void Add(string path)
        {
            if (IsPathExist(path) && pluginUtil_.Add(path))
            {
                UpdateDirListSize();
            }
        }
        private void Add(string[] path)
        {
            bool needUpdate = false;
            foreach (var item in path)
            {
                if (IsPathExist(item) && pluginUtil_.Add(item))
                {
                    needUpdate = true;
                }
            }
            if (needUpdate)
            {
                UpdateDirListSize();
            }
        }
        private void UpdateDirListSize()
        {
            listView_DirList.VirtualListSize = pluginUtil_.Count;
        }


        private void OnDirListRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(pluginUtil_.GetPath(e.ItemIndex));
        }

        private void OnDelDirButtonClick(object sender, EventArgs e)
        {
            if (listView_DirList.SelectedIndices.Count == 0)
            {
                return;
            }

            if (listView_DirList.SelectedIndices.Count == pluginUtil_.Count)
            {
                pluginUtil_.Clear();
            }
            else
            {
                List<string> select = new List<string>();
                foreach (int item in listView_DirList.SelectedIndices)
                {
                    select.Add(pluginUtil_.GetPath(item));
                }
                select.ForEach((str) => pluginUtil_.Remove(str));
            }
            UpdateDirListSize();
            listView_DirList.SelectedIndices.Clear();
        }



        private void ClearResultView()
        {
            Text = string.Empty;
            listView_DupFiles.Items.Clear();
            listView_DupFiles.Groups.Clear();
        }

        private void OnClearResultButtonClick(object sender, EventArgs e)
        {
            ClearResultView();
        }


        private void OnResultItemDoubleClick(object sender, EventArgs e)
        {
            if (listView_DupFiles.SelectedItems?.Count == 1)
            {
                var path = listView_DupFiles.SelectedItems[0].Text;
                if (IsPathExist(path))
                {
                    Process.Start(path);
                }
            }
        }

        private void OnSelectResultButtonClick(object sender, EventArgs e)
        {
            ForAllDuplicateGroups((val) => { val.Items[0].Checked = false; }, (val) => { val.Checked = true; });
        }


        private bool CheckSelect()
        {
            return !listView_DupFiles.Groups.Cast<ListViewGroup>().Any((grp) =>
                grp.Items.Cast<ListViewItem>().All((item) => item.Checked));
        }
        private void EndRemove()
        {
            HideProgess();
            UnLockResultList();
            UnLockDirList();
        }

        private void BeginRemove()
        {
            LockResultList();
            LockDirList();
            ClearProgress();
            ShowProgess();
        }

        private async void OnRemoveFileButtonClick(object sender, EventArgs e)
        {
            // 检查选择项
            if (!CheckSelect())
            {
                if (MessageBox.Show("确定删除同一特征的所有文件？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                BeginRemove();
                var del = listView_DupFiles.CheckedItems.Cast<ListViewItem>();

                SetMaxProgress(del.Count());

                foreach (var item in del)
                {
                    await Utils.RecycleFile(item.Text);
                    StepProgess();
                }
                listView_DupFiles.BeginUpdate();
                foreach (var item in del)
                {
                    listView_DupFiles.Items.Remove(item);
                }
                listView_DupFiles.EndUpdate();

            }
            finally
            {
                EndRemove();
            }
        }


        private async Task AddGroup(ConcurrentDictionary<string, ConcurrentBag<string>> grps)
        {
            // FIX: 虚拟模式不能用组吗？这样几千个文件的时候会卡
            // 这里消耗时间甚至比搜索时间要多,如果不能用组，只能换一种表示方式了
            var asyncResult = listView_DupFiles.BeginInvoke(new Action(() =>
            {
                listView_DupFiles.BeginUpdate();
                //listView_DupFiles.VirtualMode = true;
                //listView_DupFiles.RetrieveVirtualItem += (sender, e) =>
                //{
                //    e.Item = itemBag[e.ItemIndex];
                //};
                //listView_DupFiles.VirtualListSize = itemBag.Length;
                // listView_DupFiles.Items.AddRange(items);
                foreach (var grpItem in grps)
                {
                    // FIX: 这样速度不快，但是AddRange会有奇怪的并组显示问题
                    ListViewGroup grp = new ListViewGroup(grpItem.Key);

                    listView_DupFiles.Groups.Add(grp);
                    foreach (var item in grpItem.Value)
                    {
                        listView_DupFiles.Items.Add(new ListViewItem(item, grp));
                    }

                }
                listView_DupFiles.EndUpdate();
            }));
            await Task.Run(() => { asyncResult.AsyncWaitHandle.WaitOne(); });
        }

        private void BeginFind()
        {
            cancel_ = new CancellationTokenSource();
            pluginButtons.Visible = false;
            toolStripButton_Cancel.Text = "按Esc取消";
            toolStripButton_Cancel.Visible = true;

            LockResultList();
            LockDirList();
            ClearProgress();
            ShowProgess();
            ClearResultView();

            Text = "正在查找...";
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
        }
        private void EndFind()
        {
            cancel_ = null;
            pluginButtons.Visible = true;
            toolStripButton_Cancel.Visible = false;

            HideProgess();
            UnLockResultList();
            UnLockDirList();

            Text = $"找到 {listView_DupFiles.Groups.Count}个重复，共 {listView_DupFiles.Items.Count} 个文件";
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
        }

        private void ForAllDuplicateGroups(Action<ListViewGroup> grpCallback, Action<ListViewItem> itemCallback)
        {
            foreach (ListViewGroup grp in listView_DupFiles.Groups)
            {
                foreach (ListViewItem item in grp.Items)
                {
                    itemCallback?.Invoke(item);
                }
                grpCallback?.Invoke(grp);
            }
        }

        private void OnAllCheckButtonClick(object sender, EventArgs e)
        {
            ChangeDupChecked(true);
        }

        private void OnClearCheckButtonClick(object sender, EventArgs e)
        {
            ChangeDupChecked(false);
        }

        private void ChangeDupChecked(bool isChecked)
        {
            ForAllDuplicateFiles((item) => { item.Checked = isChecked; });
        }

        private void ForAllDuplicateFiles(Action<ListViewItem> itemCallback)
        {
            foreach (ListViewItem item in listView_DupFiles.Items)
            {
                itemCallback?.Invoke(item);
            }
        }

        private void OnSearchButtonClick(object sender, EventArgs e)
        {
            listView_DupFiles.SelectedItems.Clear();
            var search = toolStripTextBox_Search.Text;
            if (string.IsNullOrEmpty(search))
            {
                return;
            }
            ForAllDuplicateFiles((item) =>
            {
                if (item.Text.Contains(search))
                {
                    item.Selected = true;
                }
            });
            listView_DupFiles.Select();
        }

        private void OnGUIKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (cancel_.IsCancellationRequested)
                {
                    return;
                }
                cancel_.Cancel();
                if (cancel_.IsCancellationRequested)
                {
                    toolStripButton_Cancel.Text = "正在取消";
                }
            }
        }

        private void OnDirListDragDrop(object sender, DragEventArgs e)
        {
            var data = (string[])e.Data.GetData(DataFormats.FileDrop);
            Add(data);
        }

        private void OnDirListDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }


    }
}
