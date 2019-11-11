using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class MaterialOrderFrm : Form
    {
        Load load = new Load();
        TypeInfoFrm typeInfo = new TypeInfoFrm();
        TaskLogic task = new TaskLogic();
        DtList dtList = new DtList();

        #region 变量定义
        //获取表头ID
        private int _pid;
        //记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
        private string _funName;
        //记录审核状态(True:已审核;False:没审核)
        private bool _confirmMarkId;
        //单据状态标记(作用:记录打开此功能窗体时是 读取记录 还是 创建记录) C:创建 R:读取
        private string _funState;
        //反审核标记(注:当需要反审核的R状态单据要进行再次审核时使用)
        private bool _backconfirm;

        //记录行ID（后面的‘替换’功能使用）
        private int rowid;
        //审核 提交时使用
        private DataTable _sourcedt;
        //两页间数据转换时使用（中转DT）
        private DataTable _tempdt;
        //保存删除的明细行(当读取状态时使用;注:作提交之用)
        private DataTable _deldt;
        //保存删除的明细行(记录从‘录入页’至‘预览页’需要删除的行记录,注:不作提交之用)
        private DataTable _deltempdt;
        //保存‘预览页’的所有记录(在‘初始化’及使用‘保存’按钮的最后,将要存放至gvdtl的记录也存放到这里)
        private DataTable _gvdtldt;

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
        /// 获取单据状态标记ID C:创建 R:读取
        /// </summary>
        public string FunState { set { _funState = value; } }
        /// <summary>
        /// 获取表头ID
        /// </summary>
        public int Pid { set { _pid = value; } }
        /// <summary>
        /// 记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
        /// </summary>
        public string FunName { set { _funName = value; } }
        #endregion

        public MaterialOrderFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tmconfirm.Click += Tmconfirm_Click;
            tmsave.Click += Tmsave_Click;
            tmprint.Click += Tmprint_Click;
            btnsave.Click += Btnsave_Click;
            btnremove.Click += Btnremove_Click;
            cbshow.Click += Cbshow_Click;

            tmadd.Click += Tmadd_Click;
            tmreplace.Click += Tmreplace_Click;
            tmdel.Click += Tmdel_Click;
            gvshow.CellValueChanged += Gvshow_CellValueChanged;
            tmshowdetail.Click += Tmshowdetail_Click;
            this.FormClosing += MaterialOrderFrm_FormClosing;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.Leave += BnPositionItem_Leave;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel9.Visible = false;
        }

        /// <summary>
        /// 初始化(注:根据功能状态标记来控制)
        /// </summary>
        public void OnInitialize()
        {
            //根据功能名称 及 表头ID读取表头相关信息(包括单据编号等)
            ShowHead(_funName, _pid);
            //初始化定义_tempdt临时表
            _tempdt = dtList.Get_ProMaterialEmtrydt();

            //单据状态:创建 C (创建模板至‘录入页’)
            if (_funState == "C")
            {
                gvshow.DataSource = OnInitializeDtl();
            }
            //单据状态:读取 R (将读取的数据存放至‘预览页’)
            else
            {
                //初始化反审核标记为false
                _backconfirm = false;
                //获取读取过来的记录信息
                _gvdtldt = OnInitializeDtl();
                //todo 更新用户占用单据方法

                //通过格式转换再赋值至gvdtl内
                //LinkGridViewPageChange(ChangeDisplayStyle(0, _gvdtldt));
            }
            //控制GridView单元格显示方式
            ControlGridViewisShow();
            //权限控制
            PrivilegeControl();
        }

        /// <summary>
        /// 根据功能名称 及 表头ID读取表头相关信息(包括单据编号等)
        /// </summary>
        /// <param name="funname"></param>
        /// <param name="pid"></param>
        private void ShowHead(string funname, int pid)
        {
            //根据ID值读取T_Pro_Adorn表记录;并将结果赋给对应的文本框内
            task.TaskId = 2;
            task.FunctionId = "1.3";
            task.FunctionName = funname;
            task.Pid = pid;

            task.StartTask();
            //并将结果赋给对应的文本框内(表头信息)
            var dt = task.ResultTable;
            txtOrderNo.Text = dt.Rows[0][0].ToString();
            txtCustomer.Text = dt.Rows[0][1].ToString();
            txtHoseName.Text = dt.Rows[0][2].ToString();
            txtAdd.Text = dt.Rows[0][3].ToString();
            //获取审核标记
            _confirmMarkId = dt.Rows[0][4].ToString() == "Y";
        }

        /// <summary>
        /// 初始化获取表体信息(注:若单据状态C时,获取空白表;若为R时,就按情况判断是读取空白表还是有内容的表)
        /// </summary>
        /// <returns></returns>
        private DataTable OnInitializeDtl()
        {
            var resultdt = new DataTable();

            try
            {
                task.TaskId = 2;
                task.FunctionId = "1.2";
                task.FunState = _funState;
                task.Pid = _pid;

                task.StartTask();
                resultdt = task.ResultTable;
            }
            catch (Exception ex)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resultdt;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmconfirm_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmsave_Click(object sender, EventArgs e)
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmprint_Click(object sender, EventArgs e)
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
        /// 保存及刷新(注:将gvshow的记录通过添加至_dtl再通过转换将记录添加至gvdtl内)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnsave_Click(object sender, EventArgs e)
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
        /// 清空记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btnremove_Click(object sender, EventArgs e)
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
        /// 是否显示‘预览页’
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
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmadd_Click(object sender, EventArgs e)
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
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmreplace_Click(object sender, EventArgs e)
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
        /// 删除明细行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmdel_Click(object sender, EventArgs e)
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
        /// 当GridView内的单元格的值更改时发生（注:每当有其中一个单元格的值发生变化时,都会循环 行中所有单元格并执行此事件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gvshow_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
        /// 当点击‘预览页’某行时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmshowdetail_Click(object sender, EventArgs e)
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
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaterialOrderFrm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// 权限控制
        /// </summary>
        private void PrivilegeControl()
        {
            //若为“审核”状态的话,就执行以下语句
            if (_confirmMarkId)
            {
                //加载图片
                pbimg.Visible = true;
                pbimg.BackgroundImage = Image.FromFile(Application.StartupPath + @"\PIC\1.png");
                //'预览页'方面的设置
                btnsave.Enabled = false;

                tmadd.Visible = false;
                ts1.Visible = false;
                tmreplace.Visible = false;
                ts2.Visible = false;
                tmdel.Visible = false;

                //若单据状态为R并且不为‘反审核’时执行
                if (_funState == "R" && !_backconfirm)
                {
                    tmconfirm.Enabled = false;
                    tmsave.Enabled = false;
                }
                //单据状态为C“创建” 及R “反审核”时使用,当审核完成单据,但还没提交时执行
                else
                {
                    tmconfirm.Enabled = false;
                }
            }
            //若为“非审核”状态的,就执行以下语句
            else
            {
                pbimg.Visible = false;
                tmconfirm.Enabled = true;
                tmsave.Enabled = true;
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
        private void BnPositionItem_Leave(object sender, EventArgs e)
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
                panel9.Visible = true;
                //初始化下拉框所选择的默认值
                tmshowrows.SelectedItem = "10";
                //定义初始化标记
                _pageChange = _pageCurrent <= 1;
                //_pageChange = true;
                //GridView分页
                GridViewPageChange();
            }
            //注:当为空记录时,不显示跳转页;只需将临时表赋值至GridView内
            else
            {
                gvdtl.DataSource = dt;
                panel9.Visible = false;
            }
        }

        /// <summary>
        /// 控制GridView单元格显示方式
        /// </summary>
        private void ControlGridViewisShow()
        {
            //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            //“录入页”设置
            if (gvshow.DataSource == null) return;
            gvshow.Columns[0].Visible = false;
            gvshow.Columns[1].Visible = false;
            gvshow.Columns[2].Visible = false;
            gvshow.Columns[3].Visible = false; //大类名称

            //设置指定列不能编辑
            gvshow.Columns[4].ReadOnly = true; //装修工程类别
            gvshow.Columns[5].ReadOnly = true; //项目名称
            gvshow.Columns[6].ReadOnly = true; //单位名称
            gvshow.Columns[8].ReadOnly = true; //综合单价
            gvshow.Columns[11].ReadOnly = true; //单价
            gvshow.Columns[13].ReadOnly = true; //合计

            gvshow.Columns[gvshow.Columns.Count - 1].Visible = false; //RowId(单据状态为C时使用)
            gvshow.Columns[gvshow.Columns.Count - 2].Visible = false; //录入人
            gvshow.Columns[gvshow.Columns.Count - 3].Visible = false; //录入日期

            //“预览页”设置(注:当gvdtl.datasource有值时才会执行)
            if (gvdtl.DataSource == null) return;
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            gvdtl.Columns[2].Visible = false;
            gvdtl.Columns[gvdtl.Columns.Count - 1].Visible = false; // RowId(单据状态为C时使用)
        }
    }
}
