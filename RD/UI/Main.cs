using System;
using System.Windows.Forms;
using RD.Logic;
using RD.UI.Account;

namespace RD.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            btnSearch.Click += BtnSearch_Click;
            tmInfor.Click += TmInfor_Click;
            tmChange.Click += TmChange_Click;
            tmadorn.Click += Tmadorn_Click;
            tmMaterial.Click += TmMaterial_Click;
            tmEXCEL.Click += TmEXCEL_Click;
            tmPrint.Click += TmPrint_Click;
        }

        /// <summary>
        /// 基础信息库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmInfor_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 帐号密码修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmChange_Click(object sender, EventArgs e)
        {
            try
            {
                var account = new AccountFrm();
                account.StartPosition = FormStartPosition.CenterScreen;
                account.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 创建-室内装修工程单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmadorn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建-主材单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmMaterial_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导出-EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmEXCEL_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导出-打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmPrint_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///查询功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示用户状态信息,如:动态显示时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmt_Tick(object sender, EventArgs e)
        {
            tsStatus.Text = "你好," + GlobalClasscs.User.StrUsrName + "," + "欢迎进入系统," + "现在的时间是:" + DateTime.Now;
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        //private void Start()
        //{
        //    task.StartTask();

        //    //当完成后将Form2子窗体关闭
        //    this.Invoke((ThreadStart)(() => {
        //        load.Close();
        //    }));
        //}

    }
}
