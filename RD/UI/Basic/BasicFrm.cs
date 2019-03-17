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

        //保存初始化的表头内容
       public DataTable _dt=new DataTable();
        //保存初始化的表体内容
       public DataTable _dtldt =new DataTable(); 

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

        //初始化树形列表及GridView控件
        private void OnInitialize()
        {
            var functionName = string.Empty;

            task.TaskId = 1;                 //中转功能ID
            task.FunctionId = "1";           //功能ID(创建:0 查询:1 保存:2 审核:3)
            task.FunctionType = "T";         //表格类型(注:读取时使用) T:表头 G:表体
            task.ParentId = null;            //主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容

            //根据功能中转ID获取表头信息(树形菜单使用) 以及表体信息(GridView控件使用)
            switch (GlobalClasscs.Basic.BasicId)
            {
                //客户信息管理
                case 1:
                    task.FunctionName = "Customer";
                    functionName = "Customer";
                    this.Text = "基础信息库-客户信息管理";
                    break;
                //供应商信息管理
                case 2:
                    task.FunctionName = "Supplier";
                    functionName = "Supplier";
                    this.Text = "基础信息库-供应商信息管理";
                    break;
                //材料信息管理
                case 3:
                    task.FunctionName = "Material";
                    functionName = "Material";
                    this.Text = "基础信息库-材料信息管理";
                    break;
                //房屋类型及装修工程类别信息管理
                case 4:
                    task.FunctionName = "House";
                    functionName = "House";
                    this.Text = "基础信息库-房屋类型及装修工程类别信息管理";
                    break;
            }
            task.StartTask();
            //将返回的结果赋值至表头DT变量类型内
            _dt = task.ResultTable;
            //按照对应的参数获取表体全部内容
            _dtldt = OnInitializeDtl(1, "1", functionName, "G", null);
            //导出记录至树形菜单内
            ShowTreeList(_dt);
            //导出记录至GridView控件内
            gvdtl.DataSource = _dtldt;
            //预留(权限部份)

        }

        /// <summary>
        /// 初始化获取表体信息
        /// </summary>
        private DataTable OnInitializeDtl(int taskId, string functionId, string functionName, string functionType, string parentId)
        {
            var dt = new DataTable();

            try
            {
                task.TaskId = taskId;                        //中转功能ID
                task.FunctionId = functionId;               //功能ID(创建:0 查询:1 保存:2 审核:3)
                task.FunctionName = functionName;
                task.FunctionType = functionType;          //表格类型(注:读取时使用) T:表头 G:表体
                task.ParentId = parentId;                 //主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容
                task.StartTask();
                dt = task.ResultTable;
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        //根据获取的DT读取树形列表
        private void ShowTreeList(DataTable dt)
        {
            //记录父节点ID
            var pId = -1; 
            var tree=new TreeNode();

            try
            {
                if (dt.Rows.Count == 0)
                {
                    var anime = new TreeNode();
                    anime.Tag = 0;
                    anime.Text = "ALL";
                    tview.Nodes.Add(anime);
                }
                else
                {
                    #region 为传递过来的DT进行排序
                    //dt.DefaultView.Sort = "ParentId asc";
                    //var dtsoft = dt.DefaultView.ToTable();
                    #endregion

                    var tnode=new TreeNode();
                    tnode.Tag = 0;
                    tnode.Text = "ALL";
                    tview.Nodes.Add(tnode);

                    AddChildNode(tview,dt);


                  

                    //for (var i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    var anime = new TreeNode();
                    //    anime.Tag = Convert.ToInt32(dt.Rows[i][0]);              //自身主键ID
                    //    anime.Text = Convert.ToString(dt.Rows[i][2]);           //节点内容
                    //    pId = Convert.ToInt32(dt.Rows[i][1]);                  //上一级主键ID

                    //    if (pId == 0)
                    //    {
                    //        tview.Nodes.Add(anime);
                    //        //获取父节点信息
                    //        tree = tview.Nodes[0];
                    //        //展开
                    //        tree.Expand();
                    //    }
                    //    else
                    //    {
                    //        AddChildNode(tree,anime,pId);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取记录并增加至子节点内
        /// </summary>
        /// <returns></returns>
        private void AddChildNode(TreeView tvView,DataTable dt)
        {
            var tnode = tvView.Nodes[0];
            var pid = tnode.Tag;

            var rows = dt.Select("Parentid='" + pid + "'");
            //循环二级节点信息
            for (var i = 0; i < rows.Length; i++)
            {
                pid = Convert.ToInt32(rows[i][1]);
                var rowdtl = dt.Select("Parentid='" + pid + "'");
                //Q1:如何动态添加到关联的节点后面
                //目前的想法就是先循环2级节点,然后下面就是循环将2级节点的信息关联添加,在下面执行
                for (var j = 0; j < rowdtl.Length; j++)
                {
                   var tn=new TreeNode();
                    tn.Tag = rowdtl[j][0];
                    tn.Text = Convert.ToString(rowdtl[j][2]);
                    tnode.Nodes.Add(tn);
                }

            }


            //var a = treeF.Nodes;
            ////添加根节点以下的二级节点 

            //if ((int) treeF.Tag == pid)
            //{
            //    treeF.Nodes.Add(treeNode);
            //}
            ////添加二级节点下的节点记录
            //else
            //{
            //    AddChildChildNode(treeF, treeNode, pid);
            //}
        }

        /// <summary>
        /// 查找并添加节点下的节点(即3级或以下的节点)
        /// </summary>
        /// <param name="treeF"></param>
        /// <param name="treeNode"></param>
        /// <param name="pid"></param>
        private void AddChildChildNode(TreeNode treeF, TreeNode treeNode, int pid)
        {
            //获取最后一个节点信息
            //这里第一次读取时。只是获取根子点下的二级节点的信息,而不是从三级节点(包括三级节点)的信息
            //var i = treeF.LastNode.Index;  

            //获取所有节点的个数(包括子节点)
            var a =tview.GetNodeCount(true);
            

            foreach (TreeNode tr1 in treeF.Nodes)
            {
                //  var a1 = tr1.Nodes.Count;
                if ((int)tr1.Tag == pid)
                {
                    tr1.Nodes.Add(treeNode);
                   // return;
                }

                //若节点的节点都有记录的话。就执行这里进行添加
                //else if (tr1.Nodes.Count > 0)
                //{
                //    AddChildChildNode(treeF, treeNode, pid);
                //}
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
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新功能(重新获取表头及表体全部信息)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmReset_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击TreeView控件时执行(主要是根据节点ID读取表体数据)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tview_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
