using System;
using System.Data;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI
{
    public partial class Login : Form
    {
        //获取角色帐户DT(检测T_ad_user表是否有值时使用)
        private DataTable _userdt;
        //获取帐户明细DT
        private DataTable _userdtldt;

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
            //初始化获取T_AD_USER明细信息DT
            _userdtldt = GetListdt("T_AD_User_Privage");
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
                //检测若没有选‘进入窗体模式’下拉列表，就会提示异常
                if ((int)comchosetype.SelectedIndex == -1) throw new Exception("请选择下拉列表.");

                //判断_userdt是否有值,若没有即表示为‘初始化记录’,此时使用帐号"Admin" 密码为空进入'AdminFrm.cs'帐户信息管理界面
                if (_userdt.Rows.Count == 0)
                {
                    //判断这种情况若没有选‘进入窗体模式’为‘帐户信息管理窗体’,即跳出异常 （0:主窗体 1:帐户信息管理窗体）
                    if ((int)comchosetype.SelectedIndex==0) throw new Exception($"因'帐户信息管理窗体'没有任何记录,故不能进入'主窗体'模式,\n请选择‘帐户信息管理窗体’下拉列表");
                    //若正确将相关信息保存至结构类内
                    GlobalClasscs.User.StrUsrName = "AdministratorInitialize";  //初始化'帐户信息管理窗体'时InputUser字段使用
                    GlobalClasscs.User.ChoseTypeid = (int)comchosetype.SelectedIndex;  //获取‘进入窗体模式’ID （0:主窗体 1:帐户信息管理窗体）
                }
                //当检测到T_AD_User表有值的时候,就需要输入帐号及密码
                else
                {
                    if (txtName.Text == "" || txtpwd.Text == "") throw new Exception("请输入帐号及密码进行登录");
                    //检测所输入的值是否在T_AD_User内存在(包括是否有管理员权限);若没有,即跳出异常
                    var rows = _userdtldt.Select("UserName='" + txtName.Text + "'and UserPassword = '" + txtpwd.Text + "'");
                    if (rows.Length == 0) throw new Exception("所输入的帐户不存在或不满足某些条件,请联系管理员.");

                    //若下拉列表选择的是‘帐户信息管理窗体’,就需要检测其是不是具有‘管理员’权限
                    if ((int) comchosetype.SelectedIndex == 1)
                    {
                        //检测用户是否具有管理员权限
                        var dtlrows = _userdtldt.Select("UserName='" + txtName.Text + "' and 管理员权限='是'");
                        if(dtlrows.Length==0)throw new Exception($"检测到所输入的'{txtName.Text}'帐户没有管理员权限,故不能进入'帐户信息管理窗体'");
                    }
                        
                    //若正确将相关信息保存至结构类内
                    GlobalClasscs.User.StrUsrName = txtName.Text;
                    GlobalClasscs.User.StrUsrpwd = txtpwd.Text;
                    GlobalClasscs.User.Userid = Convert.ToInt32(rows[0][0]);             //获取并设置T_AD_User.UserID
                    GlobalClasscs.User.ChoseTypeid = (int) comchosetype.SelectedIndex;  //获取‘进入窗体模式’ID （0:主窗体 1:帐户信息管理窗体）
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Text = "";
                txtpwd.Text = "";
                comchosetype.SelectedIndex = -1;
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
