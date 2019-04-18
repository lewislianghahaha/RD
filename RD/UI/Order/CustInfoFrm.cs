using System;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class CustInfoFrm : Form
    {
        TaskLogic task = new TaskLogic();
        AdornOrderFrm adornOrder=new AdornOrderFrm();
        MaterialOrderFrm materialOrder=new MaterialOrderFrm();

        //记录功能名称 Adorn:室内装修工程 Material:室内主材单
        private string _funName;
        //单据状态标记(作用:记录创建记录) C:创建
        private string _funState;

        #region Set
            
            /// <summary>
            /// 获取单据状态标记ID C:创建 R:读取
            /// </summary>
            public string FunState { set { _funState = value; } }
            /// <summary>
            /// 记录功能名称 Adorn:室内装修工程 Material:室内主材单
            /// </summary>
            public string FunName { set { _funName = value; } }

        #endregion

        public CustInfoFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tmGet.Click += TmGet_Click;
            tmClose.Click += TmClose_Click;
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        public void OnInitialize()
        {
            //初始化 获取基础信息库-客户信息管理
            task.TaskId = 1;
            task.FunctionId = "1";
            task.FunctionName = "Customer";
            task.FunctionType = "G";
            task.ParentId = null;

            task.StartTask();
            gvdtl.DataSource = task.ResultTable;
            //设置GridView是否显示某些列
            ControlGridViewisShow();
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
                //客户名称
                var customerName = string.Empty;
                //客户主键ID
                var customerid = 0;
                //记录功能名称 Adorn:室内装修工程 Material:室内主材单
                var frmName = string.Empty;
                //主键ID(记录生成后的表头ID 即:T_PRO_Adorn 或 T_PRO_Material)
                var id = 0;

                if (gvdtl.SelectedRows.Count==0) throw new Exception("请选取某一行,然后再按此按钮");

                frmName = _funName == "Adorn" ? "室内装修工程" : "室内主材";
                //获取当前行的客户名称 及 客户ID信息
                customerid= Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value);
                customerName = Convert.ToString(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[2].Value);

                var clickMessage = $"您所选择的信息为:\n 客户名称:{customerName} \n 是否继续? \n 注:选定后,即生成对应的'{frmName}'单据信息, \n 并跳转至该窗体. \n 请谨慎处理.";

                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //根据客户信息插入至对应的 室内装修工程表 或 室内主材单表 表头内
                    task.TaskId = 2;
                    task.FunctionId = "2.3";
                    task.FunctionName = _funName;
                    task.Custid = customerid;

                    task.StartTask();
                    //返回新插入的表头ID;即T_PRO_Adorn 或 T_PRO_Material表信息
                    id = task.Orderid;                      

                    if (id == 0) throw new Exception("生成异常,请联系管理员.");
                    else
                    {
                        //当插入完成后,转移到 室内装修工程单 或 室内主材单 窗体内,并将此窗体关闭
                        MessageBox.Show($"新增{frmName}单据成功,请点击继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //室内装修工程单
                        if (_funName == "Adorn")
                        {
                            //关闭此窗体
                            this.Close();
                            adornOrder.FunState = _funState;
                            adornOrder.Pid = id;            //单据主键id

                            adornOrder.OnInitialize();
                            adornOrder.StartPosition = FormStartPosition.CenterParent;
                            adornOrder.ShowDialog();
                        }
                        //室内主材单
                        else
                        {
                            //关闭此窗体
                            this.Close();

                            materialOrder.StartPosition=FormStartPosition.CenterParent;
                            materialOrder.ShowDialog();
                        }
                    }
                }
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
           this.Close();
        }

        /// <summary>
        /// 设置GridView是否显示某些列
        /// </summary>
        private void ControlGridViewisShow()
        {
            //将GridView中的第一列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            gvdtl.Columns[3].Visible = false;
        }
    }
}
