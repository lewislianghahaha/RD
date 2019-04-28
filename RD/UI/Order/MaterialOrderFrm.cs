using System;
using System.Data;
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
        AdornTypeFrm adornType = new AdornTypeFrm();
        TypeInfoFrm typeInfoFrm = new TypeInfoFrm();
        DtList dtList = new DtList();

        //保存GridView内需要进行删除的临时表
        private DataTable _deldt = new DataTable();
        //保存树型菜单DataTable记录
        //private DataTable _treeviewdt = new DataTable();

        //单据状态标记(作用:记录打开此功能窗体时是 创建记录 还是 读取记录) C:创建 R:读取
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
            //对GridView赋值(将对应功能点的表体全部信息赋值给GV控件内)
            gvdtl.DataSource = OnInitializeDtl();
            //设置GridView是否显示某些列
            ControlGridViewisShow();
            //展开根节点
            tvView.ExpandAll();
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
                    tn.Tag = Convert.ToInt32(t[0]);
                    tn.Text = Convert.ToString(t[2]);
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
            
        }

        /// <summary>
        /// 获取材料信息明细记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetdtl_Click(object sender, System.EventArgs e)
        {
            
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, System.EventArgs e)
        {
            
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmConfirm_Click(object sender, System.EventArgs e)
        {
            
        }

        /// <summary>
        /// 导出-EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmExcel_Click(object sender, System.EventArgs e)
        {
            
        }

        /// <summary>
        /// 导出-打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmPrint_Click(object sender, System.EventArgs e)
        {
            
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



    }
}
