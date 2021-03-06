﻿using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Admin
{
    public partial class AccountAddFrm : Form
    {
        TaskLogic task = new TaskLogic();

        //状态标记(作用:记录打开此功能窗体时是 读取记录 还是 创建记录) C:创建 R:读取
        private string _funState;
        //获取传递过来的帐号ID("读取"状态时使用)
        private int _userid=0;
        //记录职员性别Id
        private int _sexid;

        //保存查询出来的GridView记录
        private DataTable _dtl;
        //记录当前页数(GridView页面跳转使用)
        private int _pageCurrent = 1;
        //记录计算出来的总页数(GridView页面跳转使用)
        private int _totalpagecount;
        //记录初始化标记(GridView页面跳转 初始化时使用)
        private bool _pageChange;

        //记录审核状态(Y:已审核;N:没审核)
        private string _confirmMarkId;
        //记录关闭状态(Y:已关闭;N:末关闭)
        private string _closeMarkId;
        //记录复选框点击值
        private string _cbshowMarkid="N";

        #region set

            /// <summary>
            /// 获取单据状态标记ID C:创建 R:读取
            /// </summary>
            public string FunState { set { _funState = value; } }
            /// <summary>
            /// 获取传递过来的帐号ID
            /// </summary>
            public int Userid { set { _userid = value; } }

        #endregion

        public AccountAddFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitializeDropDownList();
        }

        private void OnRegisterEvents()
        {
            comsex.SelectionChangeCommitted += Comsex_SelectionChangeCommitted;
            cbshow.Click += Cbshow_Click;

            tmSave.Click += TmSave_Click;
            tmConfirm.Click += TmConfirm_Click;
            tmaddclose.Click += Tmaddclose_Click;
            tmaddRole.Click += TmaddRole_Click;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel1.Visible = false;
            tabControl1.Visible = false;
            tmConfirm.Enabled = false;
            tmaddRole.Enabled = false;
        }

        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        private void OnInitializeDropDownList()
        {
            //初始化职员性别
            comsex.DataSource = GetListdt("Sex");
            comsex.DisplayMember = "SexName";
            comsex.ValueMember = "Sexid";
        }

        /// <summary>
        /// 初始化表头
        /// </summary>
        public void OnInitialize()
        {
            //读取表头信息
            ShowHead(_userid);
            //读取明细记录至GridView
            OnInitializeDtl();
            //窗体权限控制
            PrivilegeControl();
            //将隐藏的部份开启
            tabControl1.Visible = true;
        }

        /// <summary>
        /// 初始化表体
        /// </summary>
        private void OnInitializeDtl()
        {
            //读取数据
            task.TaskId = 3;
            task.FunctionId = "1.6";
            task.Userid = _userid;               //职员帐号ID
            task.ConfirmfStatus = _cbshowMarkid; //获取“显示已添加并末关闭的权限记录”复选框标记

            task.StartTask();
            //连接GridView页面跳转功能
            LinkGridViewPageChange(task.ResultTable);
            //控制GridView单元格显示方式
            ControlGridViewisShow();
        }

        /// <summary>
        ///  根据ID获取角色名称表头信息(包括“职员名称”,“审核状态”等)
        /// </summary>
        /// <param name="userid"></param>
        private void ShowHead(int userid)
        {
            task.TaskId = 3;
            task.FunctionId = "1.5";
            task.Userid = userid;

            task.StartTask();
            //并将结果赋给对应的文本框内(表头信息)
            var dt = task.ResultTable;
            //并将结果赋给对应的文本框内(表头信息)
            txtName.Text = dt.Rows[0][1].ToString();                 //职员名称
            txtEmail.Text = dt.Rows[0][5].ToString();               //职员邮箱
            comsex.SelectedIndex = Convert.ToInt32(dt.Rows[0][3]) == 1 ? 0 : 1; //职员性别 0:男 1:女
            dtin.Value = Convert.ToDateTime(dt.Rows[0][6]);       //职员入职日期
            txtAdder.Text = dt.Rows[0][4].ToString();            //职员联系方式
            _closeMarkId = dt.Rows[0][7].ToString();            //帐号关闭状态
            _confirmMarkId = dt.Rows[0][10].ToString();         //审核状态
        }

        /// <summary>
        /// 根据相关条件将功能权限记录插入至T_AD_User内(状态"创建"时使用)
        /// </summary>
        private void InsertRecordIntoUser()
        {
            try
            {
                task.TaskId = 3;
                task.FunctionId = "2.1";
                task.FunctionName = txtName.Text;                     //职员名称
                task.Sexid = _sexid == 0 ? 1 : _sexid;                //职员性别                        
                task.Usercontact = txtAdder.Text;                     //职员联系方式
                task.Useremail = txtEmail.Text;                       //职员邮箱
                task.Dtime= dtin.Value.Date;                          //职员入职日期
                task.AccountName = GlobalClasscs.User.StrUsrName;     //录入人

                task.StartTask();
                //返回新插入的表头ID;即T_AD_Role.Id值
                _userid = task.Orderid;
                //当成功后将状态更改为“读取”
                _funState = "R";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 性别下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comsex_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //获取角色名称下拉列表所选的值
                var dvsexlist = (DataRowView)comsex.Items[comsex.SelectedIndex];
                _sexid = Convert.ToInt32(dvsexlist["Sexid"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 显示已添加但末关闭的权限记录(复选框)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbshow_Click(object sender, EventArgs e)
        {
            try
            {
                _cbshowMarkid = cbshow.Checked ? "Y" : "N";
                //刷新数据
                OnInitializeDtl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {
                //检测若‘职员名称’为空，不能进行保存
                if(txtName.Text=="") throw new Exception("请至少填上'职员名称'再进行审核");

                //根据"状态"，来判定是使用“插入”或“更新”功能
                //执行插入效果
                if (_funState == "C")
                {
                    //在保存前需检测‘职员名称’不能与已录入的记录重复
                    var dt = GetListdt("User");
                    //验证所输入的"职员名称"是否已存在
                    var rows = dt.Select("UserName='" + txtName.Text + "'");
                    if (rows.Length > 0) throw new Exception("已存在相同的‘职员名称’,请再输入");

                    //执行插入T_AD_UserDtl方法
                    InsertRecordIntoUser();
                    //执行初始化方法;重新读取
                    OnInitialize();
                    //将审核按钮 以及 添加新增角色记录按钮设置为‘启用’
                    tmConfirm.Enabled = true;
                    tmaddRole.Enabled = true;
                }
                //执行更新
                else
                {
                    task.TaskId = 3;
                    task.FunctionId = "3.1";
                    task.Userid = _userid;
                    //获取文本框字段(更新时使用)
                    task.FunctionName = txtName.Text;                     //职员名称
                    task.Sexid = _sexid == 0 ? 1 : _sexid;                //职员性别                        
                    task.Usercontact = txtAdder.Text;                     //职员联系方式
                    task.Useremail = txtEmail.Text;                       //职员邮箱
                    task.Dtime = dtin.Value.Date;                         //职员入职日期

                    task.StartTask();
                    if (!task.ResultMark) throw new Exception("更新异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("保存成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //重新读取
                    OnInitialize();
                    //将审核按钮 以及 添加新增角色记录按钮设置为‘启用’
                    tmConfirm.Enabled = true;
                    tmaddRole.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Text = "";
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var clickMessage = $"您所选择的信息为:\n 职员名称:{txtName.Text} \n 是否继续? \n 注:若不进行审核,不能使用 \n 请谨慎处理.";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    task.TaskId = 3;
                    task.FunctionId = "4.1";
                    task.Id = 0;        //审核标记 0:审核 1:反审核
                    task.Userid = _userid;

                    task.StartTask();
                    if (!task.ResultMark) throw new Exception("审核异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("审核成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //审核完成后“刷新”(注:审核成功的单据,经刷新后为不可修改效果)
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加(取消)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmaddclose_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能查阅");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("没有选中的行,请选中后继续.");
                //修改帐户对应的角色权限(包括:是否添加)
                ChangeUserRoleAddState(5, gvdtl.SelectedRows);
                //完成后进行刷新
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新增新创建的角色权限记录T_AD_Role（注:必须为已审核 末关闭的记录）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmaddRole_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvdtl.Rows.Count==0)throw new Exception("没有角色明细记录,不能执行操作");
                task.Id = 3;
                task.FunctionId = "2.2";
                task.Userid = _userid;
                task.AccountName = GlobalClasscs.User.StrUsrName;
                task.Data = (DataTable) gvdtl.DataSource;

                task.StartTask();
                //返回结果;-1:异常 0:没有新增记录 1:操作成功
                switch (task.Orderid)
                {
                    case 0:
                        MessageBox.Show($"没有新增加的角色记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case -1:
                        MessageBox.Show($"发生异常,请联系管理员", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show($"操作成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //操作完成后“刷新”
            OnInitialize();
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
        /// 连接GridView页面跳转功能
        /// </summary>
        /// <param name="dt"></param>
        private void LinkGridViewPageChange(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                _dtl = dt;
                panel1.Visible = true;
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
                panel1.Visible = false;
            }
        }

        /// <summary>
        /// 权限控制 
        /// </summary>
        private void PrivilegeControl()
        {
            //若为“审核” 或 "关闭" 状态的话，就执行以下语句
            if (_confirmMarkId == "Y" || _closeMarkId == "Y")
            {
                //读取审核图片
                pbimg.Visible = true;
                pbimg.Image = Image.FromFile(Application.StartupPath + @"\PIC\1.png");
                //注:审核后只能查阅，打印;不能保存 审核 修改，除非反审核
                tmSave.Enabled = false;
                tmConfirm.Enabled = false;
                txtName.Enabled = false;
                txtEmail.Enabled = false;
                comsex.Enabled = false;
                dtin.Enabled = false;
                txtAdder.Enabled = false;
                gvdtl.Enabled = false;
                tmaddRole.Enabled = false;
            }
            //若为“非审核”状态的,就执行以下语句
            else
            {
                //将审核图片控件隐藏
                pbimg.Visible = false;
                //将所有功能的状态还原(即与审核时的控件状态相反)
                tmSave.Enabled = true;
                tmConfirm.Enabled = true;
                txtName.Enabled = true;
                txtEmail.Enabled = true;
                comsex.Enabled = true;
                dtin.Enabled = true;
                txtAdder.Enabled = true;
                gvdtl.Enabled = true;
                tmaddRole.Enabled = true;
            }
        }

        /// <summary>
        /// 更改明细功能内各状态信息(如:可添加权限)
        /// </summary>
        /// <param name="id">功能分类名称</param>
        /// <param name="dt">要执行操作的DT</param>
        /// <returns></returns>
        private bool ChangeState(int id, DataTable dt)
        {
            var result = true;
            try
            {
                task.TaskId = 3;
                task.FunctionId = "3.2";
                task.Id = id;
                task.Data = dt;

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
        /// 创建临时表(表体明细功能权限设置使用)
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTable()
        {
            var dt = new DataTable();
            for (var i = 0; i < 1; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //EntryID
                    case 0:
                        dc.ColumnName = "EntryID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 修改帐户角色功能权限(包括:是否添加)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datarow"></param>
        private void ChangeUserRoleAddState(int id,DataGridViewSelectedRowCollection datarow)
        {
            //创建Ntempdt 及 Ytempdt临时表
            var ntempdt = CreateTable();
            var ytempdt = CreateTable();

            //设置返回值
            var nbool = new bool();
            var ybool = new bool();

            //提示变量
            var message = string.Empty;

            try
            {
                //循环所选择的行中的设置状态
                foreach (DataGridViewRow row in datarow)
                {
                    var entryid = Convert.ToInt32(row.Cells[0].Value);       //获取T_AD_UserDtl.EntryId
                    var mark = Convert.ToString(row.Cells[id].Value);       //根据ID获取相关标记
                    //判断若mark变量为N,就将以上两个变量赋值到'Ntempdt'临时表内,反之,赋值到'Ytempdt'临时表内
                    if (mark == "否")
                    {
                        var newrow = ntempdt.NewRow();
                        newrow[0] = entryid;
                        ntempdt.Rows.Add(newrow);
                    }
                    else
                    {
                        var newrow = ytempdt.NewRow();
                        newrow[0] = entryid;
                        ytempdt.Rows.Add(newrow);
                    }
                }
                //判断若以上两个临时表其中一个有值的话,就进行更新操作
                switch (id)
                {
                    case 5:
                        message = $"检测到所选择的行中有 \n 已设置添加'{ytempdt.Rows.Count}'行 \n 末设置添加'{ntempdt.Rows.Count}'行 \n 是否继续?";
                        break;
                }

                if (MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (ntempdt.Rows.Count > 0)
                        nbool = ChangeState(0, ntempdt);
                    if (ytempdt.Rows.Count > 0)
                        ybool = ChangeState(1, ytempdt);
                    if (nbool || ybool)
                        MessageBox.Show("操作成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
