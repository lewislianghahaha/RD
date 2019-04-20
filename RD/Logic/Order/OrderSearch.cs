using System;
using System.Data;
using RD.DB.Search;

namespace RD.Logic.Order
{
    //作用:查询使用
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

        /// <summary>
        /// 获取“室内装修工程”表体信息
        /// </summary>
        /// <param name="funState">单据状态 R:读取 C:创建</param>
        /// <param name="pid">表头ID</param>
        /// <returns></returns>
        public DataTable Get_AdornDtl(string funState,int pid)
        {
            DataTable dt;
            try
            {
                dt = serDt.SearchAdorndtl(funState,pid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 读取“室内装修工程单”表头信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_AdornTreeView(int pid)
        {
            DataTable dt;

            try
            {
                dt = serDt.SearchAdornTreeView(pid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 根据PID获取T_Pro_Adorn 或 T_Pro_Material表头信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable Get_FirstOrderInfo(string functionName, int pid)
        {
            DataTable dt;
            try
            {
                dt = serDt.SearFirstOrderInfo(functionName,pid);
            }
            catch (Exception ex)
            {
                throw  new Exception(ex.Message);
            }
            return dt;
        }

    }
}
