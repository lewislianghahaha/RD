using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RD.Logic;
using RD.UI.Account;
using RD.UI.Basic;
using RD.UI.Order;

namespace RD.UI
{
    public partial class Main : Form
    {
        TaskLogic task=new TaskLogic();
        Load load=new Load();
        CustInfoFrm custInfo=new CustInfoFrm();

        private DataTable resultdt=new DataTable();

        public Main()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitialize();
        }

        private void OnRegisterEvents()
        {
            btnSearch.Click += BtnSearch_Click;
            tmChange.Click += TmChange_Click;
            tmadorn.Click += Tmadorn_Click;
            tmMaterial.Click += TmMaterial_Click;
            tmEXCEL.Click += TmEXCEL_Click;
            tmPrint.Click += TmPrint_Click;
            tmCustomerInfo.Click += TmCustomerInfo_Click;
            tmSuplierInfo.Click += TmSuplierInfo_Click;
            tmMaterialInfo.Click += TmMaterialInfo_Click;
            tmHouseInfo.Click += TmHouseInfo_Click;
            btnShow.Click += BtnShow_Click;
        }

        /// <summary>
        /// 初始化相关记录
        /// </summary>
        private void OnInitialize()
        {
            btnShow.Text = "隐藏更多查询";

        }

        /// <summary>
        /// 折叠或展开Panel1面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShow_Click(object sender, EventArgs e)
        {
            var a= splitContainer1.Panel1.Height;
            //var b = this.Height;  splitContainer1.Panel1.Height == 30 || splitContainer1.Panel1.Height==47
            //splitContainer1.Panel1.MinimumSize = new Size(1071, 30);

            //标记折叠 展开状态 Y：展开 N：折叠
            var showit = "Y";

            //此为折叠状态
            if (showit=="Y")
            {
                showit = "N";
                splitContainer1.Panel1.MinimumSize = new Size(1071, 60);
                btnShow.Text = "显示更多查询";
            }
            //此为展开状态
            else
            {
                showit = "Y";
                splitContainer1.Panel1.MinimumSize = new Size(1071, 30);
                btnShow.Text = "隐藏更多查询";
            }
        }
        /// <summary>
        /// 客户信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmCustomerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalClasscs.Basic.BasicId = 1;
                var basic = new BasicFrm();
                basic.StartPosition = FormStartPosition.CenterScreen;
                basic.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 供应商信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSuplierInfo_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalClasscs.Basic.BasicId = 2;
                var basic = new BasicFrm();
                basic.StartPosition = FormStartPosition.CenterScreen;
                basic.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 材料信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmMaterialInfo_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalClasscs.Basic.BasicId = 3;
                var basic = new BasicFrm();
                basic.StartPosition = FormStartPosition.CenterScreen;
                basic.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 房屋类型及装修工程类别信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmHouseInfo_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalClasscs.Basic.BasicId = 4;
                var basic = new BasicFrm();
                basic.StartPosition = FormStartPosition.CenterScreen;
                basic.ShowDialog();
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
            try
            {
                //设置单据状态为"创建"
                custInfo.FunState = "C";
                custInfo.FunName = "AdornOrder";  
                //初始化窗体信息
                custInfo.OnInitialize();
                custInfo.StartPosition=FormStartPosition.CenterParent;
                custInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 创建-主材单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                 //创建状态
                custInfo.FunState = "C";
                custInfo.FunName = "MaterialOrder";
                //初始化窗体信息
                custInfo.OnInitialize();
                custInfo.StartPosition=FormStartPosition.CenterParent;
                custInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();



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
        private void Start()
        {
            task.StartTask();

            //当完成后将Form2子窗体关闭
            this.Invoke((ThreadStart)(() =>
            {
                load.Close();
            }));
        }
    }
}
