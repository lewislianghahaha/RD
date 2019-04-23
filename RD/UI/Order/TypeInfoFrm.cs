using System;
using System.Data;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;

namespace RD.UI.Order
{
    public partial class TypeInfoFrm : Form
    {
        TaskLogic task=new TaskLogic();
        DtList dtList=new DtList();

        //获取表体DT
        private DataTable _dt;
        //获取ID信息(通过下拉列表 或 树型菜单获取)
        private int _id;
        //获取功能名称:Material 材料 HouseProject:装修工程类别
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
        public DataTable ResultTable => _resultTable;

        #endregion

        public TypeInfoFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
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
            //初始化对应的类别明细记录(包括装修工程 及 材料)
            task.TaskId = 1;
            task.FunctionId = "1";
            task.FunctionName = _funname;
            task.FunctionType = "G";
            task.ParentId = Convert.ToString(_id);

            task.StartTask();
            gvdtl.DataSource = task.ResultTable;
            _dt = task.ResultTable;

            switch (_funname)
            {
                case "HouseProject":
                    this.Text = "装修工程类别";
                    break;
                case "Material":
                    this.Text = "材料明细";
                    break;
            }
            //控制GridView指定列
            ControlGridViewisShow();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGet_Click(object sender, EventArgs e)
        {
            var rowdtl = new DataRow[0];

            try
            {
                if(gvdtl.SelectedRows.Count==0) throw new Exception("请至少选中一行");
                //根据Funname获取对应的临时表
                _resultTable = _funname == "HouseProject" ? dtList.Get_HouseProjectEmptydt() : dtList.Get_MaterialEmptydt();
                //获取当前行记录

                foreach (DataGridViewRow row in gvdtl.SelectedRows)
                {
                    //根据MaterialID 或 Prjoectid获取所选中的GridView记录
                    rowdtl = _funname == "HouseProject" ? _dt.Select("ProjectId='" + Convert.ToInt32(row.Cells[1].Value) + "'") : _dt.Select("MaterialId='" + Convert.ToInt32(row.Cells[1].Value) + "'");
                    //循环将相关值赋给到输出表内
                    foreach (var row1 in rowdtl)
                    {
                        var row2 = _resultTable.NewRow();
                        for (var i = 0; i < _dt.Columns.Count; i++)
                        {
                            row2[i] = row1[i];
                        }
                        _resultTable.Rows.Add(row2);
                    }
                }
                //完成后关闭该窗体
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
        /// 选择条件下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comlist_Click(object sender, EventArgs e)
        {
            try
            {
                task.TaskId = 1;
                task.FunctionId = "1.2";
                task.FunctionName = _funname;

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
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int)comlist.SelectedIndex == -1) throw new Exception("请选择查询条件");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 控制GridView单元格显示方式
        /// </summary>
        private void ControlGridViewisShow()
        {
            if (_funname == "HouseProject")
            {
                //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
                gvdtl.Columns[0].Visible = false;
                gvdtl.Columns[1].Visible = false;
            }
            else if(_funname== "Material")
            {
                //注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
                gvdtl.Columns[0].Visible = false;
                gvdtl.Columns[1].Visible = false;
                gvdtl.Columns[4].Visible = false;
            }
            //设置指定列不能编辑
            gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
            gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;
        }
    }
}
