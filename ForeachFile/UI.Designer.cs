namespace ForeachFile
{
    partial class UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listView_path = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_path = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton_file = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.incSubDirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton_plugin = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox_Filter = new System.Windows.Forms.ToolStripTextBox();
            this.listView_result = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_result = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addonCmdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.allButFirstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton_search = new System.Windows.Forms.ToolStripSplitButton();
            this.regexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.openFileDialog_path = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.regexFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listView_path);
            this.splitContainer.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer.Panel1MinSize = 400;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listView_result);
            this.splitContainer.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer.Size = new System.Drawing.Size(1182, 559);
            this.splitContainer.SplitterDistance = 500;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 0;
            this.splitContainer.TabStop = false;
            // 
            // listView_path
            // 
            this.listView_path.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_path.ContextMenuStrip = this.contextMenuStrip_path;
            this.listView_path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_path.FullRowSelect = true;
            this.listView_path.GridLines = true;
            this.listView_path.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_path.Location = new System.Drawing.Point(0, 27);
            this.listView_path.Name = "listView_path";
            this.listView_path.ShowGroups = false;
            this.listView_path.ShowItemToolTips = true;
            this.listView_path.Size = new System.Drawing.Size(500, 532);
            this.listView_path.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_path.TabIndex = 1;
            this.listView_path.UseCompatibleStateImageBehavior = false;
            this.listView_path.View = System.Windows.Forms.View.Details;
            this.listView_path.VirtualMode = true;
            this.listView_path.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView_path_RetrieveVirtualItem);
            // 
            // contextMenuStrip_path
            // 
            this.contextMenuStrip_path.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_path.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.contextMenuStrip_path.Name = "contextMenuStrip_path";
            this.contextMenuStrip_path.Size = new System.Drawing.Size(109, 76);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.addToolStripMenuItem.Text = "添加";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.removeToolStripMenuItem.Text = "删除";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.clearToolStripMenuItem.Text = "清空";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton_file,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton_plugin,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBox_Filter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(500, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton_file
            // 
            this.toolStripDropDownButton_file.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton_file.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_file.Name = "toolStripDropDownButton_file";
            this.toolStripDropDownButton_file.ShowDropDownArrow = false;
            this.toolStripDropDownButton_file.Size = new System.Drawing.Size(61, 24);
            this.toolStripDropDownButton_file.Text = "文件(&F)";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incSubDirToolStripMenuItem});
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.ShowDropDownArrow = false;
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(65, 24);
            this.toolStripDropDownButton2.Text = "选项(&O)";
            // 
            // incSubDirToolStripMenuItem
            // 
            this.incSubDirToolStripMenuItem.Checked = true;
            this.incSubDirToolStripMenuItem.CheckOnClick = true;
            this.incSubDirToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.incSubDirToolStripMenuItem.Name = "incSubDirToolStripMenuItem";
            this.incSubDirToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.incSubDirToolStripMenuItem.Text = "包括子文件夹";
            this.incSubDirToolStripMenuItem.CheckedChanged += new System.EventHandler(this.incSubDirToolStripMenuItem_CheckedChanged);
            // 
            // toolStripDropDownButton_plugin
            // 
            this.toolStripDropDownButton_plugin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton_plugin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_plugin.Name = "toolStripDropDownButton_plugin";
            this.toolStripDropDownButton_plugin.ShowDropDownArrow = false;
            this.toolStripDropDownButton_plugin.Size = new System.Drawing.Size(64, 24);
            this.toolStripDropDownButton_plugin.Text = "插件(&A)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripTextBox_Filter
            // 
            this.toolStripTextBox_Filter.Name = "toolStripTextBox_Filter";
            this.toolStripTextBox_Filter.Size = new System.Drawing.Size(150, 27);
            this.toolStripTextBox_Filter.TextChanged += new System.EventHandler(this.toolStripTextBox_Filter_TextChanged);
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
            this.listView_result.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_result.Location = new System.Drawing.Point(0, 27);
            this.listView_result.Name = "listView_result";
            this.listView_result.Size = new System.Drawing.Size(681, 532);
            this.listView_result.TabIndex = 1;
            this.listView_result.UseCompatibleStateImageBehavior = false;
            this.listView_result.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip_result
            // 
            this.contextMenuStrip_result.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_result.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyGroupToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addonCmdToolStripMenuItem,
            this.toolStripSeparator3,
            this.clearToolStripMenuItem2});
            this.contextMenuStrip_result.Name = "contextMenuStrip_result";
            this.contextMenuStrip_result.Size = new System.Drawing.Size(149, 112);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.copyToolStripMenuItem.Text = "复制";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyGroupToolStripMenuItem
            // 
            this.copyGroupToolStripMenuItem.Name = "copyGroupToolStripMenuItem";
            this.copyGroupToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.copyGroupToolStripMenuItem.Text = "复制(按组)";
            this.copyGroupToolStripMenuItem.Click += new System.EventHandler(this.copyGroupToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 6);
            // 
            // addonCmdToolStripMenuItem
            // 
            this.addonCmdToolStripMenuItem.Name = "addonCmdToolStripMenuItem";
            this.addonCmdToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.addonCmdToolStripMenuItem.Text = "插件命令";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.commandToolStripDropDownButton,
            this.toolStripSeparator2,
            this.toolStripDropDownButton_search,
            this.toolStripTextBox1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(681, 27);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.reverseToolStripMenuItem,
            this.clearToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.allButFirstToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(68, 24);
            this.toolStripDropDownButton1.Text = "标记(&M)";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.allToolStripMenuItem.Text = "全标记";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // reverseToolStripMenuItem
            // 
            this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
            this.reverseToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.reverseToolStripMenuItem.Text = "反选";
            this.reverseToolStripMenuItem.Click += new System.EventHandler(this.reverseToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem1
            // 
            this.clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            this.clearToolStripMenuItem1.Size = new System.Drawing.Size(244, 26);
            this.clearToolStripMenuItem1.Text = "清空";
            this.clearToolStripMenuItem1.Click += new System.EventHandler(this.clearToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(241, 6);
            // 
            // allButFirstToolStripMenuItem
            // 
            this.allButFirstToolStripMenuItem.Name = "allButFirstToolStripMenuItem";
            this.allButFirstToolStripMenuItem.Size = new System.Drawing.Size(244, 26);
            this.allButFirstToolStripMenuItem.Text = "全标记(不标每组第一个)";
            this.allButFirstToolStripMenuItem.Click += new System.EventHandler(this.allButFirstToolStripMenuItem_Click);
            // 
            // commandToolStripDropDownButton
            // 
            this.commandToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.commandToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("commandToolStripDropDownButton.Image")));
            this.commandToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.commandToolStripDropDownButton.Name = "commandToolStripDropDownButton";
            this.commandToolStripDropDownButton.ShowDropDownArrow = false;
            this.commandToolStripDropDownButton.Size = new System.Drawing.Size(93, 24);
            this.commandToolStripDropDownButton.Text = "标记操作(&C)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripDropDownButton_search
            // 
            this.toolStripDropDownButton_search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton_search.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regexToolStripMenuItem});
            this.toolStripDropDownButton_search.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton_search.Image")));
            this.toolStripDropDownButton_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_search.Name = "toolStripDropDownButton_search";
            this.toolStripDropDownButton_search.Size = new System.Drawing.Size(77, 24);
            this.toolStripDropDownButton_search.Text = "搜索(&S)";
            this.toolStripDropDownButton_search.ButtonClick += new System.EventHandler(this.toolStripDropDownButton_search_ButtonClick);
            // 
            // regexToolStripMenuItem
            // 
            this.regexToolStripMenuItem.CheckOnClick = true;
            this.regexToolStripMenuItem.Name = "regexToolStripMenuItem";
            this.regexToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.regexToolStripMenuItem.Text = "正则";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(300, 27);
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1182, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 18);
            this.toolStripProgressBar.Step = 1;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
            // 
            // clearToolStripMenuItem2
            // 
            this.clearToolStripMenuItem2.Name = "clearToolStripMenuItem2";
            this.clearToolStripMenuItem2.Size = new System.Drawing.Size(175, 24);
            this.clearToolStripMenuItem2.Text = "清空结果";
            this.clearToolStripMenuItem2.Click += new System.EventHandler(this.clearToolStripMenuItem2_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regexFilterToolStripMenuItem});
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(83, 24);
            this.toolStripLabel1.Text = "文件过滤";
            // 
            // regexFilterToolStripMenuItem
            // 
            this.regexFilterToolStripMenuItem.CheckOnClick = true;
            this.regexFilterToolStripMenuItem.Name = "regexFilterToolStripMenuItem";
            this.regexFilterToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.regexFilterToolStripMenuItem.Text = "正则";
            this.regexFilterToolStripMenuItem.Click += new System.EventHandler(this.regexFilterToolStripMenuItem_Click);
            // 
            // UI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1182, 583);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1200, 630);
            this.Name = "UI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.UI_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.UI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.UI_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UI_KeyDown);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_path;
        private System.Windows.Forms.OpenFileDialog openFileDialog_path;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Filter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_plugin;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_result;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_file;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem incSubDirToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;

        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listView_result;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem addonCmdToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton commandToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem allButFirstToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton toolStripDropDownButton_search;
        private System.Windows.Forms.ToolStripMenuItem regexToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem regexFilterToolStripMenuItem;
    }
}

