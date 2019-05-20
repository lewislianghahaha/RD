using System;
using System.Data;
using RD.DB.Import;

namespace RD.Logic.Admin
{
    public class AdminImport
    {
        ImportDt importDt=new ImportDt();

        /// <summary>
        /// 根据相关条件将功能权限记录插入至T_AD_ROLEDTL内
        /// </summary>
        /// <param name="rolename">功能名称</param>
        /// <param name="dt">功能大类名称DT</param>
        /// <param name="accountName">帐号名称</param>
        /// <returns></returns>
        public int InsertDtlIntoRole(string rolename,DataTable dt,string accountName)
        {
            var reslutid = 0;
            try
            {
                reslutid = importDt.InsertDtlIntoRole(rolename, dt,accountName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return reslutid;
        }
    }
}
