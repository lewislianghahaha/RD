using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class AdornOrderFrm : Form
    {
        TaskLogic task = new TaskLogic();
        Load load=new Load();
        AdornTypeFrm adornType=new AdornTypeFrm();

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
            var rows = dt.Select("ParentId='" + Convert.ToInt32(tnode.Tag) + "'");
            //循环子节点信息(从二级节点开始)
            foreach (var r in rows)
            {
                var tn = new TreeNode();
                tn.Tag = Convert.ToInt32(r[0]);        //自身主键ID
                tn.Text = Convert.ToString(r[2]);     //节点内容
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
                task.TaskId = 2;
                task.FunctionId = "1";
                task.StartTask();
                comHtype.DataSource = task.ResultTable;
                comHtype.DisplayMember = "HtypeName";     //设置显示值 HTypeid
                comHtype.ValueMember = "HTypeid";       //设置默认值内码(即:列名)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新建类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvview.SelectedNode == null) throw new Exception("没有选择父节点,请选择");
                //判断若新增节点不在ALL节点下,就显示异常
                if((int)tvview.SelectedNode.Tag>1)
                    if ((int)tvview.SelectedNode.Tag-1 !=1) throw new Exception("请在'ALL'节点下新建新类别");

                adornType.Pid= (int)tvview.SelectedNode.Tag;                                   //上级主键ID
                adornType.Funid = "C";                                                        //设置功能标识ID
                adornType.Id = _pid;                                                         //获取上上级表头ID

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
                if((int)tvview.SelectedNode.Tag==1) throw new Exception("'ALL'节点不能修改,请选择其它节点.");
                adornType.Pid = (int)tvview.SelectedNode.Tag;                             //获取所选择的主键ID
                adornType.Funid = "E";                                                    //设置功能标识ID

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
                if ((int)tvview.SelectedNode.Tag == 1) throw new Exception("ALL节点不能删除,请选择其它节点进行删除");

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
            //将GridView中的第一二列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            gvdtl.Columns[2].Visible = false;
            gvdtl.Columns[3].Visible = false;
            //设置指定列不能编辑
            gvdtl.Columns[7].ReadOnly = true;
            gvdtl.Columns[12].ReadOnly = true;
            gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
            gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;
        }
    }
}
