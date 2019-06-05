using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class MaterialOrderFrm : Form
    {
        TaskLogic task = new TaskLogic();
        Load load = new Load();
        TypeInfoFrm typeInfoFrm = new TypeInfoFrm();
        DtList dtList = new DtList();

        //保存GridView内需要进行删除的临时表
        private DataTable _deldt = new DataTable();
        //单据状态标记(作用:记录打开此功能窗体时是 创建记录 还是 读取记录) C:创建 R:读取
        private string _funState;
        //获取表头ID
        private int _pid;
        //记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
        private string _funName;
        //记录审核状态(Y:已审核;N:没审核)
        private string _confirmMarkId;

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
        /// 获取表头ID
        /// </summary>
        public int Pid { set { _pid = value; } }
        /// <summary>
        /// 记录功能名称 Adorn:室内装修工程 Material:室内主材单
        /// </summary>
        public string FunName { set { _funName = value; } }

        /// <summary>
        /// 获取单据状态标记ID C:创建 R:读取
        /// </summary>
        public bool CandelMarkid { set { _candelMarkid = value; } }

        #endregion

        public MaterialOrderFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tvView.AfterSelect += TvView_AfterSelect;
            btnGetdtl.Click += BtnGetdtl_Click;
            tmSave.Click += TmSave_Click;
            tmConfirm.Click += TmConfirm_Click;
            tmExcel.Click += TmExcel_Click;
            tmPrint.Click += TmPrint_Click;
            tmdel.Click += Tmdel_Click;
            gvdtl.CellValueChanged += Gvdtl_CellValueChanged;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel4.Visible = false;
        }

        /// <summary>
        /// 初始化(注:根据功能状态标记来控制)
        /// </summary>
        public void OnInitialize()
        {
            //清空树菜单信息
            tvView.Nodes.Clear();
            //根据功能名称 及 表头ID读取表头相关信息(包括单据编号等)
            ShowHead(_funName, _pid);

            //单据状态:创建 C
            if (_funState == "C")
            {
                //将“材料信息管理”的表头信息导入至对应的T_PRO_MaterialTree表内
                InsertMaterialIntoDt();
            }

            //读取树菜单信息至tview控件内
            task.TaskId = 2;
            task.FunctionId = "1.1";
            task.FunctionName = _funName;
            task.Pid = _pid;

            task.StartTask();
            //读取设置树菜单表头信息
            ShowTreeList(task.ResultTable);

            //连接GridView页面跳转功能
            LinkGridViewPageChange(OnInitializeDtl());

            //对GridView赋值(将对应功能点的表体全部信息赋值给GV控件内)
            //gvdtl.DataSource = OnInitializeDtl();

            //设置GridView是否显示某些列
            ControlGridViewisShow();
            //展开根节点
            tvView.ExpandAll();
            //预留(权限部份)
            PrivilegeControl();
        }

        /// <summary>
        /// 初始化获取表体信息(注:若单据状态C时,获取空白表;若为R时,就按情况判断是读取空白表还是有内容的表)
        /// </summary>
        /// <returns></returns>
        private DataTable OnInitializeDtl()
        {
            var dt = new DataTable();

            try
            {
                task.TaskId = 2;
                task.FunctionId = "1.2";
                task.FunState = _funState;
                task.FunctionName = _funName;
                task.Pid = _pid;

                task.StartTask();
                dt = task.ResultTable;
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// 将“材料信息管理”的表头信息插入至T_Pro_MaterialTree表
        /// </summary>
        /// <returns></returns>
        private void InsertMaterialIntoDt()
        {
            try
            {
                task.TaskId = 2;
                task.FunctionId = "2.4";
                task.FunctionName = "MaterialOrderTree";
                task.Id = _pid;

                task.StartTask();
                if(!task.ResultMark)throw new Exception("插入信息异常,请联系管理员");
                _funState = "R";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            _confirmMarkId = dt.Rows[0][4].ToString();
        }

        /// <summary>
        /// 根据获取的DT读取树形列表
        /// </summary>
        /// <param name="dt">树型菜单Datatable</param>
        private void ShowTreeList(DataTable dt)
        {
            var tnode = new TreeNode();
            tnode.Tag = 1;
            tnode.Text = "ALL";
            tvView.Nodes.Add(tnode);
            //展开根节点
            tvView.ExpandAll();
            //读取记录并增加至子节点内(从二级节点开始)
            AddChildNode(tvView, dt);
        }

        /// <summary>
        /// 读取记录并增加至子节点内(从二级节点开始)
        /// </summary>
        /// <returns></returns>
        private void AddChildNode(TreeView tvview, DataTable dt)
        {
            var tnode = tvview.Nodes[0];
            var rows = dt.Select("ParentId='" + Convert.ToInt32(tnode.Tag) + "' and Id = '" + _pid + "'");
            //循环子节点信息(从二级节点开始)
            foreach (var r in rows)
            {
                var tn = new TreeNode();
                tn.Tag = Convert.ToInt32(r[1]);        //自身主键ID
                tn.Text = Convert.ToString(r[3]);     //节点内容
                //将二级节点添加至根节点下
                tnode.Nodes.Add(tn);
                //获取在二级节点以下的节点信息并进行添加(使用递归)
                GetChildNode(dt, tn);
            }
        }

        /// <summary>
        /// 获取在二级节点以下的节点信息并进行添加(递归)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tr">节点信息</param>
        private void GetChildNode(DataTable dt, TreeNode tr)
        {
            try
            {
                var pid = Convert.ToInt32(tr.Tag);
                var rowdtl = dt.Select("ParentId='" + pid + "' and Id = '" + _pid + "'");

                if (rowdtl.Length <= 0) return;
                foreach (var t in rowdtl)
                {
                    var tn = new TreeNode();
                    tn.Tag = Convert.ToInt32(t[1]);
                    tn.Text = Convert.ToString(t[3]);
                    tr.Nodes.Add(tn);
                    //(重) 以子节点的ID作为条件,查询其有没有与它关联的记录,若有,执行递归调用
                    var result = dt.Select("ParentId='" + Convert.ToInt32(tn.Tag) + "' and Id = '" + _pid + "'");
                    //(重) 当没有记录时跳出当前循环;注:当跳出后,返回上一级节点继续执行循环
                    if (result.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        //(重)递归调用
                        //可理解为:暂存,在递归时,将之前的记录暂时存放在一个位置,当跳出递归时,才解放暂存记录
                        GetChildNode(dt, tn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 树菜单节点跳转时使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                var result = JumpNextGridViewdtl();
                if (!result) throw new Exception("发生异常,请联系管理员");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取材料信息明细记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetdtl_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvView.SelectedNode == null) throw new Exception("没有选择任何节点,请选择再继续");
                if ((string)tvView.SelectedNode.Text == "ALL") throw new Exception("不能选择ALL节点,请另选其它再继续");

                //获取所选中的树菜单节点ID
                var treeid = (int)tvView.SelectedNode.Tag;

                typeInfoFrm.Funname = "Material";
                typeInfoFrm.Id = treeid;
                //初始化记录
                typeInfoFrm.OnInitialize();
                typeInfoFrm.StartPosition = FormStartPosition.CenterScreen;
                typeInfoFrm.ShowDialog();

                //判断若返回的DT为空的话,就不需要任何效果
                if (typeInfoFrm.ResultTable == null || typeInfoFrm.ResultTable.Rows.Count == 0) return;
                //将返回的结果赋值至GridView内(注:判断若返回的DT不为空或行数大于0才执行插入效果)
                if (typeInfoFrm.ResultTable != null || typeInfoFrm.ResultTable.Rows.Count > 0)
                    InsertdtToGridView(_pid, treeid, typeInfoFrm.ResultTable);
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
            if (gvdtl.Rows.Count == 0) throw new Exception("没有任何记录,不能保存");
            //执行保存功能
            Savedtl();
            //保存成功后,再次进行初始化
            OnInitialize();
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
                if (gvdtl.Rows.Count == 0) throw new Exception("没有内容,不能审核.");
                //注:需在“审核”前将还没有进行“保存”的记录进行“保存”才能继续审核

                //获取在GridView存在的新记录
                var newRecorddt = SearchNewRecordIntoDt();
                //获取在GridView存在的更新记录
                var updateRecorddt = SearchUpdateRecordIntoDt();
                if (newRecorddt.Rows.Count > 0 || updateRecorddt.Rows.Count > 0) throw new Exception("检测到有记录没有保存,请先将记录保存再继续进行审核");

                var clickMessage = $"您所选择的信息为:\n 单据名称:{txtOrderNo.Text} \n 是否继续? \n 注:审核后需反审核才能对该单据的记录进行修改, \n 请谨慎处理.";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    task.TaskId = 2;
                    task.FunctionId = "4";
                    task.FunctionName = _funName;
                    task.Pid = _pid;      //表头ID
                    task.Confirmid = 0;  //记录审核操作标记 0:审核 1:反审核

                    task.StartTask();

                    if (!task.ResultMark) throw new Exception("审核异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("审核成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    tmConfirm.Enabled = false;
                    //审核完成后“刷新”(注:审核成功的单据,经刷新后为不可修改效果)
                    OnInitialize();
                }
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
        private void TmExcel_Click(object sender, System.EventArgs e)
        {
            try
            {
                if(gvdtl.SelectedRows.Count==0)throw new Exception("请选择一行进行导出.");

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
        private void TmPrint_Click(object sender, System.EventArgs e)
        {
            try
            {
                if(gvdtl.SelectedRows.Count==0)throw new Exception("请选择一行进行打印.");

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
        private void Gvdtl_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {
                    decimal renCost = 0;  //人工费用
                    decimal fuCost = 0;  //辅材费用
                    decimal price = 0;  //单价

                    //计算“综合单价”=人工费用(8)+辅材费用(9)+(单价(10) 或 临时价(11))

                    //人工费用
                    renCost = Convert.ToString(gvdtl.Rows[e.RowIndex].Cells[8].Value) == "" ? 0 : Convert.ToDecimal(gvdtl.Rows[e.RowIndex].Cells[8].Value);
                    //辅材费用
                    fuCost = Convert.ToString(gvdtl.Rows[e.RowIndex].Cells[9].Value) == "" ? 0 : Convert.ToDecimal(gvdtl.Rows[e.RowIndex].Cells[9].Value);
                    //单价(注:若临时价有值。就取临时价。反之用单价;若两者都没有的话,就为0)
                    if (Convert.ToString(gvdtl.Rows[e.RowIndex].Cells[11].Value) != "")
                    {
                        price = Convert.ToDecimal(gvdtl.Rows[e.RowIndex].Cells[11].Value);
                    }
                    else if (Convert.ToString(gvdtl.Rows[e.RowIndex].Cells[10].Value) != "")
                    {
                        price = Convert.ToDecimal(gvdtl.Rows[e.RowIndex].Cells[10].Value);
                    }
                    else
                    {
                        price = 0;
                    }
                    gvdtl.Rows[e.RowIndex].Cells[7].Value = renCost + fuCost + price;
                }
                else if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    decimal quantities = 0;  //工程量
                    decimal finalPrice = 0; //综合单价

                    //计算“合计”=工程量(6) * 综合单价(7)

                    //工程量
                    quantities = Convert.ToString(gvdtl.Rows[e.RowIndex].Cells[6].Value) == "" ? 0 : Convert.ToDecimal(gvdtl.Rows[e.RowIndex].Cells[6].Value);
                    //综合单价
                    finalPrice = Convert.ToString(gvdtl.Rows[e.RowIndex].Cells[7].Value) == "" ? 0 : Convert.ToDecimal(gvdtl.Rows[e.RowIndex].Cells[7].Value);

                    gvdtl.Rows[e.RowIndex].Cells[12].Value = quantities * finalPrice;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        /// 控制GridView单元格显示方式
        /// </summary>
        private void ControlGridViewisShow()
        {
            //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            gvdtl.Columns[2].Visible = false;
            gvdtl.Columns[3].Visible = false;
            //设置指定列不能编辑
            gvdtl.Columns[4].ReadOnly = true;   //材料名称
            gvdtl.Columns[5].ReadOnly = true;   //单价名称
            gvdtl.Columns[7].ReadOnly = true;   //综合单价
            gvdtl.Columns[10].ReadOnly = true;  //单价
            gvdtl.Columns[12].ReadOnly = true;  //合计
            gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true; //录入人
            gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true; //录入日期
        }

        /// <summary>
        /// 将获取的DT内容插入至GridView内
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="treeid">树菜单ID</param>
        /// <param name="sourcedt">明细窗体获取的DT</param>
        private void InsertdtToGridView(int id, int treeid,DataTable sourcedt)
        {
            //将GridView内的内容赋值到DT
            var dt = (DataTable)gvdtl.DataSource;
            //循环sourcedt内的内容,目的:将刚从明细窗体内获取的值插入到GridView内
            foreach (DataRow sourcerow in sourcedt.Rows)
            {
                var row = dt.NewRow();
                row[0] = id;                                        //表头ID
                row[1] = treeid;                                   //树菜单ID
                row[3] = Convert.ToInt32(sourcerow[1]);           //材料ID
                row[4] = Convert.ToString(sourcerow[2]);         //材料名称
                row[5] = Convert.ToString(sourcerow[6]);        //单位名称
                row[10] = Convert.ToDecimal(sourcerow[7]);     //单价
                row[14] = GlobalClasscs.User.StrUsrName;      //录入人
                row[15] = DateTime.Now.Date;                 //录入日期
                dt.Rows.Add(row);
            }
            gvdtl.DataSource = dt;
        }

        /// <summary>
        /// 删除GridView中所选择的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmdel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("请选择某一行进行删除");
                if(!_candelMarkid) throw new Exception($"用户'{GlobalClasscs.User.StrUsrName}'没有‘删除’权限,不能继续.");

                var clickMessage = $"您所选择需删除的行数为:{gvdtl.SelectedRows.Count}行 \n 是否继续?";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //注:执行方式 判断若所选择的行内的 adornid项 有值，就执行下面第一步,若没有;只需将在GridView内的行删除就行
                    //1)将所选择的行保存到_deldt内(在按“保存”时执行对该行的数据库删除)

                    //创建对应临时表
                    var tempdt = dtList.Get_ProMaterialEmtrydt();
                    //将所选择的记录赋值至tempdt临时表内
                    foreach (DataGridViewRow row in gvdtl.SelectedRows)
                    {
                        //若列adornid不为空时,才进行记录
                        if (row.Cells[2].Value.ToString() != "")
                        {
                            var row1 = tempdt.NewRow();
                            for (var i = 0; i < tempdt.Columns.Count; i++)
                            {
                                row1[i] = row.Cells[i].Value;
                            }
                            tempdt.Rows.Add(row1);
                        }
                    }
                    //若tempdt有值才赋值至_dtldt内,(供保存使用)
                    if (tempdt.Rows.Count > 0)
                    {
                        _deldt = tempdt;
                    }
                    //最后使用循环将所选择的行在GridView内删除
                    for (var i = gvdtl.SelectedRows.Count; i > 0; i--)
                    {
                        gvdtl.Rows.RemoveAt(gvdtl.SelectedRows[i - 1].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存GridView记录(作用:在按“保存”按钮时,或转移树菜单节点 或 重新选择下拉框时使用)
        /// </summary>
        private void Savedtl()
        {
            task.TaskId = 2;
            task.FunctionId = "2.2";
            task.FunctionName = _funName;                      //功能名称 (AdornOrder:室内装修工程 MaterialOrder:室内主材单)
            task.Data = (DataTable)gvdtl.DataSource;          //获取GridView内的DataTable
            task.Deldata = _deldt;                           //要进行删除的记录

            new Thread(Start).Start();
            load.StartPosition = FormStartPosition.CenterScreen;
            load.ShowDialog();

            if (!task.ResultMark) throw new Exception("保存异常,请联系管理员");
            else
            {
                MessageBox.Show("保存成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 查询GridView内的新记录,并保存至DT内并返回
        /// </summary>
        /// <returns></returns>
        private DataTable SearchNewRecordIntoDt()
        {
            //创建对应临时表
            var tempdt = dtList.Get_AdornEmptydt();
            var dt = (DataTable)gvdtl.DataSource;
            //将所选择的记录赋值至tempdt临时表内
            foreach (DataRow row in dt.Rows)
            {
                //若列adornid不为空时,才进行记录
                if (Convert.ToString(row[2]) == "")
                {
                    var rowdtl = tempdt.NewRow();
                    for (var i = 0; i < tempdt.Columns.Count; i++)
                    {
                        rowdtl[i] = row[i];
                    }
                    tempdt.Rows.Add(rowdtl);
                }
            }
            return tempdt;
        }

        /// <summary>
        /// 查询GridView内的更新记录,并保存至DT内并返回
        /// </summary>
        /// <returns></returns>
        private DataTable SearchUpdateRecordIntoDt()
        {
            //创建对应临时表
            var tempdt = dtList.Get_AdornEmptydt();
            var dt = (DataTable)gvdtl.DataSource;
            //将所选择的记录赋值至tempdt临时表内
            foreach (DataRow row in dt.Rows)
            {
                //若列adornid不为空时,才进行记录
                if (row.RowState.ToString() == "Modified")
                {
                    var rowdtl = tempdt.NewRow();
                    for (var i = 0; i < tempdt.Columns.Count; i++)
                    {
                        rowdtl[i] = row[i];
                    }
                    tempdt.Rows.Add(rowdtl);
                }
            }
            return tempdt;
        }

        /// <summary>
        /// 跳转至下一个GridView内容时使用(注:下拉列表 以及 树菜单节点改变时使用)
        /// </summary>
        /// <returns></returns>
        private bool JumpNextGridViewdtl()
        {
            var result = true;
            try
            {
                //在选择其它下拉列表值时,若GridView内有新值,即提示是否保存，若是，即保存完成后进入要移换的下拉框的GridView页面
                //注:当单据状态标记为R(读取)时才执行,因为C(创建)的时候,树菜单还没有创建任何节点,故没有任何可获取的树菜单ID

                if (_funState == "R")
                {
                    if (tvView.SelectedNode == null) throw new Exception("没有选择任何节点,请选择");

                    var gridViewdt = (DataTable)gvdtl.DataSource;
                    if (gridViewdt.Rows.Count > 0)
                    {
                        //获取在GridView存在的新记录
                        var newRecorddt = SearchNewRecordIntoDt();
                        //获取在GridView存在的更新记录
                        var updateRecorddt = SearchUpdateRecordIntoDt();

                        //若返回的DT行数大于O，就作出提示
                        if (newRecorddt.Rows.Count > 0 || updateRecorddt.Rows.Count > 0)
                        {
                            var clickMessage = $"系统检测到该页面有变动的记录(包括新建或修改的记录) \n 是否需要保存? \n 注:若不保存而转移至下一页,将会导致新增(修改)的记录清空 \n 请谨慎处理.";
                            if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                //保存GridView信息
                                Savedtl();
                            }
                        }
                    }
                    //获取下拉列表ID信息并跳转至对应下拉列表的GridView页(注:无论是否需要保存都会执行)
                    task.TaskId = 2;
                    task.FunctionId = "1.4";
                    task.FunctionName = _funName;                                                                                 //功能名称
                    task.Pid = _pid;                                                                                             //表头ID
                    task.Treeid = (string)tvView.SelectedNode.Text == "ALL" ? -1 : Convert.ToInt32(tvView.SelectedNode.Tag);    //树节点ID

                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    //连接GridView页面跳转功能
                    LinkGridViewPageChange(task.ResultTable);
                    //gvdtl.DataSource = task.ResultTable;
                    //设置GridView是否显示某些列
                    ControlGridViewisShow();
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 权限控制 
        /// </summary>
        private void PrivilegeControl()
        {
            //若为“审核”状态的话，就执行以下语句
            if (_confirmMarkId == "Y")
            {
                panel9.BackgroundImage = Image.FromFile(Application.StartupPath + @"\PIC\1.png");

                #region

                //设置整体Form都不能修改
                //foreach (Control control in this.Controls)
                //{
                //    //遍历后的操作...
                //    control.Enabled = false;
                //}

                #endregion

                //注:审核后只能查阅，打印;不能保存 审核 修改，除非反审核
                tmSave.Enabled = false;
                tmConfirm.Enabled = false;
                gvdtl.Enabled = false;
                btnGetdtl.Enabled = false;
            }
            //若为“非审核”状态的,就执行以下语句
            else
            {
                //将所有功能的状态还原(即与审核时的控件状态相反)
                tmSave.Enabled = true;
                tmConfirm.Enabled = true;
                gvdtl.Enabled = true;
                btnGetdtl.Enabled = true;
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
        /// 连接GridView页面跳转功能
        /// </summary>
        /// <param name="dt"></param>
        private void LinkGridViewPageChange(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                _dtl = dt;
                panel4.Visible = true;
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
                panel4.Visible = false;
            }
        }

    }
}
