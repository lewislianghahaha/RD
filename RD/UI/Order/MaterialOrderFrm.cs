using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;
using Stimulsoft.Report;

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

                //通过格式转换再赋值至gvdtl内 todo:读取时‘合计’项获取
                LinkGridViewPageChange(ChangeDisplayStyle(0, _gvdtldt));
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
            try
            {
                //定义_sourcedt临时表
                _sourcedt = dtList.Get_ProMaterialEmtrydt();
                //将_sourcedt删除最后一列'RowId'(便于后面的提交使用)
                _sourcedt.Columns.Remove("RowId");
                //审核条件:=>'录入页'内所有项都不能为空
                if (!CheckGvshow()) throw new Exception("检测到‘录入页’有记录,请将记录保存或清空,再执行查阅");

                var clickMessage = $"您所选择的信息为:\n 单据名称:{txtOrderNo.Text} \n 是否继续? \n 注:审核后需反审核才能对该单据的记录进行修改, \n 请谨慎处理.";

                if (MessageBox.Show(clickMessage, $"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //将_gvdtldt的数据循环插入至_sourcedt
                    foreach (DataRow rows in _gvdtldt.Rows)
                    {
                        var newrow = _sourcedt.NewRow();
                        newrow[0] = rows[0];      //ID    
                        newrow[1] = rows[1];      //EntryId
                        newrow[2] = rows[2];      //材料ID
                        newrow[3] = rows[3];      //材料大类名称
                        newrow[4] = rows[4];      //材料名称
                        newrow[5] = rows[5];      //单位名称
                        newrow[6] = rows[6];      //工程量
                        newrow[7] = rows[7];      //综合单价
                        newrow[8] = rows[8];      //人工费用
                        newrow[9] = rows[9];      //辅材费用
                        newrow[10] = rows[10];    //单价
                        newrow[11] = rows[11];    //临时材料单价
                        newrow[12] = rows[12];    //合计
                        newrow[13] = rows[13];    //备注
                        newrow[14] = rows[14];    //录入人
                        newrow[15] = rows[15];    //录入日期
                        _sourcedt.Rows.Add(newrow);
                    }

                    //审核成功后操作 => 1)审核图片显示 2)将控件设为不可修改 3)弹出成功信息窗体 4)将_confirmMarkid标记设为True
                    MessageBox.Show($"审核成功,请进行提交操作", $"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _confirmMarkId = true;
                    //若单据状态为R时,_backconfirm为TRUE
                    if (_funState == "R")
                        _backconfirm = true;
                    //权限控制
                    PrivilegeControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                //判断若没有完成审核,即不能执行
                if (!_confirmMarkId) throw new Exception("请先点击‘审核’再继续");

                task.TaskId = 2;
                task.FunctionId = "5";
                task.FunctionName = _funName;
                task.Data = _sourcedt;
                task.Deldata = _deldt;

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (!task.ResultMark) throw new Exception("提交异常,请联系管理员");
                else
                {
                    MessageBox.Show($"单据'{txtOrderNo.Text}'提交成功,可关闭此单据", $"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tmsave.Enabled = false;
                }
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
                task.TaskId = 4;
                task.FunctionId = "4";
                task.Id = _pid;
                task.FunctionName = _funName;
                Start();

                var resultdt = task.ResultTable;
                if (resultdt.Rows.Count == 0) throw new Exception("导出异常,请联系管理员");
                //调用STI模板并执行导出代码
                //加载STI模板 MaterialOrderReport
                var filepath = Application.StartupPath + "/Report/MaterialOrderReport.mrt";
                var stireport = new StiReport();
                stireport.Load(filepath);
                //加载DATASET 或 DATATABLE
                stireport.RegData("Order", resultdt);
                stireport.Compile();
                stireport.Show();   //调用预览功能
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
                //定义‘合计’值
                decimal totalamount = 0;

                //定义_gvdtldt临时表(注:若_gvdtldt!=null就不用创建)
                if (_gvdtldt == null)
                    _gvdtldt = dtList.Get_ProMaterialEmtrydt();

                if (gvshow.RowCount == 0) throw new Exception("没有明细行,不能执行操作");

                //将gvshow的记录插入至_tempdt内
                var sourceddt = (DataTable)gvshow.DataSource;
                foreach (DataRow rows in sourceddt.Rows)
                {
                    var newrow = _tempdt.NewRow();
                    newrow[0] = rows[0];              //ID
                    newrow[1] = rows[1];              //EntryID
                    newrow[2] = rows[2];              //材料ID
                    newrow[3] = rows[3];              //材料大类名称
                    newrow[4] = rows[4];              //材料名称
                    newrow[5] = rows[5];              //单位名称
                    newrow[6] = rows[6];              //工程量
                    newrow[7] = rows[7];              //综合单价
                    newrow[8] = rows[8];              //人工费用
                    newrow[9] = rows[9];              //辅材费用
                    newrow[10] = rows[10];            //单价
                    newrow[11] = rows[11];            //临时材料单价
                    newrow[12] = rows[12];            //合计
                    newrow[13] = rows[13];            //备注
                    newrow[14] = rows[14];            //录入人
                    newrow[15] = rows[15];            //录入日期
                    newrow[16] = rows[16];            //Rowid
                    _tempdt.Rows.Add(newrow);
                }

                //循环_tempdt 执行将其中EntryID为空的记录进行插入至_gvdtldt操作
                for (var i = 0; i < _tempdt.Rows.Count; i++)
                {
                    var orderid = Convert.ToInt32(_tempdt.Rows[i][1] != DBNull.Value ? _tempdt.Rows[i][1] : _tempdt.Rows[i][16]);
                    var colname = Convert.ToString(_tempdt.Rows[i][1] != DBNull.Value ? "EntryID" : "Rowid");

                    //若EntryID为空,并且(EntryID)Rowid在_gvdtldt不存在,才插入
                    if (Convert.ToString(_tempdt.Rows[i][1]) == "")
                    {
                        var rowdtl = _gvdtldt.Select($"{colname}='" + orderid + "'");
                        if (rowdtl.Length == 0)
                        {
                            //循环将记录插入至_gvdtldt内
                            var newrow = _gvdtldt.NewRow();
                            for (var j = 0; j < _tempdt.Columns.Count; j++)
                            {
                                newrow[j] = _tempdt.Rows[i][j];
                            }
                            _gvdtldt.Rows.Add(newrow);
                        }
                    }
                }

                //循环_tempdt,并以_tempdt为条件,若EntryID(Rowid)不为空并且在_gvdtldt内存在,即进行更新操作(前提条件:_gvdtldt行数不为0)
                if (_gvdtldt.Rows.Count > 0)
                {
                    for (var i = 0; i < _tempdt.Rows.Count; i++)
                    {
                        //以_tempdt.EntryID为条件,若在_gvdtldt内存在,即执行更新(注:若EntryID为空,即取Rowid)
                        var orderid = Convert.ToInt32(_tempdt.Rows[i][1] != DBNull.Value ? _tempdt.Rows[i][1] : _tempdt.Rows[i][16]);

                        foreach (DataRow rows in _gvdtldt.Rows)
                        {
                            var gvorderid = Convert.ToInt32(rows[1] != DBNull.Value ? rows[1] : rows[16]);
                            //若_tempdt.orderid与_gvdtldt.gvorderid相同,即进行更新
                            if (orderid != gvorderid) continue;

                            _gvdtldt.BeginInit();
                            rows[2] = _tempdt.Rows[i][2];   //材料ID
                            rows[3] = _tempdt.Rows[i][3];   //材料大类名称
                            rows[4] = _tempdt.Rows[i][4];   //材料名称
                            rows[5] = _tempdt.Rows[i][5];   //单位名称
                            rows[6] = _tempdt.Rows[i][6];   //工程量
                            rows[7] = _tempdt.Rows[i][7];   //综合单价
                            rows[8] = _tempdt.Rows[i][8];   //人工费用
                            rows[9] = _tempdt.Rows[i][9];   //辅材费用
                            rows[10] = _tempdt.Rows[i][10]; //单价
                            rows[11] = _tempdt.Rows[i][11]; //临时材料单价
                            rows[12] = _tempdt.Rows[i][12]; //合计
                            rows[13] = _tempdt.Rows[i][13]; //备注
                            rows[14] = _tempdt.Rows[i][14]; //录入人
                            rows[15] = _tempdt.Rows[i][15]; //录入日期
                            _gvdtldt.EndInit();
                        }
                    }
                }

                if (_deltempdt?.Rows.Count > 0)
                {
                    //删除在_gvdtldt对应的记录
                    for (var i = _gvdtldt.Rows.Count; i > 0; i--)
                    {
                        for (var j = 0; j < _deltempdt.Rows.Count; j++)
                        {
                            //从_deltempdt内获取EntryID(Rowid)记录
                            var delorderid = Convert.ToInt32(_deltempdt.Rows[j][1] != DBNull.Value ? _deltempdt.Rows[j][1] : _deltempdt.Rows[j][16]);
                            //从_gvdtldt内获取EntryID(Rowid)记录
                            var gvorderid = Convert.ToInt32(_gvdtldt.Rows[i - 1][1] != DBNull.Value ? _gvdtldt.Rows[i - 1][1] : _gvdtldt.Rows[i - 1][16]);
                            //将两者对比,若相同,即删除
                            if (gvorderid != delorderid) continue;
                            _gvdtldt.Rows.RemoveAt(i - 1);
                        }
                    }
                }

                //通过累加_gvdtldt中的‘合计’并进行显示(累加‘合计’项)
                foreach (DataRow rows in _gvdtldt.Rows)
                {
                    totalamount += Convert.ToDecimal(rows[12]);
                }

                //最后赋值到对应的项内
                lbtotal.Text = Convert.ToString(totalamount, CultureInfo.InvariantCulture);

                //最后将整理好的结果转换显示格式,并赋值至gvdtl内
                LinkGridViewPageChange(ChangeDisplayStyle(0, _gvdtldt));
                //控制GridView单元格显示方式
                ControlGridViewisShow();
                //最后将_tempdt及gvshow的内容清空
                _tempdt.Rows.Clear();

                var dt = (DataTable)gvshow.DataSource;
                dt.Rows.Clear();
                gvshow.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var dt = (DataTable)gvshow.DataSource;
                dt.Rows.Clear();
                gvshow.DataSource = dt;
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
                tabControl2.Visible = !cbshow.Checked;
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
                //初始化typeinform.cs
                typeInfo.Funname = "Material";
                typeInfo.Remark = "A";
                typeInfo.OnInitialize();
                typeInfo.StartPosition = FormStartPosition.CenterScreen;
                typeInfo.ShowDialog();

                //判断若返回的DT为空的话,就不需要任何效果
                if (typeInfo.ResultTable == null || typeInfo.ResultTable.Rows.Count == 0) return;
                //将返回的结果赋值至GridView内(注:判断若返回的DT不为空或行数大于0才执行插入效果)
                if (typeInfo.ResultTable != null || typeInfo.ResultTable.Rows.Count > 0)
                    InsertDtToGridView(typeInfo.ResultTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 将获取的数据插入至GridView内(注:可多行使用)
        /// </summary>
        private void InsertDtToGridView(DataTable sourcedt)
        {
            try
            {
                var gridViewdt = dtList.Get_ProMaterialEmtrydt();
                //先判断若gvshow.DataSource内有Adornid(Rowid)行存在,将这些记录先插入
                var gvshowdt = (DataTable)gvshow.DataSource;
                foreach (DataRow rows in gvshowdt.Rows)
                {
                    var orderid = Convert.ToInt32(rows[1] != DBNull.Value ? rows[1] : rows[16]);
                    var colname = Convert.ToString(rows[1] != DBNull.Value ? "EntryID" : "Rowid");
                    var rowdtl = gvshowdt.Select($"{colname}='" + orderid + "'");

                    for (var i = 0; i < rowdtl.Length; i++)
                    {
                        var newrow = gridViewdt.NewRow();
                        for (var j = 0; j < gridViewdt.Columns.Count; j++)
                        {
                            newrow[j] = rowdtl[i][j];
                        }
                        gridViewdt.Rows.Add(newrow);
                    }
                }

                //若_gvdtldt有值,rowid的初始值就取其最后一行的Rowid值
                if (_gvdtldt?.Rows.Count > 0)
                    rowid = Convert.ToInt32(_gvdtldt.Rows[_gvdtldt.Rows.Count - 1][16]);

                //然后再循环将获取过来的值插入至GridView内
                foreach (DataRow rows in sourcedt.Rows)
                {
                    var newrow = gridViewdt.NewRow();
                    newrow[0] = _pid;       //id(表头主键ID)
                    newrow[2] = rows[0];    //MaterialId
                    newrow[3] = rows[1];    //材料大类名称
                    newrow[4] = rows[2];    //材料名称
                    newrow[5] = rows[3];    //单位名称
                    newrow[10] = rows[4];   //单价
                    newrow[16] = ++rowid;   //RowId(单据状态为C时使用)
                    gridViewdt.Rows.Add(newrow);
                }

                //将记录写回gvshow内
                gvshow.DataSource = gridViewdt;
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
                if (gvshow.SelectedRows.Count == 0) throw new Exception("请选择一行记录进行替换.");
                if (gvshow.Rows[gvshow.CurrentCell.RowIndex].Cells[0].Value == DBNull.Value) throw new Exception("空行不能进行替换,请再次选择");
                //获取GridView内的RowID(EntryID) 注:那个不为空就取那个
                var orderid = Convert.ToInt32(gvshow.Rows[gvshow.CurrentCell.RowIndex].Cells[1].Value != DBNull.Value ?
                                            gvshow.Rows[gvshow.CurrentCell.RowIndex].Cells[1].Value : gvshow.Rows[gvshow.CurrentCell.RowIndex].Cells[16].Value);

                //初始化typeinform.cs
                typeInfo.Funname = "Material";
                typeInfo.Remark = "U";
                typeInfo.OnInitialize();
                typeInfo.StartPosition = FormStartPosition.CenterScreen;
                typeInfo.ShowDialog();

                //判断若返回的DT为空的话,就不需要任何效果
                if (typeInfo.ResultTable == null || typeInfo.ResultTable.Rows.Count == 0) return;
                //将返回的结果赋值至GridView内(注:判断若返回的DT不为空或行数大于0才执行插入效果)
                if (typeInfo.ResultTable != null || typeInfo.ResultTable.Rows.Count > 0)
                    UpdateDtToGridView(orderid, typeInfo.ResultTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 将获取的值更新至指定的GridView行内(注:只能一行使用)
        /// </summary>
        /// <param name="orderid">主键值</param>
        /// <param name="sourcedt">从typeinfofrm窗体获取的DT</param>
        private void UpdateDtToGridView(int orderid, DataTable sourcedt)
        {
            try
            {
                //循环GridView内的值,当发现ID与条件ID相同,即进入行更新
                var gridViewdt = (DataTable)gvshow.DataSource;

                foreach (DataRow rows in gridViewdt.Rows)
                {
                    //判断若EntryID(RowId)与变量orderid相同,即执行替换操作
                    var gvorderid = Convert.ToInt32(rows[1] != DBNull.Value ? rows[1] : rows[16]);
                    if (orderid != gvorderid) continue;

                    gridViewdt.BeginInit();
                    rows[2] = sourcedt.Rows[0][0];              //MaterialId
                    rows[3] = sourcedt.Rows[0][1];              //材料大料名称
                    rows[4] = sourcedt.Rows[0][2];              //材料名称
                    rows[5] = sourcedt.Rows[0][3];              //单位名称

                    rows[6] = DBNull.Value;                     //工程量(清空)
                    rows[7] = DBNull.Value;                     //综合单价(清空)
                    rows[8] = DBNull.Value;                     //人工费用(清空)
                    rows[9] = DBNull.Value;                     //辅材费用(清空)
                    rows[10] = sourcedt.Rows[0][4];             //单价
                    rows[11] = DBNull.Value;                    //临时材料单价(清空)
                    rows[12] = DBNull.Value;                    //合计(清空)
                    rows[13] = DBNull.Value;                    //备注(清空)
                    rows[14] = GlobalClasscs.User.StrUsrName;   //录入人(更新至当前用户)
                    rows[15] = DateTime.Now.Date;               //录入日期(更新至当天)
                    gridViewdt.EndInit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                try
                {
                    if (gvshow.SelectedRows.Count == 0) throw new Exception("没有选择行,不能继续");
                    if (gvshow.RowCount == 0) throw new Exception("没有明细记录,不能进行删除");

                    var clickMessage = $"您所选择需删除的行数为:'{gvshow.SelectedRows.Count}' 行 \n 是否继续?";
                    if (MessageBox.Show(clickMessage, $"提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //注:执行方式:当判断到_funState变量为R时,将要进行删除的行保存至_deldt内,(供保存时使用),完成后再删除GridView指定行;反之,只需将GridView进行指定行删除即可
                        //注:在R状态下,需判断EntryID是否为空,若不为空,才进行插入至_deldt内

                        if (_funState == "R")
                        {
                            //定义_deldt临时表
                            _deldt = dtList.Get_ProMaterialEmtrydt();

                            foreach (DataGridViewRow rows in gvshow.SelectedRows)
                            {
                                //判断若EntryId不为空,才执行插入
                                if (rows.Cells[1].Value == DBNull.Value) continue;

                                var newrow = _deldt.NewRow();
                                for (var i = 0; i < _deldt.Columns.Count; i++)
                                {
                                    newrow[i] = rows.Cells[i].Value;
                                }
                                _deldt.Rows.Add(newrow);
                            }
                        }

                        //定义_deltempdt临时表
                        _deltempdt = dtList.Get_ProMaterialEmtrydt();
                        //记录‘录入页’要删除的行记录;注:无论是那个单据状态,并且此临时表不作提交之用;
                        foreach (DataGridViewRow rows in gvshow.SelectedRows)
                        {
                            var newrow = _deltempdt.NewRow();
                            for (var i = 0; i < _deltempdt.Columns.Count; i++)
                            {
                                newrow[i] = rows.Cells[i].Value;
                            }
                            _deltempdt.Rows.Add(newrow);
                        }

                        //将GridView内的指定行进行删除
                        for (var i = gvshow.SelectedRows.Count; i > 0; i--)
                        {
                            gvshow.Rows.RemoveAt(gvshow.SelectedRows[i - 1].Index);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 1 || e.ColumnIndex == 12)
                {
                    decimal renCost = 0;  //人工费用
                    decimal fuCost = 0;  //辅材费用
                    decimal price = 0;  //单价

                    //计算“综合单价”(7)=人工费用(8)+辅材费用(9)+(单价(10) 或 临时价(11))
                    //人工费用
                    renCost = Convert.ToString(gvshow.Rows[e.RowIndex].Cells[8].Value) == "" ? 0 : Convert.ToDecimal(gvshow.Rows[e.RowIndex].Cells[8].Value);
                    //辅材费用
                    fuCost = Convert.ToString(gvshow.Rows[e.RowIndex].Cells[9].Value) == "" ? 0 : Convert.ToDecimal(gvshow.Rows[e.RowIndex].Cells[9].Value);
                    //单价(注:若临时价有值。就取临时价。反之用单价;若两者都没有的话,就为0)
                    if (Convert.ToString(gvshow.Rows[e.RowIndex].Cells[11].Value) != "")
                    {
                        price = Convert.ToDecimal(gvshow.Rows[e.RowIndex].Cells[11].Value);
                    }
                    else if (Convert.ToString(gvshow.Rows[e.RowIndex].Cells[10].Value) != "")
                    {
                        price = Convert.ToDecimal(gvshow.Rows[e.RowIndex].Cells[10].Value);
                    }
                    else
                    {
                        price = 0;
                    }
                    gvshow.Rows[e.RowIndex].Cells[7].Value = renCost + fuCost + price;
                }
                else if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    decimal quantities = 0;  //工程量
                    decimal finalPrice = 0; //综合单价

                    //计算“合计”(12)=工程量(6) * 综合单价(7)

                    //工程量
                    quantities = Convert.ToString(gvshow.Rows[e.RowIndex].Cells[6].Value) == "" ? 0 : Convert.ToDecimal(gvshow.Rows[e.RowIndex].Cells[6].Value);
                    //综合单价
                    finalPrice = Convert.ToString(gvshow.Rows[e.RowIndex].Cells[7].Value) == "" ? 0 : Convert.ToDecimal(gvshow.Rows[e.RowIndex].Cells[7].Value);

                    gvshow.Rows[e.RowIndex].Cells[12].Value = quantities * finalPrice;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("请选中一行后再继续.");
                if (gvdtl.RowCount == 0) throw new Exception("‘预览页’没有内容,不能执行查阅");

                var gvshowdt = (DataTable)gvshow.DataSource;
                if (gvshowdt.Rows.Count > 0) throw new Exception("检测到‘录入页’有记录,请将记录保存或清空,再执行查阅");

                //获取所选中的EntryId(Rowid)值
                var orderid = Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value != DBNull.Value ?
                                            gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value : gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[16].Value);

                var colname = Convert.ToString(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value != DBNull.Value ? "EntryId" : "Rowid");

                //根据EntryId(Rowid)获取对应的‘材料大类名称’
                var rowdtl = _gvdtldt.Select($"{colname}='" + orderid + "'");
                //根据获取的‘材料大类名称’再次放到_gvdtldt内进行查找对应的明细行记录
                var rows = _gvdtldt.Select("材料大类名称='" + rowdtl[0][3] + "'");

                for (var i = 0; i < rows.Length; i++)
                {
                    var newrow = _tempdt.NewRow();
                    newrow[0] = rows[i][0];              //ID
                    newrow[1] = rows[i][1];              //EntryID
                    newrow[2] = rows[i][2];              //材料ID
                    newrow[3] = rows[i][3];              //材料大类名称
                    newrow[4] = rows[i][4];              //材料名称
                    newrow[5] = rows[i][5];              //单位名称
                    newrow[6] = rows[i][6];              //工程量
                    newrow[7] = rows[i][7];              //综合单价
                    newrow[8] = rows[i][8];              //人工费用
                    newrow[9] = rows[i][9];              //辅材费用
                    newrow[10] = rows[i][10];            //单价
                    newrow[11] = rows[i][11];            //临时材料单价
                    newrow[12] = rows[i][12];            //合计
                    newrow[13] = rows[i][13];            //备注
                    newrow[14] = rows[i][14];            //录入人
                    newrow[15] = rows[i][15];            //录入日期
                    newrow[16] = rows[i][16];            //Rowid
                    _tempdt.Rows.Add(newrow);
                }

                //根据_tempdt获取其内容对以下项进行赋值
                var dt = (DataTable)gvshow.DataSource;
                //将_tempdt的内容赋值至gvshow内
                for (var i = 0; i < _tempdt.Rows.Count; i++)
                {
                    var newrow = dt.NewRow();
                    for (var j = 0; j < _tempdt.Columns.Count; j++)
                    {
                        newrow[j] = _tempdt.Rows[i][j];
                    }
                    dt.Rows.Add(newrow);
                }
                gvshow.DataSource = dt;
                //最后将_tempdt清空
                _tempdt.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var clickMessage = !_confirmMarkId ? $"提示:单据'{txtOrderNo.Text}'没提交, \n 其记录退出后将会消失,是否确定退出?" : $"是否退出? \n 注:若审核但没有提交,退出后数据也会消失";

                if (e.CloseReason != CloseReason.ApplicationExitCall)
                {
                    var result = MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    //当点击"OK"按钮时执行以下操作
                    if (result == DialogResult.Yes)
                    {
                        //当退出时,清空useid等相关占用信息
                        //当单据状态为C时执行
                        if (_funState == "C")
                        {
                            //UpdateUseValue(0, 1, txtbom.Text);
                        }
                        //当单据状态为R时执行
                        else
                        {
                            //UpdateUseValue(_fid, 1, "");
                        }

                        //允许窗体关闭
                        e.Cancel = false;
                    }
                    else
                    {
                        //将Cancel属性设置为 true 可以“阻止”窗体关闭
                        e.Cancel = true;
                    }
                }
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

            //设置指定列不能编辑
            gvshow.Columns[3].ReadOnly = true; //材料大类名称
            gvshow.Columns[5].ReadOnly = true; //单位名称
            gvshow.Columns[7].ReadOnly = true; //综合单价
            gvshow.Columns[10].ReadOnly = true; //单价
            gvshow.Columns[12].ReadOnly = true; //合计

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

        private void Start()
        {
            task.StartTask();

            this.Invoke((ThreadStart)(() =>
            {
                load.Close();
            }));
        }

        /// <summary>
        /// 检查gvshow内是否有值
        /// </summary>
        /// <returns></returns>
        private bool CheckGvshow()
        {
            var result = true;
            var dt = (DataTable)gvshow.DataSource;
            if (dt.Rows.Count > 0)
                result = false;
            return result;
        }

        /// <summary>
        /// GridView显示格式转换
        /// </summary>
        /// <param name="type">转换类型;0:从gvshow数据转换至gvdtl 以及 转换初始化读取的记录至‘预览’窗体内 1:从gvdtl数据转换至gvshow </param>
        /// <param name="sourcedt"></param>
        /// <returns></returns>
        private DataTable ChangeDisplayStyle(int type, DataTable sourcedt)
        {
            //定义转换临时表
            var changetempdt = sourcedt.Clone(); //dtList.Get_ProMaterialEmtrydt();
            //定义‘材料大类名称’变量
            var materialtypename = string.Empty;
            //定义‘合计’变量
            decimal total = 0;

            //定义DataRow数组
            var rowdtl = new DataRow[] { };

            //从gvshow数据转换至gvdtl (或转换初始化读取的记录为‘预览’显示模式)
            if (type == 0)
            {
                //整理出sourcedt内包含的‘材料大类名称’并返回DT
                var typedt = GetMaterialTypeNameDt(sourcedt);

                //使用整理出来的typedt作循环
                foreach (DataRow rows in typedt.Rows)
                {
                    rowdtl = sourcedt.Select("材料大类名称='" + rows[3] + "'");

                    //根据相关条件执行插入操作
                    for (var j = 0; j < rowdtl.Length; j++)
                    {
                        var newrow = changetempdt.NewRow();
                        newrow[0] = rowdtl[j][0];      //ID
                        newrow[1] = rowdtl[j][1];      //EntryID
                        newrow[2] = rowdtl[j][2];      //MaterialId

                        //材料大类名称
                        if (materialtypename == "")
                        {
                            newrow[3] = rowdtl[j][3];
                            materialtypename = Convert.ToString(rowdtl[j][3]);
                        }
                        else
                        {
                            newrow[3] = DBNull.Value;
                        }

                        newrow[4] = rowdtl[j][4];     //材料名称
                        newrow[5] = rowdtl[j][5];     //单位名称
                        newrow[6] = rowdtl[j][6];     //工程量
                        newrow[7] = rowdtl[j][7];     //综合单价
                        newrow[8] = rowdtl[j][8];     //人工费用
                        newrow[9] = rowdtl[j][9];     //辅材费用
                        newrow[10] = rowdtl[j][10];   //单价
                        newrow[11] = rowdtl[j][11];   //临时材料单价
                        newrow[12] = rowdtl[j][12];   //合计
                        newrow[13] = rowdtl[j][13];   //备注
                        newrow[14] = rowdtl[j][14];   //录入人
                        newrow[15] = rowdtl[j][15];   //录入日期
                        newrow[16] = rowdtl[j][16];   //Rowid
                        changetempdt.Rows.Add(newrow);
                        //累加‘合计’数-读取状态时使用
                        total += Convert.ToDecimal(rowdtl[j][12]);
                    }
                    //完成循环后将相关变量清空
                    materialtypename = "";
                }
            }
            //若为‘读取’状态,即执行
            if (_funState == "R")
            { lbtotal.Text = Convert.ToString(total, CultureInfo.InvariantCulture);}

            return changetempdt;
        }

        /// <summary>
        /// 检索出一个数源DT内有多少个‘材料大类名称’记录，整理后返回DT
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <returns></returns>
        private DataTable GetMaterialTypeNameDt(DataTable sourcedt)
        {
            var dt = sourcedt.Clone();
            //循环sourcedt,并统计出不相同的‘材料大类名称’（注:若循环的记录不在dt内存在,才进行插入）
            foreach (DataRow rows in sourcedt.Rows)
            {
                var rowdtl = dt.Select("材料大类名称='" + Convert.ToString(rows[3]) + "'");
                if (rowdtl.Length != 0) continue;
                var newrow = dt.NewRow();
                newrow[3] = rows[3]; //材料大类名称
                dt.Rows.Add(newrow);
            }
            return dt;
        }

    }
}
