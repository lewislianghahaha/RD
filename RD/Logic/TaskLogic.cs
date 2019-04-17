using System.Data;
using RD.Logic.Basic;
using RD.Logic.ChangePwd;
using RD.Logic.Order;

namespace RD.Logic
{
    public class TaskLogic
    {
        //基础信息库使用
        ChangeData change=new ChangeData();
        Search search=new Search();
        Import import=new Import();
        Del del=new Del();

        //室内装修工程 及 主材单使用
        OrderSearch orderSearch=new OrderSearch();
        OrderImport orderImport=new OrderImport();
        OrderDel odrderDel=new OrderDel();

        private int _taskid;             //记录中转ID
        private string _accountName;     //记录帐号名称
        private string _accountPwd;      //记录帐号密码
        private string _functionId;      //记录"基础信息库"内的功能ID(创建:0 查询:1 保存:2 审核:3)
        private string _functinName;     //功能名称(确定是使用那个表系列)
        private string _functionType;    //记录表格类型ID(T:0 表体:1)
        private string _parentId;        //主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容
        private string _searchName;      //查询选择列名-查询框有值时使用
        private string _searchValue;     //查询所填值-查询框有值时使用
        private DataTable _data;         //获取初始化的表体信息
        private int _pid;                //获取父级节点ID(新增或更新树形节点时使用)
        private string _treeName;        //获取同级节点时使用(新增或更新树形节点时使用)
        private string _funState;        //获取单据状态(室内装修工程单 及 室内主材单使用) R:读取 C:创建
        private int _id;                 //获取上上级节点ID(室内装修工程单 及 室内主材单使用)

        private DataTable _resultTable;  //返回DT类型
        private bool _resultMark;        //返回是否成功标记

        #region Set

        /// <summary>
        /// 中转ID
        /// </summary>
        public int TaskId { set { _taskid = value; } }

        /// <summary>
        /// 接收帐号名称
        /// </summary>
        public string AccountName { set { _accountName = value; } }

        /// <summary>
        /// 接收帐号密码
        /// </summary>
        public string AccountPwd { set { _accountPwd = value; } }

        /// <summary>
        /// 记录"基础信息库"内的功能ID(创建:0 查询:1 保存:2 审核:3)
        /// </summary>
        public string FunctionId { set { _functionId = value; } }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { set { _functinName = value; } }

        /// <summary>
        /// 表格类型(T:表头 G:表体)
        /// </summary>
        public string FunctionType { set { _functionType = value; } }

        /// <summary>
        /// 查询选择列名-查询框有值时使用
        /// </summary>
        public string SearchName { set { _searchName = value; } }

        /// <summary>
        /// 查询所填值-查询框有值时使用
        /// </summary>
        public string SearchValue { set { _searchValue = value; } }

        /// <summary>
        /// 主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容
        /// </summary>
        public string ParentId { set { _parentId = value; } }

        /// <summary>
        /// 获取初始化后的表体信息(用于后台查询时使用)
        /// </summary>
        public DataTable Data { set { _data = value; } }

        /// <summary>
        /// 获取上级或自身节点ID(注:新增时获取上级ID  更新时获取自身ID)
        /// </summary>
        public int Pid { set { _pid = value; } }

        /// <summary>
        /// 获取同级节点时使用(新增或更新树形节点时使用)
        /// </summary>
        public string TreeName { set { _treeName = value; } }

        /// <summary>
        /// 获取单据状态(室内装修工程单 及 室内主材单使用) R:读取 C:创建
        /// </summary>
        public string FunState { set { _funState = value; } }

        /// <summary>
        /// 获取上上级节点ID(室内装修工程单 及 室内主材单使用)
        /// </summary>
        public int Id { set { _id = value; } }

        #endregion

        #region Get

        /// <summary>
        ///返回DataTable至主窗体
        /// </summary>
        public DataTable ResultTable => _resultTable;

        /// <summary>
        /// 返回结果标记
        /// </summary>
        public bool ResultMark => _resultMark;

        #endregion

        public void StartTask()
        {
            switch (_taskid)
            {
                //帐号密码修改
                case 0:
                    ChangeRecord(_accountName,_accountPwd);
                    break;
                //基础信息库
                case 1:
                    BasicInfo(_functionId,_functinName,_functionType,_parentId,_searchName,_searchValue,_data,_pid,_treeName,_accountName);
                    break;
                //室内装修工程单
                case 2:
                    PrdAdornInfo(_functionId,_functinName,_funState,_pid,_treeName,_id);
                    break;
                //主材单
                case 3:
                    PrdMaterialInfo(_functionId);
                    break;
                //导出EXCEL(Main窗体使用)
                case 4:

                    break;
                //打印(Main窗体使用)
                case 5:

                    break;
                //帐户信息功能设定(帐号为:Admin时使用)
                case 6:

                    break;
                //查询功能(Main窗体使用)
                case 7:

                    break;
            }
        }

        /// <summary>
        /// 帐号密码修改
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userNewPwd"></param>
        private void ChangeRecord(string userName,string userNewPwd)
        {
            _resultMark = change.ChangeRecord(userName,userNewPwd);
        }

        /// <summary>
        /// 基础信息库
        /// </summary>
        /// <param name="functionId">功能ID(创建:0 查询:1 保存:2 审核:3)</param>
        /// <param name="functionName">功能名</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <param name="parentId">主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或 获取对应功能表体的全部内容</param>
        /// <param name="searchName">查询选择列名-查询框有值时使用</param>
        /// <param name="searchValue">查询所填值-查询框有值时使用</param>
        /// <param name="dt">初始化后的DT,已获取数据库对应表 表体的全部信息;作用:查询表体时使用到</param>
        /// <param name="pid">获取父级节点ID(新增或更新树形节点时使用)</param>
        /// <param name="treeName">获取同级节点时使用(新增或更新树形节点时使用)</param>
        /// <param name="accountName">获取帐号名称</param>
        private void BasicInfo(string functionId,string functionName,string functionType,string parentId,
                               string searchName, string searchValue,DataTable dt,int pid,string treeName,string accountName)
        {
            switch (functionId)
            {
                //查询 作用:1)初始化树形列表表头内容 2)初始化GridView表体内容 3)点击某节点读取表体内容(当查询框没有值时)
                case "1":
                    _resultTable = search.GetData(functionName,functionType,parentId);
                    break;
                //查询(作用:1)查询按钮时使用 2)点击某节点读取表体内容(当查询框值时))
                case "1.1":
                    _resultTable = search.GetBdSearchData(functionName, searchName, searchValue,dt,pid);
                    break;
                //查询 作用:根据功能名查询出对应的列名并形成DataTable(查询框下拉列表使用)
                case "1.2":
                    _resultTable = search.GetColDropDownList(functionName);
                    break;
                //查询 作用:明细窗体查询(初始化使用)
                case "1.3":
                    _resultTable = search.GetInitializeDtl(functionName);
                    break;
                //查询 作用:明细窗体查询(查询值时使用)
                case "1.4":
                    _resultTable = search.GetSearchDt(functionName, searchName, searchValue, dt);
                    break;

                //保存(作用:对表体GridView进行导入) (注:包括插入及更新操作)
                case "2":
                    _resultMark = import.Save_BaseEntry(functionName,dt,pid,accountName);
                    break;
                //保存(作用:对树形菜单进行导入 新增分组时使用)
                case "2.1":
                    _resultMark = import.InsertTreeRd(functionName, -1,pid, treeName);
                    break;
                //更新树形菜单(作用:编辑分组时使用)
                case "2.2":
                    _resultMark = import.UpdateTreeRd(functionName,pid,treeName);
                    break;

                //删除节点及对应的信息
                case "3":
                    _resultMark = del.DelBD_Record(functionName,pid,dt);
                    break;
                //审核
                case "4":

                    break;
            }
        }

        /// <summary>
        /// 室内装修工程单
        /// </summary>
        /// <param name="functionId">功能ID</param>
        /// <param name="functionName"></param>
        /// <param name="funState">单据状态 C:创建 R:读取</param>
        /// <param name="pid">表头ID</param>
        /// <param name="treeName">节点名称</param>
        /// <param name="id">上上级节点ID(室内装修工程单 及 室内主材单使用)</param>
        private void PrdAdornInfo(string functionId,string functionName,string funState,int pid,string treeName,int id)
        {
            switch (functionId)
            {
                //查询(作用:下拉列表使用)
                case "1":
                    _resultTable = orderSearch.GetHouseTypeDtl();
                    break;
                //查询(作用:初始化树菜单内容)
                case "1.1":
                    _resultTable = orderSearch.Get_AdornTreeView(pid);
                    break;
                //查询(作用:初始化GridView内容)
                case "1.2":
                    _resultTable = orderSearch.Get_AdornDtl(funState,pid);
                    break;
                //保存(作用:对树形菜单进行导入 新增分组时使用)
                case "2":
                    _resultMark = orderImport.InsertTreeRd(functionName,id,pid, treeName);
                    break;
                //更新树形菜单(作用:编辑分组时使用)
                case "2.1":
                    _resultMark = orderImport.UpdateTreeRd(functionName,pid, treeName);
                    break;
                //保存(作用:对表体GridView进行导入)
                case "2.2":

                    break;
                //删除节点及对应的信息
                case "3":

                    break;
                //审核
                case "4":

                    break;
            }
        }

        /// <summary>
        /// 主材单
        /// </summary>
        /// <param name="functionId"></param>
        private void PrdMaterialInfo(string functionId)
        {
            switch (functionId)
            {
                
            }
        }

    }
}
