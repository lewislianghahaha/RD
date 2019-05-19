using System;
using System.Data;
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
        //获取传递过来的角色名称("读取"状态时使用)
        private string _rolename;
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
            
            /// <summary>
            /// 角色名称(“读取”状态时使用)
            /// </summary>
            public string Rolename { set { _rolename = value; } }

        #endregion

        public RoleInfoDtlFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
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
            txtrolename.TextChanged += Txtrolename_TextChanged;

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
            txtrolename.Text =_rolename;
            //初始化下拉列表
            OnInitializeDropDownList();
            //读取明细记录至GridView
            OnInitializeDtl();
            //控制GridView显示
            GridViewPageChange();
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
        /// 根据相关条件将功能权限记录插入至T_AD_ROLEDTL内(状态"创建"时使用)
        /// </summary>
        private void InsertRecordIntoRoledtl()
        {
            try
            {
                task.TaskId = 3;
                task.FunctionId = "2";
                task.FunctionName = _rolename;
                task.Data = _roledt;

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
        /// 角色名称文本框值发生变化时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtrolename_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //获取最新的T_AD_Role的DataTable
                _roledt= GetListdt("Role");
                //验证所输入的"角色名称"是否已存在
                var rows = _roledt.Select("RoleName='" + txtrolename.Text + "'");
                if(rows.Length>0) throw new Exception("已存在相同的‘角色名称’,请再输入");
                //若所输入的值没有重复,即将其赋值公共变量_rolename
                _rolename = txtrolename.Text;
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

                    task.StartTask();
                    if(!task.ResultMark) throw new Exception("更新异常,请联系管理员");
                    //执行初始化;重新读取
                    OnInitialize();
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
        private void TmConfirm_Click(object sender, System.EventArgs e)
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
        /// 管理员权限“复选框”点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cbadmin_Click(object sender, EventArgs e)
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
            ////若为“审核”状态的话，就执行以下语句
            //if (_confirmMarkId == "Y")
            //{
            //    //读取审核图片
            //    pbimg.Visible = true;
            //    pbimg.Image = Image.FromFile(Application.StartupPath + @"\PIC\1.png");
            //    //注:审核后只能查阅，打印;不能保存 审核 修改，除非反审核
            //    tmSave.Enabled = false;
            //    tmConfirm.Enabled = false;
            //    gvdtl.Enabled = false;
            //    btnGetdtl.Enabled = false;
            //    comHtype.Enabled = false;
            //}
            ////若为“非审核”状态的,就执行以下语句
            //else
            //{
            //    //将审核图片控件隐藏
            //    pbimg.Visible = false;
            //    //将所有功能的状态还原(即与审核时的控件状态相反)
            //    tmSave.Enabled = true;
            //    tmConfirm.Enabled = true;
            //    gvdtl.Enabled = true;
            //    btnGetdtl.Enabled = true;
            //    comHtype.Enabled = true;
            //}
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



    }
}
