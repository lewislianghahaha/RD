using System;
using System.Data;
using System.Data.SqlClient;
using RD.DB.Search;

namespace RD.DB.Import
{
    //注:此类包括了对数据进行相关导入至数据库的功能
    public class ImportDt
    {
        SearchDt serDt=new SearchDt();
        SqlList sqlList=new SqlList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="pid">上级节点ID</param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public bool InsertTreeRecord(string functionName, int pid, string treeName)
        {
            var result = true;
            var sqlscript = string.Empty;
            //若pid为1时,要先判断pid=1时有没有值,若没有就要先插入ALL的记录,再插入要新增的记录
            var dt = SearchNum(functionName);
      
            if (pid == 1 && dt.Rows.Count==0)
            {
                sqlscript = sqlList.BD_InsertTree(functionName, 0, "ALL");
                EditDt(sqlscript);
            }
            
            sqlscript = sqlList.BD_InsertTree(functionName, pid, treeName);
            result = EditDt(sqlscript);
            
            return result;
        }

        /// <summary>
        /// 更新所选的节点信息
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="id">本级节点ID</param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public bool UpdateTreeRecord(string functionName,int id,string treeName)
        {
            var result = true;
            var sqlscript = sqlList.BD_UpdateTree(functionName, treeName, id);
            result=EditDt(sqlscript);
            return result;
        }

        /// <summary>
        /// 对指定表格执行新增或更新操作(树形菜单使用)
        /// </summary>
        /// <param name="sqlscript"></param>
        private bool EditDt(string sqlscript)
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

        /// <summary>
        /// 根据功能名查询树形菜单对应的表头数据表是否有"ALL"记录
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        private DataTable SearchNum(string functionName)
        {
            var dt=new DataTable();
            var sqlscript= sqlList.BD_SearchNum(functionName);
            var sqlDataAdapter=new SqlDataAdapter(sqlscript,serDt.GetConn());
            sqlDataAdapter.Fill(dt);
            return dt;
        }

    }
}
