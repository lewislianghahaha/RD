using System;
using System.Data;
using System.Data.SqlClient;

namespace RD.DB.Search
{
    //注:此类包括了对数据进行查询的功能
    public class SearchDt
    {
        #region 客户信息管理-表头(全部)

        private string _SearchCust = "select a.Id,a.parentid,a.CustType from T_BD_Cust a";

        #endregion

        #region 客户信息管理-表体(全部)

        private string _SearchCustEntry = @"SELECT a.CustName AS '客户名称',a.HTypeid AS '房屋类型ID',
	                                               a.Spare AS '装修地区',a.SpareAdd AS '装修地址',a.Cust_Add AS '客户通讯地址',
                                                   a.Cust_Phone AS '客户联系方式',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                            FROM dbo.T_BD_CustEntry a";

        #endregion

        #region 供应商管理-表头(全部)
        #endregion

        #region 供应商管理-表体(全部)
        #endregion


        /// <summary>
        /// 基础信息库模块查询使用
        /// </summary>
        /// <param name="functionName">功能名</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <returns></returns>
        public DataTable GetBasicTableDt(string functionName, string functionType)
        {
            var ds=new DataSet();
            var resultDt=new DataTable();

            try
            {
                switch (functionName)
                {
                    case "Customer":
                        ds = GetRecord(functionName,functionType);
                        break;
                    case "Supplier":
                        ds = GetRecord(functionName,functionType);
                        break;
                    case "Material":
                        ds = GetRecord(functionName,functionType);
                        break;
                    case "House":
                        ds = GetRecord(functionName,functionType);
                        break;
                }
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
                
            }
            return resultDt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName">功能名</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        /// <returns></returns>
        private DataSet GetRecord(string functionName,string functionType)
        {
            var ds=new DataSet();
            var sqlscript = string.Format();
            var sqlDataAdapter=new SqlDataAdapter(sqlscript,GetConn());
            sqlDataAdapter.Fill(ds);
            return ds;
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
