﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using RD.DB;
using RD.Logic;

namespace RD.UI.Basic
{
    public partial class BasicFrm : Form
    {
        TaskLogic task=new TaskLogic();
        Load load=new Load();
        AddEditor add=new AddEditor();
        ShowDtlListFrm showDtl=new ShowDtlListFrm();
        HtypeProjectFrm htype=new HtypeProjectFrm();
        DtList dtList=new DtList();

        //保存初始化的表头内容
        public DataTable _dt=new DataTable();
        //保存初始化的表体内容
        public DataTable _dtldt =new DataTable();
        //保存GridView内需要进行删除的临时表
        private DataTable _deldt = new DataTable();

        //保存查询出来的GridView记录（GridView页面跳转时使用）
        private DataTable _dtl;
        //记录当前页数(GridView页面跳转使用)
        private int _pageCurrent = 1;
        //记录计算出来的总页数(GridView页面跳转使用)
        private int _totalpagecount;
        //记录初始化标记(GridView页面跳转 初始化时使用)
        private bool _pageChange;

        //记录能否删除ID(删除权限使用)
        private bool _candelMarkid;

        #region Set

        /// <summary>
        /// 获取单据状态标记ID C:创建 R:读取
        /// </summary>
        public bool CandelMarkid { set { _candelMarkid = value; } }

        #endregion

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
            tmShowdtl.Click += TmShowdtl_Click;
            tmdelrow.Click += Tmdelrow_Click;

            bnMoveFirstItem.Click += BnMoveFirstItem_Click;
            bnMovePreviousItem.Click += BnMovePreviousItem_Click;
            bnMoveNextItem.Click += BnMoveNextItem_Click;
            bnMoveLastItem.Click += BnMoveLastItem_Click;
            bnPositionItem.TextChanged += BnPositionItem_TextChanged;
            tmshowrows.DropDownClosed += Tmshowrows_DropDownClosed;
            panel6.Visible = false;
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

            //连接GridView页面跳转功能
            LinkGridViewPageChange(_dtldt);

            //设置GridView是否显示某些列
            ControlGridViewisShow();
            //展开根节点
            tview.ExpandAll();
            //设置，若功能不是 "房屋类型及装修工程类别信息管理明细",那就将右键菜单功能隐藏
            tmShowdtl.Visible = GlobalClasscs.Basic.BasicId == 4;
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
                //将文本框清空
                txtValue.Text = "";

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
                //检测当前用户是否有‘删除’权限
                if (!_candelMarkid) throw new Exception($"用户'{GlobalClasscs.User.StrUsrName}'没有‘删除’权限,不能继续.");
                //检测若所选择行中的值已给其它地方使用,就不能进行删除(如:客户已让某一张单据使用,就不能进行删除)
                if(!CheckCanDel(0)) throw new Exception($"检测到需要删除的分组中有明细记录已在其它地方使用的情况, \n故不能删除.");

                //节点ID
                var treeid = Convert.ToInt32(tview.SelectedNode.Tag);
                //节点名称
                var treeName = tview.SelectedNode.Text;

                var clickMessage = $"您所选择的信息为:\n 节点名称:{treeName} \n 是否继续? \n 注:若点选的节点下有内容的话(包括明细内容),就会将与它对应的记录都删除, \n 请谨慎处理.";

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

                //连接GridView页面跳转功能
                LinkGridViewPageChange(task.ResultTable);
                //设置GridView是否显示某些列
                ControlGridViewisShow();
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
                if (tview.SelectedNode == null) throw new Exception("没有选择节点,请选择某一节点");
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
                    task.Deldata = _deldt;                           //要进行删除的记录

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
                //当查询框不为空并且节点不为"ALL"
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

                //连接GridView页面跳转功能
                LinkGridViewPageChange(task.ResultTable);
                //设置GridView是否显示某些列
                ControlGridViewisShow();
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
                    if (e.ColumnIndex == 4)
                    {
                        showDtl.Funid = 0;
                        //使用房屋类型名称
                        showDtl.FactionName = ShowBasicFunctionName(4);
                        ShowdtlMessageInfo(e.RowIndex,e.ColumnIndex);
                    }
                    break;
                //供应商信息管理(Hold)
                case 2:
                    break;
                //材料信息管理
                case 3:
                    if (e.ColumnIndex == 5)
                    {
                        showDtl.Funid = 1;
                        //使用供应商名称
                        showDtl.FactionName = ShowBasicFunctionName(2);
                        ShowdtlMessageInfo(e.RowIndex,e.ColumnIndex);
                    }
                    break;
                //房屋类型及装修工程类别信息管理(Hold)
                case 4:
                    break;
            }
        }

        /// <summary>
        /// 明细框信息设置
        /// </summary>
        void ShowdtlMessageInfo(int rowindex,int colindex)
        {
            //初始化明细记录框(获取DataTable记录)
            showDtl.OnInitialize();
            //调用明细记录框
            showDtl.StartPosition = FormStartPosition.CenterScreen;
            showDtl.ShowDialog();
            //将从明细框里获取的值返回至指定的GridView单元格内
            //返回ID值
            gvdtl.Rows[rowindex].Cells[colindex - 1].Value = showDtl.ResultId;
            //返回对应的名称
            gvdtl.Rows[rowindex].Cells[colindex].Value = showDtl.ResultName;
        }

        /// <summary>
        /// 控制GridView列显示情况
        /// </summary>
        private void ControlGridViewisShow()
        {
            //将GridView中的第一二列(ID值)隐去 注:当没有值时,若还设置某一行Row不显示的话,就会出现异常
            gvdtl.Columns[0].Visible = false;
            gvdtl.Columns[1].Visible = false;
            //设置最后两列不能编辑
            gvdtl.Columns[gvdtl.Columns.Count - 1].ReadOnly = true;
            gvdtl.Columns[gvdtl.Columns.Count - 2].ReadOnly = true;

            switch (GlobalClasscs.Basic.BasicId)
            {
                //客户信息管理
                case 1:
                    //id值列不显示
                    gvdtl.Columns[3].Visible = false;
                    gvdtl.Columns[4].ReadOnly = true;
                    break;
                //材料信息管理
                case 3:
                    gvdtl.Columns[4].Visible = false;
                    gvdtl.Columns[5].ReadOnly = true;
                    break;
            }
        }

        /// <summary>
        /// 显示项目明细名称(装修工程信息管理使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmShowdtl_Click(object sender, EventArgs e)
        {
            try
            {
                if (tview.SelectedNode == null) throw new Exception("请选择某一名称再继续");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("请选取某一行,然后再按此按钮");

                //获取所选择的树型菜单Text信息(装修工程类别名称)
                htype.TypeName = tview.SelectedNode.Text;
                //根据所选择的GridView中的行获取其HTypeID值
                htype.HTypeid = Convert.ToInt32(gvdtl.Rows[gvdtl.CurrentCell.RowIndex].Cells[1].Value);

                htype.OnInitialize();
                htype.StartPosition = FormStartPosition.CenterScreen;
                htype.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除GridView中所选择的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmdelrow_Click(object sender, EventArgs e)
        {
            var tempdt=new DataTable();

            try
            {
                if (tview.SelectedNode == null) throw new Exception("请选择某一名称再继续");
                if (gvdtl.SelectedRows.Count == 0) throw new Exception("请选取某一行,再继续");
                //检测当前用户是否有‘删除’权限
                if (!_candelMarkid) throw new Exception($"用户'{GlobalClasscs.User.StrUsrName}'没有‘删除’权限,不能继续.");
                //检测若所选择行中的值已给其它地方使用,就不能进行删除(如:客户已让某一张单据使用,就不能进行删除)
                if (!CheckCanDel(1)) throw new Exception($"检测到所选中的行中有已在其它地方使用的情况, \n故不能删除.");

                var clickMessage = $"您所选择需删除的行数为:{gvdtl.SelectedRows.Count}行 \n 是否继续?";
                if (MessageBox.Show(clickMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //注:执行方式 判断若所选择的行内的 row[1](即各功能的表体主键)项 有值，就执行下面第一步,若没有;只需将在GridView内的行删除就行
                    //1)将所选择的行保存到_deldt内(在按“保存”时执行对该行的数据库删除)
                    //根据各功能ID创建对应临时表
                    switch (GlobalClasscs.Basic.BasicId)
                    {
                        //客户信息管理
                        case 1:
                            tempdt = dtList.Get_CustEmptydt();
                            break;
                        //供应商信息管理
                        case 2:
                            tempdt = dtList.Get_SupplierEmptydt();
                            break;
                        //材料信息管理
                        case 3:
                            tempdt = dtList.Get_MaterialEmptydt();
                            break;
                        //房屋类型及装修工程类别信息管理
                        case 4:
                            tempdt = dtList.Get_HouseEmptydt();
                            break;
                    }
                    //将所选择的记录赋值至tempdt临时表内
                    foreach (DataGridViewRow row in gvdtl.SelectedRows)
                    {
                        //若各功能表体的主键不为空时,才进行记录(如:客户信息管理 Custid不为空时,就执行插入临时表操作)
                        if (row.Cells[1].Value.ToString() != "")
                        {
                            var row1 = tempdt.NewRow();
                            for (var i = 0; i < tempdt.Columns.Count; i++)
                            {
                                row1[i] = row.Cells[i].Value;
                            }
                            tempdt.Rows.Add(row1);
                        }
                    }
                    //若tempdt有值才赋值至_dtldt内,(供保存使用)
                    if (tempdt.Rows.Count > 0)
                    {
                        _deldt = tempdt;
                    }
                    //最后使用循环将所选择的行在GridView内删除
                    for (var i = gvdtl.SelectedRows.Count; i > 0; i--)
                    {
                        gvdtl.Rows.RemoveAt(gvdtl.SelectedRows[i - 1].Index);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 首页按钮(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveFirstItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将当前页变量PageCurrent=1; 2)并将“首页” 及 “上一页”按钮设置为不可用 将“下一页” “末页”按设置为可用
                _pageCurrent = 1;
                bnMoveFirstItem.Enabled = false;
                bnMovePreviousItem.Enabled = false;

                bnMoveNextItem.Enabled = true;
                bnMoveLastItem.Enabled = true;
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 上一页(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMovePreviousItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将PageCurrent自减 2)将“下一页” “末页”按钮设置为可用
                _pageCurrent--;
                bnMoveNextItem.Enabled = true;
                bnMoveLastItem.Enabled = true;
                //判断若PageCurrent=1的话,就将“首页” “上一页”按钮设置为不可用
                if (_pageCurrent == 1)
                {
                    bnMoveFirstItem.Enabled = false;
                    bnMovePreviousItem.Enabled = false;
                }
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 下一页按钮(GridView页面跳转时使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveNextItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将PageCurrent自增 2)将“首页” “上一页”按钮设置为可用
                _pageCurrent++;
                bnMoveFirstItem.Enabled = true;
                bnMovePreviousItem.Enabled = true;
                //判断若PageCurrent与“总页数”一致的话,就将“下一页” “末页”按钮设置为不可用
                if (_pageCurrent == _totalpagecount)
                {
                    bnMoveNextItem.Enabled = false;
                    bnMoveLastItem.Enabled = false;
                }
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 末页按钮(GridView页面跳转使用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnMoveLastItem_Click(object sender, EventArgs e)
        {
            try
            {
                //1)将“总页数”赋值给PageCurrent 2)将“下一页” “末页”按钮设置为不可用 并将 “上一页” “首页”按钮设置为可用
                _pageCurrent = _totalpagecount;
                bnMoveNextItem.Enabled = false;
                bnMoveLastItem.Enabled = false;

                bnMovePreviousItem.Enabled = true;
                bnMoveFirstItem.Enabled = true;

                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 跳转页文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BnPositionItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //判断所输入的跳转数必须为整数
                if (!Regex.IsMatch(bnPositionItem.Text, @"^-?[1-9]\d*$|^0$")) throw new Exception("请输入整数再继续");
                //判断所输入的跳转数不能大于总页数
                if (Convert.ToInt32(bnPositionItem.Text) > _totalpagecount) throw new Exception("所输入的页数不能超出总页数,请修改后继续");
                //判断若所填跳转数为0时跳出异常
                if (Convert.ToInt32(bnPositionItem.Text) == 0) throw new Exception("请输入大于0的整数再继续");

                //将所填的跳转页赋值至“当前页”变量内
                _pageCurrent = Convert.ToInt32(bnPositionItem.Text);
                //根据所输入的页数动态控制四个方向键是否可用
                //若为第1页，就将“首页” “上一页”按钮设置为不可用 将“下一页” “末页”设置为可用
                if (_pageCurrent == 1)
                {
                    bnMoveFirstItem.Enabled = false;
                    bnMovePreviousItem.Enabled = false;

                    bnMoveNextItem.Enabled = true;
                    bnMoveLastItem.Enabled = true;
                }
                //若为末页,就将"下一页" “末页”按钮设置为不可用 将“上一页” “首页”设置为可用
                else if (_pageCurrent == _totalpagecount)
                {
                    bnMoveNextItem.Enabled = false;
                    bnMoveLastItem.Enabled = false;

                    bnMovePreviousItem.Enabled = true;
                    bnMoveFirstItem.Enabled = true;
                }
                //否则四个按钮都可用
                else
                {
                    bnMoveFirstItem.Enabled = true;
                    bnMovePreviousItem.Enabled = true;
                    bnMoveNextItem.Enabled = true;
                    bnMoveLastItem.Enabled = true;
                }
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bnPositionItem.Text = Convert.ToString(_pageCurrent);
            }
        }

        /// <summary>
        /// 每页显示行数 下拉框关闭时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmshowrows_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //每次选择新的“每页显示行数”，都要 1)将_pageChange标记设为true(即执行初始化方法) 2)将“当前页”初始化为1
                _pageChange = true;
                _pageCurrent = 1;
                GridViewPageChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// GridView分页功能
        /// </summary>
        private void GridViewPageChange()
        {
            try
            {
                //获取查询的总行数
                var dtltotalrows = _dtl.Rows.Count;
                //获取“每页显示行数”所选择的行数
                var pageCount = Convert.ToInt32(tmshowrows.SelectedItem);
                //计算出总页数
                _totalpagecount = dtltotalrows % pageCount == 0 ? dtltotalrows / pageCount : dtltotalrows / pageCount + 1;
                //赋值"总页数"项
                bnCountItem.Text = $"/ {_totalpagecount} 页";

                //初始化BindingNavigator控件内的各子控件 及 对应初始化信息
                if (_pageChange)
                {
                    bnPositionItem.Text = Convert.ToString(1);                       //初始化填充跳转页为1
                    tmshowrows.Enabled = true;                                      //每页显示行数（下拉框）  

                    //初始化时判断;若“总页数”=1，四个按钮不可用；若>1,“下一页” “末页”按钮可用
                    if (_totalpagecount == 1)
                    {
                        bnMoveNextItem.Enabled = false;
                        bnMoveLastItem.Enabled = false;
                        bnMoveNextItem.Enabled = false;
                        bnMoveLastItem.Enabled = false;
                        bnPositionItem.Enabled = false;                             //跳转页文本框
                    }
                    else
                    {
                        bnMoveNextItem.Enabled = true;
                        bnMoveLastItem.Enabled = true;
                        bnPositionItem.Enabled = true;                             //跳转页文本框
                    }
                    _pageChange = false;
                }

                //显示_dtl的查询总行数
                tstotalrow.Text = $"共 {_dtl.Rows.Count} 行";

                //根据“当前页” 及 “固定行数” 计算出新的行数记录并进行赋值
                //计算进行循环的起始行
                var startrow = (_pageCurrent - 1) * pageCount;
                //计算进行循环的结束行
                var endrow = _pageCurrent == _totalpagecount ? dtltotalrows : _pageCurrent * pageCount;
                //复制 查询的DT的列信息（不包括行）至临时表内
                var tempdt = _dtl.Clone();
                //循环将所需的_dtl的行记录复制至临时表内
                for (var i = startrow; i < endrow; i++)
                {
                    tempdt.ImportRow(_dtl.Rows[i]);
                }

                //最后将刷新的DT重新赋值给GridView
                gvdtl.DataSource = tempdt;
                //将“当前页”赋值给"跳转页"文本框内
                bnPositionItem.Text = Convert.ToString(_pageCurrent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 连接GridView页面跳转功能
        /// </summary>
        /// <param name="dt"></param>
        private void LinkGridViewPageChange(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                _dtl = dt;
                panel6.Visible = true;
                //初始化下拉框所选择的默认值
                tmshowrows.SelectedItem = "10";
                //定义初始化标记
                _pageChange = true;
                //GridView分页
                GridViewPageChange();
            }
            //注:当为空记录时,不显示跳转页;只需将临时表赋值至GridView内
            else
            {
                gvdtl.DataSource = dt;
                panel6.Visible = false;
            }
        }

        /// <summary>
        /// 检测到所选中的行中是否有已在其它地方使用的情况
        /// </summary>
        /// <param name="typeid">0:删除分组功能使用 1:删除所选行功能使用</param>
        /// <returns></returns>
        private bool CheckCanDel(int typeid)
        {
            var result = true;
            try
            {
                switch (typeid)
                {
                    //'删除分组'按钮使用
                    case 0:
                        task.Data = (DataTable) gvdtl.DataSource;
                        break;
                    //'删除所选行'按钮使用
                    case 1:
                        task.Datarow = gvdtl.SelectedRows;
                        break;
                }
                task.TaskId = 1;
                task.FunctionId = "1.6";
                task.FunctionName = Convert.ToString(typeid);
                task.Pid = GlobalClasscs.Basic.BasicId; //1:客户信息管理 2:供应商信息管理 3:材料信息管理 4:房屋类型及装修工程类别信息管理
                task.StartTask();
                result = task.ResultMark;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

    }
}
