using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Admin
{
    public partial class RoleInfoFrm : Form
    {
        TaskLogic task = new TaskLogic();
        Load load = new Load();

        //保存查询出来的角色名称DT
        private DataTable _roledt;
        //保存所选择的roleid
        private int _roleid;

        //保存查询出来的GridView记录
        private DataTable _dtl;
        //记录当前页数(GridView页面跳转使用)
        private int _pageCurrent = 1;
        //记录计算出来的总页数(GridView页面跳转使用)
        private int _totalpagecount;
        //记录初始化标记(GridView页面跳转 初始化时使用)
        private bool _pageChange;

        public RoleInfoFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitializeDropDownList();
        }

        private void OnRegisterEvents()
        {
            tmaddrole.Click += Tmaddrole_Click;
            tmreset.Click += Tmreset_Click;
            tmclose.Click += Tmclose_Click;

            tmshowdtl.Click += Tmshowdtl_Click;
            tmCloseInfo.Click += TmCloseInfo_Click;
            tmconfirm.Click += Tmconfirm_Click;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel1.Visible = false;

            comrole.SelectionChangeCommitted += Comrole_SelectionChangeCommitted;
        }

        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        private void OnInitializeDropDownList()
        {
            //初始化角色名称
            _roledt= GetDropdownListdt("Role");
            comrole.DataSource = _roledt;
            comrole.DisplayMember = "RoleName";
            comrole.ValueMember = "id";
            //初始化表体
            OnInitializedtl();
        }

        /// <summary>
        /// 初始化获取角色表体内容
        /// </summary>
        private void OnInitializedtl()
        {
            //获取角色名称下拉列表所选的值
            var dvrolelist = (DataRowView)comrole.Items[comrole.SelectedIndex];
            _roleid = Convert.ToInt32(dvrolelist["id"]);

            //初始化角色明细信息
            task.TaskId = 3;
            task.FunctionId = "1.2";
            task.Roleid = _roleid;

            task.StartTask();
            //连接GridView页面跳转功能
            LinkGridViewPageChange(task.ResultTable);
            //控制GridView单元格显示方式
            ControlGridViewisShow();
        }

        /// <summary>
        /// 角色名称下拉列表所选项发生改变时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comrole_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                OnInitializedtl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmaddrole_Click(object sender, EventArgs e)
        {
            try
            {
                //将当前窗体隐藏
                this.Visible = false;

                var roleInfo = new RoleInfoDtlFrm();
                //初始化信息赋值
                roleInfo.FunState = "C";
                roleInfo.Roledt = _roledt;

                roleInfo.StartPosition = FormStartPosition.CenterScreen;
                roleInfo.ShowDialog();
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
        private void Tmreset_Click(object sender, EventArgs e)
        {
            try
            {
                OnInitializeDropDownList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查看明细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmshowdtl_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvdtl.Rows.Count==0)throw new Exception("没有内容,不能查阅");
                if(gvdtl.SelectedRows.Count==0) throw new Exception("没有选中的行,请选中后继续.");

                //将当前窗体隐藏
                //this.Visible = false;

                var roleInfo = new RoleInfoDtlFrm();
                //初始化信息赋值
                roleInfo.FunState = "R";
                roleInfo.Roledt = _roledt;
                roleInfo.Roleid = Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[0].Value);  //获取所选择行的roleid
                roleInfo.OnInitialize();

                roleInfo.StartPosition = FormStartPosition.CenterScreen;
                roleInfo.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭(恢复)指定信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmCloseInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能查阅");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("没有选中的行,请选中后继续.");
                //批量修改角色功能权限(包括:是否关闭 审核)
                ChangeRoleStatue("5", 4, gvdtl.SelectedRows);
                //完成后刷新
                OnInitializedtl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 审核(反审核)指定信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能查阅");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("没有选中的行,请选中后继续.");
                //批量修改角色功能权限(包括:是否关闭 审核)
                ChangeRoleStatue("4", 5, gvdtl.SelectedRows);
                //完成后刷新
                OnInitializedtl();
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
        private void Tmclose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        /// 更改功能内各状态信息(如:审核 关闭权限)
        /// </summary>
        /// <param name="functionid">功能ID 4：审核 5：关闭</param>
        /// <param name="id">0:审核(关闭) 1:反审核(反审核)</param>
        /// <param name="dt">要执行操作的DT</param>
        /// <returns></returns>
        private bool ChangeState(string functionid,int id,DataTable dt)
        {
            var result = true;
            try
            {
                task.TaskId = 3;
                task.FunctionId = functionid;
                task.Data = dt;

                switch (functionid)
                {
                    case "4":
                        task.Confirmid = id;
                        break;
                    case "5":
                        task.Id = id;
                        break;
                }

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
                    //ID
                    case 0:
                        dc.ColumnName = "ID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 修改角色功能权限(包括:审核 关闭)
        /// </summary>
        /// <param name="functionid">功能ID 4：审核 5：关闭</param>
        /// <param name="rowid">行ID</param>
        /// <param name="datarow">所选择的行</param>
        private void ChangeRoleStatue(string functionid, int rowid, DataGridViewSelectedRowCollection datarow)
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
                    var roleid = Convert.ToInt32(row.Cells[0].Value);           //获取T_AD_Role.Id
                    var mark = Convert.ToString(row.Cells[rowid].Value);       //根据ID获取相关标记
                    
                    if (mark == "末关闭" || mark == "末审核")
                    {
                        var newrow = ntempdt.NewRow();
                        newrow[0] = roleid;
                        ntempdt.Rows.Add(newrow);
                    }
                    else
                    {
                        var newrow = ytempdt.NewRow();
                        newrow[0] = roleid;
                        ytempdt.Rows.Add(newrow);
                    }
                }

                switch (rowid)
                {
                    //关闭
                    case 4:
                        message = $"检测到所选择的行中有 \n 已设置关闭'{ytempdt.Rows.Count}'行 \n 末设置关闭'{ntempdt.Rows.Count}'行 \n 是否继续?";
                        break;
                    //审核
                    case 5:
                        message = $"检测到所选择的行中有 \n 已设置审核'{ytempdt.Rows.Count}'行 \n 末设置审核'{ntempdt.Rows.Count}'行 \n 是否继续?";
                        break;
                }
                //判断若以上两个临时表其中一个有值的话,就进行更新操作
                if (MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (ntempdt.Rows.Count > 0)
                        nbool = ChangeState(functionid, 0, ntempdt);
                    if (ytempdt.Rows.Count > 0)
                        ybool = ChangeState(functionid, 1, ytempdt);
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
