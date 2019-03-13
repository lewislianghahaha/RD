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

        /// <summary>
        /// 基础信息库各模块查询使用
        /// </summary>
        /// <param name="functionName">功能名</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <param name="parentId">主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容</param>
        /// <returns></returns>
        public DataTable GetBdTableDt(string functionName, string functionType,string parentId)
        {
            var dt=new DataTable();

            try
            {
                dt = GetRecord(functionName, functionType,parentId);
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
        /// <returns></returns>
        public DataTable GetBdSearchValueRecord(string functionName, string searchName, string searchValue,DataTable dtdtl)
        {
            var dt = new DataTable();
            var resultDt = new DataTable();

            try
            {
                dt = GetSearchValueRecord(functionName,searchName,searchValue,dtdtl);
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return resultDt;
        }

        /// <summary>
        /// 作用:1)初始化树形列表表头内容 2)初始化GridView表体内容 3)点击某节点读取表体内容
        /// </summary>
        /// <param name="functionName">功能名:决定是用那个系列表格</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <param name="parentId">主键ID,用于表体查询时使用 注:当为null时,表示按了"全部"树形列表节点 或获取对应功能表体的全部内容</param>
        /// <returns></returns>
        private DataTable GetRecord(string functionName,string functionType,string parentId)
        {
            var dt = new DataTable();
            var resultdt=new DataTable();
            var sqlscript = string.Empty;

            switch (functionName)
            {
                //客户信息管理
                case "Customer":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("0", null, null, null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("1", null, null, null)
                            : sqlList.BD_SQLList("2", parentId, null, null));
                    break;
                //供应商管理
                case "Supplier":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("3", null, null, null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("4", null, null, null)
                            : sqlList.BD_SQLList("5", parentId, null, null));
                    break;
                //材料信息管理
                case "Material":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("6", null, null, null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("7", null, null, null)
                            : sqlList.BD_SQLList("8", parentId, null, null));
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    sqlscript = functionType == "T"
                        ? sqlList.BD_SQLList("9", null, null, null)
                        : (parentId == null
                            ? sqlList.BD_SQLList("10", null, null, null)
                            : sqlList.BD_SQLList("11", parentId, null, null));
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
        /// <returns></returns>
        private DataTable GetSearchValueRecord(string functionName,string searchName,string searchValue,DataTable dtdtl)
        {
            var tempdt=new DataTable();
            var dt=new DataTable();
            var rows=new DataRow[0];
            var sqlscript = $"'{searchName}' like '" + '%' + searchValue + '%' + "'";

            switch (functionName)
            {
                //"客户信息管理"
                case "Customer":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    rows = dtdtl.Select(sqlscript); 
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = rows.Length == 0 ? tempdt : IntoTempdt(rows, tempdt, tempdt.Columns.Count);
                    break;
                //"供应商信息管理"
                case "Supplier":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    rows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = rows.Length == 0 ? tempdt : IntoTempdt(rows, tempdt, tempdt.Columns.Count);
                    break;
                //材料信息管理
                case "Material":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    rows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = rows.Length == 0 ? tempdt : IntoTempdt(rows, tempdt, tempdt.Columns.Count);
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    //根据功能名称创建对应的空表
                    tempdt = GetTempdt(functionName);
                    //根据条件从DT内获取对应记录
                    rows = dtdtl.Select(sqlscript);
                    //若rows返回的结果为0时,返回临时表,反之循环将数据插入至临时表并返回
                    dt = rows.Length == 0 ? tempdt : IntoTempdt(rows, tempdt, tempdt.Columns.Count);
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
        private DataTable IntoTempdt(DataRow[] rows,DataTable tempdt,int tempdtColNum)
        {
            foreach (var i in rows)
            {
                for (var j = 0; j < tempdtColNum; j++)
                {
                    var row = tempdt.NewRow();
                    row[j] = i[j];
                    tempdt.Rows.Add(row);
                }
            }
            return tempdt;
        }

        /// <summary>
        /// 根据功能ID获取对应的空白临时表
        /// </summary>
        /// <param name="functionName">功能名,作用:获取空白表格</param>
        /// <returns></returns>
        private DataTable GetTempdt(string functionName)
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
            }
            return resultDt;
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetConn()
        {
            var conn = new Conn();
            var sqlcon = new SqlConnection(conn.GetConnectionString());
            return sqlcon;
        }
    }
}
