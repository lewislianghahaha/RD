using System;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Account
{
    public partial class AccountFrm : Form
    {
        TaskLogic task = new TaskLogic();

        public AccountFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            Show();
        }

        private void OnRegisterEvents()
        {
            btnChange.Click += BtnChange_Click;
            btnexit.Click += Btnexit_Click;
        }

        private new void Show()
        {
            txtname.Text = GlobalClasscs.User.StrUsrName;
            txtoldpwd.Text = GlobalClasscs.User.StrUsrpwd;
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
                if(txtoldpwd.Text==txtnewpwd.Text) throw new Exception("旧密码不能与新密码相同,请重新输入");
                if(txtnewpwd.Text.Contains("$") || txtnewpwd.Text.Contains("@")) throw new Exception("密码内不能包含如$ @的非法字符,请重新输入");

                task.TaskId = 0;
                task.AccountName = txtname.Text;
                task.AccountPwd = txtnewpwd.Text;
                //开始执行Task,进行分派任务
                task.StartTask();

                if (task.ResultMark)
                {
                    MessageBox.Show(@"帐号密码修改成功,请在下一次登录时填上新的密码进入登录","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    throw new Exception("更新失败,请联系管理员");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtnewpwd.Text = "";
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
