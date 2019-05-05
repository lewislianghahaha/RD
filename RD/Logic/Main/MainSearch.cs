using System;
using System.Data;
using RD.DB.Search;

namespace RD.Logic.Main
{
    //主窗体查询功能
    public class MainSearch
    {
        SearchDt serDt = new SearchDt();

        /// <summary>
        /// 根据指定的功能名称获取对应的dt
        /// </summary>
        /// <returns></returns>
        public DataTable SearchDropdownDt(string functionName)
        {
            DataTable dt;

            try
            {
                dt = serDt.SearchDropdownDt(functionName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 查询按钮使用
        /// </summary>
        /// <param name="custid">客户ID</param>
        /// <param name="yearid">单据创建年份ID</param>
        /// <param name="ordertypeId">单据类型ID</param>
        /// <param name="hTypeid">房屋类型ID</param>
        /// <param name="confirmfStatus">审核状态</param>
        /// <param name="confirmdt">审核日期</param>
        /// <returns></returns>
        public DataTable Searchdtldt(int custid,int yearid,int ordertypeId,int hTypeid,string confirmfStatus,DateTime confirmdt)
        {
            DataTable dt;

            try
            {
                dt = serDt.Searchdtldt(custid,yearid,ordertypeId,hTypeid,confirmfStatus,confirmdt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

    }
}
