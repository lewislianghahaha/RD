namespace RD.UI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.tmSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tmInfor = new System.Windows.Forms.ToolStripMenuItem();
            this.tmCustomerInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tmSuplierInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tmMaterialInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tmHouseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tmCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tmadorn = new System.Windows.Forms.ToolStripMenuItem();
            this.tmMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.tmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmt = new System.Windows.Forms.Timer(this.components);
            this.showDtl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmShowdtl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tmConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tmDelOrderdtl = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpick = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.comconfirm = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comhousetype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comordertype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comyear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comcustomer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gvdtl = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bngat = new System.Windows.Forms.BindingNavigator(this.components);
            this.bnCountItem = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.bnPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bnMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bnMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bnMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bnMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tmshowrows = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tstotalrow = new System.Windows.Forms.ToolStripLabel();
            this.Menu.SuspendLayout();
            this.status.SuspendLayout();
            this.showDtl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bngat)).BeginInit();
            this.bngat.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmSet,
            this.tmCreate,
            this.tmImport,
            this.tmRefresh});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1071, 25);
            this.Menu.TabIndex = 1;
            this.Menu.Text = "Menu";
            // 
            // tmSet
            // 
            this.tmSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmInfor,
            this.toolStripSeparator1,
            this.tmChange});
            this.tmSet.Name = "tmSet";
            this.tmSet.Size = new System.Drawing.Size(44, 21);
            this.tmSet.Text = "设置";
            // 
            // tmInfor
            // 
            this.tmInfor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmCustomerInfo,
            this.tmSuplierInfo,
            this.tmMaterialInfo,
            this.tmHouseInfo});
            this.tmInfor.Name = "tmInfor";
            this.tmInfor.Size = new System.Drawing.Size(148, 22);
            this.tmInfor.Text = "基础信息库";
            // 
            // tmCustomerInfo
            // 
            this.tmCustomerInfo.Name = "tmCustomerInfo";
            this.tmCustomerInfo.Size = new System.Drawing.Size(256, 22);
            this.tmCustomerInfo.Text = "客户信息管理";
            // 
            // tmSuplierInfo
            // 
            this.tmSuplierInfo.Name = "tmSuplierInfo";
            this.tmSuplierInfo.Size = new System.Drawing.Size(256, 22);
            this.tmSuplierInfo.Text = "供应商信息管理";
            // 
            // tmMaterialInfo
            // 
            this.tmMaterialInfo.Name = "tmMaterialInfo";
            this.tmMaterialInfo.Size = new System.Drawing.Size(256, 22);
            this.tmMaterialInfo.Text = "材料信息管理";
            // 
            // tmHouseInfo
            // 
            this.tmHouseInfo.Name = "tmHouseInfo";
            this.tmHouseInfo.Size = new System.Drawing.Size(256, 22);
            this.tmHouseInfo.Text = "房屋类型及装修工程类别信息管理";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // tmChange
            // 
            this.tmChange.Name = "tmChange";
            this.tmChange.Size = new System.Drawing.Size(148, 22);
            this.tmChange.Text = "帐号密码修改";
            // 
            // tmCreate
            // 
            this.tmCreate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmadorn,
            this.tmMaterial});
            this.tmCreate.Name = "tmCreate";
            this.tmCreate.Size = new System.Drawing.Size(44, 21);
            this.tmCreate.Text = "创建";
            // 
            // tmadorn
            // 
            this.tmadorn.Name = "tmadorn";
            this.tmadorn.Size = new System.Drawing.Size(160, 22);
            this.tmadorn.Text = "室内装修工程单";
            // 
            // tmMaterial
            // 
            this.tmMaterial.Name = "tmMaterial";
            this.tmMaterial.Size = new System.Drawing.Size(160, 22);
            this.tmMaterial.Text = "室内主材单";
            // 
            // tmImport
            // 
            this.tmImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmPrint});
            this.tmImport.Name = "tmImport";
            this.tmImport.Size = new System.Drawing.Size(44, 21);
            this.tmImport.Text = "导出";
            // 
            // tmPrint
            // 
            this.tmPrint.Name = "tmPrint";
            this.tmPrint.Size = new System.Drawing.Size(152, 22);
            this.tmPrint.Text = "打印";
            // 
            // tmRefresh
            // 
            this.tmRefresh.Name = "tmRefresh";
            this.tmRefresh.Size = new System.Drawing.Size(44, 21);
            this.tmRefresh.Text = "刷新";
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus});
            this.status.Location = new System.Drawing.Point(0, 637);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1071, 22);
            this.status.TabIndex = 5;
            this.status.Text = "status";
            // 
            // tsStatus
            // 
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tmt
            // 
            this.tmt.Enabled = true;
            this.tmt.Tick += new System.EventHandler(this.tmt_Tick);
            // 
            // showDtl
            // 
            this.showDtl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmShowdtl,
            this.toolStripSeparator7,
            this.tmConfirm,
            this.toolStripSeparator8,
            this.tmDelOrderdtl});
            this.showDtl.Name = "showDtl";
            this.showDtl.Size = new System.Drawing.Size(173, 82);
            // 
            // tmShowdtl
            // 
            this.tmShowdtl.Name = "tmShowdtl";
            this.tmShowdtl.Size = new System.Drawing.Size(172, 22);
            this.tmShowdtl.Text = "显示明细信息";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(169, 6);
            // 
            // tmConfirm
            // 
            this.tmConfirm.Name = "tmConfirm";
            this.tmConfirm.Size = new System.Drawing.Size(172, 22);
            this.tmConfirm.Text = "审核(反审核)";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(169, 6);
            // 
            // tmDelOrderdtl
            // 
            this.tmDelOrderdtl.Name = "tmDelOrderdtl";
            this.tmDelOrderdtl.Size = new System.Drawing.Size(172, 22);
            this.tmDelOrderdtl.Text = "删除单据相关信息";
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
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.splitContainer1.Panel1.Controls.Add(this.dtpick);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.comconfirm);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.comhousetype);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.comordertype);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.comyear);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.comcustomer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1071, 612);
            this.splitContainer1.SplitterDistance = 57;
            this.splitContainer1.TabIndex = 6;
            // 
            // dtpick
            // 
            this.dtpick.CustomFormat = "";
            this.dtpick.Location = new System.Drawing.Point(315, 32);
            this.dtpick.Name = "dtpick";
            this.dtpick.Size = new System.Drawing.Size(163, 21);
            this.dtpick.TabIndex = 13;
            this.dtpick.Value = new System.DateTime(2019, 6, 14, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "审核日期:";
            // 
            // comconfirm
            // 
            this.comconfirm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comconfirm.FormattingEnabled = true;
            this.comconfirm.Location = new System.Drawing.Point(70, 32);
            this.comconfirm.Name = "comconfirm";
            this.comconfirm.Size = new System.Drawing.Size(148, 20);
            this.comconfirm.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "审核状态:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(839, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // comhousetype
            // 
            this.comhousetype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comhousetype.FormattingEnabled = true;
            this.comhousetype.Location = new System.Drawing.Point(693, 5);
            this.comhousetype.Name = "comhousetype";
            this.comhousetype.Size = new System.Drawing.Size(126, 20);
            this.comhousetype.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(635, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "房屋类型:";
            // 
            // comordertype
            // 
            this.comordertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comordertype.FormattingEnabled = true;
            this.comordertype.Location = new System.Drawing.Point(477, 5);
            this.comordertype.Name = "comordertype";
            this.comordertype.Size = new System.Drawing.Size(138, 20);
            this.comordertype.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(419, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "单据类型:";
            // 
            // comyear
            // 
            this.comyear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comyear.FormattingEnabled = true;
            this.comyear.Location = new System.Drawing.Point(315, 5);
            this.comyear.Name = "comyear";
            this.comyear.Size = new System.Drawing.Size(86, 20);
            this.comyear.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "单据创建年份:";
            // 
            // comcustomer
            // 
            this.comcustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comcustomer.FormattingEnabled = true;
            this.comcustomer.Location = new System.Drawing.Point(70, 5);
            this.comcustomer.Name = "comcustomer";
            this.comcustomer.Size = new System.Drawing.Size(148, 20);
            this.comcustomer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户名称:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gvdtl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1069, 523);
            this.panel2.TabIndex = 1;
            // 
            // gvdtl
            // 
            this.gvdtl.AllowUserToAddRows = false;
            this.gvdtl.AllowUserToDeleteRows = false;
            this.gvdtl.AllowUserToOrderColumns = true;
            this.gvdtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdtl.ContextMenuStrip = this.showDtl;
            this.gvdtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdtl.Location = new System.Drawing.Point(0, 0);
            this.gvdtl.Name = "gvdtl";
            this.gvdtl.ReadOnly = true;
            this.gvdtl.RowTemplate.Height = 23;
            this.gvdtl.Size = new System.Drawing.Size(1069, 523);
            this.gvdtl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.bngat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 523);
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
            this.toolStripLabel2,
            this.bnPositionItem,
            this.toolStripLabel3,
            this.bnCountItem,
            this.bindingNavigatorSeparator1,
            this.bnMoveFirstItem,
            this.bnMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bnMoveNextItem,
            this.bnMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel1,
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
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(20, 21);
            this.toolStripLabel2.Text = "第";
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
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(20, 21);
            this.toolStripLabel3.Text = "页";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // bnMoveFirstItem
            // 
            this.bnMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMoveFirstItem.Image")));
            this.bnMoveFirstItem.Name = "bnMoveFirstItem";
            this.bnMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bnMoveFirstItem.Size = new System.Drawing.Size(23, 21);
            this.bnMoveFirstItem.Text = "移到首页记录";
            // 
            // bnMovePreviousItem
            // 
            this.bnMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMovePreviousItem.Image")));
            this.bnMovePreviousItem.Name = "bnMovePreviousItem";
            this.bnMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bnMovePreviousItem.Size = new System.Drawing.Size(23, 21);
            this.bnMovePreviousItem.Text = "移到上一页记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 24);
            // 
            // bnMoveNextItem
            // 
            this.bnMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMoveNextItem.Image")));
            this.bnMoveNextItem.Name = "bnMoveNextItem";
            this.bnMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bnMoveNextItem.Size = new System.Drawing.Size(23, 21);
            this.bnMoveNextItem.Text = "移到下一页记录";
            // 
            // bnMoveLastItem
            // 
            this.bnMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bnMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bnMoveLastItem.Image")));
            this.bnMoveLastItem.Name = "bnMoveLastItem";
            this.bnMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bnMoveLastItem.Size = new System.Drawing.Size(23, 21);
            this.bnMoveLastItem.Text = "移到末页记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 24);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 21);
            this.toolStripLabel1.Text = "每页显示:";
            // 
            // tmshowrows
            // 
            this.tmshowrows.BackColor = System.Drawing.SystemColors.Window;
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
            // Main
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 659);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "Main";
            this.Text = "RD-Main";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.showDtl.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem tmSet;
        private System.Windows.Forms.ToolStripMenuItem tmInfor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tmCreate;
        private System.Windows.Forms.ToolStripMenuItem tmadorn;
        private System.Windows.Forms.ToolStripMenuItem tmMaterial;
        private System.Windows.Forms.ToolStripMenuItem tmImport;
        private System.Windows.Forms.ToolStripMenuItem tmPrint;
        private System.Windows.Forms.ToolStripMenuItem tmChange;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.Timer tmt;
        private System.Windows.Forms.ToolStripMenuItem tmCustomerInfo;
        private System.Windows.Forms.ToolStripMenuItem tmSuplierInfo;
        private System.Windows.Forms.ToolStripMenuItem tmMaterialInfo;
        private System.Windows.Forms.ToolStripMenuItem tmHouseInfo;
        private System.Windows.Forms.ContextMenuStrip showDtl;
        private System.Windows.Forms.ToolStripMenuItem tmShowdtl;
        private System.Windows.Forms.ToolStripMenuItem tmRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tmConfirm;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comcustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comyear;
        private System.Windows.Forms.ComboBox comordertype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comhousetype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox comconfirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpick;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gvdtl;
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tmshowrows;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel tstotalrow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem tmDelOrderdtl;
    }
}