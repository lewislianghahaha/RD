﻿namespace RD.UI.Order
{
    partial class MaterialOrderFrm
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.tmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tmConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.tmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tmExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAdd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.cmCustomer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvView = new System.Windows.Forms.TreeView();
            this.gvdtl = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MainMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmSave,
            this.tmConfirm,
            this.tmExport});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.MainMenu.Size = new System.Drawing.Size(995, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // tmSave
            // 
            this.tmSave.Name = "tmSave";
            this.tmSave.Size = new System.Drawing.Size(44, 22);
            this.tmSave.Text = "保存";
            // 
            // tmConfirm
            // 
            this.tmConfirm.Name = "tmConfirm";
            this.tmConfirm.Size = new System.Drawing.Size(44, 22);
            this.tmConfirm.Text = "审核";
            // 
            // tmExport
            // 
            this.tmExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmExcel,
            this.toolStripSeparator1,
            this.tmPrint});
            this.tmExport.Name = "tmExport";
            this.tmExport.Size = new System.Drawing.Size(44, 22);
            this.tmExport.Text = "导出";
            // 
            // tmExcel
            // 
            this.tmExcel.Name = "tmExcel";
            this.tmExcel.Size = new System.Drawing.Size(105, 22);
            this.tmExcel.Text = "Excel";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(102, 6);
            // 
            // tmPrint
            // 
            this.tmPrint.Name = "tmPrint";
            this.tmPrint.Size = new System.Drawing.Size(105, 22);
            this.tmPrint.Text = "打印";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtAdd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtHoseName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCustomer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(995, 27);
            this.panel1.TabIndex = 1;
            // 
            // txtAdd
            // 
            this.txtAdd.Enabled = false;
            this.txtAdd.Location = new System.Drawing.Point(675, 3);
            this.txtAdd.Name = "txtAdd";
            this.txtAdd.Size = new System.Drawing.Size(315, 21);
            this.txtAdd.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(620, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "装修地址:";
            // 
            // txtHoseName
            // 
            this.txtHoseName.Enabled = false;
            this.txtHoseName.Location = new System.Drawing.Point(491, 3);
            this.txtHoseName.Name = "txtHoseName";
            this.txtHoseName.Size = new System.Drawing.Size(127, 21);
            this.txtHoseName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "房屋类型名称:";
            // 
            // txtCustomer
            // 
            this.txtCustomer.ContextMenuStrip = this.cmCustomer;
            this.txtCustomer.Location = new System.Drawing.Point(273, 3);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(132, 21);
            this.txtCustomer.TabIndex = 1;
            // 
            // cmCustomer
            // 
            this.cmCustomer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmCustomer});
            this.cmCustomer.Name = "cmCustomer";
            this.cmCustomer.Size = new System.Drawing.Size(125, 26);
            // 
            // tmCustomer
            // 
            this.tmCustomer.Name = "tmCustomer";
            this.tmCustomer.Size = new System.Drawing.Size(124, 22);
            this.tmCustomer.Text = "客户资料";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户名称:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvdtl);
            this.splitContainer1.Size = new System.Drawing.Size(995, 575);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 3;
            // 
            // tvView
            // 
            this.tvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvView.Location = new System.Drawing.Point(0, 0);
            this.tvView.Name = "tvView";
            this.tvView.Size = new System.Drawing.Size(186, 571);
            this.tvView.TabIndex = 0;
            // 
            // gvdtl
            // 
            this.gvdtl.AllowUserToDeleteRows = false;
            this.gvdtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdtl.Location = new System.Drawing.Point(0, 0);
            this.gvdtl.Name = "gvdtl";
            this.gvdtl.RowTemplate.Height = 23;
            this.gvdtl.Size = new System.Drawing.Size(797, 571);
            this.gvdtl.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(65, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 21);
            this.textBox1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "单据名称:";
            // 
            // MaterialOrderFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 626);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MaterialOrderFrm";
            this.Text = "主材单";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmCustomer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem tmSave;
        private System.Windows.Forms.ToolStripMenuItem tmConfirm;
        private System.Windows.Forms.ToolStripMenuItem tmExport;
        private System.Windows.Forms.ToolStripMenuItem tmExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tmPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmCustomer;
        private System.Windows.Forms.ToolStripMenuItem tmCustomer;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHoseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvView;
        private System.Windows.Forms.DataGridView gvdtl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
    }
}