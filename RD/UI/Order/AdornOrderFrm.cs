using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class AdornOrderFrm : Form
    {
        TaskLogic task = new TaskLogic();
        Load load=new Load();
        AdornTypeFrm adornType=new AdornTypeFrm();
        TypeInfoFrm typeInfoFrm=new TypeInfoFrm();
        DtList dtList = new DtList();

        //保存GridView内需要进行删除的临时表
        private DataTable _deldt=new DataTable();
        //保存树型菜单DataTable记录
        private DataTable _treeviewdt=new DataTable();
        //单据状态标记(作用:记录打开此功能窗体时是 读取记录 还是 创建记录) C:创建 R:读取
        private string _funState;
        //获取表头ID
        private int _pid;
        //记录功能名称 AdornOrder:室内装修工程 MaterialOrder:室内主材单
        private string _funName;

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

        #endregion

        public AdornOrderFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            comHtype.Click += ComHtype_Click;
            tmSave.Click += TmSave_Click;
            tmConfirm.Click += TmConfirm_Click;
            tmExcel.Click += TmExcel_Click;
            tmPrint.Click += TmPrint_Click;
            btnCreate.Click += BtnCreate_Click;
            btnChange.Click += BtnChange_Click;
            btnDel.Click += BtnDel_Click;
            tvview.AfterSelect += Tvview_AfterSelect;
            btnGetdtl.Click += BtnGetdtl_Click;
            tmdel.Click += Tmdel_Click;
            gvdtl.CellValueChanged += Gvdtl_CellValueChanged;
        }

        /// <summary>
        /// 初始化(注:根据功能状态标记来控制)
        /// </summary>
        public void OnInitialize()
        {
            //清空树菜单信息
            tvview.Nodes.Clear();
            //根据功能名称 及 表头ID读取表头相关信息(包括单据编号等)
            ShowHead(_funName,_pid);

            //单据状态:创建 C
            if (_funState == "C")
            {
                //设置树菜单表头信息(只需显示ALL字段)
                ShowTreeList(_funState,null);
                //对GridView赋值(输出空白表)
                gvdtl.DataSource = OnInitializeDtl();
            }
            //单据状态:读取 R （读取顺序:树型菜单->表体内容）
            else
            {
                task.TaskId = 2;
                task.FunctionId = "1.1";
                task.Pid = _pid;

                task.StartTask();
                //将树菜单的DT保存（后面删除功能有用）
                _treeviewdt = task.ResultTable;
                //读取设置树菜单表头信息
                ShowTreeList(_funState, task.ResultTable);
                //对GridView赋值(将对应功能点的表体全部信息赋值给GV控件内)
                gvdtl.DataSource = OnInitializeDtl();
            }
            //初始化装修工程类别下拉列表
            OnInitializeDropDownList();
            //设置GridView是否显示某些列
            ControlGridViewisShow();
            //展开根节点
            tvview.ExpandAll();
            //预留(权限部份)

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
        /// 初始化下拉列表
        /// </summary>
        private void OnInitializeDropDownList()
        {
            task.TaskId = 2;
            task.FunctionId = "1";
            task.StartTask();
            comHtype.DataSource = task.ResultTable;
            comHtype.DisplayMember = "HtypeName";     //设置显示值 HTypeid
            comHtype.ValueMember = "HTypeid";       //设置默认值内码(即:列名)
        }

        /// <summary>
        /// 根据功能名称 及 表头ID读取表头相关信息(包括单据编号等)
        /// </summary>
        /// <param name="funname"></param>
        /// <param name="pid"></param>
        private void ShowHead(string funname,int pid)
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
        }

        /// <summary>
        /// 根据获取的DT读取树形列表
        /// </summary>
        /// <param name="funState">单据状态</param>
        /// <param name="dt">树型菜单Datatable</param>
        private void ShowTreeList(string funState,DataTable dt)
        {
            //当单据状态为"创建"时使用
            if (funState == "C" || dt.Rows.Count == 0)
            {
                var tree = new TreeNode();
                tree.Tag = 1;
                tree.Text = "ALL";
                tvview.Nodes.Add(tree);
            }
            //当单据状态为"读取"及dt有值时使用
            else
            {
                var tnode = new TreeNode();
                tnode.Tag = 1;
                tnode.Text = "ALL";
                tvview.Nodes.Add(tnode);
                //展开根节点
                tvview.ExpandAll();
                //读取记录并增加至子节点内(从二级节点开始)
                AddChildNode(tvview,dt);
            }
        }

        /// <summary>
        /// 读取记录并增加至子节点内(从二级节点开始)
        /// </summary>
        /// <returns></returns>
        private void AddChildNode(TreeView tvView, DataTable dt)
        {
            var tnode = tvView.Nodes[0];
            var rows = dt.Select("ParentId='" + Convert.ToInt32(tnode.Tag) + "' and Id = '" + _pid + "'");
            //循环子节点信息(从二级节点开始)
            foreach (var r in rows)
            {
                var tn = new TreeNode();
                tn.Tag = Convert.ToInt32(r[1]);        //自身主键ID
                tn.Text = Convert.ToString(r[3]);     //节点内容
                //将二级节点添加至根节点下
                tnode.Nodes.Add(tn);
            }
        }

        /// <summary>
        /// 装修类别下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComHtype_Click(object sender, EventArgs e)
        {
            try
            {
                //在选择其它下拉列表值时,若GridView内有新值,即提示是否保存，若是，即保存完成后进入要移换的下拉框的GridView页面

                //获取下拉列表信息
                var dvColIdlist = (DataRowView)comHtype.Items[comHtype.SelectedIndex];
                var hTypeid = Convert.ToInt32(dvColIdlist["HTypeid"]);

                //获取在GridView存在的新记录
                var dt = SearchNewRecordIntoDt();
                //若返回的DT行数大于O，就作出提示
                if (dt.Rows.Count>0)
                {
                    var clickMessage = $@"系统检测到该页面有没保存的记录'{dt.Rows.Count}'行 \n 是否需要保存? 
                                        \n 注:若不保存而转移至下一页,将会导致新增的记录清空 \n 请谨慎处理.";
                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //保存GridView信息
                        Savedtl();
                    }
                }
                //获取下拉列表ID信息并跳转至对应下拉列表的GridView页(注:无论是否需要保存都会执行)
                task.TaskId = 2;
                task.FunctionId = "";

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                gvdtl.DataSource = task.ResultTable;
                //设置GridView是否显示某些列
                ControlGridViewisShow();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新建类别(注:不能在除ALL节点下创建新节点)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvview.SelectedNode == null) throw new Exception("没有选择任何节点,请选择");
                //判断若新增节点不在ALL节点下,就显示异常
                if((string)tvview.SelectedNode.Text!="ALL") throw new Exception("请在'ALL'节点下新建新类别");

                adornType.Pid= (int)tvview.SelectedNode.Tag;                                   //上级节点ID
                adornType.Funid = "C";                                                        //设置功能标识ID
                adornType.Id = _pid;                                                         //获取上级表头ID

                adornType.OnInitialize();
                adornType.StartPosition = FormStartPosition.CenterScreen;
                adornType.ShowDialog();
                //当成功新增后,将状态标记更新为R（读取）并执行"刷新"操作 
                if (adornType.ResultMark)
                    _funState = "R";
                    OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvview.SelectedNode == null) throw new Exception("没有选择父节点,请选择");
                if((string)tvview.SelectedNode.Text=="ALL") throw new Exception("'ALL'节点不能修改,请选择其它节点.");
                adornType.Pid = (int)tvview.SelectedNode.Tag;                             //获取所选择的主键ID
                adornType.Funid = "E";                                                    //设置功能标识ID

                adornType.OnInitialize();
                adornType.StartPosition = FormStartPosition.CenterScreen;
                adornType.ShowDialog();
                //当成功新增后,将状态标记更新为R（读取）并执行"刷新"操作 
                if (adornType.ResultMark)
                    _funState = "R";
                    OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvview.SelectedNode == null) throw new Exception("没有选择父节点,请选择");
                if ((string)tvview.SelectedNode.Text == "ALL") throw new Exception("'ALL'节点不能删除,请选择其它节点进行删除");

                //节点ID
                var treeid = Convert.ToInt32(tvview.SelectedNode.Tag);
                //节点名称
                var treeName = tvview.SelectedNode.Text;

                var clickMessage = $"您所选择的信息为:\n 节点名称:{treeName} \n 是否继续? \n 注:若点选的节点下有内容的话(包括明细内容),就会将与它对应的记录都删除, \n 请谨慎处理.";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    task.TaskId = 2;
                    task.FunctionId = "3";
                    task.FunctionName = _funName;
                    task.Pid = treeid;
                    task.Data = _treeviewdt;

                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    if (!task.ResultMark) throw new Exception("删除异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("删除成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //删除完成后“刷新”
                    OnInitialize();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存(作用:主要对GridView控件进行数据导入)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvdtl.Rows.Count==0) throw new Exception("没有任何记录,不能保存");
                //if ((int)comHtype.SelectedIndex == -1) throw new Exception("请选择装修工程类别.");
                //执行保存功能
                Savedtl();
                //保存成功后,再次进行初始化
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取工程类别明细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetdtl_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvview.SelectedNode == null) throw new Exception("没有选择任何节点,请选择再继续");
                if ((string)tvview.SelectedNode.Text == "ALL") throw new Exception("不能选择ALL节点,请另选其它再继续");
                if ((int)comHtype.SelectedIndex == -1) throw new Exception("请选择装修工程类别");

                //获取下拉列表值
                var dvColIdlist = (DataRowView)comHtype.Items[comHtype.SelectedIndex];
                var hTypeid = Convert.ToInt32(dvColIdlist["HTypeid"]);
                //获取所选中的树菜单节点ID
                var treeid = (int) tvview.SelectedNode.Tag;

                typeInfoFrm.Funname = "HouseProject";
                typeInfoFrm.Id = hTypeid;
                //初始化记录
                typeInfoFrm.OnInitialize();
                typeInfoFrm.StartPosition = FormStartPosition.CenterScreen;
                typeInfoFrm.ShowDialog();

                //返回获取的行记录至GridView内
                if(typeInfoFrm.ResultTable.Rows.Count==0) throw new Exception("没有行记录,请重新选择");
                //将返回的结果赋值至GridView内
                InsertdtToGridView(_pid,treeid,hTypeid, typeInfoFrm.ResultTable);
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
                var clickMessage = $"您所选择的信息为:\n 单据名称:{txtOrderNo.Text} \n 是否继续? \n 注:审核后需反审核才能对该单据的记录进行修改, \n 请谨慎处理.";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    task.TaskId = 2;
                    task.FunctionId = "4";
                    task.FunctionName = _funName;
                    task.Id = _pid;      //表头ID
                    task.Confirmid = 0; //记录审核操作标记 0:审核 1:反审核

                    task.StartTask();

                    if (!task.ResultMark) throw new Exception("审核异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("审核成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
        /// 导出-Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmExcel_Click(object sender, EventArgs e)
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
        /// 导出-打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmPrint_Click(object sender, EventArgs e)
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
        /// 点击树节点（在点击中节点后才执行此事件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tvview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //判断若GridView有内容时,就提示是否“保存” 先保存再继续
                //检测在GridView是否有还没有保存的记录(注:以adornid字段为空为条件)
                if (gvdtl.Rows.Count > 0)
                {
                    var clickMessage = $"检测到有还没保存的记录. \n 是否继续?";
                    if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        
                    }
                }
                else
                {
                    
                }

                task.TaskId = 2;
                task.FunctionId = "1.4";
                task.FunctionName = _funName;
                task.Id = _pid;  //表头ID
                task.Pid = (int)tvview.SelectedNode.Tag == 1 ? -1 : Convert.ToInt32(tvview.SelectedNode.Tag);  //树节点ID
                //(int)comHtype.SelectedIndex  增加下拉框ID

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                gvdtl.DataSource = task.ResultTable;
                //设置GridView是否显示某些列
                ControlGridViewisShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            gvdtl.Columns[4].ReadOnly = true;   //工程类别-项目名称
            gvdtl.Columns[5].ReadOnly = true;   //单位名称
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
        /// <param name="hTypeid">工程类别ID</param>
        /// <param name="sourcedt">明细窗体获取的DT</param>
        private void InsertdtToGridView(int id,int treeid,int hTypeid, DataTable sourcedt)
        {
            //将GridView内的内容赋值到DT
            var dt = (DataTable) gvdtl.DataSource;
            //循环sourcedt内的内容,目的:将刚从明细窗体内获取的值插入到GridView内
            foreach (DataRow sourcerow in sourcedt.Rows)
            {
                var row = dt.NewRow();
                row[0] = id;                                        //表头ID
                row[1] = treeid;                                   //树菜单ID
                row[3] = hTypeid;                                 //工程类别ID
                row[4] = Convert.ToString(sourcerow[2]);         //工程类别-项目名称
                row[5] = Convert.ToString(sourcerow[3]);        //单位名称
                row[10] = Convert.ToDecimal(sourcerow[4]);     //单价
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
                if(gvdtl.SelectedRows.Count==0) throw new Exception("请选择某一行进行删除");
                
                var clickMessage = $"您所选择需删除的行数为:{gvdtl.SelectedRows.Count}行 \n 是否继续?";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //注:执行方式 判断若所选择的行内的 adornid项 有值，就执行下面第一步,若没有;只需将在GridView内的行删除就行
                    //1)将所选择的行保存到_deldt内(在按“保存”时执行对该行的数据库删除)
                    
                    //创建对应临时表
                    var tempdt = dtList.Get_AdornEmptydt();
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
                    decimal fuCost = 0; //辅材费用
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
        
        /// <summary>
        /// 保存GridView记录(作用:在按“保存”按钮时,或转移树菜单节点 或 重新选择下拉框时使用)
        /// </summary>
        private void Savedtl()
        {
            task.TaskId = 2;
            task.FunctionId = "2.2";
            task.FunctionName = _funName;                      //功能名称 (AdornOrder:室内装修工程 MaterialOrder:室内主材单)
            task.Data = (DataTable) gvdtl.DataSource;         //获取GridView内的DataTable
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
            var dt = (DataTable) gvdtl.DataSource;
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
    }
}
