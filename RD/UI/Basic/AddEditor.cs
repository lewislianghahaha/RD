using System;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class AddEditor : Form
    {
        TaskLogic task=new TaskLogic();

        private string _funid;           //功能ID 0：新增 1:修改
        private int _pid;               //记录上级ID
        private string _pName;         //记录上级节点名称
        private string _functionName; //记录功能名称

        private bool _resultMark;     //返回结果信息

        #region Set

        /// <summary>
        /// 记录上级ID
        /// </summary>
        public int pid { set { _pid = value; } }

        /// <summary>
        /// 记录上级节点名称
        /// </summary>
        public string PName { set { _pName = value; } }

        /// <summary>
        /// 记录功能ID 2.1:新增 2.2:编辑(更新)
        /// </summary>
        public string Funid { set { _funid = value; } }

        /// <summary>
        /// 获取功能名称
        /// </summary>
        public string FunName { set { _functionName = value; } }

        #endregion

        #region Get
        /// <summary>
        /// 返回结果标记
        /// </summary>
        public bool ResultMark => _resultMark;
        #endregion

        public AddEditor()
        {
            InitializeComponent();
            OnInitialize();
        }

        void OnInitialize()
        {
            tmSave.Click += TmSave_Click;
            tmClose.Click += TmClose_Click;
        }

        /// <summary>
        /// 显示基本信息
        /// </summary>
        public new void Show()
        {
            switch (_funid)
            {
                case "2.1":
                    this.Text = "新增分组";
                    break;
                case "2.2":
                    lbl1.Text = "原节点名称:";
                    lbl2.Text = "新节点名称:";
                    this.Text = "编辑分组";
                    break;
            }
            txtUpName.Text = _pName;
        }

        /// <summary>
        /// 保存(作用:用于将新增或编辑的记录插入至对应的数据表内)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtName.Text==null) throw new Exception("节点名称不能为空,请填上后再按保存");

                task.TaskId = 1;
                task.FunctionName = _functionName;      //记录所属功能名称
                task.Pid = _pid;                        //获取主键ID
                task.TreeName = txtName.Text;           //记录同级节点名称

                switch (_funid)
                {
                    //新增
                    case "2.1":
                        task.FunctionId = "2.1";                //功能ID   
                        task.StartTask();
                        _resultMark = task.ResultMark;
                        if (!task.ResultMark)throw new Exception("新增异常,请联系管理员");
                        break;
                    //编辑
                    case "2.2":
                        task.FunctionId = "2.2";               //功能ID
                        task.StartTask();
                        _resultMark = task.ResultMark;
                        if (!task.ResultMark) throw new Exception("编辑异常,请联系管理员");
                        break;
                }
                //结束后将对应的文本框清空
                txtUpName.Text = "";
                txtName.Text = "";
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
