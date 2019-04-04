using System;
using System.Data;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class ShowDtlListFrm : Form
    {
        TaskLogic task = new TaskLogic();

        //保存初始化的内容
        public DataTable _dt = new DataTable();

        private int _funid;           //功能ID 0：房屋类型明细 1:材料供应商明细
        private string _factionName;  //功能名称

        private int _resultid;        //返回结果信息ID
        private string _resultName;   //返回结果信息名称

        #region Set

        /// <summary>
        /// 记录功能ID 0：房屋类型明细 1:材料供应商明细
        /// </summary>
        public int Funid { set { _funid = value; } }

        /// <summary>
        /// 获取功能ID信息
        /// </summary>
        public string FactionName { set { _factionName = value; } }

        #endregion

        #region Get

            /// <summary>
            /// 返回结果标记
            /// </summary>
            public int ResultId => _resultid;
            
            /// <summary>
            /// 返回结果名称
            /// </summary>
            public string ResultName => _resultName;

        #endregion

        public ShowDtlListFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        void OnRegisterEvents()
        {
            tmGet.Click += TmGet_Click;
            tmClose.Click += TmClose_Click;
            comlist.Click += Comlist_Click;
            btnSearch.Click += BtnSearch_Click;
        }

        /// <summary>
        /// 根据条件初始化获取DataTable记录
        /// </summary>
        public void OnInitialize()
        {
            //根据条件初始化并获得DataTable
            task.TaskId = 1;
            task.FunctionId = "1.3";
            task.FunctionName = _factionName;
            task.StartTask();
            //根据功能ID定义界面相关值
            Show();

            gvdtl.DataSource = task.ResultTable;
            _dt = task.ResultTable;
            //控制GridView列显示情况
            ControlGridViewisShow();
        }

        /// <summary>
        /// 显示基本信息
        /// </summary>
        private void Show()
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
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int)comlist.SelectedIndex == -1) throw new Exception("请选择下拉列表.");
                //获取下拉列表值
                var dvColIdlist = (DataRowView)comlist.Items[comlist.SelectedIndex];
                var colName = Convert.ToString(dvColIdlist["ColName"]);

                task.TaskId = 1;
                task.FunctionId = "1.4";
                task.FunctionName = _factionName;
                task.SearchName = colName;
                task.SearchValue = txtValue.Text;
                task.Data = _dt;

                task.StartTask();
                gvdtl.DataSource = task.ResultTable;
                ControlGridViewisShow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comlist_Click(object sender, EventArgs e)
        {
            try
            {
                task.TaskId = 1;
                task.FunctionId = "1.2";
                task.FunctionName = _funid == 0 ? "House" : "Supplier";

                task.StartTask();
                comlist.DataSource = task.ResultTable;
                comlist.DisplayMember = "ColName";     //设置显示值
                comlist.ValueMember = "ColId";       //设置默认值内码(即:列名)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 选取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGet_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvdtl.SelectedRows.Count ==0) throw new Exception("请选取某一行,然后再按此按钮");
                //获取当前行ID  var rowid = gvdtl.CurrentCell.RowIndex;
                //获取当前行指定单元格的值
                //返回ID值
                _resultid = Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value);
                //返回对应名称
                _resultName = Convert.ToString(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[2].Value);
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
