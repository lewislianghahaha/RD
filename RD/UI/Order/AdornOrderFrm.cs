using System;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class AdornOrderFrm : Form
    {
        TaskLogic task = new TaskLogic();


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

        }

        /// <summary>
        /// 装修类别下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComHtype_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmConfirm_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 导出-Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmExcel_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 导出-打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmPrint_Click(object sender, EventArgs e)
        {
            
        }
    }
}
