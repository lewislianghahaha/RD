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
        /// 插入树形菜单记录
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

        /// <summary>
        /// 对基础信息库表体进行保存操作
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="dt">从GridView控件内获取的DT</param>
        /// <returns></returns>
        public bool SavebaseEntryrd(string functionName,DataTable dt)
        {
            var result = true;
            //根据功能名称获取对应在的临时表信息
            var tempInsertdt = serDt.GetTempdt(functionName);
            var tempUpdt = serDt.GetTempdt(functionName);
            //根据FunctionName获得对应的表体表名
            var tableName = GetTableName(functionName);

            try
            {
                //循环参数DT
                foreach (DataRow row in dt.Rows)
                {
                    //若行状态为"已添加",就添加至Insert内;若行状态为"更新",就添加至Update内;
                    switch (row.RowState.ToString())
                    {
                        //添加状态
                        case "Added":
                            tempInsertdt = GetTempRd(row, tempInsertdt);
                            break;
                        //修改状态
                        case "Modified":
                            tempUpdt = GetTempRd(row, tempUpdt);
                            break;
                    }
                }
                //循环结束后分别将累积的临时表信息,进行插入或更新操作
                Importdt(tableName,tempInsertdt);
                UpEntrydt(tableName,tempUpdt);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 将记录插入至临时表内
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tempdt"></param>
        /// <returns></returns>
        private DataTable GetTempRd(DataRow row,DataTable tempdt)
        {
            for (var i = 0; i < row.ItemArray.Length; i++)
            {
                var tempinsertrow = tempdt.NewRow();
                tempinsertrow[i] = row[i];
                tempdt.Rows.Add(tempinsertrow);
            }
            return tempdt;
        }

        /// <summary>
        /// 针对指定表进行数据插入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        private void Importdt(string tableName, DataTable dt)
        {
            var conn = new Conn();
            var sqlcon = conn.GetConnectionString();
            // sqlcon.Open(); 若返回一个SqlConnection的话,必须要显式打开 
            //注:1)要插入的DataTable内的字段数据类型必须要与数据库内的一致;并且要按数据表内的字段顺序 2)SqlBulkCopy类只提供将数据写入到数据库内
            using (var sqlBulkCopy = new SqlBulkCopy(sqlcon))
            {
                sqlBulkCopy.BatchSize = 1000;                    //表示以1000行 为一个批次进行插入
                sqlBulkCopy.DestinationTableName = tableName;  //数据库中对应的表名
                sqlBulkCopy.NotifyAfter = dt.Rows.Count;      //赋值DataTable的行数
                sqlBulkCopy.WriteToServer(dt);               //数据导入数据库
                sqlBulkCopy.Close();                        //关闭连接 
            }
            // sqlcon.Close();
        }

        /// <summary>
        /// 更新“基础信息库”内的记录
        /// </summary>
        private void UpEntrydt(string tableName,DataTable updt)
        {
            //根据功能名称获取对应的SQL语句
            foreach (DataRow row in updt.Rows)
            {
                
            }
        }

        /// <summary>
        /// 根据功能名称获取对应的表名
        /// </summary>
        /// <returns></returns>
        private string GetTableName(string factionName)
        {
            var result = string.Empty;
            switch (factionName)
            {
                case "Customer":
                    result = "T_BD_CustEntry";
                    break;
                case "Supplier":
                    result = "T_BD_SupplierEntry";
                    break;
                case "Material":
                    result = "T_BD_MaterialEntry";
                    break;
                case "House":
                    result = "T_BD_HTypeEntry";
                    break;
            }
            return result;
        }

    }
}
