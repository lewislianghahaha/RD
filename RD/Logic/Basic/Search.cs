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
            DataTable dt;

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
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetBdSearchData(string functionName, string searchName, string searchValue,DataTable dtdtl,int pid)
        {
            DataTable dt;

            try
            {
                dt = serDt.GetBdSearchValueRecord(functionName, searchName, searchValue,dtdtl,pid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 获取"基础信息库"下拉信息列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetColDropDownList(string functionName)
        {
            DataTable dt;

            try
            {
                dt = serDt.SearchColList(functionName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 基础信息库-弹出明细窗体 及 类别项目名称窗体 使用(初始化时使用)
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitializeDtl(string factionName)
        {
            DataTable dt;

            try
            {
                dt = serDt.SearchInitializeDtl(factionName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 基础信息库-弹出明细窗体 及 类别项目名称窗体 使用(查询值时使用)
        /// </summary>
        /// <param name="funcationName"></param>
        /// <param name="searchName"></param>
        /// <param name="searchValue"></param>
        /// <param name="resultdt"></param>
        /// <returns></returns>
        public DataTable GetSearchDt(string funcationName, string searchName, string searchValue, DataTable resultdt)
        {
            DataTable dt;
            try
            {
                dt = serDt.SearchShowDtl(funcationName, searchName, searchValue, resultdt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
    }
}
