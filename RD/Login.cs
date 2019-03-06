using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            btnLogin.Click += BtnLogin_Click;
            btnexit.Click += Btnexit_Click;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtpwd.Text == "") throw new Exception("请输入帐号及密码进行登录");
                if (txtName.Text == "Lewis" && txtpwd.Text == "124")
                {
                    GlobalClasscs.User.StrUsrName = txtName.Text;
                    GlobalClasscs.User.StrUsrpwd = txtpwd.Text;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    txtName.Text = "";
                    txtpwd.Text = "";
                    throw new Exception("帐号或密码错误,不能登录");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
