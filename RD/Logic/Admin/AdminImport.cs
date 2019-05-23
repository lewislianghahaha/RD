using System;
using System.Data;
using RD.DB.Import;

namespace RD.Logic.Admin
{
    public class AdminImport
    {
        ImportDt importDt=new ImportDt();

        /// <summary>
        /// 根据相关条件将功能权限记录插入至T_AD_Role 及 T_AD_ROLEDTL内
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

        /// <summary>
        /// 根据条件更新T_AD_ROLE表头信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="roleid"></param>
        /// <param name="canallmark"></param>
        /// <returns></returns>
        public bool UpdateRole(string functionName , int roleid,string canallmark)
        {
            var result = true;
            try
            {
                result = importDt.UpdateRole(functionName, roleid, canallmark);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 根据相关条件将功能权限记录插入至T_AD_User 及 T_AD_UserDtl内
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="sexid"></param>
        /// <param name="usercontact"></param>
        /// <param name="useremail"></param>
        /// <param name="dtTime"></param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public int InsertRecordIntoUser(string functionName, int sexid,string usercontact,string useremail,DateTime dtTime,string accountName)
        {
            var reslutid = 0;
            try
            {
                reslutid = importDt.InsertRecordIntoUser(functionName,sexid,usercontact,useremail,dtTime,accountName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return reslutid;
        }

        /// <summary>
        /// 对T_AD_User更新 (AccountAddFrm.cs使用)
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="functionName"></param>
        /// <param name="sexid"></param>
        /// <param name="usercontact"></param>
        /// <param name="useremail"></param>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public bool UpdateUser(int userid,string functionName,int sexid,string usercontact,string useremail,DateTime dtTime)
        {
            var result = true;
            try
            {
                result = importDt.UpdateUser(userid, functionName, sexid, usercontact, useremail, dtTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
