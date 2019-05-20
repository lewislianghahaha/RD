using System;
using System.Data;
using System.Data.SqlClient;

namespace RD.DB.Search
{
    //注:此类包括了对数据进行查询的功能
    public class SearchDt
    {
        SqlList sqlList=new SqlList();
        DtList dlDtList=new DtList();

        #region 基础信息库

        /// <summary>
        /// 基础信息库各模块查询使用
        /// </summary>
        /// <param name="functionName">功能名</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <param name="parentId">主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容</param>
        /// <returns></returns>
        public DataTable GetBdTableDt(string functionName, string functionType, string parentId)
        {
            var dt = new DataTable();

            try
            {
                dt = GetRecord(functionName, functionType, parentId);
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// 根据查询框获取记录(注:当查询框有值时使用)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="searchName"></param>
        /// <param name="searchValue"></param>
        /// <param name="dtdtl"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetBdSearchValueRecord(string functionName, string searchName, string searchValue, DataTable dtdtl, int pid)
        {
            var dt = new DataTable();

            try
            {
                dt = GetSearchValueRecord(functionName, searchName, searchValue, dtdtl, pid);
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 作用:1)初始化树形列表表头内容 2)初始化GridView表体内容 3)点击某节点读取表体内容
        /// </summary>
        /// <param name="functionName">功能名:决定是用那个系列表格</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <param name="parentId">主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容</param>
        /// <returns></returns>
        private DataTable GetRecord(string functionName, string functionType, string parentId)
        {
            var dt = new DataTable();
            var resultdt = new DataTable();
            var sqlscript = string.Empty;

            switch (functionName)
            {
                //客户信息管理
                case "Customer":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("0", null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("1", null)
                            : sqlList.BD_SQLList("2", parentId));
                    break;
                //供应商管理
                case "Supplier":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("3", null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("4", null)
                            : sqlList.BD_SQLList("5", parentId));
                    break;
                //材料信息管理
                case "Material":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("6", null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("7", null)
                            : sqlList.BD_SQLList("8", parentId));
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("9", null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("10", null)
                            : sqlList.BD_SQLList("11", parentId));
                    break;
                //房屋类型及装修工程类别信息管理-类别项目名称
                case "HouseProject":
                    if (functionType == "G")
                        sqlscript = sqlList.BD_SQLList("12", parentId);
                    break;
            }

            var sqlDataAdapter = new SqlDataAdapter(sqlscript, GetConn());
            sqlDataAdapter.Fill(dt);
            //这里区分表头或表体输出的效果 注:表体:无论是否有值都是要将记录填充到临时表
            if (functionType == "T")
            {
                resultdt = dt;
            }
            else
            {
                resultdt = dt.Rows.Count == 0 ? GetTempdt(functionName) : dt;
            }
            return resultdt;
        }

        /// <summary>
        /// 根据查询框获取记录(注:当查询框有值时使用)
        /// </summary>
        /// <param name="functionName">功能名:决定是用那个系列表格</param>
        /// <param name="searchName">查询选择列名-查询框有值时使用</param>
        /// <param name="searchValue">查询所填值-查询框有值时使用</param>
        /// <param name="dtdtl"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private DataTable GetSearchValueRecord(string functionName, string searchName, string searchValue, DataTable dtdtl, int pid)
        {
            var sqlscript = string.Empty;
            var tempdt = new DataTable();
            var dt = new DataTable();

            //当点击了"ALL"节点或点击"查询"按钮时使用
            if (pid == -1)
            {
                sqlscript = $"{searchName} like '" + '%' + searchValue + '%' + "'";
            }
            //当点击某一节点(不包括ALL节点)时使用
            else
            {
                sqlscript = $"{searchName} like '" + '%' + searchValue + '%' + "' and id = '" + pid + "'";
            }

            switch (functionName)
            {
                //"客户信息管理"
                case "Customer":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    var custrows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = custrows.Length == 0 ? tempdt : IntoTempdt(custrows, tempdt, tempdt.Columns.Count);
                    break;
                //"供应商信息管理"
                case "Supplier":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    var suprows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = suprows.Length == 0 ? tempdt : IntoTempdt(suprows, tempdt, tempdt.Columns.Count);
                    break;
                //材料信息管理
                case "Material":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    var materialrows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = materialrows.Length == 0 ? tempdt : IntoTempdt(materialrows, tempdt, tempdt.Columns.Count);
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    var houserows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = houserows.Length == 0 ? tempdt : IntoTempdt(houserows, tempdt, tempdt.Columns.Count);
                    break;
                //装修工程类别
                case "HouseProject":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    var houseProjectrows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = houseProjectrows.Length == 0 ? tempdt : IntoTempdt(houseProjectrows, tempdt, tempdt.Columns.Count);
                    break;
            }
            return dt;
        }

        /// <summary>
        /// 将记录填充至临时表内
        /// </summary>
        /// <param name="rows">根据DT查询后返回过来的行数数组</param>
        /// <param name="tempdt">空白表</param>
        /// <param name="tempdtColNum">空白表列数</param>
        /// <returns></returns>
        private DataTable IntoTempdt(DataRow[] rows, DataTable tempdt, int tempdtColNum)
        {
            foreach (var i in rows)
            {
                var row = tempdt.NewRow();
                for (var j = 0; j < tempdtColNum; j++)
                {
                    row[j] = i[j];
                }
                tempdt.Rows.Add(row);
            }
            return tempdt;
        }

        /// <summary>
        /// 根据功能ID获取对应的空白临时表
        /// </summary>
        /// <param name="functionName">功能名,作用:获取空白表格</param>
        /// <returns></returns>
        public DataTable GetTempdt(string functionName)
        {
            var resultDt = new DataTable();
            switch (functionName)
            {
                //客户信息管理
                case "Customer":
                    resultDt = dlDtList.Get_CustEmptydt();
                    break;
                //供应商管理
                case "Supplier":
                    resultDt = dlDtList.Get_SupplierEmptydt();
                    break;
                //材料信息管理
                case "Material":
                    resultDt = dlDtList.Get_MaterialEmptydt();
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    resultDt = dlDtList.Get_HouseEmptydt();
                    break;
                //房屋类型及装修工程类别信息管理-类别项目名称
                case "HouseProject":
                    resultDt = dlDtList.Get_HouseProjectEmptydt();
                    break;
                //室内装修工程单
                case "AdornOrder":
                    resultDt = dlDtList.Get_AdornEmptydt();
                    break;
                //室内主材单
                case "MaterialOrder":
                    resultDt = dlDtList.Get_ProMaterialEmtrydt();
                    break;
                //室内装修工程单-表头记录(用于生成单据头使用)
                case "AdornOrderHead":
                    resultDt = dlDtList.Get_ProAdorndt();
                    break;
                //室内主材单-表头记录(用于生成单据头使用)
                case "MaterialOrderHead":
                    resultDt = dlDtList.Get_ProMaterialdt();
                    break;
                //室内主材单-树菜单记录(用于生成室内主材单-树菜单信息记录)
                case "MaterialOrderTree":
                    resultDt = dlDtList.Get_ProMaterialTreedt();
                    break;
            }
            return resultDt;
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConn()
        {
            var conn = new Conn();
            var sqlcon = new SqlConnection(conn.GetConnectionString());
            return sqlcon;
        }

        /// <summary>
        /// 根据功能名称获取对应列名并形成Dt(查询框下拉列表使用)
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public DataTable SearchColList(string functionName)
        {
            var ds = new DataSet();
            var sqlscript = sqlList.BD_FunList(functionName);
            var sqlDataAdapter = new SqlDataAdapter(sqlscript, GetConn());
            sqlDataAdapter.Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 基础信息库-弹出明细窗体使用(初始化时使用)
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <returns></returns>
        public DataTable SearchInitializeDtl(string functionName)
        {
            var dt = new DataTable();
            var resultdt = new DataTable();
            var sqlscript = string.Empty;

            switch (functionName)
            {
                //客户信息管理
                case "Customer":
                    sqlscript = sqlList.BD_SQLList("1", null);
                    break;
                //供应商管理
                case "Supplier":
                    sqlscript = sqlList.BD_SQLList("4", null);
                    break;
                //材料信息管理
                case "Material":
                    sqlscript = sqlList.BD_SQLList("7", null);
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    sqlscript = sqlList.BD_SQLList("10.1", null);
                    break;
            }
            var sqlDataAdapter = new SqlDataAdapter(sqlscript, GetConn());
            sqlDataAdapter.Fill(dt);
            resultdt = dt.Rows.Count == 0 ? GetTempdt(functionName) : dt;
            return resultdt;
        }

        /// <summary>
        /// 基础信息库-弹出明细窗体使用(查询值时使用)
        /// </summary>
        /// <returns></returns>
        public DataTable SearchShowDtl(string functionName, string searchName, string searchValue, DataTable searchdt)
        {
            var dt = new DataTable();

            try
            {
                dt = GetSearchValueRecord(functionName, searchName, searchValue, searchdt, -1);
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 室内装修工程(装修工程类别下拉列表使用)
        /// </summary>
        /// <returns></returns>
        public DataTable SearchCustList()
        {
            var sqlscript = sqlList.CustInfoList();
            var dt = GetData(sqlscript);
            return dt;
        }

        #endregion

        #region 室内装修工程

        /// <summary>
        /// 室内装修工程(装修工程类别下拉列表使用)
        /// </summary>
        /// <returns></returns>
        public DataTable SearchHouseType()
        {
            var sqlscript = sqlList.Order_Adorn_SearchDropDownList();
            var dt = GetData(sqlscript);
            return dt;
        }

        /// <summary>
        /// 读取表体信息(作用:初始化GridView内容)
        /// </summary>
        /// <param name="funState">单据状态</param>
        /// <param name="functionname"></param>
        /// <param name="pid">表头ID</param>
        /// <returns></returns>
        public DataTable SearchOrderdtl(string funState,string functionname,int pid)
        {
            var reslut = new DataTable();

            try
            {
                //获取相关SQL查询语句
                var sqlscript = sqlList.Order_SearchDtl(functionname,pid);
                //执行查询功能并将结填充到DataTable内
                var dt = GetData(sqlscript);

                //若返回的DataTable行数为0,或单据状态为C(创建)时,就返回空白表
                if (dt.Rows.Count == 0 || funState == "C")
                {
                    reslut = dlDtList.Get_AdornEmptydt();
                }
                //否则返回有值的结果集
                else
                {
                    reslut = dt;
                }
            }
            catch (Exception ex)
            {
                reslut.Rows.Clear();
                reslut.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return reslut;
        }

        /// <summary>
        /// 读取“室内装修工程单”表头信息(TreeView使用)
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable SearchOrderTreeView(string functionname,int pid)
        {
            var dt = new DataTable();

            try
            {
                //获取相关SQL查询语句
                var sqlscript = sqlList.Order_TreeView(functionname,pid);
                //执行查询功能并将结填充到DataTable内
                dt = GetData(sqlscript);
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 根据PID获取T_Pro_Adorn 或 T_Pro_Material表头信息
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable SearFirstOrderInfo(string functionname,int pid)
        {
            var dt = new DataTable();

            try
            {
                var sqlscript = sqlList.Get_OrderInfo(functionname, pid);
                dt = GetData(sqlscript);
            }
            catch (Exception)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw;
            }
            return dt;
        }

        /// <summary>
        /// 根据指定SQL语句执行并返回DT
        /// </summary>
        /// <param name="sqlscript"></param>
        /// <returns></returns>
        public DataTable GetData(string sqlscript)
        {
            var dt = new DataTable();
            try
            {
                var sqlDataAdapter = new SqlDataAdapter(sqlscript, GetConn());
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
            }
            return dt;
        }

        /// <summary>
        ///  根据获取树菜单节点刷新表体内容
        /// </summary>
        /// <param name="factionname"></param>
        /// <param name="pid">主键ID</param>
        /// <param name="treeid">树菜单ID treeid 当为-1时,表体读取全部记录</param>
        /// <param name="dropdownlistid">下拉列表ID</param>
        /// <returns></returns>
        public DataTable Get_Orderdtl(string factionname, int pid, int treeid,int dropdownlistid)
        {
            var resultdt=new DataTable();

            try
            {
                //获取对应SQL语句
                var sqlscript = sqlList.OrderInfo_dtl(factionname, pid, treeid,dropdownlistid);
                //获取对应临时表信息(当没有查询结果时使用)
                var tempdt = factionname == "AdornOrder" ? dlDtList.Get_AdornEmptydt() : dlDtList.Get_ProMaterialEmtrydt();
                //若返回行数为0，即需用对应空表(临时表)填充
                var dt = GetData(sqlscript);
                resultdt = dt.Rows.Count == 0 ? tempdt : dt;
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }


        #endregion

        #region 主窗体

        /// <summary>
        /// 根据指定的功能名称获取对应的dt
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <returns></returns>
        public DataTable SearchDropdownDt(string functionName)
        {
            var resultdt=new DataTable();

            try
            {
                var sqlscript=sqlList.Main_Admin_Dropdownlist(functionName);
                resultdt=GetData(sqlscript);
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }

        /// <summary>
        /// 查询内容
        /// </summary>
        /// <param name="custid">客户ID</param>
        /// <param name="yearid">年份</param>
        /// <param name="ordertypeId">单据类型 1:室内装修工程单 2:室内主材单</param>
        /// <param name="hTypeid">房屋类型ID</param>
        /// <param name="confirmfStatus">审核状态 Y:已审核 N:末审核</param>
        /// <param name="confirmdt">审核日期</param>
        /// <returns></returns>
        public DataTable Searchdtldt(int custid, int yearid, int ordertypeId, int hTypeid, string confirmfStatus,DateTime confirmdt)
        {
            var resultdt = new DataTable();
            DataTable tempdt;
            string sqlscript;

            try
            {
                //创建临时表(当没有查询到值时使用)
                tempdt = dlDtList.Get_Maindtl();
                //根据各参数获取对应的SQL语句
                //单据类型:1:室内装修工程 2:室内主材单
                sqlscript = ordertypeId == 1 ? sqlList.Main_Adorndtl(custid, yearid, hTypeid, confirmfStatus, confirmdt) : 
                                                sqlList.Main_Material(custid, yearid, hTypeid, confirmfStatus, confirmdt);

                //执行SQL语句,并返回DT
                var dt = GetData(sqlscript);
                resultdt = dt.Rows.Count == 0 ? tempdt : dt;
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }

        #endregion

        #region 帐户信息功能设定

        /// <summary>
        /// 查询内容(权限窗体)
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sexid"></param>
        /// <param name="closeid"></param>
        /// <param name="confirmstatus"></param>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public DataTable Admin_Searchdtldt(int userid, int sexid, string closeid, string confirmstatus, DateTime dtTime)
        {
            var resultdt=new DataTable();
            DataTable tempdt;
            string sqlscript;

            try
            {
                //创建临时表(当没有查询到值时使用)
                tempdt = dlDtList.Get_Admindtl();
                //获取SQL语句
                sqlscript = sqlList.Admindtl(userid, sexid, closeid, confirmstatus, dtTime);
                //执行SQL语句,并返回DT
                var dt = GetData(sqlscript);
                resultdt = dt.Rows.Count == 0 ? tempdt : dt;
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }

        /// <summary>
        /// 角色信息管理查询
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public DataTable Admin_Searchroledt(int roleid)
        {
            var resultdt = new DataTable();
            DataTable tempdt;
            string sqlscript;

            try
            {
                //创建临时表(当没有查询到值时使用)
                tempdt = dlDtList.Get_Admin_roledtl();
                //获取SQL语句
                sqlscript = sqlList.Admin_roledtl(roleid);
                //执行SQL语句,并返回DT
                var dt = GetData(sqlscript);
                resultdt = dt.Rows.Count == 0 ? tempdt : dt;
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }

        /// <summary>
        /// 角色信息管理-功能权限明细查询
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="funtypeid"></param>
        /// <returns></returns>
        public DataTable Admin_SearchRoleFundt(int roleid, int funtypeid)
        {
            var resultdt = new DataTable();
            DataTable tempdt;
            string sqlscript;

            try
            {
                //创建临时表(当没有查询到值时使用)
                tempdt = dlDtList.Get_Admin_roleFundtl();
                //获取SQL语句
                sqlscript = sqlList.Admin_roleFundtl(roleid,funtypeid);
                //执行SQL语句,并返回DT
                var dt = GetData(sqlscript);
                resultdt = dt.Rows.Count == 0 ? tempdt : dt;
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }

        /// <summary>
        /// 根据roleid获取T_AD_Role 或 T_AD_Fun表头内容
        /// </summary>
        /// <param name="markid">标记ID;0:读T_AD_Role记录 1:读T_AD_Fun记录</param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public DataTable SearchRoleHeadorFundt(int markid,int roleid)
        {
            var resultdt = new DataTable();
            var sqlscript=string.Empty;

            try
            {
                switch (markid)
                {
                    case 0:
                        sqlscript = sqlList.Get_RoleHead(roleid);
                        break;
                    case 1:
                        sqlscript = sqlList.Get_RoleDtl(roleid);
                        break;
                }

                //执行SQL语句,并返回DT
                resultdt = GetData(sqlscript);
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw;
            }
            return resultdt;
        }



        #endregion

    }
}
