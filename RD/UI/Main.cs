using System;
using System.Data;
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
        AdornOrderFrm adornOrder=new AdornOrderFrm();
        MaterialOrderFrm materialOrder=new MaterialOrderFrm();

        //保存查询出来的GridView记录
        private DataTable _dtl;

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
            tmRefresh.Click += TmRefresh_Click;
            tmShowdtl.Click += TmShowdtl_Click;
            tmConfirm.Click += TmConfirm_Click;
            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            panel1.Visible = false;
        }

        /// <summary>
        /// 初始化相关记录
        /// </summary>
        private void OnInitialize()
        {
            //初始化各下拉列表信息
            //客户名称信息
            OnInitializeCustDropDownList();
            //单据创建年份
            OnInitializeYearDropDownList();
            //单据类型
            OnInitializeOrderTypeDropDownList();
            //房屋类型
            OnInitializeHouseTypeDropDownList();
            //审核状态
            OnInitializeConfirmTypeDropDownList();
        }

        /// <summary>
        /// 初始化客户名称列表
        /// </summary>
        private void OnInitializeCustDropDownList()
        {
            comcustomer.DataSource = GetDropdownListdt("Customer");
            comcustomer.DisplayMember = "CustName";       //设置显示值
            comcustomer.ValueMember = "Custid";          //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 初始化单据创建年份
        /// </summary>
        private void OnInitializeYearDropDownList()
        {
            comyear.DataSource = GetDropdownListdt("OrderYear");
            comyear.DisplayMember = "YearName";     //设置显示值
            comyear.ValueMember = "Yearid";       //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 初始化单据类型
        /// </summary>
        private void OnInitializeOrderTypeDropDownList()
        {
            comordertype.DataSource = GetDropdownListdt("OrderType");
            comordertype.DisplayMember = "OrderName";     //设置显示值
            comordertype.ValueMember = "OrderId";       //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 初始化房屋类型
        /// </summary>
        private void OnInitializeHouseTypeDropDownList()
        {
            comhousetype.DataSource = GetDropdownListdt("HouseType");
            comhousetype.DisplayMember = "HtypeName";     //设置显示值
            comhousetype.ValueMember = "HTypeid";       //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 初始化审核状态
        /// </summary>
        private void OnInitializeConfirmTypeDropDownList()
        {
            comconfirm.DataSource = GetDropdownListdt("ConfirmType");
            comconfirm.DisplayMember = "FStatusName";     //设置显示值
            comconfirm.ValueMember = "FStatus";       //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 根据对应的功能名称获取对应的DataTable
        /// </summary>
        /// <param name="functionName"></param>
        private DataTable GetDropdownListdt(string functionName)
        {
            task.TaskId = 4;
            task.FunctionId = "1";
            task.FunctionName = functionName;
            task.StartTask();
            var dt = task.ResultTable;
            return dt;
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
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出-打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmPrint_Click(object sender, EventArgs e)
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
        ///查询功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var confirmdt=new DateTime();

            try
            {
                //获取"客户名称"下拉列表信息
                var dvCustidlist = (DataRowView)comcustomer.Items[comcustomer.SelectedIndex];
                var custid = Convert.ToInt32(dvCustidlist["Custid"]);

                //获取"单据创建年份"下拉列表信息
                var dvyearidlist = (DataRowView)comyear.Items[comyear.SelectedIndex];
                var yearid = Convert.ToInt32(dvyearidlist["Yearid"]);

                //获取"单据类型"下拉列表信息
                var dvordertypelist = (DataRowView)comordertype.Items[comordertype.SelectedIndex];
                var ordertypeId = Convert.ToInt32(dvordertypelist["OrderId"]);

                //获取"房屋类型"下拉列表信息
                var dvHTypeidlist = (DataRowView)comhousetype.Items[comhousetype.SelectedIndex];
                var hTypeid = Convert.ToInt32(dvHTypeidlist["HTypeid"]);

                //获取"审核状态"下拉列表信息
                var dvConfirmIdlist = (DataRowView)comconfirm.Items[comconfirm.SelectedIndex];
                var fStatus = Convert.ToString(dvConfirmIdlist["FStatus"]);

                //获取“审核日期”(若“审核状态”选择了“已审核”才获取)
                if (fStatus == "Y")
                    confirmdt = dtpick.Value.Date;

                task.TaskId = 4;
                task.FunctionId = "1.1";
                task.Custid = custid;
                task.Yearid = yearid;
                task.OrdertypeId = ordertypeId;
                task.HTypeid = hTypeid;
                task.ConfirmfStatus = fStatus;
                task.Confirmdt = confirmdt;

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                //gvdtl.DataSource = task.ResultTable;
                _dtl = task.ResultTable;
                panel1.Visible = true;
                //GridView分页
                GridViewPageChange();
                //控制GridView单元格显示方式
                ControlGridViewisShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 审核(反审核)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmConfirm_Click(object sender, EventArgs e)
        {
            string clickMessage;

            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能选择.");
                if (gvdtl.SelectedRows.Count==0) throw new Exception("请至少选择一行进行审核");

                //获取所选择的行中首行的“审核状态”及“单据类型”记录
                //“审核状态”
                var fStatus = Convert.ToString(gvdtl.SelectedRows[0].Cells[9].Value);
                //“单据类型”
                var ordertype = Convert.ToString(gvdtl.SelectedRows[0].Cells[1].Value);

                if (fStatus == "已审核")
                {
                    //权限控制(注:若不是可以反审核的帐号就弹出异常)
                    if(!Getpriavepower(GlobalClasscs.User.StrUsrName)) throw new Exception($"用户{GlobalClasscs.User.StrUsrName}没有‘反审核’权限,不能继续.");
                    //提示信息
                    clickMessage = $"您所选择需要进行反审核的信息有'{gvdtl.SelectedRows.Count}'行 \n 是否继续?";
                }
                else
                {
                    //提示信息
                    clickMessage = $"您所选择需要进行审核的信息有'{gvdtl.SelectedRows.Count}'行 \n 是否继续?";
                }

                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //获取所选择的行记录并进行反审核
                    if (!ChangeState(fStatus, ordertype)) throw new Exception("出现异常,请联系管理员");
                    else
                    {
                        MessageBox.Show($"已完成操作,请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //成功后,先将GridView记录清空
                gvdtl.DataSource = null;
                panel1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示明细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmShowdtl_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvdtl.Rows.Count==0) throw new Exception("没有内容,不能选择.");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("请选择一行查看明细");

                //获取从GridView所选择的功能名称
                var funname= Convert.ToString(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value);

                if (funname == "AdornOrder")
                {
                    adornOrder.FunState = "R";
                    adornOrder.Pid = Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[0].Value);
                    adornOrder.FunName = funname;
                    //初始化窗体信息
                    adornOrder.OnInitialize();
                    adornOrder.StartPosition = FormStartPosition.CenterParent;
                    adornOrder.ShowDialog();
                }
                else
                {
                    materialOrder.FunState = "R";
                    materialOrder.Pid = Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[0].Value);
                    materialOrder.FunName = funname;
                    //初始化窗体信息
                    materialOrder.OnInitialize();
                    materialOrder.StartPosition = FormStartPosition.CenterParent;
                    materialOrder.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 首页按钮(GridView页面跳转使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveLastItem_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 上一页按钮(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveNextItem_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 下一页(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMovePreviousItem_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 末页(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveFirstItem_Click(object sender, EventArgs e)
        {
            try
            {

                GridViewPageChange();
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

        /// <summary>
        /// 控制GridView单元格显示方式
        /// </summary>
        private void ControlGridViewisShow()
        {
            //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
        }

        /// <summary>
        /// 根据用户名称获取其对应的权限
        /// </summary>
        private bool Getpriavepower(string username)
        {
            var result = true;
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        /// <summary>
        /// 根据从GridView获取所选的行。进行审核（反审核）操作
        /// </summary>
        /// <param name="fStatus">审核状态 Y：已审核 N:末审核 审核ID 0:审核 1:反审核</param>
        /// <param name="ordertype">单据类型 (1:室内装修工程单 AdornOrder 2:室内主材单 MaterialOrder)</param>
        /// <returns></returns>
        private bool ChangeState(string fStatus,string ordertype)
        {
            var result = true;
            try
            {
                task.TaskId = 4;
                task.FunctionId = "2";
                task.FunctionName = ordertype;
                task.Confirmid = fStatus == "已审核" ? 1 : 0;
                task.Datarow = gvdtl.SelectedRows;

                task.StartTask();
                result = task.ResultMark;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        /// <summary>
        /// GridView分页功能
        /// </summary>
        private void GridViewPageChange()
        {
            if (bnPositionItem.Enabled == false)
            {
                //初始化BindingNavigator控件内的各子控件
                bnPositionItem.Enabled = true;       //跳转页
                //bnMoveFirstItem.Enabled = true;    //首页
                //bnMovePreviousItem.Enabled = true; //上一页
                bnMoveNextItem.Enabled = true;       //下一页
                bnMoveLastItem.Enabled = true;       //末页
                tmshowrows.Enabled = true;           //每页显示行数（下拉框）
                bnPositionItem.Text = "1";           //初始化填充跳转页为1
                tmshowrows.SelectedItem = "10";      //初始化下拉框所选择的默认值
            }
            //获取查询的总行数
            var dtltotalrows = _dtl.Rows.Count;
            //获取“每页显示行数”所选择的行数
            var pageCount = Convert.ToInt32(tmshowrows.SelectedItem);
            //计算出总页数
            var totalpagecount = dtltotalrows % pageCount == 0  ? dtltotalrows / pageCount : dtltotalrows / pageCount + 1;
            //赋值
            bnCountItem.Text = $"/ {totalpagecount} 页";

            //


            //显示_dtl的查询总行数
            tstotalrow.Text = $"共 {_dtl.Rows.Count} 行";
            //最后将刷新的DT重新赋值给GridView
            gvdtl.DataSource = _dtl;

            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = _dtl;
            // bngat.BindingSource = bindingSource;


        }



    }
}
