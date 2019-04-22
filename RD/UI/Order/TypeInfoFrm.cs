using System;
using System.Data;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class TypeInfoFrm : Form
    {

        TaskLogic taskLogic=new TaskLogic();

        //获取ID信息
        private int _id;
        //获取功能名称:Material 材料 HouseType:装修工程类别
        private string _funname;


        private DataTable _resultTable;  //返回DT类型

        #region Set

        /// <summary>
        /// 获取ID信息
        /// </summary>
        public int Id { set { _id = value; } }
        /// <summary>
        /// 获取功能名称
        /// </summary>
        public string Funname { set { _funname = value; } }

        #endregion

        #region Get

        /// <summary>
        /// 返回DT
        /// </summary>
        public DataTable ResultTable { set { _resultTable = value; }}

        #endregion

        public TypeInfoFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitialize();
        }

        private void OnRegisterEvents()
        {
            tmGet.Click += TmGet_Click;
            tmClose.Click += TmClose_Click;
            comlist.Click += Comlist_Click;
            btnSearch.Click += BtnSearch_Click;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInitialize()
        {
            //初始化对应的装修工程类别明细记录
            taskLogic.TaskId = 1;

            switch (_funname)
            {
                case "HouseType":
                    this.Text = "装修工程类别";
                    break;
                case "Material":
                    this.Text = "材料明细";
                    break;
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGet_Click(object sender, System.EventArgs e)
        {
            try
            {
                if(gvdtl.Rows.Count==0) throw new Exception("请至少选中一行");

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
        private void TmClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择条件下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comlist_Click(object sender, System.EventArgs e)
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
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
