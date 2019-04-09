using System;
using System.Windows.Forms;

namespace RD.UI.Order
{
    public partial class AdornTypeFrm : Form
    {


        private bool _resultMark;     //返回结果信息

        #region Set

        #endregion

        #region Get
        /// <summary>
        /// 返回结果标记
        /// </summary>
        public bool ResultMark => _resultMark;
        #endregion

        public AdornTypeFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tmSet.Click += TmSet_Click;
            tmClose.Click += TmClose_Click;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSet_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtValue.Text=="")throw new Exception("节点名称不能为空,请填上后再按保存");
                
                this.Close();
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
    }
}
