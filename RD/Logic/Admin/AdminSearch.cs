using System;
using System.Data;
using RD.DB.Search;

namespace RD.Logic.Admin
{
    public class AdminSearch
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
        /// <param name="userid"></param>
        /// <param name="sexid"></param>
        /// <param name="closeid"></param>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public DataTable Searchdtldt(int userid, int sexid, string closeid, DateTime dtTime)
        {
            DataTable dt;

            try
            {
                dt = serDt.Admin_Searchdtldt(userid,sexid,closeid,dtTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

    }
}
