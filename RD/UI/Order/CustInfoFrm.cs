﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class CustInfoFrm : Form
    {
        TaskLogic task = new TaskLogic();
        AdornOrderFrm adornOrder=new AdornOrderFrm();
        MaterialOrderFrm materialOrder=new MaterialOrderFrm();

        //记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
        private string _funName;
        //单据状态标记(作用:记录创建记录) C:创建
        private string _funState;

        //保存查询出来的GridView记录（GridView页面跳转时使用）
        private DataTable _dtl;
        //记录当前页数(GridView页面跳转使用)
        private int _pageCurrent = 1;
        //记录计算出来的总页数(GridView页面跳转使用)
        private int _totalpagecount;
        //记录初始化标记(GridView页面跳转 初始化时使用)
        private bool _pageChange;

        //记录能否删除ID(删除权限使用)
        private bool _candelMarkid;

        #region Set

            /// <summary>
            /// 获取单据状态标记ID C:创建 R:读取
            /// </summary>
            public string FunState { set { _funState = value; } }
            /// <summary>
            /// 记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
            /// </summary>
            public string FunName { set { _funName = value; } }

            /// <summary>
            /// 获取单据状态标记ID C:创建 R:读取
            /// </summary>
            public bool CandelMarkid { set { _candelMarkid = value; } }

        #endregion

        public CustInfoFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitialize();
        }

        private void OnRegisterEvents()
        {
            tmGet.Click += TmGet_Click;
            tmClose.Click += TmClose_Click;
            comlist.SelectionChangeCommitted += Comlist_SelectionChangeCommitted;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel3.Visible = false;
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        public void OnInitialize()
        {
            //初始化 客户类型名称列表
            OnInitializeDropDownList();
            //初始化Dt并返回至GridView
            ShowCustInfoIntoGridView();
        }

        /// <summary>
        /// 初始化-客户类型名称列表
        /// </summary>
        private void OnInitializeDropDownList()
        {
            task.TaskId = 1;
            task.FunctionId = "1.5";
            task.StartTask();
            comlist.DataSource = task.ResultTable;
            comlist.DisplayMember = "CustType";     //设置显示值
            comlist.ValueMember = "Id";             //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 下拉列表选择后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comlist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ShowCustInfoIntoGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGet_Click(object sender, EventArgs e)
        {
            try
            {
                //客户主键ID
                //var customerid = 0;
                //记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
                string frmName;

                if (gvdtl.SelectedRows.Count==0) throw new Exception("请选取某一行,然后再按此按钮");
                _funName = "AdornOrder";
                frmName = _funName == "AdornOrder" ? "室内装修工程" : "室内主材";
                //获取当前行的客户名称 及 客户ID信息
                var customerid= Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value);
                var customerName = Convert.ToString(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[2].Value);

                var clickMessage = $"您所选择的信息为:\n 客户名称:{customerName} \n 是否继续? \n 注:选定后,即生成对应的'{frmName}'单据信息, \n 并跳转至该窗体. \n 请谨慎处理.";

                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //根据客户信息插入至对应的 室内装修工程表 或 室内主材单表 表头内
                    task.TaskId = 2;
                    task.FunctionId = "2.3";
                    task.FunctionName = _funName;
                    task.Custid = customerid;
                    task.AccountName = GlobalClasscs.User.StrUsrName;

                    task.StartTask();
                    //返回新插入的表头ID;即T_PRO_Adorn 或 T_PRO_Material表信息
                    var id = task.Orderid;                      

                    if (id == 0) throw new Exception("生成异常,请联系管理员.");
                    else
                    {
                        //当插入完成后,转移到 室内装修工程单 或 室内主材单 窗体内,并将此窗体关闭
                        MessageBox.Show($"新增'{frmName}'类型单据成功,请点击继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //室内装修工程单
                        if (_funName == "AdornOrder")
                        {
                            //将当前窗体隐藏
                            this.Visible = false;
                            //弹出对应窗体相关设置
                            adornOrder.FunState ="C"; //_funState;        //单据状态
                            adornOrder.Pid = id;                         //单据主键id
                            adornOrder.FunName = _funName;              //功能名称
                           // adornOrder.CandelMarkid = _candelMarkid; //能否删除权限标记(删除时作权限使用)
                            adornOrder.OnInitialize();                //初始化信息
                            adornOrder.StartPosition = FormStartPosition.CenterParent;
                            adornOrder.ShowDialog();
                        }
                        //室内主材单
                        else
                        {
                            //将当前窗体隐藏
                            this.Visible = false;
                            //弹出对应窗体相关设置
                            //materialOrder.FunState = _funState;
                            //materialOrder.Pid = id;                       //单据主键id
                            //materialOrder.FunName = _funName;            //功能名称
                            //materialOrder.CandelMarkid = _candelMarkid; //能否删除权限标记(删除时作权限使用)
                            //materialOrder.OnInitialize();               //初始化信息
                            materialOrder.StartPosition=FormStartPosition.CenterParent;
                            materialOrder.ShowDialog();
                        }
                    }
                }
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

        /// <summary>
        /// 设置GridView是否显示某些列
        /// </summary>
        private void ControlGridViewisShow()
        {
            //将GridView中的第一列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            gvdtl.Columns[3].Visible = false;
        }

        /// <summary>
        /// 首页按钮(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveFirstItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将当前页变量PageCurrent=1; 2)并将“首页” 及 “上一页”按钮设置为不可用 将“下一页” “末页”按设置为可用
                _pageCurrent = 1;
                bnMoveFirstItem.Enabled = false;
                bnMovePreviousItem.Enabled = false;

                bnMoveNextItem.Enabled = true;
                bnMoveLastItem.Enabled = true;
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 上一页(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMovePreviousItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将PageCurrent自减 2)将“下一页” “末页”按钮设置为可用
                _pageCurrent--;
                bnMoveNextItem.Enabled = true;
                bnMoveLastItem.Enabled = true;
                //判断若PageCurrent=1的话,就将“首页” “上一页”按钮设置为不可用
                if (_pageCurrent == 1)
                {
                    bnMoveFirstItem.Enabled = false;
                    bnMovePreviousItem.Enabled = false;
                }
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 下一页按钮(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveNextItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将PageCurrent自增 2)将“首页” “上一页”按钮设置为可用
                _pageCurrent++;
                bnMoveFirstItem.Enabled = true;
                bnMovePreviousItem.Enabled = true;
                //判断若PageCurrent与“总页数”一致的话,就将“下一页” “末页”按钮设置为不可用
                if (_pageCurrent == _totalpagecount)
                {
                    bnMoveNextItem.Enabled = false;
                    bnMoveLastItem.Enabled = false;
                }
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 末页按钮(GridView页面跳转使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveLastItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将“总页数”赋值给PageCurrent 2)将“下一页” “末页”按钮设置为不可用 并将 “上一页” “首页”按钮设置为可用
                _pageCurrent = _totalpagecount;
                bnMoveNextItem.Enabled = false;
                bnMoveLastItem.Enabled = false;

                bnMovePreviousItem.Enabled = true;
                bnMoveFirstItem.Enabled = true;

                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 跳转页文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnPositionItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //判断所输入的跳转数必须为整数
                if (!Regex.IsMatch(bnPositionItem.Text, @"^-?[1-9]\d*$|^0$")) throw new Exception("请输入整数再继续");
                //判断所输入的跳转数不能大于总页数
                if (Convert.ToInt32(bnPositionItem.Text) > _totalpagecount) throw new Exception("所输入的页数不能超出总页数,请修改后继续");
                //判断若所填跳转数为0时跳出异常
                if (Convert.ToInt32(bnPositionItem.Text) == 0) throw new Exception("请输入大于0的整数再继续");

                //将所填的跳转页赋值至“当前页”变量内
                _pageCurrent = Convert.ToInt32(bnPositionItem.Text);
                //根据所输入的页数动态控制四个方向键是否可用
                //若为第1页，就将“首页” “上一页”按钮设置为不可用 将“下一页” “末页”设置为可用
                if (_pageCurrent == 1)
                {
                    bnMoveFirstItem.Enabled = false;
                    bnMovePreviousItem.Enabled = false;

                    bnMoveNextItem.Enabled = true;
                    bnMoveLastItem.Enabled = true;
                }
                //若为末页,就将"下一页" “末页”按钮设置为不可用 将“上一页” “首页”设置为可用
                else if (_pageCurrent == _totalpagecount)
                {
                    bnMoveNextItem.Enabled = false;
                    bnMoveLastItem.Enabled = false;

                    bnMovePreviousItem.Enabled = true;
                    bnMoveFirstItem.Enabled = true;
                }
                //否则四个按钮都可用
                else
                {
                    bnMoveFirstItem.Enabled = true;
                    bnMovePreviousItem.Enabled = true;
                    bnMoveNextItem.Enabled = true;
                    bnMoveLastItem.Enabled = true;
                }
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bnPositionItem.Text = Convert.ToString(_pageCurrent);
            }
        }

        /// <summary>
        /// 每页显示行数 下拉框关闭时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmshowrows_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //每次选择新的“每页显示行数”，都要 1)将_pageChange标记设为true(即执行初始化方法) 2)将“当前页”初始化为1
                _pageChange = true;
                _pageCurrent = 1;
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// GridView分页功能
        /// </summary>
        private void GridViewPageChange()
        {
            try
            {
                //获取查询的总行数
                var dtltotalrows = _dtl.Rows.Count;
                //获取“每页显示行数”所选择的行数
                var pageCount = Convert.ToInt32(tmshowrows.SelectedItem);
                //计算出总页数
                _totalpagecount = dtltotalrows % pageCount == 0 ? dtltotalrows / pageCount : dtltotalrows / pageCount + 1;
                //赋值"总页数"项
                bnCountItem.Text = $"/ {_totalpagecount} 页";

                //初始化BindingNavigator控件内的各子控件 及 对应初始化信息
                if (_pageChange)
                {
                    bnPositionItem.Text = Convert.ToString(1);                       //初始化填充跳转页为1
                    tmshowrows.Enabled = true;                                      //每页显示行数（下拉框）  

                    //初始化时判断;若“总页数”=1，四个按钮不可用；若>1,“下一页” “末页”按钮可用
                    if (_totalpagecount == 1)
                    {
                        bnMoveNextItem.Enabled = false;
                        bnMoveLastItem.Enabled = false;
                        bnMoveNextItem.Enabled = false;
                        bnMoveLastItem.Enabled = false;
                        bnPositionItem.Enabled = false;                             //跳转页文本框
                    }
                    else
                    {
                        bnMoveNextItem.Enabled = true;
                        bnMoveLastItem.Enabled = true;
                        bnPositionItem.Enabled = true;                             //跳转页文本框
                    }
                    _pageChange = false;
                }

                //显示_dtl的查询总行数
                tstotalrow.Text = $"共 {_dtl.Rows.Count} 行";

                //根据“当前页” 及 “固定行数” 计算出新的行数记录并进行赋值
                //计算进行循环的起始行
                var startrow = (_pageCurrent - 1) * pageCount;
                //计算进行循环的结束行
                var endrow = _pageCurrent == _totalpagecount ? dtltotalrows : _pageCurrent * pageCount;
                //复制 查询的DT的列信息（不包括行）至临时表内
                var tempdt = _dtl.Clone();
                //循环将所需的_dtl的行记录复制至临时表内
                for (var i = startrow; i < endrow; i++)
                {
                    tempdt.ImportRow(_dtl.Rows[i]);
                }

                //最后将刷新的DT重新赋值给GridView
                gvdtl.DataSource = tempdt;
                //将“当前页”赋值给"跳转页"文本框内
                bnPositionItem.Text = Convert.ToString(_pageCurrent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 连接GridView页面跳转功能
        /// </summary>
        /// <param name="dt"></param>
        private void LinkGridViewPageChange(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                _dtl = dt;
                panel3.Visible = true;
                //初始化下拉框所选择的默认值
                tmshowrows.SelectedItem = "10";
                //定义初始化标记
                _pageChange = true;
                //GridView分页
                GridViewPageChange();
            }
            //注:当为空记录时,不显示跳转页;只需将临时表赋值至GridView内
            else
            {
                gvdtl.DataSource = dt;
                panel3.Visible = false;
            }
        }

        /// <summary>
        /// 根据下拉列表所选择的值，查询到结果后返回DT至GridView
        /// </summary>
        private void ShowCustInfoIntoGridView()
        {
            //获取下拉列表信息
            var dvColIdlist = (DataRowView)comlist.Items[comlist.SelectedIndex];
            var pid = Convert.ToString(dvColIdlist["Id"]);

            //初始化 根据“客户类型名称列表”主键ID 获取基础信息库-客户信息管理明细客户信息
            task.TaskId = 1;
            task.FunctionId = "1";
            task.FunctionName = "Customer";
            task.FunctionType = "G";
            task.ParentId = pid;

            task.StartTask();

            //连接GridView页面跳转功能
            LinkGridViewPageChange(task.ResultTable);
            //设置GridView是否显示某些列
            ControlGridViewisShow();
        }

    }
}
