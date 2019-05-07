﻿namespace RD.UI
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
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tmSuplierInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmMaterialInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tmHouseInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tmCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tmadorn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmMaterial = new System.Windows.Forms.ToolStripMenuItem();
            this.tmImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tmEXCEL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmt = new System.Windows.Forms.Timer(this.components);
            this.showDtl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmShowdtl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tmConfirm = new System.Windows.Forms.ToolStripMenuItem();
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
            this.bnav = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.bnav)).BeginInit();
            this.bnav.SuspendLayout();
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
            this.toolStripSeparator4,
            this.tmSuplierInfo,
            this.toolStripSeparator5,
            this.tmMaterialInfo,
            this.toolStripSeparator6,
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(253, 6);
            // 
            // tmSuplierInfo
            // 
            this.tmSuplierInfo.Name = "tmSuplierInfo";
            this.tmSuplierInfo.Size = new System.Drawing.Size(256, 22);
            this.tmSuplierInfo.Text = "供应商信息管理";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(253, 6);
            // 
            // tmMaterialInfo
            // 
            this.tmMaterialInfo.Name = "tmMaterialInfo";
            this.tmMaterialInfo.Size = new System.Drawing.Size(256, 22);
            this.tmMaterialInfo.Text = "材料信息管理";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(253, 6);
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
            this.toolStripSeparator2,
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
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
            this.tmEXCEL,
            this.toolStripSeparator3,
            this.tmPrint});
            this.tmImport.Name = "tmImport";
            this.tmImport.Size = new System.Drawing.Size(44, 21);
            this.tmImport.Text = "导出";
            // 
            // tmEXCEL
            // 
            this.tmEXCEL.Name = "tmEXCEL";
            this.tmEXCEL.Size = new System.Drawing.Size(112, 22);
            this.tmEXCEL.Text = "EXCEL";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(109, 6);
            // 
            // tmPrint
            // 
            this.tmPrint.Name = "tmPrint";
            this.tmPrint.Size = new System.Drawing.Size(112, 22);
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
            this.tmConfirm});
            this.showDtl.Name = "showDtl";
            this.showDtl.Size = new System.Drawing.Size(149, 54);
            // 
            // tmShowdtl
            // 
            this.tmShowdtl.Name = "tmShowdtl";
            this.tmShowdtl.Size = new System.Drawing.Size(148, 22);
            this.tmShowdtl.Text = "显示明细信息";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(145, 6);
            // 
            // tmConfirm
            // 
            this.tmConfirm.Name = "tmConfirm";
            this.tmConfirm.Size = new System.Drawing.Size(148, 22);
            this.tmConfirm.Text = "审核(反审核)";
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
            this.dtpick.Value = new System.DateTime(2019, 5, 6, 0, 0, 0, 0);
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
            this.comconfirm.Size = new System.Drawing.Size(95, 20);
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
            this.panel1.Controls.Add(this.bnav);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 523);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1069, 26);
            this.panel1.TabIndex = 0;
            // 
            // bnav
            // 
            this.bnav.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnav.CountItem = this.bindingNavigatorCountItem;
            this.bnav.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bnav.Location = new System.Drawing.Point(0, 0);
            this.bnav.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnav.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnav.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnav.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnav.Name = "bnav";
            this.bnav.PositionItem = this.bindingNavigatorPositionItem;
            this.bnav.Size = new System.Drawing.Size(1067, 25);
            this.bnav.TabIndex = 0;
            this.bnav.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
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
            ((System.ComponentModel.ISupportInitialize)(this.bnav)).EndInit();
            this.bnav.ResumeLayout(false);
            this.bnav.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tmMaterial;
        private System.Windows.Forms.ToolStripMenuItem tmImport;
        private System.Windows.Forms.ToolStripMenuItem tmEXCEL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tmPrint;
        private System.Windows.Forms.ToolStripMenuItem tmChange;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.Timer tmt;
        private System.Windows.Forms.ToolStripMenuItem tmCustomerInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tmSuplierInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tmMaterialInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
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
        private System.Windows.Forms.BindingNavigator bnav;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
    }
}