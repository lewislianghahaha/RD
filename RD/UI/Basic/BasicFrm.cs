using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class BasicFrm : Form
    {
        Task task=new Task();
        Load load=new Load();

        public BasicFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnInitialize();
        }

        private void OnRegisterEvents()
        {
            btnSearch.Click += BtnSearch_Click;
            btnCreate.Click += BtnCreate_Click;
            btnChange.Click += BtnChange_Click;
            btnDel.Click += BtnDel_Click;
            tmSave.Click += TmSave_Click;
            tview.Click += Tview_Click;
            tmReset.Click += TmReset_Click;
        }

        //初始化树形列表 及 GridView
        private void OnInitialize()
        {
            switch (GlobalClasscs.Basic.BasicId)
            {
                //客户信息管理
                case 1:
                    task.TaskId = 1;                 //中转功能ID
                    task.FunctionId = "1";           //功能ID(创建:0 查询:1 保存:2 审核:3)
                    task.FunctionName = "Customer";  //功能名
                    task.FunctionType = "T";         //表格类型(注:读取时使用) T:表头 G:表体
                    task.ParentId = null;            //主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容
                    this.Text = "基础信息库-客户信息管理";
                    break;
                //供应商信息管理
                case 2:
                    task.TaskId = 1;                 
                    task.FunctionId = "1";
                    task.FunctionName = "Supplier";
                    task.FunctionType = "T";
                    task.ParentId = null;
                    this.Text = "基础信息库-供应商信息管理";
                    break;
                //材料信息管理
                case 3:
                    task.TaskId = 1;                 
                    task.FunctionId = "1";
                    task.FunctionName = "Material";
                    task.FunctionType = "T";
                    task.ParentId = null;
                    this.Text = "基础信息库-材料信息管理";
                    break;
                //房屋类型及装修工程类别信息管理
                case 4:
                    task.TaskId = 1;                 
                    task.FunctionId = "1";
                    task.FunctionName = "House";
                    task.FunctionType = "T";
                    task.ParentId = null;
                    this.Text = "基础信息库-房屋类型及装修工程类别信息管理";
                    break;
            }
            task.StartTask();
            GetTreeList(task.RestulTable);
            //Q:如何在获取初始化信息后进行使用?（主要是表体信息）
        }

        //根据获取的DT读取树形列表
        private void GetTreeList(DataTable dt)
        {
            var pId = -1; //记录父节点ID
            var anime = new TreeNode();

            try
            {
                if (dt.Rows.Count == 0)
                {
                    anime.Tag = 1;
                    anime.Text = "ALL";
                    tview.Nodes.Add(anime);
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 新建分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var treeNode=new TreeNode();
                treeNode.Tag = 1;
                treeNode.Text = "hahaha";
                tview.SelectedNode.Nodes.Add(treeNode); //对所选择的节点增加子节点
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 编辑分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                //var a = (int) tview.SelectedNode.Tag;
                var change = new AddEditor();
                change.StartPosition = FormStartPosition.CenterScreen;
                change.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                tview.SelectedNode.Remove();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// 刷新功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmReset_Click(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// 点击TreeView控件时执行(主要是根据节点ID读取表体数据)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tview_Click(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        private void Start()
        {
            task.StartTask();

            //当完成后将Form2子窗体关闭
            this.Invoke((ThreadStart)(() =>
            {
                load.Close();
            }));
        }
    }
}
