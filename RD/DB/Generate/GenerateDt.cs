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

        /// <summary>
        /// 删除记录(作用:针对基础信息库)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="pid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DelBD_Rd(string functionName,int pid,DataTable dt)
        {
            var result = true;
            var tempdt = dtList.Get_TreeidTemp();

            try
            {
                //先将选择的节点ID赋值给临时表
                var row = tempdt.NewRow();
                row[0] = pid;
                tempdt.Rows.Add(row);
                var a = tempdt;

                //使用递归将相关的节点信息添加至临时表内
                var resultdt = GetTreeRecord(dt, tempdt, pid);
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
        private DataTable GetTreeRecord(DataTable dt,DataTable tempdt,int pid)
        {
            var rowdtl = dt.Select("Parentid='" + pid + "'");
            if (rowdtl.Length > 0)
            {
                foreach (var t in rowdtl)
                {
                    var row = tempdt.NewRow();
                    row[0] = t[0];
                    tempdt.Rows.Add(row);

                    var result = dt.Select("Parentid='" + Convert.ToInt32(t[0]) + "'");

                    if (result.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        //(重)递归调用
                        GetTreeRecord(dt, tempdt, Convert.ToInt32(t[0]));
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
        private bool Del(string functionName,DataTable dt)
        {
            var result = true;
            foreach (DataRow row in dt.Rows)
            {
                var sqlscript = sqlList.BD_Del(functionName, Convert.ToInt32(row[0]));
                result = DelDt(sqlscript);
            }
            return result;
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
    }      
}
