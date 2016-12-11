using MakeUnique.Lib;
using MakeUnique.Lib.Detail;
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

            ClearResultView();
        }

        private DuplicateFileFinder finder_ = new DuplicateFileFinder();
        private CancellationTokenSource cancel_;
        private void toolStripButton_Add_Click(object sender, EventArgs e)
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

        private void listView_DirList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(finder_.ElementAt(e.ItemIndex));
        }


        private void toolStripButton_Del_Click(object sender, EventArgs e)
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
                    select.Add(finder_.ElementAt(item));
                }
                select.ForEach((str) => finder_.Remove(str));
            }
            UpdateDirListSize();
            listView_DirList.SelectedIndices.Clear();
        }


        private async void ToolStripMenuItem_FindSameName_Click(object sender, EventArgs e)
        {
            await FindFiles(new FileNameReader());
        }

        private async void ToolStripMenuItem_FindSameSize_Click(object sender, EventArgs e)
        {
            await FindFiles(new FileSizeReader());
        }

        private async void ToolStripMenuItem_FindSameMd5_Click(object sender, EventArgs e)
        {
            await FindFiles(new FileMd5Reader());
        }
        private void toolStripButton_Clear_Click(object sender, EventArgs e)
        {
            ClearResultView();
        }

        private void listView_DupFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listView_DupFiles.SelectedItems?.Count == 1)
            {
                Process.Start(listView_DupFiles.SelectedItems[0].Text);
            }
        }

        private void toolStripButton_Select_Click(object sender, EventArgs e)
        {
            ForAllDuplicateGroups((val) => { val.Items[0].Checked = false; }, (val) => { val.Checked = true; });
        }


        private bool CheckSelect()
        {
            return !listView_DupFiles.Groups.Cast<ListViewGroup>().Any((grp) =>
                grp.Items.Cast<ListViewItem>().All((item) => item.Checked));
        }
        private async void toolStripButton_RemoveFile_Click(object sender, EventArgs e)
        {
            // 检查选择项
            if (!CheckSelect())
            {
                if (MessageBox.Show("确定删除同一特征的所有文件？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            BeginRemove();

            var del = listView_DupFiles.CheckedItems.Cast<ListViewItem>();

            SetMaxProgress(del.Count());

            foreach (var item in del)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        FileUtils.DeleteFile(item.Text, false, true);
                    }
                    catch (Exception)
                    {

                    }
                });
                StepProgess();
                listView_DupFiles.Items.Remove(item);
            }

            EndRemove();
        }

        private void StepProgess()
        {
            toolStripProgressBar.PerformStep();
        }

        private void SetMaxProgress(int val)
        {
            toolStripProgressBar.Maximum = val;
        }

        private void ClearResultView()
        {
            Text = string.Empty;
            listView_DupFiles.Items.Clear();
            listView_DupFiles.Groups.Clear();
        }
        private async Task FindFiles(IFileInfoReader reader)
        {
            if (finder_.Count() == 0)
            {
                return;
            }
            BeginFind();
            var option = toolStripButton_IncludeSub.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var pattern = toolStripTextBox_Filter.Text.Trim();
            pattern = string.IsNullOrEmpty(pattern) ? "*" : pattern;

            await DoFindFiles(reader, option, pattern);

        }

        private async Task DoFindFiles(IFileInfoReader reader, SearchOption option, string pattern)
        {
            ConcurrentBag<ListViewGroup> grpBag = new ConcurrentBag<ListViewGroup>();
            ConcurrentBag<ListViewItem> itemBag = new ConcurrentBag<ListViewItem>();
            await Task.Run(() =>
            {
                var result = finder_.GetDuplicateFiles(pattern, option, reader, cancel_);
                try
                {
                    result.ForAll((item) =>
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
                    MessageBox.Show(this, "查找过程由用户取消");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(this, exc.InnerException.Message, exc.Message);
                }
            });
            AddGroup(grpBag, itemBag);
        }

        private void BeginFind()
        {
            cancel_ = new CancellationTokenSource();
            toolStripDropDownButton.Visible = false;
            toolStripButton_Cancel.Visible = true;
            toolStrip_Dir.Enabled = false;
            BeginRemove();
            ClearResultView();
            Text = "正在查找...";
            toolStripProgressBar.Style = ProgressBarStyle.Marquee;
        }
        private void EndFind()
        {
            cancel_ = null;
            toolStripDropDownButton.Visible = true;
            toolStripButton_Cancel.Visible = false;
            toolStrip_Dir.Enabled = true;
            EndRemove();
            Text = $"找到 {listView_DupFiles.Groups.Count}个重复，共 {listView_DupFiles.Items.Count} 个文件";
            toolStripProgressBar.Style = ProgressBarStyle.Blocks;
        }


        private void AddGroup(ConcurrentBag<ListViewGroup> grpBag, ConcurrentBag<ListViewItem> itemBag)
        {
            // FIX: 虚拟模式不能用组吗？这样几千个文件的时候会卡
            // 这里消耗时间甚至比搜索时间要多,如果不能用组，只能换一种表示方式了
            listView_DupFiles.BeginInvoke(new Action(() =>
            {
                listView_DupFiles.BeginUpdate();
                listView_DupFiles.Groups.AddRange(grpBag.ToArray());
                //listView_DupFiles.VirtualMode = true;
                //listView_DupFiles.RetrieveVirtualItem += (sender, e) =>
                //{
                //    e.Item = itemBag[e.ItemIndex];
                //};
                //listView_DupFiles.VirtualListSize = itemBag.Length;
                listView_DupFiles.Items.AddRange(itemBag.ToArray());
                listView_DupFiles.EndUpdate();

                EndFind();
            }));
            
        }
        private void EndRemove()
        {
            toolStripProgressBar.Visible = false;
            toolStrip_Dir.Enabled = true;
            toolStrip_File.Enabled = true;
            listView_DupFiles.Enabled = true;
        }

        private void BeginRemove()
        {
            toolStrip_Dir.Enabled = false;
            toolStrip_File.Enabled = false;
            listView_DupFiles.Enabled = false;
            toolStripProgressBar.Visible = true;
            toolStripProgressBar.Value = 0;
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

        private void toolStripButton_AllCheck_Click(object sender, EventArgs e)
        {
            ChangeDupChecked(true);
        }

        private void toolStripButton_ClearCheck_Click(object sender, EventArgs e)
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

        private void toolStripButton_Search_Click(object sender, EventArgs e)
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

        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cancel_?.Cancel();
            }
        }

        private void listView_DirList_DragDrop(object sender, DragEventArgs e)
        {
            var data = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var item in data)
            {
                AddDir(item);
            }
        }

        private void listView_DirList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }


    }
}
