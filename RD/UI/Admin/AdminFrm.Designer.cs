namespace RD.UI.Admin
{
    partial class AdminFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminFrm));
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.tmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tmroleinfo = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comColseStatus = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comconfirm = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comsex = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.comUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvdtl = new System.Windows.Forms.DataGridView();
            this.showDtl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmshowDtl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmCloseAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tmChangeAccountPwd = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bngat = new System.Windows.Forms.BindingNavigator(this.components);
            this.bnCountItem = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.bnPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bnMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bnMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bnMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bnMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tmshowrows = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tstotalrow = new System.Windows.Forms.ToolStripLabel();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).BeginInit();
            this.showDtl.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bngat)).BeginInit();
            this.bngat.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmAdd,
            this.tmroleinfo});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1071, 25);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "Menu";
            // 
            // tmAdd
            // 
            this.tmAdd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tmAdd.Name = "tmAdd";
            this.tmAdd.Size = new System.Drawing.Size(68, 21);
            this.tmAdd.Text = "帐户新增";
            // 
            // tmroleinfo
            // 
            this.tmroleinfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tmroleinfo.Name = "tmroleinfo";
            this.tmroleinfo.Size = new System.Drawing.Size(92, 21);
            this.tmroleinfo.Text = "角色信息管理";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.comColseStatus);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.comconfirm);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.comsex);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.dtin);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.comUser);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvdtl);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1071, 634);
            this.splitContainer1.SplitterDistance = 59;
            this.splitContainer1.TabIndex = 1;
            // 
            // comColseStatus
            // 
            this.comColseStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comColseStatus.FormattingEnabled = true;
            this.comColseStatus.Location = new System.Drawing.Point(464, 33);
            this.comColseStatus.Name = "comColseStatus";
            this.comColseStatus.Size = new System.Drawing.Size(156, 20);
            this.comColseStatus.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(896, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // comconfirm
            // 
            this.comconfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comconfirm.FormattingEnabled = true;
            this.comconfirm.Location = new System.Drawing.Point(729, 5);
            this.comconfirm.Name = "comconfirm";
            this.comconfirm.Size = new System.Drawing.Size(115, 20);
            this.comconfirm.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(675, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "审核状态:";
            // 
            // comsex
            // 
            this.comsex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comsex.FormattingEnabled = true;
            this.comsex.Location = new System.Drawing.Point(464, 5);
            this.comsex.Name = "comsex";
            this.comsex.Size = new System.Drawing.Size(156, 20);
            this.comsex.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "职员性别:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(356, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "职员帐号关闭状态:";
            // 
            // dtin
            // 
            this.dtin.Location = new System.Drawing.Point(105, 33);
            this.dtin.Name = "dtin";
            this.dtin.Size = new System.Drawing.Size(184, 21);
            this.dtin.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "职员入职日期:";
            // 
            // comUser
            // 
            this.comUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comUser.FormattingEnabled = true;
            this.comUser.Location = new System.Drawing.Point(105, 5);
            this.comUser.Name = "comUser";
            this.comUser.Size = new System.Drawing.Size(184, 20);
            this.comUser.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "职员名称:";
            // 
            // gvdtl
            // 
            this.gvdtl.AllowUserToAddRows = false;
            this.gvdtl.AllowUserToDeleteRows = false;
            this.gvdtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdtl.ContextMenuStrip = this.showDtl;
            this.gvdtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdtl.Location = new System.Drawing.Point(0, 0);
            this.gvdtl.Name = "gvdtl";
            this.gvdtl.ReadOnly = true;
            this.gvdtl.RowTemplate.Height = 23;
            this.gvdtl.Size = new System.Drawing.Size(1069, 543);
            this.gvdtl.TabIndex = 1;
            // 
            // showDtl
            // 
            this.showDtl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmshowDtl,
            this.toolStripSeparator1,
            this.tmConfirm,
            this.toolStripSeparator2,
            this.tmCloseAccount,
            this.toolStripSeparator3,
            this.tmChangeAccountPwd});
            this.showDtl.Name = "showDtl";
            this.showDtl.Size = new System.Drawing.Size(185, 110);
            // 
            // tmshowDtl
            // 
            this.tmshowDtl.Name = "tmshowDtl";
            this.tmshowDtl.Size = new System.Drawing.Size(184, 22);
            this.tmshowDtl.Text = "查阅明细信息";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // tmConfirm
            // 
            this.tmConfirm.Name = "tmConfirm";
            this.tmConfirm.Size = new System.Drawing.Size(184, 22);
            this.tmConfirm.Text = "审核(反审核)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // tmCloseAccount
            // 
            this.tmCloseAccount.Name = "tmCloseAccount";
            this.tmCloseAccount.Size = new System.Drawing.Size(184, 22);
            this.tmCloseAccount.Text = "关闭帐户及相关信息";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // tmChangeAccountPwd
            // 
            this.tmChangeAccountPwd.Name = "tmChangeAccountPwd";
            this.tmChangeAccountPwd.Size = new System.Drawing.Size(184, 22);
            this.tmChangeAccountPwd.Text = "职员帐户密码修改";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bngat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 543);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1069, 26);
            this.panel1.TabIndex = 0;
            // 
            // bngat
            // 
            this.bngat.AddNewItem = null;
            this.bngat.CountItem = this.bnCountItem;
            this.bngat.CountItemFormat = "/ {0} 页";
            this.bngat.DeleteItem = null;
            this.bngat.Dock = System.Windows.Forms.DockStyle.Right;
            this.bngat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.bnPositionItem,
            this.toolStripLabel2,
            this.bnCountItem,
            this.bindingNavigatorSeparator,
            this.bnMoveFirstItem,
            this.bnMovePreviousItem,
            this.bindingNavigatorSeparator1,
            this.bnMoveNextItem,
            this.bnMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel3,
            this.tmshowrows,
            this.toolStripLabel4,
            this.toolStripLabel5,
            this.tstotalrow});
            this.bngat.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.bngat.Location = new System.Drawing.Point(581, 0);
            this.bngat.MoveFirstItem = this.bnMoveFirstItem;
            this.bngat.MoveLastItem = this.bnMoveLastItem;
            this.bngat.MoveNextItem = this.bnMoveNextItem;
            this.bngat.MovePreviousItem = this.bnMovePreviousItem;
            this.bngat.Name = "bngat";
            this.bngat.PositionItem = this.bnPositionItem;
            this.bngat.Size = new System.Drawing.Size(486, 24);
            this.bngat.TabIndex = 0;
            this.bngat.Text = "bindingNavigator1";
            // 
            // bnCountItem
            // 
            this.bnCountItem.Name = "bnCountItem";
            this.bnCountItem.Size = new System.Drawing.Size(48, 21);
            this.bnCountItem.Text = "/ {0} 页";
            this.bnCountItem.ToolTipText = "总项数";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 21);
            this.toolStripLabel1.Text = "第";
            // 
            // bnPositionItem
            // 
            this.bnPositionItem.AccessibleName = "位置";
            this.bnPositionItem.AutoSize = false;
            this.bnPositionItem.Name = "bnPositionItem";
            this.bnPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bnPositionItem.Text = "0";
            this.bnPositionItem.ToolTipText = "当前位置";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 21);
            this.toolStripLabel2.Text = "页";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 24);
            // 
            // bnMoveFirstItem
            // 
            this.bnMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMoveFirstItem.Image")));
            this.bnMoveFirstItem.Name = "bnMoveFirstItem";
            this.bnMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bnMoveFirstItem.Size = new System.Drawing.Size(23, 21);
            this.bnMoveFirstItem.Text = "移到第一条记录";
            // 
            // bnMovePreviousItem
            // 
            this.bnMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMovePreviousItem.Image")));
            this.bnMovePreviousItem.Name = "bnMovePreviousItem";
            this.bnMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bnMovePreviousItem.Size = new System.Drawing.Size(23, 21);
            this.bnMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // bnMoveNextItem
            // 
            this.bnMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMoveNextItem.Image")));
            this.bnMoveNextItem.Name = "bnMoveNextItem";
            this.bnMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bnMoveNextItem.Size = new System.Drawing.Size(23, 21);
            this.bnMoveNextItem.Text = "移到下一条记录";
            // 
            // bnMoveLastItem
            // 
            this.bnMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMoveLastItem.Image")));
            this.bnMoveLastItem.Name = "bnMoveLastItem";
            this.bnMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bnMoveLastItem.Size = new System.Drawing.Size(23, 21);
            this.bnMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 24);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(59, 21);
            this.toolStripLabel3.Text = "每页显示:";
            // 
            // tmshowrows
            // 
            this.tmshowrows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tmshowrows.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "1000"});
            this.tmshowrows.Name = "tmshowrows";
            this.tmshowrows.Size = new System.Drawing.Size(75, 24);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(20, 21);
            this.toolStripLabel4.Text = "行";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(13, 21);
            this.toolStripLabel5.Text = "/";
            // 
            // tstotalrow
            // 
            this.tstotalrow.Name = "tstotalrow";
            this.tstotalrow.Size = new System.Drawing.Size(55, 21);
            this.tstotalrow.Text = "共 {0} 行";
            // 
            // AdminFrm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 659);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "AdminFrm";
            this.Text = "职员帐户信息功能设定";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).EndInit();
            this.showDtl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bngat)).EndInit();
            this.bngat.ResumeLayout(false);
            this.bngat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private new System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem tmAdd;
        private System.Windows.Forms.ToolStripMenuItem tmroleinfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingNavigator bngat;
        private System.Windows.Forms.ToolStripLabel bnCountItem;
        private System.Windows.Forms.ToolStripButton bnMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bnMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bnPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bnMoveNextItem;
        private System.Windows.Forms.ToolStripButton bnMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView gvdtl;
        private System.Windows.Forms.DateTimePicker dtin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comsex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comconfirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ContextMenuStrip showDtl;
        private System.Windows.Forms.ToolStripMenuItem tmshowDtl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tmConfirm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tmCloseAccount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tmChangeAccountPwd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox tmshowrows;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel tstotalrow;
        private System.Windows.Forms.ComboBox comColseStatus;
    }
}