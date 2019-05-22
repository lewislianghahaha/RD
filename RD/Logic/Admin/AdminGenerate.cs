using System.Data;
using System.Windows.Forms;
using RD.DB.Generate;

namespace RD.Logic.Admin
{
    public class AdminGenerate
    {
        GenerateDt generateDt = new GenerateDt();

        /// <summary>
        /// 角色权限明细审核（反审核）操作
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

    }
}
