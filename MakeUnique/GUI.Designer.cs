namespace MakeUnique
{
    partial class GUI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.listView_DupFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip_Dir = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Add = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Del = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_IncludeSub = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_Filter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton_Find = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton_Cancel = new System.Windows.Forms.ToolStripButton();
            this.listView_DirList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip_File = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_RemoveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Clear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Select = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_ClearCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_AllCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Search = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox_Search = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip_Dir.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip_File.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_DupFiles
            // 
            this.listView_DupFiles.CheckBoxes = true;
            this.listView_DupFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_DupFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_DupFiles.FullRowSelect = true;
            this.listView_DupFiles.GridLines = true;
            this.listView_DupFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_DupFiles.Location = new System.Drawing.Point(3, 30);
            this.listView_DupFiles.Name = "listView_DupFiles";
            this.listView_DupFiles.ShowItemToolTips = true;
            this.listView_DupFiles.Size = new System.Drawing.Size(873, 528);
            this.listView_DupFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_DupFiles.TabIndex = 3;
            this.listView_DupFiles.UseCompatibleStateImageBehavior = false;
            this.listView_DupFiles.View = System.Windows.Forms.View.Details;
            this.listView_DupFiles.DoubleClick += new System.EventHandler(this.OnResultItemDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "重复文件列表";
            this.columnHeader1.Width = 850;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1MinSize = 400;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel2MinSize = 450;
            this.splitContainer1.Size = new System.Drawing.Size(1281, 561);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip_Dir, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listView_DirList, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 561);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip_Dir
            // 
            this.toolStrip_Dir.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_Dir.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_Dir.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Add,
            this.toolStripButton_Del,
            this.toolStripSeparator3,
            this.toolStripButton_IncludeSub,
            this.toolStripLabel2,
            this.toolStripTextBox_Filter,
            this.toolStripSeparator1,
            this.toolStripDropDownButton_Find,
            this.toolStripButton_Cancel});
            this.toolStrip_Dir.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_Dir.Name = "toolStrip_Dir";
            this.toolStrip_Dir.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip_Dir.Size = new System.Drawing.Size(400, 27);
            this.toolStrip_Dir.TabIndex = 3;
            this.toolStrip_Dir.Text = "toolStrip1";
            // 
            // toolStripButton_Add
            // 
            this.toolStripButton_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Add.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Add.Image")));
            this.toolStripButton_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Add.Name = "toolStripButton_Add";
            this.toolStripButton_Add.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_Add.Text = "添加";
            this.toolStripButton_Add.Click += new System.EventHandler(this.OnAddDirButtonClick);
            // 
            // toolStripButton_Del
            // 
            this.toolStripButton_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Del.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Del.Image")));
            this.toolStripButton_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Del.Name = "toolStripButton_Del";
            this.toolStripButton_Del.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_Del.Text = "删除";
            this.toolStripButton_Del.Click += new System.EventHandler(this.OnDelDirButtonClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_IncludeSub
            // 
            this.toolStripButton_IncludeSub.Checked = true;
            this.toolStripButton_IncludeSub.CheckOnClick = true;
            this.toolStripButton_IncludeSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton_IncludeSub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_IncludeSub.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_IncludeSub.Image")));
            this.toolStripButton_IncludeSub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_IncludeSub.Name = "toolStripButton_IncludeSub";
            this.toolStripButton_IncludeSub.Size = new System.Drawing.Size(73, 24);
            this.toolStripButton_IncludeSub.Text = "包含子级";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel2.Text = "筛选";
            // 
            // toolStripTextBox_Filter
            // 
            this.toolStripTextBox_Filter.Name = "toolStripTextBox_Filter";
            this.toolStripTextBox_Filter.Size = new System.Drawing.Size(50, 27);
            this.toolStripTextBox_Filter.Text = "*";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDropDownButton_Find
            // 
            this.toolStripDropDownButton_Find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton_Find.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton_Find.Image")));
            this.toolStripDropDownButton_Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_Find.Name = "toolStripDropDownButton_Find";
            this.toolStripDropDownButton_Find.Size = new System.Drawing.Size(83, 24);
            this.toolStripDropDownButton_Find.Text = "查找重复";
            // 
            // toolStripButton_Cancel
            // 
            this.toolStripButton_Cancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Cancel.Image")));
            this.toolStripButton_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Cancel.Name = "toolStripButton_Cancel";
            this.toolStripButton_Cancel.Size = new System.Drawing.Size(81, 24);
            this.toolStripButton_Cancel.Text = "按Esc取消";
            this.toolStripButton_Cancel.Visible = false;
            // 
            // listView_DirList
            // 
            this.listView_DirList.AllowDrop = true;
            this.listView_DirList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_DirList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_DirList.FullRowSelect = true;
            this.listView_DirList.GridLines = true;
            this.listView_DirList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_DirList.Location = new System.Drawing.Point(3, 30);
            this.listView_DirList.Name = "listView_DirList";
            this.listView_DirList.ShowGroups = false;
            this.listView_DirList.Size = new System.Drawing.Size(394, 528);
            this.listView_DirList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_DirList.TabIndex = 2;
            this.listView_DirList.UseCompatibleStateImageBehavior = false;
            this.listView_DirList.View = System.Windows.Forms.View.Details;
            this.listView_DirList.VirtualMode = true;
            this.listView_DirList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.OnDirListRetrieveVirtualItem);
            this.listView_DirList.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDirListDragDrop);
            this.listView_DirList.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDirListDragEnter);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "文件夹列表";
            this.columnHeader2.Width = 389;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.listView_DupFiles, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip_File, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(879, 561);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // toolStrip_File
            // 
            this.toolStrip_File.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_File.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip_File.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_RemoveFile,
            this.toolStripSeparator2,
            this.toolStripButton_Clear,
            this.toolStripSeparator5,
            this.toolStripButton_Select,
            this.toolStripSeparator4,
            this.toolStripButton_ClearCheck,
            this.toolStripButton_AllCheck,
            this.toolStripSeparator6,
            this.toolStripButton_Search,
            this.toolStripTextBox_Search});
            this.toolStrip_File.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_File.Name = "toolStrip_File";
            this.toolStrip_File.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip_File.Size = new System.Drawing.Size(879, 27);
            this.toolStrip_File.TabIndex = 4;
            this.toolStrip_File.Text = "toolStrip2";
            // 
            // toolStripButton_RemoveFile
            // 
            this.toolStripButton_RemoveFile.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_RemoveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_RemoveFile.ForeColor = System.Drawing.Color.Red;
            this.toolStripButton_RemoveFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_RemoveFile.Image")));
            this.toolStripButton_RemoveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_RemoveFile.Name = "toolStripButton_RemoveFile";
            this.toolStripButton_RemoveFile.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_RemoveFile.Text = "清理";
            this.toolStripButton_RemoveFile.Click += new System.EventHandler(this.OnRemoveFileButtonClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_Clear
            // 
            this.toolStripButton_Clear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Clear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Clear.Image")));
            this.toolStripButton_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Clear.Name = "toolStripButton_Clear";
            this.toolStripButton_Clear.Size = new System.Drawing.Size(73, 24);
            this.toolStripButton_Clear.Text = "清空列表";
            this.toolStripButton_Clear.Click += new System.EventHandler(this.OnClearResultButtonClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_Select
            // 
            this.toolStripButton_Select.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_Select.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Select.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Select.Image")));
            this.toolStripButton_Select.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Select.Name = "toolStripButton_Select";
            this.toolStripButton_Select.Size = new System.Drawing.Size(133, 24);
            this.toolStripButton_Select.Text = "自动标记重复文件";
            this.toolStripButton_Select.Click += new System.EventHandler(this.OnSelectResultButtonClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_ClearCheck
            // 
            this.toolStripButton_ClearCheck.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_ClearCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_ClearCheck.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_ClearCheck.Image")));
            this.toolStripButton_ClearCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ClearCheck.Name = "toolStripButton_ClearCheck";
            this.toolStripButton_ClearCheck.Size = new System.Drawing.Size(103, 24);
            this.toolStripButton_ClearCheck.Text = "清除所有标记";
            this.toolStripButton_ClearCheck.Click += new System.EventHandler(this.OnClearCheckButtonClick);
            // 
            // toolStripButton_AllCheck
            // 
            this.toolStripButton_AllCheck.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_AllCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_AllCheck.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_AllCheck.Image")));
            this.toolStripButton_AllCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AllCheck.Name = "toolStripButton_AllCheck";
            this.toolStripButton_AllCheck.Size = new System.Drawing.Size(58, 24);
            this.toolStripButton_AllCheck.Text = "全标记";
            this.toolStripButton_AllCheck.Click += new System.EventHandler(this.OnAllCheckButtonClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_Search
            // 
            this.toolStripButton_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton_Search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Search.Image")));
            this.toolStripButton_Search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Search.Name = "toolStripButton_Search";
            this.toolStripButton_Search.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_Search.Text = "搜索";
            this.toolStripButton_Search.Click += new System.EventHandler(this.OnSearchButtonClick);
            // 
            // toolStripTextBox_Search
            // 
            this.toolStripTextBox_Search.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_Search.Name = "toolStripTextBox_Search";
            this.toolStripTextBox_Search.Size = new System.Drawing.Size(120, 27);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 561);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1281, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 18);
            this.toolStripProgressBar.Step = 1;
            this.toolStripProgressBar.Visible = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "选择要查找重复文件的文件夹";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1281, 583);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1020, 630);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "简单测试";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnGUIKeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip_Dir.ResumeLayout(false);
            this.toolStrip_Dir.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip_File.ResumeLayout(false);
            this.toolStrip_File.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView_DupFiles;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ListView listView_DirList;
        private System.Windows.Forms.ToolStrip toolStrip_Dir;
        private System.Windows.Forms.ToolStrip toolStrip_File;
        private System.Windows.Forms.ToolStripButton toolStripButton_Add;
        private System.Windows.Forms.ToolStripButton toolStripButton_Del;
        private System.Windows.Forms.ToolStripButton toolStripButton_RemoveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Select;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_IncludeSub;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Filter;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_Find;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_Clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_AllCheck;
        private System.Windows.Forms.ToolStripButton toolStripButton_ClearCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Search;
        private System.Windows.Forms.ToolStripButton toolStripButton_Search;
        private System.Windows.Forms.ToolStripButton toolStripButton_Cancel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}

