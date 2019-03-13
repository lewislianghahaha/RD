using System;
using System.Data;
using RD.DB.Search;

namespace RD.Logic.Basic
{
    //作用:对"基础信息库"进行查询
    public class Search
    {
        SearchDt serDt=new SearchDt();

        /// <summary>
        /// 根据功能名称及获取数据
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="functionType"></param>
        /// <param name="parentId">主键ID,用于表体查询时使用 注:当为null时,为表体全部查询</param>
        /// <returns></returns>
        public DataTable GetData(string functionName, string functionType,string parentId)
        {
            var dt = new DataTable();

            try
            {
                dt = serDt.GetBdTableDt(functionName, functionType, parentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 根据查询框获取记录(注:当查询框有值时使用)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="searchName">查询选择列名-查询框有值时使用</param>
        /// <param name="searchValue">查询所填值-查询框有值时使用</param>
        /// <param name="dtdtl"></param>
        /// <returns></returns>
        public DataTable GetBdSearchData(string functionName, string searchName, string searchValue,DataTable dtdtl)
        {
            var dt = new DataTable();

            try
            {
                dt = serDt.GetBdSearchValueRecord(functionName, searchName, searchValue,dtdtl);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

    }
}
