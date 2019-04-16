using System;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class HtypeProjectFrm : Form
    {
        TaskLogic task = new TaskLogic();

        private int _hTypeid;  //获取T_BD_HTypeEntry的HTypeid
        private string _TypeName;  //获取所选择的是房屋类型 或 装修工程 

        #region set

            public int HTypeid { set { _hTypeid = value; } }
            public string TypeName { set { _TypeName = value; } }

        #endregion

        #region get

        #endregion


        public HtypeProjectFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        public void OnInitialize()
        {

            Show();
        }

        void OnRegisterEvents()
        {
            tmSave.Click += TmSave_Click;
        }

        private void Show()
        {
            switch (_TypeName)
            {
                case "房屋类型":
                    this.Text = "房屋类型-项目明细";
                    break;
                case "装修工程":
                    this.Text = "装修工程-项目明细";
                    break;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, System.EventArgs e)
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
