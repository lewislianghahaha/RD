using System;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class AdornTypeFrm : Form
    {
        TaskLogic task = new TaskLogic();

        //功能标识ID(新建：C 编辑 E)
        private string _funid;
        //树菜单上级主键ID
        private int _pid;     
        //表头ID
        private int _id;

        //返回结果信息
        private bool _resultMark;

        #region Set
            /// <summary>
            /// 记录上级ID
            /// </summary>
            public int Pid {set { _pid = value; } }
            /// <summary>
            /// 获取功能标识ID(新建：C 编辑 A)
            /// </summary>
            public string Funid { set { _funid = value; } }
            /// <summary>
            /// 表头ID
            /// </summary>
            public int Id { set { _id = value; } }
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
        /// 初始化记录
        /// </summary>
        public void OnInitialize()
        {
            switch (_funid)
            {
                case "C":
                    this.Text = "新建类别";
                    break;
                case "E":
                    this.Text = "编辑类别";
                    break;
            }
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
                if (txtValue.Text == "") throw new Exception("节点名称不能为空,请填上后再按保存");

                task.TaskId = 2;
                task.Pid = _pid;                         //获取主键ID
                task.TreeName = txtValue.Text;           //记录节点名称

                switch (_funid)
                {
                    //新建
                    case "C":
                        task.FunctionId = "2";
                        task.FunctionName = "AdornOrder";
                        task.Id = _id;                        //获取上级表头ID

                        task.StartTask();
                        _resultMark = task.ResultMark;
                        if (!task.ResultMark) throw new Exception("新建异常,请联系管理员");
                        break;
                    //编辑
                    case "E":
                        task.FunctionId = "2.1";
                        task.FunctionName = "AdornOrder";

                        task.StartTask();
                        _resultMark = task.ResultMark;
                        if (!task.ResultMark) throw new Exception("编辑异常,请联系管理员");
                        break;
                }
                //结束后将对应的文本框清空
                txtValue.Text = "";
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
