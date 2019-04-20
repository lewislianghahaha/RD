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
                    resultDt = dlDtList.Get_ProAdorndt();
                    break;
                //室内主材单
                case "MaterialOrder":
                    resultDt = dlDtList.Get_ProMaterialdt();
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
        /// 读取表体信息
        /// </summary>
        /// <param name="funState">单据状态</param>
        /// <param name="pid">表头ID</param>
        /// <returns></returns>
        public DataTable SearchAdorndtl(string funState,int pid)
        {
            var reslut = new DataTable();

            try
            {
                //获取相关SQL查询语句
                var sqlscript = sqlList.Order_Adorn_SearchDtl(pid);
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
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable SearchAdornTreeView(int pid)
        {
            var dt = new DataTable();

            try
            {
                //获取相关SQL查询语句
                var sqlscript = sqlList.Order_Adorn_SearchTreeView(pid);
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
        /// <param name="factionname"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable SearFirstOrderInfo(string factionname,int pid)
        {
            var dt = new DataTable();

            try
            {
                var sqlscript = sqlList.Get_OrderInfo(factionname, pid);
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
        private DataTable GetData(string sqlscript)
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

        #endregion

        #region 室内主材单



        #endregion
    }
}
