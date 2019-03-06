using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = RD.Logic.Task;

namespace RD.UI.Account
{
    public partial class AccountFrm : Form
    {
        Task task = new Task();

        public AccountFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            btnChange.Click += BtnChange_Click;
            btnexit.Click += Btnexit_Click;
        }

        /// <summary>
        /// 更换密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                task.TaskId = 0;
                task.AccountName = GlobalClasscs.User.StrUsrName;
                task.AccountPwd = GlobalClasscs.User.StrUsrpwd;

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
