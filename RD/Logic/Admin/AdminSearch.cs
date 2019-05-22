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
        /// <param name="confirmstatus"></param>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public DataTable Searchdtldt(int userid, int sexid, string closeid, string confirmstatus, DateTime dtTime)
        {
            DataTable dt;

            try
            {
                dt = serDt.Admin_Searchdtldt(userid,sexid,closeid, confirmstatus, dtTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 角色信息管理查询
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public DataTable SearchRoledt(int roleid)
        {
            DataTable dt;

            try
            {
                dt = serDt.Admin_Searchroledt(roleid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 角色信息管理-功能权限明细查询
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="funtypeid"></param>
        /// <returns></returns>
        public DataTable SearchRoleFundt(int roleid ,int funtypeid )
        {
            DataTable dt;

            try
            {
                dt = serDt.Admin_SearchRoleFundt(roleid,funtypeid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 根据roleid获取T_AD_Role表头信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public DataTable SearchRoleHeaddt(int roleid)
        {
            DataTable dt;
            try
            {
                var markid = 0;
                dt = serDt.SearchRoleHeadorFundt(markid,roleid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }



    }
}
