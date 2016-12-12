using MakeUnique.Lib;
using MakeUnique.Lib.Detail;
using MakeUnique.Lib.Finder;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            InitFinderButton();

            ClearResultView();
        }
        private void InitFinderButton()
        {
            toolStrip_Dir.SuspendLayout();
            toolStripDropDownButton_Find.DropDownItems.Clear();
            foreach (var item in FinderFactory.GetDuplicateFinders())
            {
                var tmp = toolStripDropDownButton_Find.DropDownItems.Add(item.Name);
                tmp.Click += async (sender, e) =>
                {
                    await FindFiles(item);
                };
            }

            toolStrip_Dir.ResumeLayout();

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
            toolStripDropDownButton_Find.Enabled = false;
        }
        private void UnLockFindButton()
        {
            toolStripDropDownButton_Find.Enabled = true;
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


        private DuplicateFileFinder finder_ = new DuplicateFileFinder();
        private CancellationTokenSource cancel_;
        private void OnAddDirButtonClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                AddDir(folderBrowserDialog.SelectedPath);
            }
        }

        private void AddDir(string path)
        {
            if (Directory.Exists(path) && finder_.Add(path))
            {
                UpdateDirListSize();
            }
        }

        private void UpdateDirListSize()
        {
            listView_DirList.VirtualListSize = finder_.Count();
        }


        private void OnDirListRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(finder_.GetDir(e.ItemIndex));
        }

        private void OnDelDirButtonClick(object sender, EventArgs e)
        {
            if (listView_DirList.SelectedIndices.Count == 0)
            {
                return;
            }

            if (listView_DirList.SelectedIndices.Count == finder_.Count())
            {
                finder_.Clear();
            }
            else
            {
                List<string> select = new List<string>();
                foreach (int item in listView_DirList.SelectedIndices)
                {
                    select.Add(finder_.GetDir(item));
                }
                select.ForEach((str) => finder_.Remove(str));
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
                Process.Start(listView_DupFiles.SelectedItems[0].Text);
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
                    await DelFile(item.Text);
                    StepProgess();
                    listView_DupFiles.Items.Remove(item);
                }
            }
            finally
            {
                EndRemove();
            }
        }

        private static async Task DelFile(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                    FileUtils.DeleteFile(path, false, true);
                }
                catch
                {

                }
            });
        }

        private async Task FindFiles(IGetDuplicate reader)
        {
            if (finder_.Count() == 0)
            {
                return;
            }
            try
            {
                BeginFind();
                var option = toolStripButton_IncludeSub.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var pattern = toolStripTextBox_Filter.Text.Trim();
                pattern = string.IsNullOrEmpty(pattern) ? "*" : pattern;

                var found = await DoFindFiles(reader, option, pattern);
                await AddGroup(found.Item1, found.Item2);
            }
            finally
            {
                EndFind();
            }
        }

        private async Task<Tuple<ListViewGroup[], ListViewItem[]>> DoFindFiles(IGetDuplicate reader, SearchOption option, string pattern)
        {
            Tuple<ListViewGroup[], ListViewItem[]> result = null;

            await Task.Run(() =>
            {
                ConcurrentBag<ListViewGroup> grpBag = new ConcurrentBag<ListViewGroup>();
                ConcurrentBag<ListViewItem> itemBag = new ConcurrentBag<ListViewItem>();
                var files = finder_.GetDuplicateFiles(pattern, option, reader, cancel_);
                try
                {
                    files.ForAll((item) =>
                    {
                        var grp = new ListViewGroup(reader.ConvertGroupKey(item.Key));

                        foreach (var str in item)
                        {
                            var listviewItem = new ListViewItem(str, grp);
                            grp.Items.Add(listviewItem);
                            itemBag.Add(listviewItem);
                        }
                        grpBag.Add(grp);
                    });
                }
                catch (OperationCanceledException)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, "查找过程由用户取消");
                    }));
                }
                catch (Exception exc)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, exc.InnerException.Message, exc.Message);
                    }));
                }
                finally
                {
                    result = new Tuple<ListViewGroup[], ListViewItem[]>(grpBag.ToArray(), itemBag.ToArray());
                }
            });
            return result;
        }

        private async Task AddGroup(ListViewGroup[] grps, ListViewItem[] items)
        {
            // FIX: 虚拟模式不能用组吗？这样几千个文件的时候会卡
            // 这里消耗时间甚至比搜索时间要多,如果不能用组，只能换一种表示方式了
            var asyncResult = listView_DupFiles.BeginInvoke(new Action(() =>
            {
                listView_DupFiles.BeginUpdate();
                listView_DupFiles.Groups.AddRange(grps);
                //listView_DupFiles.VirtualMode = true;
                //listView_DupFiles.RetrieveVirtualItem += (sender, e) =>
                //{
                //    e.Item = itemBag[e.ItemIndex];
                //};
                //listView_DupFiles.VirtualListSize = itemBag.Length;
                listView_DupFiles.Items.AddRange(items);
                listView_DupFiles.EndUpdate();
            }));
            await Task.Run(() => { asyncResult.AsyncWaitHandle.WaitOne(); });
        }

        private void BeginFind()
        {
            cancel_ = new CancellationTokenSource();
            toolStripDropDownButton_Find.Visible = false;
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
            toolStripDropDownButton_Find.Visible = true;
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

            foreach (var item in data)
            {
                AddDir(item);
            }
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
