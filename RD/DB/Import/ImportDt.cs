using System;
using System.Data;
using System.Data.SqlClient;
using RD.DB.Generate;
using RD.DB.Search;

namespace RD.DB.Import
{
    //注:此类包括了对数据进行相关导入至数据库的功能
    public class ImportDt
    {
        SearchDt serDt=new SearchDt();
        GenerateDt generateDt=new GenerateDt();
        SqlList sqlList=new SqlList();

        /// <summary>
        /// 插入树形菜单记录
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="id">表头ID(室内装修工程单 及 室内主材单使用)</param>
        /// <param name="pid">上级节点ID</param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public bool InsertTreeRecord(string functionName, int id,int pid, string treeName)
        {
            var result = true;
            var sqlscript = string.Empty;

            //判断若功能名称不是AdornOrder 或 MaterialOrder时才执行(即基础信息库才使用)
            if (functionName != "AdornOrder" && functionName != "MaterialOrder")
            {
                //若pid为1时,要先判断pid=1时有没有值,若没有就要先插入ALL的记录,再插入要新增的记录
                var dt = SearchNum(functionName);

                if (pid == 1 && dt.Rows.Count == 0)
                {
                    sqlscript = GetSqlscript(functionName, id, pid, treeName, 0);
                    EditDt(sqlscript);
                }
            }

            //插入要新增的记录
            sqlscript = GetSqlscript(functionName, id, pid, treeName, 1);
            result = EditDt(sqlscript);
            return result;
        }

        /// <summary>
        /// 获取插入语句
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <param name="treeName"></param>
        /// <param name="markid">区分ID;是插头首行;还是其它行</param>
        /// <returns></returns>
        private string GetSqlscript(string functionName,int id,int pid,string treeName,int markid)
        {
            var sqlscript = string.Empty;
            if (markid == 0)
            {
                //单据使用
                if (functionName == "AdornOrder" || functionName == "MaterialOrder")
                {
                    sqlscript = sqlList.Order_InsertTree(functionName, id, 0, "ALL");

                }
                //基础信息库使用
                else
                {
                    sqlscript = sqlList.BD_InsertTree(functionName, 0, "ALL");
                }
            }
            else
            {
                //插入要新增的记录
                if (functionName == "AdornOrder" || functionName == "MaterialOrder")
                {
                    sqlscript = sqlList.Order_InsertTree(functionName, id, pid, treeName);

                }
                else
                {
                    sqlscript = sqlList.BD_InsertTree(functionName, pid, treeName);
                }
            }
            return sqlscript;
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
            var sqlscript = string.Empty;

            //单据使用
            if (functionName == "AdornOrder" || functionName == "MaterialOrder")
            {
                sqlscript = sqlList.Order_UpdateTree(functionName, treeName, id);
            }
            //基础信息库使用
            else
            {
                sqlscript = sqlList.BD_UpdateTree(functionName, treeName, id);
            }
            result = EditDt(sqlscript);
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
            var sqlscript = string.Empty;

            //单据使用
            //if (functionName == "AdornOrder" || functionName =="MaterialOrder")
            //{
            //    sqlscript = sqlList.Order_SearchNum(functionName);
            //}
            ////基础信息库使用
            //else
            //{
                sqlscript = sqlList.BD_SearchNum(functionName);
            //}
            var sqlDataAdapter=new SqlDataAdapter(sqlscript,serDt.GetConn());
            sqlDataAdapter.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 对基础信息库表体进行保存操作(注:包括插入及更新操作)
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="dt">从GridView控件内获取的DT</param>
        /// <param name="pid">对应的表头ID</param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public bool SavebaseEntryrd(string functionName,DataTable dt,int pid,string accountName)
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
                            tempInsertdt = GetTempRd(row, tempInsertdt,pid,accountName,row.RowState.ToString());
                            break;
                        //修改状态
                        case "Modified":
                            tempUpdt = GetTempRd(row, tempUpdt,pid,accountName, row.RowState.ToString());
                            break;
                    }
                }
                //循环结束后分别将累积的临时表信息,进行插入或更新操作
                if(tempInsertdt.Rows.Count>0)
                    Importdt(tableName,tempInsertdt);
                if(tempUpdt.Rows.Count>0)
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
        /// <param name="pid"></param>
        /// <param name="accountName">帐号名称</param>
        /// <param name="rowState">行状态</param>
        /// <returns></returns>
        private DataTable GetTempRd(DataRow row, DataTable tempdt,int pid,string accountName,string rowState)
        {
            var temprow = tempdt.NewRow();
            for (var j = 0; j < tempdt.Columns.Count; j++)
            {
                if (j == 0 && rowState=="Added")
                {
                    temprow[j] = pid;
                }
                else if (j == tempdt.Columns.Count - 1 && rowState== "Added")
                {
                    temprow[j] = DateTime.Now.Date;
                }
                else if (j == tempdt.Columns.Count - 2 && rowState == "Added")
                {
                    temprow[j] = accountName;
                }
                else
                {
                    temprow[j] = row[j];
                }
            }
            tempdt.Rows.Add(temprow);
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
        /// 更新表体内的记录
        /// </summary>
        private void UpEntrydt(string tableName,DataTable updt)
        {
            var sqladpter = new SqlDataAdapter();
            var ds = new DataSet();

            //根据表格名称获取对应的模板表记录
            var searList = sqlList.BD_SearchTempEntry(tableName);

            using (sqladpter.SelectCommand = new SqlCommand(searList,serDt.GetConn()))
            {
                //将查询的记录填充至ds(查询表记录;后面的更新作赋值使用)
                sqladpter.Fill(ds);
                //建立更新模板相关信息(包括更新语句 以及 变量参数)
                sqladpter = GetUpdateAdapter(tableName, serDt.GetConn(), sqladpter);
                //开始更新(注:通过对DataSet中存在的表进行循环赋值;并进行更新)
                for (var i = 0; i < updt.Rows.Count; i++)
                {
                    for (var j = 0; j < updt.Columns.Count; j++)
                    {
                        ds.Tables[0].Rows[i].BeginEdit();
                        ds.Tables[0].Rows[i][j] = updt.Rows[i][j];
                        ds.Tables[0].Rows[i].EndEdit();
                    }
                    sqladpter.Update(ds.Tables[0]);
                }
                //完成更新后将相关内容清空
                ds.Tables[0].Clear();
                sqladpter.Dispose();
                ds.Dispose();
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
                case "HouseProject":
                    result = "T_BD_HTypeProjectDtl";
                    break;
                case "AdornOrderHead":
                    result = "T_PRO_Adorn";
                    break;
                case "MaterialOrderHead":
                    result = "T_PRO_Material";
                    break;
                case "AdornOrder":
                    result = "T_PRO_AdornEntry";
                    break;
                case "MaterialOrder":
                    result = "T_PRO_MaterialEntry";
                    break;
                case "MaterialOrderTree":
                    result = "T_PRO_MaterialTree";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="conn"></param>
        /// <param name="da"></param>
        /// <returns></returns>
        private SqlDataAdapter GetUpdateAdapter(string tableName,SqlConnection conn,SqlDataAdapter da)
        {
            //根据标记获取对应的更新语句
            var script = tableName == "T_PRO_AdornEntry" || tableName == "T_PRO_MaterialEntry"
                ? sqlList.Order_UpdateEntry(tableName)
                : sqlList.BD_UpdateEntry(tableName);

            da.UpdateCommand = new SqlCommand(script, conn);

            switch (tableName)
            {
                //定义所需的变量参数
                case "T_BD_CustEntry":
                    da.UpdateCommand.Parameters.Add("@Custid", SqlDbType.Int, 8, "Custid");
                    da.UpdateCommand.Parameters.Add("@CustName",SqlDbType.NVarChar,300,"CustName");
                    da.UpdateCommand.Parameters.Add("@HTypeid",SqlDbType.Int,8, "HTypeid");
                    da.UpdateCommand.Parameters.Add("@HTypeName", SqlDbType.NVarChar, 200, "HTypeName");
                    da.UpdateCommand.Parameters.Add("@Spare",SqlDbType.NVarChar,100, "Spare");
                    da.UpdateCommand.Parameters.Add("@SpareAdd",SqlDbType.NVarChar,300, "SpareAdd");
                    da.UpdateCommand.Parameters.Add("@Cust_Add",SqlDbType.NVarChar,300, "Cust_Add");
                    da.UpdateCommand.Parameters.Add("@Cust_Phone",SqlDbType.NVarChar, 300, "Cust_Phone");
                    break;
                case "T_BD_SupplierEntry":
                    da.UpdateCommand.Parameters.Add("@Supid",SqlDbType.Int,8, "Supid");
                    da.UpdateCommand.Parameters.Add("@SupName",SqlDbType.NVarChar,500,"SupName");
                    da.UpdateCommand.Parameters.Add("@Address",SqlDbType.NVarChar,200, "Address");
                    da.UpdateCommand.Parameters.Add("@ContactName",SqlDbType.NVarChar,100, "ContactName");
                    da.UpdateCommand.Parameters.Add("@ContactPhone",SqlDbType.NVarChar,100, "ContactPhone");
                    da.UpdateCommand.Parameters.Add("@GoNum",SqlDbType.NVarChar,200, "GoNum");
                    break;
                case "T_BD_MaterialEntry":
                    da.UpdateCommand.Parameters.Add("@MaterialId",SqlDbType.Int,8, "MaterialId");
                    da.UpdateCommand.Parameters.Add("@MaterialName",SqlDbType.NVarChar,200, "MaterialName");
                    da.UpdateCommand.Parameters.Add("@MaterialSize",SqlDbType.NVarChar,200, "MaterialSize");
                    da.UpdateCommand.Parameters.Add("@Supid", SqlDbType.Int, 8, "Supid");
                    da.UpdateCommand.Parameters.Add("@SupName",SqlDbType.NVarChar,500, "SupName");
                    da.UpdateCommand.Parameters.Add("@Unit",SqlDbType.NVarChar,10, "Unit");
                    da.UpdateCommand.Parameters.Add("@Price",SqlDbType.Decimal,2, "Price");
                    break;
                case "T_BD_HTypeEntry":
                    da.UpdateCommand.Parameters.Add("@HTypeid", SqlDbType.Int, 8, "HTypeid");
                    da.UpdateCommand.Parameters.Add("@HtypeName",SqlDbType.NVarChar,200, "HtypeName");
                    break;
                case "T_BD_HTypeProjectDtl":
                    da.UpdateCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 8, "ProjectId");
                    da.UpdateCommand.Parameters.Add("@ProjectName", SqlDbType.NVarChar, 300, "ProjectName");
                    da.UpdateCommand.Parameters.Add("@Unit", SqlDbType.NVarChar, 10, "Unit");
                    da.UpdateCommand.Parameters.Add("@Price", SqlDbType.Decimal, 2, "Price");
                    break;
                case "T_PRO_AdornEntry":
                    da.UpdateCommand.Parameters.Add("@adornid", SqlDbType.Int, 8, "adornid");
                    da.UpdateCommand.Parameters.Add("@HTypeProjectName", SqlDbType.NVarChar,300, "HTypeProjectName");
                    da.UpdateCommand.Parameters.Add("@Unit", SqlDbType.NVarChar, 50, "Unit");
                    da.UpdateCommand.Parameters.Add("@quantities",SqlDbType.Decimal,2, "quantities");
                    da.UpdateCommand.Parameters.Add("@FinalPrice", SqlDbType.Decimal, 2, "FinalPrice");
                    da.UpdateCommand.Parameters.Add("@Ren_Cost",SqlDbType.Decimal,2,"Ren_Cost");
                    da.UpdateCommand.Parameters.Add("@Fu_Cost",SqlDbType.Decimal,2,"Fu_Cost");
                    da.UpdateCommand.Parameters.Add("@Price", SqlDbType.Decimal, 2, "Price");
                    da.UpdateCommand.Parameters.Add("@Temp_Price",SqlDbType.Decimal,2, "Temp_Price");
                    da.UpdateCommand.Parameters.Add("@Amount",SqlDbType.Decimal,2, "Amount");
                    da.UpdateCommand.Parameters.Add("@FRemark",SqlDbType.NVarChar,500, "FRemark");
                    break;
                case "T_PRO_MaterialEntry":
                    da.UpdateCommand.Parameters.Add("@EntryID", SqlDbType.Int, 8, "EntryID");
                    da.UpdateCommand.Parameters.Add("@MaterialId",SqlDbType.Int,8,"MaterialId");
                    da.UpdateCommand.Parameters.Add("@MaterialName", SqlDbType.NVarChar, 300, "MaterialName");
                    da.UpdateCommand.Parameters.Add("@Unit",SqlDbType.NVarChar,50, "Unit");
                    da.UpdateCommand.Parameters.Add("@quantities",SqlDbType.Decimal,2, "quantities");
                    da.UpdateCommand.Parameters.Add("@FinalPrice", SqlDbType.Decimal, 2, "FinalPrice");
                    da.UpdateCommand.Parameters.Add("@Ren_Cost", SqlDbType.Decimal, 2, "Ren_Cost");
                    da.UpdateCommand.Parameters.Add("@Fu_Cost", SqlDbType.Decimal, 2, "Fu_Cost");
                    da.UpdateCommand.Parameters.Add("@Price", SqlDbType.Decimal, 2, "Price");
                    da.UpdateCommand.Parameters.Add("@Temp_Price", SqlDbType.Decimal, 2, "Temp_Price");
                    da.UpdateCommand.Parameters.Add("@Amount", SqlDbType.Decimal, 2, "Amount");
                    da.UpdateCommand.Parameters.Add("@FRemark",SqlDbType.NVarChar,500,"FRemark");
                    break;
            }
            return da;
        }

        #region 单据代码相关

        /// <summary>
        /// 根据相关条件插入信息至T_PRO_Material 或 T_PRO_Adorn表内,并返回新插入的主键ID值
        /// </summary>
        /// <param name="functionName">功能名称 Adorn:室内装修工程单 Material:室内主材单</param>
        /// <param name="custid">所选择的客户ID</param>
        /// <param name="accountname">帐号名称</param>
        /// <returns></returns>
        public int InsertOrderFirstDt(string functionName, int custid, string accountname)
        {
            //获取对应表的最大ID值
            var reslutid = 0;
            var dt = new DataTable();

            try
            {
                var name = functionName == "AdornOrder" ? "AdornOrderHead" : "MaterialOrderHead";

                var tablename = GetTableName(name);

                switch (name)
                {
                    //室内装修工程单
                    case "AdornOrderHead":
                        //获取T_PRO_Adorn表MAX(ID)值
                        reslutid = Maxid(tablename);
                        //插入相关记录至T_PRO_Adorn临时表内
                        dt = Get_OrderTemp(name, custid, reslutid, accountname);
                        break;
                    //室内主材单
                    case "MaterialOrderHead":
                        //获取T_PRO_Material表MAX(ID)值
                        reslutid = Maxid(tablename);
                        //插入相关记录至T_PRO_Material临时表内
                        dt = Get_OrderTemp(name, custid, reslutid, accountname);
                        break;
                }
                //将相关记录进行插入
                Importdt(tablename, dt);
            }
            catch (Exception)
            {
                reslutid = 0;
            }
            return reslutid;
        }

        /// <summary>
        /// 获取主键ID值
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private int Maxid(string tableName)
        {
            var dt = new DataTable();
            //获取对应的SQL语句
            var sqlscript = sqlList.Get_OrderMaxid(tableName);
            //执行SQL
            var sqlDataAdapter = new SqlDataAdapter(sqlscript, serDt.GetConn());
            sqlDataAdapter.Fill(dt);
            //赋值
            var id = Convert.ToInt32(dt.Rows[0][0]);
            return id;
        }

        /// <summary>
        /// 将相关值插入至对应的临时表内
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="custid"></param>
        /// <param name="maxid"></param>
        /// <param name="accountname"></param>
        /// <returns></returns>
        private DataTable Get_OrderTemp(string functionName, int custid, int maxid, string accountname)
        {
            var remark = string.Empty;
            var dt = new DataTable();

            switch (functionName)
            {
                //室内装修工程
                case "AdornOrderHead":
                    //获取对应临时表
                    dt = serDt.GetTempdt(functionName);
                    remark = "AD";
                    break;
                //室内主材单
                case "MaterialOrderHead":
                    //获取对应临时表
                    dt = serDt.GetTempdt(functionName);
                    remark = "M";
                    break;
            }

            //将临时表进行赋值
            var row = dt.NewRow();
            row[0] = maxid;
            row[1] = remark + '-' + DateTime.Now.ToString("yyMMdd") + '-' + maxid;
            row[2] = custid;
            row[3] = accountname;
            row[4] = DateTime.Now.Date;
            row[5] = 'N';
            //row[6] = null;
            dt.Rows.Add(row);

            return dt;
        }

        /// <summary>
        /// (作用:对表体GridView进行导入) (注:包括插入及更新 及删除操作)
        /// </summary>
        /// <param name="functionName">功能名称(AdornOrder:室内装修工程 MaterialOrder:室内主材单)</param>
        /// <param name="dt">获取GridView内的DataTable</param>
        /// <param name="deldt">要进行删除的记录</param>
        /// <returns></returns>
        public bool SaveOrderEntry(string functionName, DataTable dt, DataTable deldt)
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
                            tempInsertdt = GetOrderTempRd(row, tempInsertdt);
                            break;
                        //修改状态
                        case "Modified":
                            tempUpdt = GetOrderTempRd(row, tempUpdt);
                            break;
                    }
                }
                //循环结束后分别将累积的临时表信息,进行插入或更新操作
                if (tempInsertdt.Rows.Count > 0)
                    Importdt(tableName, tempInsertdt);
                if (tempUpdt.Rows.Count > 0)
                    UpEntrydt(tableName, tempUpdt);
                //判断若deldt的行数>0的话,就循环将对应的记录删除
                if (deldt.Rows.Count > 0)
                    generateDt.Del(functionName, deldt);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 将记录插入至对应的临时表内(单据信息使用)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tempdt"></param>
        /// <returns></returns>
        private DataTable GetOrderTempRd(DataRow row, DataTable tempdt)
        {
            var temprow = tempdt.NewRow();
            for (var j = 0; j < tempdt.Columns.Count; j++)
            {
                temprow[j] = row[j];
            }
            tempdt.Rows.Add(temprow);
            return tempdt;
        }

        /// <summary>
        /// 将“材料信息管理”的表头信息插入至T_Pro_MaterialTree表内
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="pid">表头ID</param>
        /// <returns></returns>
        public bool InsertMaterialIntoDt(string functionName,int pid)
        {
            var result = true;
            try
            {
                //获取临时表
                var tempdt= serDt.GetTempdt(functionName);
                //获取对应表名
                var tablename= GetTableName(functionName);
                //获取“材料信息管理”-表头信息
                var sqlscript = sqlList.BD_SQLList("6", null);
                var materialdt=serDt.GetData(sqlscript);

                //循环将相关信息插入至临时表
                foreach (DataRow rows in materialdt.Rows)
                {
                    var row = tempdt.NewRow();
                    for (var i = 0; i < tempdt.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            row[0] = pid;
                        }
                        else
                        {
                            row[i] = rows[i-1];
                        }
                    }
                    tempdt.Rows.Add(row);
                }
                //执行插入信息
                if(tempdt.Rows.Count>0)
                    Importdt(tablename, tempdt);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region 角色权限

        /// <summary>
        /// 利用DT将相关记录插入至T_AD_Role 及 T_AD_RoleDtl内
        /// </summary>
        /// <param name="rolename"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertDtlIntoRole(string rolename,DataTable dt)
        {
            //获取对应表的最大ID值
            var reslutid = 0;
            //定义表头及表体临时表
            var tempdt=new DataTable();
            var tempdtdtl=new DataTable();

            try
            {
                //获取T_AD_Role表MAX(ID)值
                reslutid = Maxid("T_AD_Role");
                //将相关记录分别插入至T_AD_Role 及 T_AD_RoleDtl内


                //先将T_AD_ROLE表头信息插入
                Importdt("T_AD_Role", tempdt);
                //再将T_AD_RoleDtl表体信息插入
                Importdt("T_AD_RoleDtl", tempdtdtl);
            }
            catch (Exception)
            {
                reslutid = 0;
            }
            return reslutid;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Get_RoleTemp()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void Get_RoleDtlTemp()
        {
            
        }

        #endregion

    }
}
