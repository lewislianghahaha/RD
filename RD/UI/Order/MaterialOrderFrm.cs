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
        private DataTable _treeviewdt = new DataTable();
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
                //设置树菜单表头信息

                //对GridView赋值(输出空白表)
                gvdtl.DataSource = OnInitializeDtl();
            }
            //单据状态:读取 R （读取顺序:树型菜单->表体内容）
            else
            {
                //将材料信息管理的大类信息插入到这

                //对GridView赋值(将对应功能点的表体全部信息赋值给GV控件内)
                gvdtl.DataSource = OnInitializeDtl();
            }
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
        /// 树菜单节点跳转时使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        /// <summary>
        /// 
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
