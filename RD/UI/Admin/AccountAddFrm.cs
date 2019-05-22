using System;
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
        private int _userid;
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

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel1.Visible = false;
        }

        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        private void OnInitializeDropDownList()
        {
            //初始化职员性别
            comsex.DataSource = GetDropdownListdt("Sex");
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
            //控制GridView显示
            GridViewPageChange();
            //窗体权限控制
            PrivilegeControl();
        }

        /// <summary>
        /// 初始化表体
        /// </summary>
        private void OnInitializeDtl()
        {
            //读取数据
            task.TaskId = 3;
            task.FunctionId = "1.5";
            //
            //角色名称ID

        }

        /// <summary>
        ///  根据ID获取角色名称表头信息(包括“职员名称”，“审核状态”等)
        /// </summary>
        /// <param name="userid"></param>
        private void ShowHead(int userid)
        {
            task.TaskId = 3;
            task.FunctionId = "1.4";
            task.Id = userid;

            task.StartTask();
            //并将结果赋给对应的文本框内(表头信息)
            var dt = task.ResultTable;
            //

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
                task.Sexid = _sexid;                                  //职员性别
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
                //根据"状态"，来判定是使用“插入”或“更新”功能
                //执行插入效果
                if (_funState == "C")
                {
                    //执行插入T_AD_UserDtl方法
                    InsertRecordIntoUser();
                    //执行初始化方法;重新读取
                    OnInitialize();
                }
                else
                {
                    //执行更新
                    task.TaskId = 3;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private DataTable GetDropdownListdt(string functionName)
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
            //if (_confirmMarkId == "Y" || _closeMarkId == "Y")
            //{
            //    //读取审核图片
            //    pbimg.Visible = true;
            //    pbimg.Image = Image.FromFile(Application.StartupPath + @"\PIC\1.png");
            //    //注:审核后只能查阅，打印;不能保存 审核 修改，除非反审核
            //    tmSave.Enabled = false;
            //    tmConfirm.Enabled = false;
            //    cbadmin.Enabled = false;
            //    txtrolename.Enabled = false;
            //    comType.Enabled = false;
            //    gvdtl.Enabled = false;
            //    cbadmin.Checked = _adminMarkId == "Y";
            //}
            ////若为“非审核”状态的,就执行以下语句
            //else
            //{
            //    //将审核图片控件隐藏
            //    pbimg.Visible = false;
            //    //将所有功能的状态还原(即与审核时的控件状态相反)
            //    tmSave.Enabled = true;
            //    tmConfirm.Enabled = true;
            //    cbadmin.Enabled = true;
            //    txtrolename.Enabled = true;
            //    comType.Enabled = true;
            //    gvdtl.Enabled = true;
            //    cbadmin.Checked = _adminMarkId == "Y";
            //}
        }

    }
}
