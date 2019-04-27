namespace RD.UI.Order
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.tmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tmConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.tmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tmExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvView = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvdtl = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGetdtl = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).BeginInit();
            this.panel2.SuspendLayout();
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
            this.MainMenu.Size = new System.Drawing.Size(1492, 32);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "MainMenu";
            // 
            // tmSave
            // 
            this.tmSave.Name = "tmSave";
            this.tmSave.Size = new System.Drawing.Size(58, 28);
            this.tmSave.Text = "保存";
            // 
            // tmConfirm
            // 
            this.tmConfirm.Name = "tmConfirm";
            this.tmConfirm.Size = new System.Drawing.Size(58, 28);
            this.tmConfirm.Text = "审核";
            // 
            // tmExport
            // 
            this.tmExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmExcel,
            this.toolStripSeparator1,
            this.tmPrint});
            this.tmExport.Name = "tmExport";
            this.tmExport.Size = new System.Drawing.Size(58, 28);
            this.tmExport.Text = "导出";
            // 
            // tmExcel
            // 
            this.tmExcel.Name = "tmExcel";
            this.tmExcel.Size = new System.Drawing.Size(135, 30);
            this.tmExcel.Text = "Excel";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // tmPrint
            // 
            this.tmPrint.Name = "tmPrint";
            this.tmPrint.Size = new System.Drawing.Size(135, 30);
            this.tmPrint.Text = "打印";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtOrderNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtAdd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtHoseName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCustomer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1492, 40);
            this.panel1.TabIndex = 1;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Enabled = false;
            this.txtOrderNo.Location = new System.Drawing.Point(98, 4);
            this.txtOrderNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(222, 28);
            this.txtOrderNo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "单据名称:";
            // 
            // txtAdd
            // 
            this.txtAdd.Enabled = false;
            this.txtAdd.Location = new System.Drawing.Point(1012, 4);
            this.txtAdd.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdd.Name = "txtAdd";
            this.txtAdd.Size = new System.Drawing.Size(470, 28);
            this.txtAdd.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(930, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "装修地址:";
            // 
            // txtHoseName
            // 
            this.txtHoseName.Enabled = false;
            this.txtHoseName.Location = new System.Drawing.Point(736, 4);
            this.txtHoseName.Margin = new System.Windows.Forms.Padding(4);
            this.txtHoseName.Name = "txtHoseName";
            this.txtHoseName.Size = new System.Drawing.Size(188, 28);
            this.txtHoseName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(615, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "房屋类型名称:";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Enabled = false;
            this.txtCustomer.Location = new System.Drawing.Point(410, 4);
            this.txtCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(196, 28);
            this.txtCustomer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户名称:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 72);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1492, 867);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 3;
            // 
            // tvView
            // 
            this.tvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvView.Location = new System.Drawing.Point(0, 0);
            this.tvView.Margin = new System.Windows.Forms.Padding(4);
            this.tvView.Name = "tvView";
            this.tvView.Size = new System.Drawing.Size(186, 863);
            this.tvView.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gvdtl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1292, 824);
            this.panel3.TabIndex = 1;
            // 
            // gvdtl
            // 
            this.gvdtl.AllowUserToDeleteRows = false;
            this.gvdtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdtl.Location = new System.Drawing.Point(0, 0);
            this.gvdtl.Margin = new System.Windows.Forms.Padding(4);
            this.gvdtl.Name = "gvdtl";
            this.gvdtl.RowTemplate.Height = 23;
            this.gvdtl.Size = new System.Drawing.Size(1292, 824);
            this.gvdtl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnGetdtl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1292, 39);
            this.panel2.TabIndex = 0;
            // 
            // btnGetdtl
            // 
            this.btnGetdtl.Location = new System.Drawing.Point(6, 2);
            this.btnGetdtl.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetdtl.Name = "btnGetdtl";
            this.btnGetdtl.Size = new System.Drawing.Size(208, 34);
            this.btnGetdtl.TabIndex = 0;
            this.btnGetdtl.Text = "获取材料类别明细信息";
            this.btnGetdtl.UseVisualStyleBackColor = true;
            // 
            // MaterialOrderFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 939);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MaterialOrderFrm";
            this.Text = "主材单";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvdtl)).EndInit();
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHoseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvView;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGetdtl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gvdtl;
    }
}