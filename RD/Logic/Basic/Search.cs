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
        /// <returns></returns>
        public DataTable GetData(string functionName, string functionType)
        {
            var dt=new DataTable();

            try
            {
                dt = serDt.GetBasicTableDt(functionName, functionType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
    }
}
