using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RD.DB.Generate
{
    //注:此类包括了对功能的运算以及帐号密码修改的功能
    public class GenerateDt
    {
        Conn conn=new Conn();
        SqlList sqlList=new SqlList();
        DtList dtList=new DtList();

        #region 修改帐号密码
        /// <summary>
        /// 修改帐号密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        public bool ChangeUserPwd(string username, string userpwd)
        {
            var result = true;
            try
            {
                var sqlscipt= sqlList.Up_User(username, userpwd);
                result = GenerDt(sqlscipt);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 删除树形菜单记录

        /// <summary>
        /// 删除记录(作用:针对基础信息库)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="pid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DelBD_Rd(string functionName, int pid, DataTable dt)
        {
            var result = true;
            string name;
            //设置主键临时表(用于保存要删除的ID值)
            var tempdt = dtList.Get_TreeidTemp();
            
            switch (functionName)
            {
                case "AdornOrder":
                    name = "AdornOrderHead";
                    break;
                case "MaterialOrder":
                    name = "MaterialOrderHead";
                    break;
                default:
                    name = functionName;
                    break;
            }

            try
            {
                //先将选择的节点ID赋值给临时表(基础信息库才使用)
                if (name != "AdornOrderHead" && name != "MaterialOrderHead")
                {
                    var row = tempdt.NewRow();
                    row[0] = pid;
                    tempdt.Rows.Add(row);
                }
                    
                //使用递归将相关的节点信息添加至临时表内
                var resultdt = GetTreeRecord(name, dt, tempdt, pid);
                //循环执行删除操作
                result = Del(name, resultdt);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 获取树形菜单相关记录
        /// </summary> 
        private DataTable GetTreeRecord(string factionname,DataTable dt, DataTable tempdt, int pid)
        {
            //保存主键ID
            var id = 0;
            var rowdtl=new DataRow[0];

            //若factionname为AdornOrder 或 MaterialOrder时，是用treeid为条件
            if (factionname == "AdornOrderHead" || factionname == "MaterialOrderHead")
            {
                rowdtl = dt.Select("Treeid='" + pid + "'");
            }
            else
            {
                rowdtl = dt.Select("Parentid='" + pid + "'");
            }

            if (rowdtl.Length <= 0) return tempdt;

            foreach (var t in rowdtl)
            {
                var row = tempdt.NewRow();
                //当检测到单据类型为室内装修工程单 或 室内主材单时,要取第二列的ID记录才是树菜单的主键值
                if (factionname == "AdornOrderHead" || factionname == "MaterialOrderHead")
                {
                    row[0] = t[1];
                    id = Convert.ToInt32(t[1]);
                }
                //基础信息库使用
                else
                {
                    row[0] = t[0];
                    id = Convert.ToInt32(t[0]);
                }

                tempdt.Rows.Add(row);

                //若功能名称不是AdornOrder 或 MaterialOrder这两个单据类型时才执行递归
                if (factionname != "AdornOrderHead" && factionname != "MaterialOrderHead")
                {
                    var result = dt.Select("Parentid='" + id + "'");

                    if (result.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        //(重)递归调用
                        GetTreeRecord(factionname, dt, tempdt, id);
                    }
                }
            }
            return tempdt;
        }

        /// <summary>
        /// 循环执行删除操作
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Del(string functionName, DataTable dt)
        {
            var result = true;
            foreach (DataRow row in dt.Rows)
            {
                var sqlscript = Get_DelScript(functionName, row);
                result = GenerDt(sqlscript);
            }
            return result;
        }

        /// <summary>
        /// 根据functionName获取对应的删除SQL语句
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string Get_DelScript(string functionName, DataRow row)
        {
            string sqlscript;

            //单据(树菜单使用)
            if (functionName == "AdornOrderHead" || functionName == "MaterialOrderHead")
            {
                sqlscript= sqlList.OrderInfo_Del(functionName, Convert.ToInt32(row[0]));
            }
            //单据表体使用
            else if (functionName == "AdornOrder" || functionName == "MaterialOrder")
            {
                sqlscript= sqlList.OrderInfo_Del(functionName, Convert.ToInt32(row[2]));
            }
            //基础信息库使用
            else
            {
                sqlscript = sqlList.BD_Del(functionName, Convert.ToInt32(row[0]));
            }
            return sqlscript;
        }

        #endregion

        #region 审核（反审核）单据记录

        /// <summary>
        /// 单据审核（反审核）操作
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="confirmid">审核ID 0:审核 1:反审核</param>
        /// <param name="pid">主键ID</param>
        /// <returns></returns>
        public bool ConfirmOrderDtl(string functionName,int confirmid,int pid)
        {
            var result = true;

            try
            {
                var sqlscript = sqlList.ChangeOrderState(functionName, confirmid, pid);
                result = GenerDt(sqlscript);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 按照指定的SQL语句执行记录并返回执行结果（true 或 false）
        /// </summary>
        /// <param name="sqlscript"></param>
        /// <returns></returns>
        private bool GenerDt(string sqlscript)
        {
            var result = true;

            try
            {
                using (var sql = new SqlConnection(conn.GetConnectionString()))
                {
                    sql.Open();
                    var sqlCommand = new SqlCommand(sqlscript, sql);
                    sqlCommand.ExecuteNonQuery();
                    sql.Close();
                }
                #region 
                //var sqlconn = serDt.GetConn();
                //sqlconn.Open();
                //var sqlCommand = new SqlCommand(sqlscript, sqlconn);
                //sqlCommand.ExecuteNonQuery();
                //sqlconn.Close();
                #endregion
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #region 单据审核（反审核）操作 主窗体使用

        /// <summary>
        /// 单据审核（反审核）操作 主窗体使用
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="confirmid">审核ID 0:审核 1:反审核</param>
        /// <param name="datarow"></param>
        /// <returns></returns>
        public bool Main_ConfirmOrderDtl(string functionName, int confirmid, DataGridViewSelectedRowCollection datarow)
        {
            var result = true;

            try
            {
                //循环从GridView中所选择的行记录
                foreach (DataGridViewRow row in datarow)
                {
                    var sqlscript = sqlList.ChangeOrderState(functionName, confirmid, Convert.ToInt32(row.Cells[0].Value));
                    result = GenerDt(sqlscript); 
                }    
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region 角色权限 相关操作-包括审核(反审核) 关闭

        /// <summary>
        /// 角色权限明细审核（反审核）操作
        /// </summary>
        /// <param name="confirmid">审核标记;0:审核 1:反审核</param>
        /// <param name="roleid">角色ID</param>
        /// <param name="dt">所选择的表</param>
        /// <returns></returns>
        public bool ConfirmRoleFunOrderDtl(int confirmid, int roleid,DataTable dt)
        {
            var result = true;
            try
            {
                //此为角色权限明细窗体使用 RoleInfoDtlFrm.cs
                if (dt.Rows.Count == 0)
                {
                    var sqlscript = sqlList.Update_RoleConfirmStatus(confirmid, roleid);
                    result = GenerDt(sqlscript);
                }
                //循环从GridView中所选择的行记录 RoleInfoFrm.cs
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var sqlscript = sqlList.Update_RoleConfirmStatus(confirmid, Convert.ToInt32(row[0]));
                        result = GenerDt(sqlscript);
                    }
                }  
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 角色权限明细关闭(反关闭)操作
        /// </summary>
        /// <param name="closeid">关闭标记;0:关闭 1:反关闭</param>
        /// <param name="roleid">角色ID</param>
        /// <param name="dt">所选择的表</param>
        /// <returns></returns>
        public bool CloseRoleFunOrderDtl(int closeid, int roleid, DataTable dt)
        {
            var result = true;
            try
            {
                //此为角色权限明细窗体使用 RoleInfoDtlFrm.cs
                if (dt.Rows.Count == 0)
                {
                    var sqlscipt = sqlList.Update_RoleCloseStatus(closeid, roleid);
                    result = GenerDt(sqlscipt);
                }
                //循环从GridView中所选择的行记录 RoleInfoFrm.cs
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var sqlscipt = sqlList.Update_RoleCloseStatus(closeid, Convert.ToInt32(row[0]));
                        result = GenerDt(sqlscipt);
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 根据指定条件-对“显示” “反审核” “删除”权限设置
        /// </summary>
        /// <param name="functionname">功能分类名称</param>
        /// <param name="typeid">0:正面操作(如:显示) 1:反面操作(如:不显示)</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool Update_RoleFunStatus(string functionname, int typeid, DataTable dt)
        {
            var result = true;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    var sqlscipt = sqlList.Update_RoleFunStatus(functionname, typeid, Convert.ToInt32(row[0]));
                    result = GenerDt(sqlscipt);
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

    }
}
