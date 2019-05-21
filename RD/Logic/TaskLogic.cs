using System;
using System.Data;
using System.Windows.Forms;
using RD.Logic.Admin;
using RD.Logic.Basic;
using RD.Logic.ChangePwd;
using RD.Logic.Main;
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

        //室内装修工程 及 室内主材单使用
        OrderSearch orderSearch=new OrderSearch();
        OrderImport orderImport=new OrderImport();
        OrderDel odrderDel=new OrderDel();
        OrderGenerate orderGenerate=new OrderGenerate();

        //主窗体使用
        MainSearch mainSearch=new MainSearch();
        MainGenerate mainGenerate=new MainGenerate();

        //帐户角色权限使用
        AdminSearch adminSearch=new AdminSearch();
        AdminImport adminImport=new AdminImport();
        AdminGenerate adminGenerate=new AdminGenerate();

        #region 变量定义

            private int _taskid;             //记录中转ID
            private string _accountName;     //记录帐号名称
            private string _accountPwd;      //记录帐号密码
            private string _functionId;      //功能ID
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
            private int _custid;             //获取客户ID（室内装修工程单 及 室内主材单 主窗体使用）
            private int _confirmid;          //审核ID
            private DataTable _deldata;      //获取需要删除的表体记录信息(室内装修工程单 及 室内主材单使用)
            private int _treeid;             //树菜单ID(室内装修工程单 及 室内主材单使用)
            private int _dropdownlistid;     //下拉列表ID(室内装修工程单 及 室内主材单使用)
            private int _yearid;             //单据创建年份（主窗体使用）
            private int _ordertypeId;        //单据类型(主窗体使用)
            private int _hTypeid;            //房屋类型(主窗体使用)
            private string _confirmfStatus;  //审核状态(主窗体使用)
            private DateTime _dtime;            //日期(主窗体使用)
            private DataGridViewSelectedRowCollection _datarow; //保存从GridView中所选择的行

            private int _userid;             //职员名称ID(权限窗体使用)
            private int _sexid;              //职员性别ID(权限窗体使用)
            private string _closeid;         //职员帐号关闭状态(权限窗体使用)
            private int _roleid;             //角色ID(权限窗体使用)
            private string _canallmark;      //管理员权限标记         

            private DataTable _resultTable;  //返回DT类型
            private bool _resultMark;        //返回是否成功标记
            private int _orderid;            //返回生成后的单据主键ID(室内装修工程单 及 室内主材单使用)
            private int _funtypeid;          //返回功能大类ID

        #endregion

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

        /// <summary>
        /// //获取客户ID（室内装修工程单 及 室内主材单使用）
        /// </summary>
        public int Custid { set { _custid = value; } }

        /// <summary>
        /// 审核ID
        /// </summary>
        public int Confirmid {set { _confirmid = value; } }

        /// <summary>
        /// 获取需要删除的表体记录信息(室内装修工程单 及 室内主材单使用)
        /// </summary>
        public DataTable Deldata { set { _deldata = value; } }

        /// <summary>
        /// 树菜单ID(室内装修工程单 及 室内主材单使用)
        /// </summary>
        public int Treeid { set { _treeid = value; } }

        /// <summary>
        /// 返回生成后的单据主键ID(室内装修工程单 及 室内主材单使用)
        /// </summary>
        public int Dropdownlistid { set { _dropdownlistid = value; } }

        /// <summary>
        /// 单据创建年份（主窗体使用）
        /// </summary>
        public int Yearid { set { _yearid = value; } }

        /// <summary>
        /// 单据类型(主窗体使用)
        /// </summary>
        public int OrdertypeId { set { _ordertypeId = value; } }

        /// <summary>
        /// 房屋类型(主窗体使用)
        /// </summary>
        public int HTypeid { set { _hTypeid = value; } }

        /// <summary>
        /// 审核状态(主窗体使用)
        /// </summary>
        public string ConfirmfStatus { set { _confirmfStatus = value; } }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Dtime { set { _dtime = value; } }

        /// <summary>
        /// 保存从GridView中所选择的行
        /// </summary>
        public DataGridViewSelectedRowCollection Datarow { set { _datarow = value; } }

        /// <summary>
        /// 职员名称ID
        /// </summary>
        public int Userid { set { _userid = value; } }

        /// <summary>
        /// 职员性别ID(权限窗体使用)
        /// </summary>
        public int Sexid {set { _sexid = value; } }

        /// <summary>
        ///  //职员帐号关闭状态(权限窗体使用)
        /// </summary>
        public string Closeid {set { _closeid = value; } }        

        /// <summary>
        /// 角色ID(权限窗体使用)
        /// </summary>
        public int Roleid { set { _roleid = value; } }

        /// <summary>
        /// 返回功能大类ID
        /// </summary>
        public int Funtypeid { set { _funtypeid = value; } }

        /// <summary>
        /// 管理员权限标记
        /// </summary>
        public string Canallmark { set { _canallmark = value; } }

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

        /// <summary>
        /// 返回生成后的单据编号(室内装修工程单 及 室内主材单使用)
        /// </summary>
        public int Orderid => _orderid;

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
                //单据信息(包括：室内装修工程单 及 室内主材单)
                case 2:
                    OrderInfo(_functionId,_functinName,_funState,_pid,_treeName,_id,_custid,_accountName,_data,_confirmid,_deldata,_treeid,_dropdownlistid);
                    break;
                //帐户信息功能设定(帐号为:Admin时使用)
                case 3:
                    AdminInfo(_functionId, _functinName,_userid,_sexid,_closeid,_confirmfStatus,_dtime,_roleid,_funtypeid,_data,_accountName, 
                              _canallmark,_confirmid,_datarow,_id);
                    break;
                //Main窗体使用(注:包括查询，审核，反审核，导出功能)
                case 4:
                    MainInfo(_functionId,_functinName, _custid, _yearid, _ordertypeId, _hTypeid, _confirmfStatus, _dtime, _confirmid,_datarow);
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
                //查询 作用:客户类型名称下拉列表 CustInfoFrm窗体使用
                case "1.5":
                    _resultTable = search.GetCustList();
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

                //审核（反审核）
                case "4":

                    break;
            }
        }

        /// <summary>
        /// 单据信息(包括：室内装修工程单 及 室内主材单)
        /// </summary>
        /// <param name="functionId">功能ID</param>
        /// <param name="functionName">功能名称</param>
        /// <param name="funState">单据状态 C:创建 R:读取</param>
        /// <param name="pid">表头ID</param>
        /// <param name="treeName">节点名称</param>
        /// <param name="id">上级节点ID</param>
        /// <param name="custid">客户ID</param>
        /// <param name="accountName">帐号名称</param>
        /// <param name="dt">初始化后的DT,获取数据库对应表的全部信息;</param>
        /// <param name="confirmid">审核ID</param>
        /// <param name="deldt">获取需要删除的表体记录信息</param>
        /// <param name="treeid">树菜单ID</param>
        /// <param name="dropdownlistid">下拉列表ID</param>
        private void OrderInfo(string functionId,string functionName,string funState,int pid,string treeName,int id,
                                  int custid,string accountName,DataTable dt,int confirmid,DataTable deldt,int treeid,int dropdownlistid)
        {
            switch (functionId)
            {
                //查询(作用:下拉列表使用)
                case "1":
                    _resultTable = orderSearch.GetHouseTypeDtl();
                    break;
                //查询(作用:初始化树菜单内容)
                case "1.1":
                    _resultTable = orderSearch.Get_OrderTreeView(functionName,pid);
                    break;
                //查询(作用:初始化GridView内容)
                case "1.2":
                    _resultTable = orderSearch.Get_OrderDtl(funState,functionName,pid);
                    break;
                //查询(作用:根据PID获取T_Pro_Adorn 或 T_Pro_Material表头信息)
                case "1.3":
                    _resultTable = orderSearch.Get_FirstOrderInfo(functionName,pid);
                    break;
                //查询(作用:树菜单节点击刷新 及 下拉列表刷新使用)
                case "1.4":
                    _resultTable = orderSearch.Get_Orderdtl(functionName, pid,treeid, dropdownlistid);
                    break;

                //保存(作用:对树形菜单进行导入 新增分组时使用)
                case "2":
                    _resultMark = orderImport.InsertTreeRd(functionName,id,pid, treeName);
                    break;
                //更新树形菜单(作用:编辑分组时使用)
                case "2.1":
                    _resultMark = orderImport.UpdateTreeRd(functionName,pid, treeName);
                    break;
                //保存(作用:对表体GridView进行导入) (注:包括插入及更新 及删除操作)
                case "2.2":
                    _resultMark = orderImport.Save_OrderEntry(functionName,dt,deldt);
                    break;
                //保存(作用:导入信息至表头T_PRO_Adorn 或 T_PRO_Material 注:插入成功后,返回单据ID,若异常返回0) 生成单据表头时使用
                case "2.3":
                    _orderid = orderImport.InsertOrderFirstDt(functionName,custid,accountName);
                    break;
                //保存(作用:对“室内主材单”树型菜单信息导入->将“材料信息管理”的大类插入至对应的T_PRO_MaterialTree内)
                case "2.4":
                    _resultMark =orderImport.InsertMaterialIntoDt(functionName,pid);
                    break;

                //删除节点及对应的表体信息
                case "3":
                    _resultMark = odrderDel.DelBD_Record(functionName, pid, dt);
                    break;

                //审核（反审核）
                case "4":
                    _resultMark = orderGenerate.ConfirmOrderDtl(functionName,confirmid,pid);
                    break;
            }
        }

        /// <summary>
        /// 帐户信息功能设定
        /// </summary>
        /// <param name="functionId">功能ID</param>
        /// <param name="functionName">功能名称</param>
        /// <param name="userid">帐号ID</param>
        /// <param name="sexid">性别ID</param>
        /// <param name="closeid">关闭状态ID</param>
        /// <param name="confirmstatus">审核状态ID</param>
        /// <param name="dtTime">入职日期</param>
        /// <param name="roleid">角色ID</param>
        /// <param name="funtypeid">功能大类ID</param>
        /// <param name="dt">功能大类名称DT</param>
        /// <param name="accountName">帐户名称</param>
        /// <param name="canallmark">管理员权限标记</param>
        /// <param name="confirmid">审核ID</param>
        /// <param name="datarow">保存从GridView选择的行</param>
        /// <param name="id">获取关闭等相关标记信息</param>
        private void AdminInfo(string functionId, string functionName,int userid,int sexid,string closeid,string confirmstatus,
                               DateTime dtTime,int roleid,int funtypeid,DataTable dt,string accountName,string canallmark,int confirmid,
                               DataGridViewSelectedRowCollection datarow,int id)
        {
            switch (functionId)
            {
                //下拉列表初始化
                case "1":
                    _resultTable = adminSearch.SearchDropdownDt(functionName);
                    break;
                //查询功能(根据所选择的下拉列表参数，查询结果并返回DT；若没有，返回空表)
                case "1.1":
                    _resultTable = adminSearch.Searchdtldt(userid,sexid,closeid, confirmstatus,dtTime);
                    break;
                //查询功能(角色信息管理查询)
                case "1.2":
                    _resultTable = adminSearch.SearchRoledt(roleid);
                    break;
                //查询功能(角色信息管理-功能权限明细查询)
                case "1.3":
                    _resultTable = adminSearch.SearchRoleFundt(roleid,funtypeid);
                    break;
                //查询(作用:利用roleid获取T_AD_Role表内容)
                case "1.4":
                    _resultTable = adminSearch.SearchRoleHeaddt(roleid);
                    break;
                
                //根据相关条件将功能权限记录插入至T_AD_ROLEDTL内
                case "2":
                    _orderid = adminImport.InsertDtlIntoRole(functionName,dt, accountName);
                    break;

                //更新功能(更新:角色名称 管理员权限T_AD_Role)
                case "3":
                    _resultMark = adminImport.UpdateRole(functionName,roleid,canallmark);
                    break;

                //审核(反审核) 针对T_AD_Role进行操作 datarow 作用:从GridView中所选择的行(注:角色信息管理界面-批量选择多行时使用)
                case "4":
                    _resultMark = adminGenerate.ConfirmRoleFunOrderDtl(confirmid,roleid,datarow);
                    break;

                //关闭(反关闭) 针对T_AD_Role进行操作 datarow 作用:从GridView中所选择的行(注:角色信息管理界面-批量选择多行时使用)
                case "5":
                    _resultMark = adminGenerate.CloseRoleFunOrderDtl(id,roleid,datarow);
                    break;
                //针对T_AD_RoleDtl进行'显示' ‘反审核’ ‘能删除’ 权限标记改变
                case "6":
                    _resultMark = adminGenerate.Update_RoleFunStatus(functionName,funtypeid, dt);
                    break;
            }
        }

        /// <summary>
        /// 主窗体
        /// </summary>
        /// <param name="functionId">功能ID</param>
        /// <param name="functionName">功能名称</param>
        /// <param name="custid">客户ID</param>
        /// <param name="yearid">单据创建年份ID</param>
        /// <param name="ordertypeId">单据类型ID</param>
        /// <param name="hTypeid">房屋类型ID</param>
        /// <param name="confirmfStatus">审核状态</param>
        /// <param name="confirmdt">审核日期</param>
        /// <param name="confirmid">审核ID</param>
        /// <param name="datarow">保存从GridView选择的行</param>
        private void MainInfo(string functionId,string functionName,int custid,int yearid,int ordertypeId,int hTypeid,string confirmfStatus, DateTime confirmdt,
                              int confirmid, DataGridViewSelectedRowCollection datarow)
        {
            switch (functionId)
            {
                //查询(初始化各下拉列表)
                case "1":
                    _resultTable = mainSearch.SearchDropdownDt(functionName);
                    break;
                //查询(根据所选择的下拉列表参数，查询结果并返回DT；若没有，返回空表)
                case "1.1":
                    _resultTable = mainSearch.Searchdtldt(custid, yearid, ordertypeId, hTypeid, confirmfStatus, confirmdt);
                    break;

                //审核(反审核)
                case "2":
                    _resultMark = mainGenerate.Main_ConfirmOrderDtl(functionName, confirmid,datarow);
                    break;
                //导出-EXCEL
                case "3":

                    break;
                //打印
                case "4":

                    break;
            }
        }
    }
}
