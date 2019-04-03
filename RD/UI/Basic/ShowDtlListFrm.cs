using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class ShowDtlListFrm : Form
    {
        TaskLogic task = new TaskLogic();

        private int _funid;           //功能ID 0：房屋类型明细 1:材料供应商明细



        private bool _resultMark;     //返回结果信息

        #region Set

        /// <summary>
        /// 记录功能ID 0：房屋类型明细 1:材料供应商明细
        /// </summary>
        public int Funid { set { _funid = value; } }

        #endregion

        #region Get
        /// <summary>
        /// 返回结果标记
        /// </summary>
        public bool ResultMark => _resultMark;
        #endregion

        public ShowDtlListFrm()
        {
            InitializeComponent();
            OnInitialize();
        }

        void OnInitialize()
        {
            tmGet.Click += TmGet_Click;
            tmClose.Click += TmClose_Click;
            comlist.Click += Comlist_Click;
            btnSearch.Click += BtnSearch_Click;
        }

        /// <summary>
        /// 显示基本信息
        /// </summary>
        public void Show()
        {
            switch (_funid)
            {
                case 0:
                    this.Text = "房屋类型明细";
                    break;
                case 1:
                    this.Text = "材料供应商明细";
                    break;     
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, System.EventArgs e)
        {
           
        }

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comlist_Click(object sender, System.EventArgs e)
        {
            
        }

        /// <summary>
        /// 选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGet_Click(object sender, System.EventArgs e)
        {
            
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
    }
}
