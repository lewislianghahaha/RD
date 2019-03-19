using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RD.UI.Basic
{
    public partial class AddEditor : Form
    {
        private int _funid;           //功能ID 0：新增 1:修改
        private int _pid;             //记录上级ID
        private string _pName;        //记录上级节点名称

        /// <summary>
        /// 记录上级ID
        /// </summary>
        public int pid { set { _pid = value; } }

        /// <summary>
        /// 记录上级节点名称
        /// </summary>
        public string PName { set { _pName = value; } }

        /// <summary>
        /// 记录功能ID 0:新增 1:修改
        /// </summary>
        public int Funid { set { _funid = value; } }

        public AddEditor()
        {
            InitializeComponent();
            OnInitialize();
        }

        void OnInitialize()
        {
            tmSave.Click += TmSave_Click;
            tmClose.Click += TmClose_Click;
        }


        /// <summary>
        /// 显示基本信息
        /// </summary>
        public void Show()
        {
            switch (_funid)
            {
                case 0:
                    this.Text = "新增分组";
                    break;
                case 1:
                    this.Text = "创建分组";
                    break;
            }
            txtUpName.Text = _pName;
        }

        /// <summary>
        /// 保存(作用:用于将新增或编辑的记录插入至对应的数据表内)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtName.Text==null) throw new Exception("请填写名称");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
