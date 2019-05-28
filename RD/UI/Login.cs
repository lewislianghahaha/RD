using System;
using System.Data;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI
{
    public partial class Login : Form
    {
        //获取角色帐户DT
        private DataTable _userdt;
        
        TaskLogic task=new TaskLogic();

        public Login()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitialize();
        }

        private void OnRegisterEvents()
        {
            btnLogin.Click += BtnLogin_Click;
        }

        /// <summary>
        /// 初始化相关所需信息
        /// </summary>
        private void OnInitialize()
        {
            //初始化获取帐号T_ad_User相关信息DT
            _userdt = GetListdt("T_AD_User");
            //将‘最大化’按钮设置为不可用
            this.MaximizeBox = false;
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
                //检测所输入的值是否在T_AD_User内存在;若没有,即跳出异常
                var rows=_userdt.Select("UserName='" + txtName.Text + "'and UserPassword = '" + txtpwd.Text + "'");
                if(rows.Length==0) throw new Exception("所输入的帐户不存在或不满足某些条件,请联系管理员.");
                
                //若正确将相关信息保存至结构类内
                GlobalClasscs.User.StrUsrName = txtName.Text;
                GlobalClasscs.User.StrUsrpwd = txtpwd.Text;
                GlobalClasscs.User.Userid = Convert.ToInt32(rows[0][0]);   //获取并设置T_AD_User.UserID

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Text = "";
                txtpwd.Text = "";
            }
        }

        /// <summary>
        /// 根据对应的功能名称获取对应的DataTable
        /// </summary>
        /// <param name="functionName"></param>
        private DataTable GetListdt(string functionName)
        {
            task.TaskId = 3;
            task.FunctionId = "1";
            task.FunctionName = functionName;
            task.StartTask();
            var dt = task.ResultTable;
            return dt;
        }

    }
}
