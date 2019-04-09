using System;
using System.Data;
using RD.DB.Search;

namespace RD.Logic.Order
{
    //室内装修工程单 以及 主材单查询使用
    public class OrderSearch
    {
        SearchDt serDt=new SearchDt();

        /// <summary>
        /// 获取装修工程类别内容(室内装修工程单据使用)
        /// </summary>
        /// <returns></returns>
        public DataTable GetHouseTypeDtl()
        {
            DataTable dt;

            try
            {
                dt = serDt.SearchHouseType();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }



    }
}
