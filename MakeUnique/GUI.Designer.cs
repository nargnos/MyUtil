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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listView_path = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_path = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_Filter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_IncludeSub = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton_plugin = new System.Windows.Forms.ToolStripDropDownButton();
            this.listView_result = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_result = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.doToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox_filterText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_regex = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_search = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.selectCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_clearResult = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog_path = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.invertSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip_path.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip_result.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listView_path);
            this.splitContainer.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listView_result);
            this.splitContainer.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer.Size = new System.Drawing.Size(1281, 561);
            this.splitContainer.SplitterDistance = 422;
            this.splitContainer.TabIndex = 0;
            // 
            // listView_path
            // 
            this.listView_path.AllowDrop = true;
            this.listView_path.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_path.ContextMenuStrip = this.contextMenuStrip_path;
            this.listView_path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_path.FullRowSelect = true;
            this.listView_path.GridLines = true;
            this.listView_path.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_path.Location = new System.Drawing.Point(0, 27);
            this.listView_path.Name = "listView_path";
            this.listView_path.ShowGroups = false;
            this.listView_path.Size = new System.Drawing.Size(422, 534);
            this.listView_path.TabIndex = 1;
            this.listView_path.UseCompatibleStateImageBehavior = false;
            this.listView_path.View = System.Windows.Forms.View.Details;
            this.listView_path.VirtualMode = true;
            this.listView_path.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.OnPathViewRetrieveVirtualItem);
            this.listView_path.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnPathDragDrop);
            this.listView_path.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnPathDragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 418;
            // 
            // contextMenuStrip_path
            // 
            this.contextMenuStrip_path.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_path.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.contextMenuStrip_path.Name = "contextMenuStrip_path";
            this.contextMenuStrip_path.Size = new System.Drawing.Size(115, 82);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.addToolStripMenuItem.Text = "添加";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.OnAddPathButtonClick);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.removeToolStripMenuItem.Text = "删除";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.OnRemovePathButtonClick);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.clearToolStripMenuItem.Text = "清空";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.OnClearPathButtonClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox_Filter,
            this.toolStripButton_IncludeSub,
            this.toolStripSeparator1,
            this.toolStripDropDownButton_plugin});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(422, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 24);
            this.toolStripLabel1.Text = "文件过滤";
            // 
            // toolStripTextBox_Filter
            // 
            this.toolStripTextBox_Filter.Name = "toolStripTextBox_Filter";
            this.toolStripTextBox_Filter.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripButton_IncludeSub
            // 
            this.toolStripButton_IncludeSub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripButton_IncludeSub.Checked = true;
            this.toolStripButton_IncludeSub.CheckOnClick = true;
            this.toolStripButton_IncludeSub.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton_IncludeSub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_IncludeSub.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_IncludeSub.Image")));
            this.toolStripButton_IncludeSub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_IncludeSub.Name = "toolStripButton_IncludeSub";
            this.toolStripButton_IncludeSub.Size = new System.Drawing.Size(103, 24);
            this.toolStripButton_IncludeSub.Text = "包含子文件夹";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDropDownButton_plugin
            // 
            this.toolStripDropDownButton_plugin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton_plugin.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton_plugin.Image")));
            this.toolStripDropDownButton_plugin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_plugin.Name = "toolStripDropDownButton_plugin";
            this.toolStripDropDownButton_plugin.Size = new System.Drawing.Size(53, 24);
            this.toolStripDropDownButton_plugin.Text = "任务";
            // 
            // listView_result
            // 
            this.listView_result.CheckBoxes = true;
            this.listView_result.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_result.ContextMenuStrip = this.contextMenuStrip_result;
            this.listView_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_result.FullRowSelect = true;
            this.listView_result.GridLines = true;
            this.listView_result.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_result.Location = new System.Drawing.Point(0, 27);
            this.listView_result.Name = "listView_result";
            this.listView_result.ShowItemToolTips = true;
            this.listView_result.Size = new System.Drawing.Size(855, 534);
            this.listView_result.TabIndex = 2;
            this.listView_result.UseCompatibleStateImageBehavior = false;
            this.listView_result.View = System.Windows.Forms.View.Details;
            this.listView_result.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_result_ItemSelectionChanged);
            this.listView_result.DoubleClick += new System.EventHandler(this.OnResultItemDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 840;
            // 
            // contextMenuStrip_result
            // 
            this.contextMenuStrip_result.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_result.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyGroupToolStripMenuItem,
            this.toolStripMenuItem1,
            this.doToolStripMenuItem});
            this.contextMenuStrip_result.Name = "contextMenuStrip_result";
            this.contextMenuStrip_result.Size = new System.Drawing.Size(205, 88);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.copyToolStripMenuItem.Text = "复制";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyGroupToolStripMenuItem
            // 
            this.copyGroupToolStripMenuItem.Name = "copyGroupToolStripMenuItem";
            this.copyGroupToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.copyGroupToolStripMenuItem.Text = "复制(按组)";
            this.copyGroupToolStripMenuItem.Click += new System.EventHandler(this.copyGroupToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(201, 6);
            // 
            // doToolStripMenuItem
            // 
            this.doToolStripMenuItem.Name = "doToolStripMenuItem";
            this.doToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.doToolStripMenuItem.Text = "对标记项应用插件";
            this.doToolStripMenuItem.Click += new System.EventHandler(this.doToolStripMenuItem_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox_filterText,
            this.toolStripButton_regex,
            this.toolStripButton_search,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripButton2,
            this.toolStripSeparator3,
            this.toolStripButton_clearResult});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(855, 27);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripTextBox_filterText
            // 
            this.toolStripTextBox_filterText.Name = "toolStripTextBox_filterText";
            this.toolStripTextBox_filterText.Size = new System.Drawing.Size(350, 27);
            // 
            // toolStripButton_regex
            // 
            this.toolStripButton_regex.CheckOnClick = true;
            this.toolStripButton_regex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_regex.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_regex.Image")));
            this.toolStripButton_regex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_regex.Name = "toolStripButton_regex";
            this.toolStripButton_regex.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_regex.Text = "正则";
            // 
            // toolStripButton_search
            // 
            this.toolStripButton_search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_search.Image")));
            this.toolStripButton_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_search.Name = "toolStripButton_search";
            this.toolStripButton_search.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_search.Text = "搜索";
            this.toolStripButton_search.Click += new System.EventHandler(this.toolStripButton_search_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectCheckToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.invertSelectToolStripMenuItem,
            this.clearSelectToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(53, 24);
            this.toolStripDropDownButton1.Text = "选择";
            // 
            // selectCheckToolStripMenuItem
            // 
            this.selectCheckToolStripMenuItem.Name = "selectCheckToolStripMenuItem";
            this.selectCheckToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.selectCheckToolStripMenuItem.Text = "标记项";
            this.selectCheckToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton_selectCheck_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.selectAllToolStripMenuItem.Text = "全选";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.checkAutoToolStripMenuItem,
            this.invertCheckToolStripMenuItem,
            this.clearCheckToolStripMenuItem});
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(53, 24);
            this.toolStripButton2.Text = "标记";
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.checkAllToolStripMenuItem.Text = "全标记";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // checkAutoToolStripMenuItem
            // 
            this.checkAutoToolStripMenuItem.Name = "checkAutoToolStripMenuItem";
            this.checkAutoToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.checkAutoToolStripMenuItem.Text = "全标记(不标第一个)";
            this.checkAutoToolStripMenuItem.Click += new System.EventHandler(this.checkAutoToolStripMenuItem_Click);
            // 
            // clearCheckToolStripMenuItem
            // 
            this.clearCheckToolStripMenuItem.Name = "clearCheckToolStripMenuItem";
            this.clearCheckToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.clearCheckToolStripMenuItem.Text = "清空标记";
            this.clearCheckToolStripMenuItem.Click += new System.EventHandler(this.clearCheckToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton_clearResult
            // 
            this.toolStripButton_clearResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_clearResult.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_clearResult.Image")));
            this.toolStripButton_clearResult.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_clearResult.Name = "toolStripButton_clearResult";
            this.toolStripButton_clearResult.Size = new System.Drawing.Size(43, 24);
            this.toolStripButton_clearResult.Text = "清空";
            this.toolStripButton_clearResult.Click += new System.EventHandler(this.toolStripButton_clearResult_Click);
            // 
            // openFileDialog_path
            // 
            this.openFileDialog_path.Multiselect = true;
            this.openFileDialog_path.Title = "添加路径";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1281, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Step = 1;
            // 
            // invertSelectToolStripMenuItem
            // 
            this.invertSelectToolStripMenuItem.Name = "invertSelectToolStripMenuItem";
            this.invertSelectToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.invertSelectToolStripMenuItem.Text = "反选";
            this.invertSelectToolStripMenuItem.Click += new System.EventHandler(this.invertSelectToolStripMenuItem_Click);
            // 
            // invertCheckToolStripMenuItem
            // 
            this.invertCheckToolStripMenuItem.Name = "invertCheckToolStripMenuItem";
            this.invertCheckToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.invertCheckToolStripMenuItem.Text = "反标记";
            this.invertCheckToolStripMenuItem.Click += new System.EventHandler(this.invertCheckToolStripMenuItem_Click);
            // 
            // clearSelectToolStripMenuItem
            // 
            this.clearSelectToolStripMenuItem.Name = "clearSelectToolStripMenuItem";
            this.clearSelectToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.clearSelectToolStripMenuItem.Text = "取消选择";
            this.clearSelectToolStripMenuItem.Click += new System.EventHandler(this.clearSelectToolStripMenuItem_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1281, 583);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1020, 630);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "简单测试";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnGUIKeyDown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip_path.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip_result.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView listView_path;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_path;
        private System.Windows.Forms.OpenFileDialog openFileDialog_path;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Filter;
        private System.Windows.Forms.ToolStripButton toolStripButton_IncludeSub;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_plugin;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ListView listView_result;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_result;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_filterText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_clearResult;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_regex;
        private System.Windows.Forms.ToolStripButton toolStripButton_search;
        private System.Windows.Forms.ToolStripMenuItem checkAutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem selectCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectToolStripMenuItem;
    }
}

