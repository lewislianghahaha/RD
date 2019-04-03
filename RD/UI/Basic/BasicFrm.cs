using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class BasicFrm : Form
    {
        TaskLogic task=new TaskLogic();
        Load load=new Load();
        AddEditor add=new AddEditor();
        ShowDtlListFrm showDtl=new ShowDtlListFrm();

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
            tview.AfterSelect += Tview_AfterSelect;
            tmReset.Click += TmReset_Click;
            comList.Click += ComList_Click;
            gvdtl.CellDoubleClick += Gvdtl_CellDoubleClick;
        }

        //初始化树形列表及GridView控件
        private void OnInitialize()
        {
            tview.Nodes.Clear();

            task.TaskId = 1;                 //中转功能ID
            task.FunctionId = "1";           //功能ID(创建:0 查询:1 保存:2 审核:3)
            task.FunctionType = "T";         //表格类型(注:读取时使用) T:表头 G:表体
            task.ParentId = null;            //主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容

            //根据功能中转ID获取表头信息(树形菜单使用) 以及表体信息(GridView控件使用)
            var functionName = ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);
            task.StartTask();
            //将返回的结果赋值至表头DT变量类型内
            _dt = task.ResultTable;
            //按照对应的参数获取表体全部内容
            _dtldt = OnInitializeDtl(1, "1", functionName, "G", null);
            //导出记录至树形菜单内
            ShowTreeList(_dt);
            //对GridView赋值(将对应功能点的表体全部信息赋值给GV控件内)
            gvdtl.DataSource = _dtldt;
            //将GridView中的第一列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            //设置GridView是否显示某些列
            ControlGridViewisShow();
            //预留(权限部份)


            //展开根节点
            tview.ExpandAll();
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
            try
            {
                if (dt.Rows.Count == 0)
                {
                    var anime = new TreeNode();
                    anime.Tag = 1;
                    anime.Text = "ALL";
                    tview.Nodes.Add(anime);
                }
                else
                {
                    #region 为传递过来的DT进行排序
                    //dt.DefaultView.Sort = "ParentId asc";
                    //var dtsoft = dt.DefaultView.ToTable();
                    #endregion

                    var tnode = new TreeNode();
                    tnode.Tag = 1;
                    tnode.Text = "ALL";
                    tview.Nodes.Add(tnode);
                    //展开根节点
                    tview.ExpandAll();
                    AddChildNode(tview,dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取记录并增加至子节点内(从二级节点开始)
        /// </summary>
        /// <returns></returns>
        private void AddChildNode(TreeView tvView,DataTable dt)
        {
            var tnode = tvView.Nodes[0];
            var rows = dt.Select("Parentid='" + Convert.ToInt32(tnode.Tag) + "'");
            //循环子节点信息(从二级节点开始)
            foreach (var r in rows)
            {
                var tn = new TreeNode();
                tn.Tag = Convert.ToInt32(r[0]);        //自身主键ID
                tn.Text = Convert.ToString(r[2]);     //节点内容
                //将二级节点添加至根节点下
                tnode.Nodes.Add(tn);
                //获取在二级节点以下的节点信息并进行添加(使用递归)
                GetChildNode(dt,tn);
            }
        }

        /// <summary>
        /// 获取在二级节点以下的节点信息并进行添加(递归)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tr">节点信息</param>
        private void GetChildNode(DataTable dt, TreeNode tr)
        {
            try
            {
                var pid = Convert.ToInt32(tr.Tag);
                var rowdtl = dt.Select("Parentid='" + pid + "'");

                if (rowdtl.Length <= 0) return;
                foreach (var t in rowdtl)
                {
                    var tn = new TreeNode();
                    tn.Tag = Convert.ToInt32(t[0]);
                    tn.Text = Convert.ToString(t[2]);
                    tr.Nodes.Add(tn);
                    //(重) 以子节点的ID作为条件,查询其有没有与它关联的记录,若有,执行递归调用
                    var result = dt.Select("Parentid='" + Convert.ToInt32(tn.Tag) + "'");
                    //(重) 当没有记录时跳出当前循环;注:当跳出后,返回上一级节点继续执行循环
                    if (result.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        //(重)递归调用
                        //可理解为:暂存,在递归时,将之前的记录暂时存放在一个位置,当跳出递归时,才解放暂存记录
                        GetChildNode(dt, tn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 下拉列表读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComList_Click(object sender, EventArgs e)
        {
            try
            {
                task.TaskId = 1;
                task.FunctionId = "1.2";
                ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);
                task.StartTask();
                comList.DataSource = task.ResultTable;
                comList.DisplayMember = "ColName";     //设置显示值
                comList.ValueMember = "ColId";       //设置默认值内码(即:列名)
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
                if (tview.SelectedNode == null) throw new Exception("没有选择父节点,请选择");

                add.pid = (int)tview.SelectedNode.Tag;                                //上级主键ID
                add.PName = tview.SelectedNode.Text;                                 //获取上级名称
                add.FunName=ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);     //功能名称
                add.Funid = "2.1";

                add.Show();
                add.StartPosition = FormStartPosition.CenterScreen;
                add.ShowDialog();
                //当成功新增后,执行"刷新"操作
                if (add.ResultMark)
                    OnInitialize();
                #region 增加子节点
                //treeNode.Tag = 1;
                //treeNode.Text = "hahaha";
                //tview.SelectedNode.Nodes.Add(treeNode); //对所选择的节点增加子节点
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if(tview.SelectedNode == null) throw new Exception("没有选择节点,请选择某一节点");

                add.pid = (int)tview.SelectedNode.Tag;                                  //获取所选择的主键ID
                add.PName = tview.SelectedNode.Text;                                   //获取本级名称
                add.FunName = ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);     //功能名称
                add.Funid = "2.2";

                add.Show();
                add.StartPosition = FormStartPosition.CenterScreen;
                add.ShowDialog();
                //当编辑成功后,执行"刷新"操作
                if(add.ResultMark)
                    OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if(tview.SelectedNode==null) throw new Exception("请选择某一名称再继续");
                if ((int)tview.SelectedNode.Tag == 1) throw new Exception("ALL节点不能删除,请选择其它节点进行删除");

                //节点ID
                var treeid = Convert.ToInt32(tview.SelectedNode.Tag);
                //节点名称
                var treeName = tview.SelectedNode.Text;

                var clickMessage = $"您所选择的信息为:\n 节点名称:{treeName} \n 是否继续? \n 注:若点选的节点下有内容的话,就会将与它对应的记录都删除, \n 请谨慎处理.";

                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    task.TaskId = 1;
                    task.FunctionId = "3";
                    task.FunctionName = ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);
                    task.Pid = treeid;
                    task.Data = _dt;

                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    if (!task.ResultMark) throw new Exception("删除异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("删除成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    OnInitialize();
                }
                
                #region 删除节点
                // tview.SelectedNode.Remove();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if((int)comList.SelectedIndex ==-1) throw new Exception("请选择下拉列表.");
               // if(txtValue.Text == "") throw new Exception("请在查询框填上查询条件再继续.");
                //获取下拉列表值
                var dvColIdlist = (DataRowView)comList.Items[comList.SelectedIndex];
                var colName = Convert.ToString(dvColIdlist["ColName"]);
                //TaskLogic各参数赋值
                task.TaskId = 1;
                task.FunctionId = "1.1";
                task.FunctionName = ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);
                task.SearchName = colName;
                task.SearchValue = txtValue.Text;
                task.Data = _dtldt;
                task.Pid = -1;

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                gvdtl.DataSource = task.ResultTable;
                //将GridView中的第一列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
                gvdtl.Columns[0].Visible = false;
                gvdtl.Columns[1].Visible = false;
                //设置最后两列不能编辑
                gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
                gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存(作用:主要对GridView控件进行数据导入)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmSave_Click(object sender, EventArgs e)
        {
            try
            {
                //从GridView获取其DataTable内容(注:包括已存在的以及新增的记录)
                var dt = (DataTable)gvdtl.DataSource;
                if (dt.Rows.Count != 0)
                {
                    task.TaskId = 1;
                    task.FunctionId = "2";
                    task.FunctionName = ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);
                    task.Data = dt;
                    //获取点选中的节点ID(用于最后的表体外键值插入)
                    task.Pid = Convert.ToInt32(tview.SelectedNode.Tag);
                    task.AccountName = GlobalClasscs.User.StrUsrName;

                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    if (!task.ResultMark) throw new Exception("保存异常,请联系管理员");
                    else
                    {
                        MessageBox.Show("保存成功,请点击后继续", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //保存成功后,再次进行初始化
                    OnInitialize();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新功能(重新获取表头及表体全部信息) 在点击中节点后才执行此事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //当选择框没有值时的操作
                task.TaskId = 1;
                task.FunctionName = ShowBasicFunctionName(GlobalClasscs.Basic.BasicId);

                //当查询框为空时
                if (txtValue.Text == "")
                {
                    task.FunctionId = "1";
                    task.FunctionType = "G";
                    task.ParentId = (int) tview.SelectedNode.Tag == 1 ? null : Convert.ToString(tview.SelectedNode.Tag);
                }
                //当查询框不为空并且节点为"ALL"
                else if (txtValue.Text !="" && (int)tview.SelectedNode.Tag == 1)
                {
                    //获取下拉列表值
                    var dvColIdlist = (DataRowView)comList.Items[comList.SelectedIndex];
                    var colName = Convert.ToString(dvColIdlist["ColName"]);
                    task.FunctionId = "1.1";
                    task.SearchName = colName;
                    task.SearchValue = txtValue.Text;
                    task.Data = _dtldt;
                    task.Pid = -1;
                }
                else
                {
                    //获取下拉列表值
                    var dvColIdlist = (DataRowView)comList.Items[comList.SelectedIndex];
                    var colName = Convert.ToString(dvColIdlist["ColName"]);
                    task.FunctionId = "1.1";
                    task.SearchName = colName;
                    task.SearchValue = txtValue.Text;
                    task.Data = _dtldt;
                    //获取所选中的节点
                    task.Pid= Convert.ToInt32(tview.SelectedNode.Tag);
                }

                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();
                
                gvdtl.DataSource = task.ResultTable;
                //将GridView中的第一列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
                gvdtl.Columns[0].Visible = false;
                gvdtl.Columns[1].Visible = false;
                //设置最后两列不能编辑
                gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
                gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;
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
                OnInitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 返回对应的功能点名称
        /// </summary>
        /// <param name="basicId">功能点</param>
        /// <returns></returns>
        private string ShowBasicFunctionName(int basicId)
        {
            var result = string.Empty;

            switch (basicId)
            {
                //客户信息管理
                case 1:
                    task.FunctionName = "Customer";
                    result = "Customer";
                    this.Text = "基础信息库-客户信息管理";
                    break;
                //供应商信息管理
                case 2:
                    task.FunctionName = "Supplier";
                    result="Supplier";
                    this.Text = "基础信息库-供应商信息管理";
                    break;
                //材料信息管理
                case 3:
                    task.FunctionName = "Material";
                    result = "Material";
                    this.Text = "基础信息库-材料信息管理";
                    break;
                //房屋类型及装修工程类别信息管理
                case 4:
                    task.FunctionName = "House";
                    result = "House";
                    this.Text = "基础信息库-房屋类型及装修工程类别信息管理";
                    break;
            }
            return result;
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

        /// <summary>
        /// 当双击GridView控件内某个单元格内发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gvdtl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //根据功能名指定某一单元格可以弹出明细内容
            switch (GlobalClasscs.Basic.BasicId)
            {
                //客户信息管理
                case 1:
                    if (e.ColumnIndex == 3)
                    {
                        //调用明细记录框
                        
                        showDtl.StartPosition = FormStartPosition.CenterScreen;
                        showDtl.ShowDialog();
                    }
                    break;
                //供应商信息管理(Hold)
                case 2:
                    break;
                //材料信息管理
                case 3:
                    if (e.ColumnIndex == 4)
                    {
                        //调用明细记录框

                        showDtl.StartPosition = FormStartPosition.CenterScreen;
                        showDtl.ShowDialog();
                    }
                    break;
                //房屋类型及装修工程类别信息管理(Hold)
                case 4:
                    break;
            }
        }

        /// <summary>
        /// 控制GridView
        /// </summary>
        private void ControlGridViewisShow()
        {
            gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
            gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;
        }

    }
}
