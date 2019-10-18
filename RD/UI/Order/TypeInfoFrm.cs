using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class TypeInfoFrm : Form
    {
        TaskLogic task=new TaskLogic();
        Load load=new Load();
        DtList dtList=new DtList();

        #region 参数
        //获取表体DT
        private DataTable _dt;
        //获取ID信息(通过下拉列表 或 树型菜单获取)
        private int _id;
        //获取功能名称:Material 材料 HouseProject:装修工程类别
        private string _funname;
        //获取remark值(替换时使用 A:新增;U:替换)
        private string _remark;

        //返回DT类型
        private DataTable _resultTable;  

        //保存查询出来的GridView记录（GridView页面跳转时使用）
        private DataTable _dtl;
        //记录当前页数(GridView页面跳转使用)
        private int _pageCurrent = 1;
        //记录计算出来的总页数(GridView页面跳转使用)
        private int _totalpagecount;
        //记录初始化标记(GridView页面跳转 初始化时使用)
        private bool _pageChange;
        #endregion

        #region Set
        /// <summary>
        /// 获取ID信息(来源:房屋装修类型ID 或 材料信息管理ID)
        /// </summary>
        public int Id { set { _id = value; } }
        /// <summary>
        /// 获取功能名称
        /// </summary>
        public string Funname { set { _funname = value; } }
        /// <summary>
        /// 获取ID值(新增 替换时使用)
        /// </summary>
        public string Remark { set { _remark = value; } }
        #endregion

        #region Get

        /// <summary>
        /// 返回DT
        /// </summary>
        public DataTable ResultTable => _resultTable;

        #endregion

        public TypeInfoFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tmGet.Click += TmGet_Click;
            tmClose.Click += TmClose_Click;
            comlist.Click += Comlist_Click;
            btnSearch.Click += BtnSearch_Click;
            
            rdchooseBd.Click += RdchooseBd_Click;
            rdchooseOrderHistory.Click += RdchooseOrderHistory_Click;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel3.Visible = false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInitialize()
        {
            //初始化工程类别下拉列表
            OnShowTypeList();
            //初始化下拉列表
            OnInitializeDropdownlist();

            //控制若_funname为"装修工程类别明细"的话,panel5才显示
            switch (_funname)
            {
                case "HouseProject":
                    panel5.Visible = true;
                    break;
                case "Material":
                    panel5.Visible = false;
                    break;
            }
            //获取对应的类别明细记录(包括装修工程 及 材料)
            Getdtl(0);
            //连接GridView页面跳转功能
            LinkGridViewPageChange(_dt);
            //控制GridView指定列
            ControlGridViewisShow();
        }

        /// <summary>
        /// 初始化下拉菜单
        /// </summary>
        private void OnInitializeDropdownlist()
        {
            try
            {
                //若文本框不为空,先将文本框清空
                txtValue.Text = "";

                //判断若"从历史单据获取"单选按钮是否选中
                task.FunctionName = rdchooseOrderHistory.Checked ? "HistoryAdornEmpty" : _funname;
                task.TaskId = 1;
                task.FunctionId = "1.2";
                task.StartTask();

                comlist.DataSource = task.ResultTable;
                comlist.DisplayMember = "ColName";     //设置显示值
                comlist.ValueMember = "ColId";        //设置默认值内码(即:列名)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 从基础信息库获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdchooseBd_Click(object sender, EventArgs e)
        {
            try
            {
                Getdtl(0);
                //连接GridView页面跳转功能
                LinkGridViewPageChange(_dt);
                //初始化下拉列表
                OnInitializeDropdownlist();
                //设置GridView是否显示某些列
                ControlGridViewisShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 从历史单据获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdchooseOrderHistory_Click(object sender, EventArgs e)
        {
            try
            {
                //初始化获取历史单据记录
                Getdtl(1);
                //连接GridView页面跳转功能
                LinkGridViewPageChange(_dt);
                //初始化下拉列表
                OnInitializeDropdownlist();
                //设置GridView是否显示某些列
                ControlGridViewisShow();
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


                //当为‘新增’操作时执行
                if (_remark == "A")
                {

                }
                //当为‘替换’操作时
                else
                {

                }
                //完成后关闭该窗体
                this.Close();
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
            //若返回的DT有值时，先将内容清空,再执行“关闭”(注:必须有值)
            if (_resultTable?.Rows.Count > 0)
            {
                _resultTable.Rows.Clear();
                _resultTable.Columns.Clear();
            }
            this.Close();
        }

        /// <summary>
        /// 选择条件下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comlist_Click(object sender, EventArgs e)
        {
            //若文本框不为空,先将文本框清空
            txtValue.Text = "";
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int)comlist.SelectedIndex == -1) throw new Exception("请选择查询条件");
                //获取下拉列表值
                var dvColIdlist = (DataRowView)comlist.Items[comlist.SelectedIndex];
                var colName = Convert.ToString(dvColIdlist["ColName"]);

                //判断若"从历史单据获取"单选按钮是否选中
                task.FunctionName = rdchooseOrderHistory.Checked ? "HistoryAdornEmpty" : _funname;
                task.TaskId = 1;
                task.FunctionId = "1.1";
                task.SearchName = colName;
                task.SearchValue = txtValue.Text;
                task.Data = _dt;
                task.Pid = -1;

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                //连接GridView页面跳转功能
                LinkGridViewPageChange(task.ResultTable);
                //设置GridView是否显示某些列
                ControlGridViewisShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 控制GridView单元格显示方式
        /// </summary>
        private void ControlGridViewisShow()
        {
            if (_funname == "HouseProject")
            {
                //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
                gvdtl.Columns[0].Visible = false;
                gvdtl.Columns[1].Visible = false;
            }
            else if(_funname== "Material")
            {
                //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
                gvdtl.Columns[0].Visible = false;
                gvdtl.Columns[1].Visible = false;
                gvdtl.Columns[4].Visible = false;
            }
            //设置指定列不能编辑
            gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
            gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;
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
        /// 根据条件获取对应的DT记录(获取数据源)
        /// </summary>
        /// <param name="genid">运行ID 0:从基础信息库获取 1:从单据历史记录获取</param>
        /// <returns></returns>
        private void Getdtl(int genid)
        {
            //从基础信息库获取(注:主要区分来源)
            if (genid == 0)
            {
                task.TaskId = 1;
                task.FunctionId = "1.7";
                task.FunctionName = _funname;

                if (_funname == "HouseProject")
                {
                    //获取下拉列表所选值
                    var dvordertylelist = (DataRowView)comtype.Items[comtype.SelectedIndex];
                    var typeId = Convert.ToInt32(dvordertylelist["Id"]);

                    switch (typeId)
                    {
                        //土建工程
                        case 1:
                            task.ParentId = "5";
                            break;
                        //天花工程
                        case 2:
                            task.ParentId = "6";
                            break;
                        //地面工程
                        case 3:
                            task.ParentId = "7";
                            break;
                        //墙身工程
                        case 4:
                            task.ParentId = "8";
                            break;
                    }
                }
                //当_funname=Material时执行
                else
                {
                    task.ParentId = null;
                }
            }
            //从单据历史记录获取(包括‘室内装修工程’以及‘室内主材单’)
            else
            {
                task.TaskId = 2;
                task.FunctionId = "1.5";

                //功能名称:Material 材料 HouseProject:装修工程类别
                if (_funname== "HouseProject")
                {
                    task.Pid = _id;
                }
                //_funname='Material'
                else
                {
                    
                }
                
            }
            task.StartTask();
            _dt = task.ResultTable;
        }

        /// <summary>
        /// 工程类别下拉列表
        /// </summary>
        private void OnShowTypeList()
        {
            var dt = new DataTable();

            //创建表头
            for (var i = 0; i < 2; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        break;
                    case 1:
                        dc.ColumnName = "Name";
                        break;
                }
                dt.Columns.Add(dc);
            }

            //创建行内容
            for (var j = 0; j < 4; j++)
            {
                var dr = dt.NewRow();

                switch (j)
                {
                    case 0:
                        dr[0] = "1";
                        dr[1] = "土建工程";
                        break;
                    case 1:
                        dr[0] = "2";
                        dr[1] = "天花工程";
                        break;
                    case 2:
                        dr[0] = "3";
                        dr[1] = "地面工程";
                        break;
                    case 3:
                        dr[0] = "4";
                        dr[1] = "墙身工程";
                        break;
                }
                dt.Rows.Add(dr);
            }

            comtype.DataSource = dt;
            comtype.DisplayMember = "Name"; //设置显示值
            comtype.ValueMember = "Id";    //设置默认值内码
        }
    }
}
