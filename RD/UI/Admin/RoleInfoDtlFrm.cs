using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Admin
{
    public partial class RoleInfoDtlFrm : Form
    {
        TaskLogic task = new TaskLogic();
        Load load=new Load();

        //状态标记(作用:记录打开此功能窗体时是 读取记录 还是 创建记录) C:创建 R:读取
        private string _funState;
        //获取传递过来的角色名称DT(“创建”状态时作检验使用)
        private DataTable _roledt;
        //获取传递过来的角色ID("读取"状态时使用)
        private int _roleid;
        //获取功能大类名称DT("创建"状态时使用)
        private DataTable _funTypedt;

        //保存查询出来的GridView记录（GridView页面跳转时使用）
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
        //记录是否管理员状态(Y:是 N:否)
        private string _adminMarkId;

        #region Set

            /// <summary>
            /// 获取单据状态标记ID C:创建 R:读取
            /// </summary>
            public string FunState { set { _funState = value; } }

            /// <summary>
            /// 获取传递过来的角色DT
            /// </summary>
            public DataTable Roledt { set { _roledt = value; } }

            /// <summary>
            /// 角色ID("读取"状态时使用)
            /// </summary>
            public int Roleid { set { _roleid = value; } }

        #endregion

        public RoleInfoDtlFrm()
        {
            InitializeComponent();
            //初始化窗体各控件
            OnRegisterEvents();
            //初始化下拉列表
            OnInitializeDropDownList();
        }

        private void OnRegisterEvents()
        {
            tmSave.Click += TmSave_Click;
            tmConfirm.Click += TmConfirm_Click;
            tmSetshow.Click += TmSetshow_Click;
            tmSetConfirm.Click += TmSetConfirm_Click;
            tmSetCanDel.Click += TmSetCanDel_Click;
            comType.SelectionChangeCommitted += ComType_SelectionChangeCommitted;
            cbadmin.Click += Cbadmin_Click;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel1.Visible = false;
            tabControl1.Visible = false;
        }

        /// <summary>
        /// 初始化基础信息
        /// </summary>
        public void OnInitialize()
        {
            //读取表头信息
            ShowHead(_roleid);
            //读取明细记录至GridView
            OnInitializeDtl();
            //窗体权限控制
            PrivilegeControl();
            //将隐藏的部份开启
            tabControl1.Visible = true;
        }

        /// <summary>
        /// 初始化大类名称下拉列表
        /// </summary>
        private void OnInitializeDropDownList()
        {
            //初始化功能大类名称
            _funTypedt= GetListdt("FunType");
            comType.DataSource = _funTypedt;
            comType.DisplayMember = "FunName";
            comType.ValueMember = "Funid";
        }

        /// <summary>
        /// 初始化表体信息("读取" 及 "刷新下拉列表"时使用)
        /// </summary>
        /// <returns></returns>
        private void OnInitializeDtl()
        {
            //获取下拉列表所选的记录
            var dvFunTypelist = (DataRowView)comType.Items[comType.SelectedIndex];
            var funid = Convert.ToInt32(dvFunTypelist["Funid"]);

            //读取数据
            task.TaskId = 3;
            task.FunctionId = "1.3";
            task.Roleid = _roleid;   //角色ID
            task.Funtypeid = funid;  //功能大类ID

            task.StartTask();
            //连接GridView页面跳转功能
            LinkGridViewPageChange(task.ResultTable);
            //控制GridView单元格显示方式
            ControlGridViewisShow();
        }

        /// <summary>
        /// 根据ID获取角色名称表头信息(包括“角色名称”，“审核状态”等)
        /// </summary>
        /// <param name="roleid">角色ID</param>
        private void ShowHead(int roleid)
        {
            task.TaskId = 3;
            task.FunctionId = "1.4";
            task.Roleid = roleid;

            task.StartTask();
            //并将结果赋给对应的文本框内(表头信息)
            var dt = task.ResultTable;
            txtrolename.Text = dt.Rows[0][1].ToString();           //角色名称
            _adminMarkId = dt.Rows[0][4].ToString();              //管理员权限标记
            _closeMarkId = dt.Rows[0][5].ToString();             //关闭状态
            _confirmMarkId = dt.Rows[0][6].ToString();          //审核状态
        }

        /// <summary>
        /// 根据相关条件将功能权限记录插入至T_AD_ROLEDTL内(状态"创建"时使用)
        /// </summary>
        private void InsertRecordIntoRoledtl()
        {
            try
            {
                task.TaskId = 3;
                task.FunctionId = "2";
                task.FunctionName = txtrolename.Text;
                task.Data = _funTypedt;
                task.AccountName = GlobalClasscs.User.StrUsrName;

                task.StartTask();
                //返回新插入的表头ID;即T_AD_Role.Id值
                _roleid = task.Orderid;
                if (_roleid == 0) throw new Exception("发生异常,请联系管理员");
                //当成功后将状态更改为“读取”
                _funState = "R";
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
                if (_funState=="C")
                {
                    //保存前先检测文本框的值是否重复
                    //获取最新的T_AD_Role的DataTable
                    _roledt = GetListdt("Role");
                    //验证所输入的"角色名称"是否已存在
                    var rows = _roledt.Select("RoleName='" + txtrolename.Text + "'");
                    if (rows.Length > 0) throw new Exception("已存在相同的‘角色名称’,请再输入");

                    //执行插入T_AD_RoleDtl方法
                    InsertRecordIntoRoledtl();
                    //执行初始化方法;重新读取
                    OnInitialize();
                }
                //执行更新效果(主要是对T_AD_Role表头信息进行更新)
                else
                {
                    //执行更新
                    task.TaskId = 3;
                    task.FunctionId = "3";
                    task.Roleid = _roleid;
                    task.FunctionName = txtrolename.Text;                //角色名称
                    task.Canallmark = cbadmin.Checked ? "Y" : "N";       //管理员权限复选框

                    task.StartTask();
                    if(!task.ResultMark) throw new Exception("更新异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("保存成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //执行初始化;重新读取
                    OnInitialize();
                }
            }
            catch (Exception ex)
            {
                txtrolename.Text = "";
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
                var newcbadmin = cbadmin.Checked?"Y":"N";

                //当检测到“管理员权限”不相同的,会提示异常
                if (_adminMarkId != newcbadmin) throw new Exception("检测到'管理员权限'已修改但没有保存,请先进行保存后再进行审核");

                var clickMessage = $"您所选择的信息为:\n 角色名称:{txtrolename.Text} \n 是否继续? \n 注:若不进行审核,职员帐户不能使用 \n 请谨慎处理.";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    task.TaskId = 3;
                    task.FunctionId = "4";
                    task.Confirmid = 0;        //审核标记 0:审核 1:反审核
                    task.Roleid = _roleid;

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
        /// 管理员权限“复选框”点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbadmin_Click(object sender, EventArgs e)
        {
            try
            {
                //当为选中状态时
                if (cbadmin.Checked)
                {
                    comType.Enabled = false;
                    gvdtl.Enabled = false;
                }
                //当为非选中状态时
                else
                {
                    comType.Enabled = true;
                    gvdtl.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 大类名称下拉列表值发生改变后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                OnInitializeDtl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置角色功能权限能否"显示"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSetshow_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能查阅");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("没有选中的行,请选中后继续.");
                //修改角色功能权限(包括:是否显示 显示反审核 是否可删除)
                ChangeRoleFunState(2, "CanShow", gvdtl.SelectedRows);
                //完成后进行刷新
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置角色功有权限能否"反审核"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSetConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能查阅");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("没有选中的行,请选中后继续.");
                 
                //先检测所选中的行是否已设置了“已显示”，若没有，跳出异常
                if(CheckCanShowMark(gvdtl.SelectedRows)>0) throw new Exception("检测到所选择的行中没有设置‘已显示’权限,请先进行设置,再继续");
                //修改角色功能权限(包括:是否显示 显示反审核 是否可删除)
                ChangeRoleFunState(3, "CanBackConfirm", gvdtl.SelectedRows);
                //完成后进行刷新
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置角色功有权限能否"可删除"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSetCanDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能查阅");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("没有选中的行,请选中后继续.");

                //先检测所选中的行是否已设置了“已显示”，若没有，跳出异常
                if (CheckCanShowMark(gvdtl.SelectedRows) > 0) throw new Exception("检测到所选择的行中没有设置‘已显示’权限,请先进行设置,再继续");
                //修改角色功能权限(包括:是否显示 显示反审核 是否可删除)
                ChangeRoleFunState(4, "CanDel", gvdtl.SelectedRows);
                //完成后进行刷新
                OnInitialize();
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
                cbadmin.Enabled = false;
                txtrolename.Enabled = false;
                comType.Enabled = false;
                gvdtl.Enabled = false;
                cbadmin.Checked = _adminMarkId == "Y";
            }
            //若为“非审核”状态的,就执行以下语句
            else
            {
                //将审核图片控件隐藏
                pbimg.Visible = false;
                //将所有功能的状态还原(即与审核时的控件状态相反)
                tmSave.Enabled = true;
                tmConfirm.Enabled = true;
                cbadmin.Enabled = true;
                txtrolename.Enabled = true;
                comType.Enabled = true;
                gvdtl.Enabled = true;
                cbadmin.Checked = _adminMarkId == "Y";
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
        /// 控制GridView单元格显示方式
        /// </summary>
        private void ControlGridViewisShow()
        {
            //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
        }

        /// <summary>
        /// 更改明细功能内各状态信息(如:显示 反审核 删除权限)
        /// </summary>
        /// <param name="functionName">功能分类名称</param>
        /// <param name="funtypeid">0:正面操作(如:显示) 1:反面操作(如:不显示)</param>
        /// <param name="dt">要执行操作的DT</param>
        /// <returns></returns>
        private bool ChangeState(string functionName, int funtypeid,DataTable dt)
        {
            var result = true;
            try
            {
                task.TaskId = 3;
                task.FunctionId = "6";
                task.FunctionName = functionName;
                task.Funtypeid = funtypeid;
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
        /// 检测所选中的GridView行是否有设置“已显示”标记
        /// </summary>
        /// <param name="datarow"></param>
        /// <returns></returns>
        private int CheckCanShowMark(DataGridViewSelectedRowCollection datarow)
        {
            var result = 0;

            foreach (DataGridViewRow row in datarow)
            {
                //获取"是否显示"标记
                var canshowmark = Convert.ToString(row.Cells[2].Value); 
                if (canshowmark == "否")
                {
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// 修改角色功能权限(包括:是否显示 显示反审核 是否可删除)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="funname"></param>
        /// <param name="datarow"></param>
        private void ChangeRoleFunState(int id,string funname,DataGridViewSelectedRowCollection datarow)
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
                    var entryid = Convert.ToInt32(row.Cells[0].Value);       //获取T_AD_RoleDtl.EntryId
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
                    case 2:
                        message = $"检测到所选择的行中有 \n 已设置显示'{ytempdt.Rows.Count}'行 \n 末设置显示'{ntempdt.Rows.Count}'行 \n 是否继续?";
                        break;
                    case 3:
                        message = $"检测到所选择的行中有 \n 已设置反审核'{ytempdt.Rows.Count}'行 \n 末设置反审核'{ntempdt.Rows.Count}'行 \n 是否继续?";
                        break;
                    case 4:
                        message = $"检测到所选择的行中有 \n 已设置可删除'{ytempdt.Rows.Count}'行 \n 末设置可删除'{ntempdt.Rows.Count}'行 \n 是否继续?";
                        break;
                }

                if (MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (ntempdt.Rows.Count > 0)
                        nbool = ChangeState(funname, 0, ntempdt);
                    if (ytempdt.Rows.Count > 0)
                        ybool = ChangeState(funname, 1, ytempdt);
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
