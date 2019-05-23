using System.Data;
using RD.DB.Generate;

namespace RD.Logic.Admin
{
    public class AdminGenerate
    {
        GenerateDt generateDt = new GenerateDt();

        /// <summary>
        /// 角色权限明细审核（反审核）操作 T_AD_Role表使用
        /// </summary>
        /// <param name="confirmid">审核ID 0:审核 1:反审核</param>
        /// <param name="roleid">角色ID</param>
        /// <param name="dt">从GridView中所选择的行(注:角色信息管理界面-批量选择多行时使用)</param>
        /// <returns></returns>
        public bool ConfirmRoleFunOrderDtl(int confirmid, int roleid, DataTable dt)
        {
            var result = true;
            result = generateDt.ConfirmRoleFunOrderDtl(confirmid, roleid, dt);
            return result;
        }

        /// <summary>
        /// 角色权限明细关闭(反关闭) 操作
        /// </summary>
        /// <param name="closeid">关闭标记;0:关闭 1:反关闭</param>
        /// <param name="roleid">角色ID</param>
        /// <param name="dt">从GridView中所选择的行(注:角色信息管理界面-批量选择多行时使用)</param>
        /// <returns></returns>
        public bool CloseRoleFunOrderDtl(int closeid, int roleid, DataTable dt)
        {
            var result = true;
            result = generateDt.CloseRoleFunOrderDtl(closeid, roleid,dt);
            return result;
        }

        /// <summary>
        /// 根据指定条件-对“显示” “反审核” “删除”权限设置
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="typeid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Update_RoleFunStatus(string functionname, int typeid, DataTable dt)
        {
            var result = true;
            result = generateDt.Update_RoleFunStatus(functionname, typeid, dt);
            return result;
        }

        /// <summary>
        /// 审核(反审核) 针对T_AD_User进行操作
        /// </summary>
        /// <param name="confirmid"></param>
        /// <param name="userid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool ConfirmUser(int confirmid,int userid,DataTable dt)
        {
            var result = true;
            result = generateDt.ConfirmUser(confirmid, userid, dt);
            return result;
        }

        /// <summary>
        /// 更新功能(对T_AD_UserDtl更新) ‘是否添加’功能
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool AddRoleIntoUserdtl(int id ,DataTable dt)
        {
            var result = true;
            result = generateDt.AddRoleIntoUserdtl(id,dt);
            return result;
        }

        /// <summary>
        /// 批量关闭帐户信息 针对T_AD_User进行操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CloseUser(int id, DataTable dt)
        {
            var result = true;
            result = generateDt.CloseUser(id, dt);
            return result;
        }

    }
}
