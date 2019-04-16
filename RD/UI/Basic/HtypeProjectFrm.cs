using System;
using System.Data;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class HtypeProjectFrm : Form
    {
        TaskLogic task = new TaskLogic();

        private int _hTypeid;  //获取T_BD_HTypeEntry的HTypeid
        private string _typeName;  //获取所选择的名称是房屋类型 或 装修工程 

        #region set
        /// <summary>
        /// 获取T_BD_HTypeEntry的HTypeid
        /// </summary>
        public int HTypeid { set { _hTypeid = value; } }

        /// <summary>
        /// 获取所选择的名称是房屋类型 或 装修工程
        /// </summary>
        public string TypeName { set { _typeName = value; } }

        #endregion

        public HtypeProjectFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        /// <summary>
        /// 初始化记录(注:根据Htypeid为条件获取Datatable)
        /// </summary>
        public void OnInitialize()
        {
            task.TaskId = 1;
            task.FunctionId = "1.3";
            task.FunctionName = "HouseProject";

            Show();

            gvdtl.DataSource = task.ResultTable;
            //_dt = task.ResultTable;
            //控制GridView列显示情况
            //ControlGridViewisShow();
        }

        void OnRegisterEvents()
        {
            tmSave.Click += TmSave_Click;
        }

        private new void Show()
        {
            switch (_typeName)
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
        /// 保存(包括插入及更新操作)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {
                task.TaskId = 1;
                task.FunctionId = "2";
                task.FunctionName = "HouseProject";
                task.Data = (DataTable) gvdtl.DataSource;
                task.Pid = _hTypeid;
                task.AccountName = null;

                task.StartTask();
                if (!task.ResultMark) throw new Exception("保存异常,请联系管理员");
                else
                {
                    MessageBox.Show("保存成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 控制GridView列显示情况
        /// </summary>
        private void ControlGridViewisShow()
        {
            //将GridView中的第一二列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
        }
    }
}
