using System;
using System.Data;
using System.Data.SqlClient;
using RD.DB.Search;

namespace RD.DB.Generate
{
    //注:此类包括了对功能的运算以及帐号密码修改的功能
    public class GenerateDt
    {
        Conn conn=new Conn();
        SearchDt serDt = new SearchDt();
        SqlList sqlList=new SqlList();
        DtList dtList=new DtList();

        /// <summary>
        /// 修改帐号密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        public bool ChangeUserPwd(string username,string userpwd)
        {
            var result = true;
            try
            {
                using (var sql = new SqlConnection(conn.GetConnectionString()))
                {
                    var sqlCommand = new SqlCommand(sqlList.Up_User(username,userpwd), sql);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

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
            //设置主键临时表(用于保存要删除的ID值)
            var tempdt = dtList.Get_TreeidTemp();

            try
            {
                //先将选择的节点ID赋值给临时表
                var row = tempdt.NewRow();
                row[0] = pid;
                tempdt.Rows.Add(row);

                //使用递归将相关的节点信息添加至临时表内
                var resultdt = GetTreeRecord(functionName, dt, tempdt, pid);
                //循环执行删除操作
                result = Del(functionName, resultdt);
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
            var rowdtl = dt.Select("Parentid='" + pid + "'");
            if (rowdtl.Length <= 0) return tempdt;

            foreach (var t in rowdtl)
            {
                var row = tempdt.NewRow();
                //当检测到单据类型为室内装修工程单 或 室内主材单时,要取第二列的ID记录才是树菜单的主键值
                if (factionname == "AdornOrder" || factionname == "MaterialOrder")
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

                var result = dt.Select("Parentid='" + id + "'");

                if (result.Length == 0)
                {
                    continue;
                }
                else
                {
                    //(重)递归调用
                    GetTreeRecord(factionname,dt, tempdt, id);
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
        private bool Del(string functionName, DataTable dt)
        {
            var result = true;
            foreach (DataRow row in dt.Rows)
            {
                var sqlscript = Get_DelScript(functionName, row);
                result = DelDt(sqlscript);
            }
            return result;
        }

        /// <summary>
        /// 根据functionName获取对就的删除SQL语句
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string Get_DelScript(string functionName, DataRow row)
        {
            var sqlscript = string.Empty;
            //单据使用
            if (functionName == "AdornOrder" || functionName == "MaterialOrder")
            {
                sqlscript= sqlList.OrderInfo_Del(functionName, Convert.ToInt32(row[0]));
            }
            //基础信息库使用
            else
            {
                sqlscript = sqlList.BD_Del(functionName, Convert.ToInt32(row[0]));
            }
            return sqlscript;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="sqlscript"></param>
        /// <returns></returns>
        private bool DelDt(string sqlscript)
        {
            var result = true;
            try
            {
                var sqlconn = serDt.GetConn();
                sqlconn.Open();
                var sqlCommand = new SqlCommand(sqlscript, sqlconn);
                sqlCommand.ExecuteNonQuery();
                sqlconn.Close();
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
