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
            var ds=new DataSet();
            var resultDt=new DataTable();

            try
            {
                ds = GetRecord(functionName, functionType,parentId);
            }
            catch (Exception ex)
            {
                ds.Tables[0].Rows.Clear();
                ds.Tables[0].Columns.Clear();
                throw new Exception(ex.Message);
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                resultDt = ds.Tables[0];
            }
            //若没有记录,就创建一个空DataTable(注:这里主要针对表体)
            else
            {
                GetTempdt(functionName);
            }
            return resultDt;
        }

        /// <summary>
        /// 根据查询框获取记录(注:当查询框有值时使用)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="searchName"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public DataTable GetBdSearchValueRecord(string functionName, string searchName, string searchValue)
        {
            var ds = new DataSet();
            var resultDt = new DataTable();

            try
            {
                ds = GetSearchValueRecord(functionName,searchName,searchValue);
            }
            catch (Exception ex)
            {
                ds.Tables[0].Rows.Clear();
                ds.Tables[0].Columns.Clear();
                throw new Exception(ex.Message);
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                resultDt = ds.Tables[0];
            }
            //若没有记录,就创建一个空DataTable(注:这里主要针对表体)
            else
            {
                GetTempdt(functionName);
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
        private DataSet GetRecord(string functionName,string functionType,string parentId)
        {
            var ds = new DataSet();
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
            
            var sqlDataAdapter=new SqlDataAdapter(sqlscript,GetConn());
            sqlDataAdapter.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 根据查询框获取记录(注:当查询框有值时使用)
        /// </summary>
        /// <param name="functionName">功能名:决定是用那个系列表格</param>
        /// <param name="searchName">查询选择列名-查询框有值时使用</param>
        /// <param name="searchValue">查询所填值-查询框有值时使用</param>
        /// <returns></returns>
        private DataSet GetSearchValueRecord(string functionName,string searchName,string searchValue)
        {
            var ds=new DataSet();
            var sqlscript = string.Empty;
            switch (functionName)
            {
                case "Customer":
                    sqlscript = sqlList.BD_SQLList("2.1", null, searchName, searchValue);
                    break;
                case "Supplier":
                    sqlscript = sqlList.BD_SQLList("5.1",null,searchName,searchValue);
                    break;
                case "Material":
                    sqlscript = sqlList.BD_SQLList("8.1", null, searchName, searchValue);
                    break;
                case "House":
                    sqlscript = sqlList.BD_SQLList("11.1", null, searchName, searchValue);
                    break;
            }

            var sqlDataAdapter = new SqlDataAdapter(sqlscript, GetConn());
            sqlDataAdapter.Fill(ds);
            return ds;
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
