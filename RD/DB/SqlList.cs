using System;

namespace RD.DB
{
    //作用:定义所需的SQL语句
    public class SqlList
    {
        //根据SQLID返回对应的SQL语句  
        private string _result;

        #region 基础信息库
        /// <summary>
        /// "基础信息库"SQL列表
        /// </summary>
        /// <param name="sqlid">SQL中转ID</param>
        /// <param name="parentId">表头ID</param>
        /// <returns></returns>
        public string BD_SQLList(string sqlid, string parentId)
        {
            //记录SQL报表中转ID
            switch (sqlid)
            {
                //客户信息管理-表头(全部)
                case "0":
                    _result = "select a.Id,a.parentid,a.CustType from dbo.T_BD_Cust a order by a.Parentid";
                    break;
                //客户信息管理-表体(全部)
                case "1":
                    _result = @"
                                SELECT a.Id,a.Custid,a.CustName AS '客户名称',a.HTypeid AS '房屋类型',a.HTypeName as '房屋类型名称',
	                                   a.Spare AS '装修地区',a.SpareAdd AS '装修地址',a.Cust_Add AS '客户通讯地址',
                                       a.Cust_Phone AS '客户联系方式',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                FROM dbo.T_BD_CustEntry a";
                    break;
                //客户信息管理-表体(针对表体信息查询)
                case "2":
                    _result = $@"
                                SELECT a.Id,a.Custid,a.CustName AS '客户名称',a.HTypeid AS '房屋类型',a.HTypeName as '房屋类型名称',
	                                   a.Spare AS '装修地区',a.SpareAdd AS '装修地址',a.Cust_Add AS '客户通讯地址',
                                       a.Cust_Phone AS '客户联系方式',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                FROM dbo.T_BD_CustEntry a
                                where a.id='{parentId}'";
                    break;
                #region 客户信息管理-表体(查询框有值时使用)
                //
                //case "2.1":
                //    _result = $@"
                //                 SELECT a.CustName AS '客户名称',a.HTypeid AS '房屋类型ID',)
                //                     a.Spare AS '装修地区',a.SpareAdd AS '装修地址',a.Cust_Add AS '客户通讯地址',
                //                        a.Cust_Phone AS '客户联系方式',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                //                 FROM dbo.T_BD_CustEntry a
                //                 where a.'{searchName}' like '%'+'{searchValue}'+'%'";
                //    break;
                #endregion
                //******************************************************************//
                //供应商管理-表头(全部)
                case "3":
                    _result = @"SELECT a.Id,a.parentid,a.SupType FROM dbo.T_BD_Supplier a order by a.Parentid";
                    break;
                //供应商管理-表体(全部)
                case "4":
                    _result = @"
                                   SELECT a.Id,a.Supid,a.SupName AS '供应商名称',a.Address AS '通讯地址',a.ContactName AS '联系人',
                                          a.ContactPhone AS '联系方式',a.GoNum AS '工商登记号',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                   FROM dbo.T_BD_SupplierEntry a
                               ";
                    break;
                //供应商管理-表体(针对表体信息查询)
                case "5":
                    _result = $@"
                                  SELECT a.Id,a.Supid,a.SupName AS '供应商名称',a.Address AS '通讯地址',a.ContactName AS '联系人',
                                          a.ContactPhone AS '联系方式',a.GoNum AS '工商登记号',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                   FROM dbo.T_BD_SupplierEntry a
                                   where a.id='{parentId}'";
                    break;
                #region 供应商管理-表体(查询框有值时使用)
                //
                //case "5.1":
                //    _result = $@"
                //                  SELECT a.SupName AS '供应商名称',a.Address AS '通讯地址',a.ContactName AS '联系人',
                //                          a.ContactPhone AS '联系方式',a.GoNum AS '工商登记号',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                //                   FROM dbo.T_BD_SupplierEntry a
                //                   where a.'{searchName}' like '%'+'{searchValue}'+'%'";
                //    break;
                #endregion
                //******************************************************************//
                //材料信息管理-表头(全部)
                case "6":
                    _result = @"SELECT a.Id,a.parentid,a.MaterialType FROM dbo.T_BD_Material a order by a.Parentid";
                    break;
                //材料信息管理-表体(全部)
                case "7":
                    _result = @"
                                  SELECT a.Id,a.MaterialId,a.MaterialName as '材料名称',a.MaterialSize as '材料规格',
                                         a.Supid as '材料供应商',a.SupName as '材料供应商名称',
                                         a.Unit as '单位',a.Price as '单价',
                                         a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_MaterialEntry a
                               ";
                    break;
                //材料信息管理-表体(针对表体信息查询)
                case "8":
                    _result = $@"
                                    SELECT a.Id,a.MaterialId,a.MaterialName as '材料名称',a.MaterialSize as '材料规格',
                                           a.Supid as '材料供应商',a.SupName as '材料供应商名称',
                                           a.Unit as '单位',a.Price as '单价',
                                           a.InputUser as '录入人',a.InputDt as '录入日期'
                                    FROM dbo.T_BD_MaterialEntry a
                                    where a.id='{parentId}'";
                    break;
                #region 材料信息管理-表体(查询框有值时使用)
                //
                //case "8.1":
                //    _result = $@"
                //                  SELECT a.MaterialName as '材料名称',a.MaterialSize as '材料规格',a.Supid as '材料供应商ID',
                //                           a.Unit as '单位',a.Price as '单价',
                //                           a.InputUser as '录入人',a.InputDt as '录入日期'
                //                  FROM dbo.T_BD_MaterialEntry a
                //                  where a.'{searchName}' like '%'+'{searchValue}'+'%'";
                //    break;
                #endregion
                //******************************************************************//
                //房屋类型及装修工程类别信息管理(表头)
                case "9":
                    _result = @"SELECT a.Id,a.parentid,a.HType FROM dbo.T_BD_HType a order by a.Parentid";
                    break;
                //房屋类型及装修工程类别信息管理(表体)
                case "10":
                    _result = @"
                                  SELECT a.Id,a.HTypeid,a.HtypeName as '类型信息名称',a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_HTypeEntry a";
                    break;
                //房屋类型及装修工程类别信息管理(针对明细信息框查询)
                case "10.1":
                    _result = @"
                                  SELECT a.Id,a.HTypeid,a.HtypeName as '类型信息名称',a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_HTypeEntry a
                                  inner join dbo.T_BD_HType b on a.id=b.id
                                  where b.HType='房屋类型'";
                    break;
                //房屋类型及装修工程类别信息管理(针对表体信息查询)
                case "11":
                    _result = $@"
                                  SELECT a.Id,a.HTypeid,a.HtypeName as '类型信息名称',a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_HTypeEntry a
                                  where a.id='{parentId}'";
                    break;
                #region 房屋类型及装修工程类别信息管理(查询框有值时使用)
                //
                //case "11.1":
                //    _result = $@"
                //                  SELECT a.HtypeName as '类型信息名称',a.InputUser as '录入人',a.InputDt as '录入日期'
                //                  FROM dbo.T_BD_HTypeEntry a
                //                  where a.'{searchName}' like '%'+'{searchValue}'+'%'";
                //    break;
                #endregion
                //房屋类型及装修工程类别信息管理-类别项目名称(针对表体信息查询)
                case "12":
                    _result = $@"
                                    SELECT a.Htypeid,a.ProjectId,a.ProjectName AS '项目名称',a.Unit AS '单位',a.price '单价',a.InputUser '录入人',a.InputDt '录入日期'
                                    FROM dbo.T_BD_HTypeProjectdtl a
                                    WHERE a.Htypeid='{parentId}'";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 根据功能名称查询功能列表
        /// </summary>
        /// <param name="factionName"></param>
        /// <returns></returns>
        public string BD_FunList(string factionName)
        {
            switch (factionName)
            {
                //客户信息管理
                case "Customer":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=1";
                    break;
                //供应商信息管理
                case "Supplier":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=2";
                    break;
                //材料信息管理
                case "Material":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=3";
                    break;
                //房屋类型及装修工程类别信息管理
                case "House":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=4";
                    break;
                //房屋类型及装修工程类别信息管理-类别项目名称
                case "HouseProject":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=5";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 根据功能名称插入表头信息
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="pid"></param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public string BD_InsertTree(string factionName, int pid, string treeName)
        {
            switch (factionName)
            {
                case "Customer":
                    _result = $@"INSERT INTO dbo.T_BD_Cust( ParentId, CustType )
                                 VALUES  ({pid},'{treeName}')";
                    break;
                case "Supplier":
                    _result = $@"INSERT INTO dbo.T_BD_Supplier(parentid,SupType)
                                VALUES ({pid},'{treeName}')";
                    break;
                case "Material":
                    _result = $@"INSERT INTO dbo.T_BD_Material(parentid,MaterialType)
                                 VALUES ({pid},'{treeName}')";
                    break;
                case "House":
                    _result = $@"INSERT INTO dbo.T_BD_HType(parentid,HType)
                                 VALUES({pid},'{treeName}')";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 根据功能名称更新节点信息
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="treeName">需要更新的节点名称</param>
        /// <param name="id">原节点ID</param>
        /// <returns></returns>
        public string BD_UpdateTree(string factionName, string treeName, int id)
        {
            switch (factionName)
            {
                case "Customer":
                    _result = $@"UPDATE dbo.T_BD_Cust SET CustType='{treeName}' WHERE Id={id}";
                    break;
                case "Supplier":
                    _result = $@"UPDATE dbo.T_BD_Supplier SET SupType='{treeName}' WHERE Id={id}";
                    break;
                case "Material":
                    _result = $@"UPDATE dbo.T_BD_Material SET MaterialType='{treeName}' WHERE Id={id}";
                    break;
                case "House":
                    _result = $@"UPDATE dbo.T_BD_HType SET HType='{treeName}' WHERE Id={id}";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 根据功能名称查询id=1时,各表头记录有没有值
        /// </summary>
        /// <returns></returns>
        public string BD_SearchNum(string factionName)
        {
            switch (factionName)
            {
                case "Customer":
                    _result = "select a.* from dbo.T_BD_Cust a where id=1";
                    break;
                case "Supplier":
                    _result = "select a.* from dbo.T_BD_Supplier a where id=1";
                    break;
                case "Material":
                    _result = "select a.* from dbo.T_BD_Material a where id=1";
                    break;
                case "House":
                    _result = "select a.* from dbo.T_BD_HType a where id=1";
                    break;
            }

            return _result;
        }

        /// <summary>
        /// 更新帐号密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpw"></param>
        /// <returns></returns>
        public string Up_User(string username, string userpw)
        {
            _result = $@"update T_AD_User set UserPassword='{userpw}' where UserName='{username}'";
            return _result;
        }

        /// <summary>
        /// 删除记录 包括表头及表体信息(基础信息库)
        /// </summary>
        /// <param name="factionName">功能名称</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public string BD_Del(string factionName, int id)
        {
            switch (factionName)
            {
                case "Customer":
                    _result =
                        $@"delete from dbo.T_BD_Cust where id='{id}';
                           delete from dbo.T_BD_CustEntry where id='{id}'";
                    break;
                case "Supplier":
                    _result =
                        $@"delete from dbo.T_BD_Supplier where id='{id}';
                           delete from dbo.T_BD_SupplierEntry where id='{id}'";
                    break;
                case "Material":
                    _result =
                        $@"delete from dbo.T_BD_Material where id='{id}';
                           delete from dbo.T_BD_MaterialEntry where id='{id}'";
                    break;
                case "House":
                    _result =
                        $@"delete from dbo.T_BD_HType where id='{id}';
                           delete from dbo.T_BD_HTypeEntry where id='{id}'";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 基础信库更新语句(更新表体时使用)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string BD_UpdateEntry(string tableName)
        {
            switch (tableName)
            {
                case "T_BD_CustEntry":
                    _result = @"
                                     Update a set a.CustName=@CustName,a.HTypeid=@HTypeid,a.HTypeName=@HTypeName,a.Spare=@Spare,a.SpareAdd=@SpareAdd,
                                                  a.Cust_Add=@Cust_Add,a.Cust_Phone=@Cust_Phone
                                     from dbo.T_BD_CustEntry a
                                     where Custid=@Custid;
                                ";
                    break;
                case "T_BD_SupplierEntry":
                    _result = @"
                                     Update a set a.SupName=@SupName,a.Address=@Address,a.ContactName=@ContactName,a.ContactPhone=@ContactPhone,
                                                  a.GoNum=@GoNum
                                     from  dbo.T_BD_SupplierEntry a
                                     where a.Supid=@Supid;                       
                                ";
                    break;
                case "T_BD_MaterialEntry":
                    _result = @"
                                     Update a set a.MaterialName=@MaterialName,a.MaterialSize=@MaterialSize,
                                                  a.Supid=@Supid,a.SupName=@SupName,a.Unit=@Unit,a.Price=@Price
                                     from dbo.T_BD_MaterialEntry a
                                     where a.MaterialId=@MaterialId;
                               ";
                    break;
                case "T_BD_HTypeEntry":
                    _result = @"
                                    Update a set a.HtypeName=@HtypeName
                                    from dbo.T_BD_HTypeEntry a
                                    where a.HTypeid=@HTypeid;
                                ";
                    break;
                case "T_BD_HTypeProjectDtl":
                    _result = @"
                                    UPDATE T_BD_HTypeProjectDtl SET ProjectName=@ProjectName,Unit=@Unit,Price=@Price
                                    WHERE ProjectId=@ProjectId;
                                ";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 基础信息库查询表体语句(更新时使用) 只显示TOP 1记录
        /// </summary>
        /// <returns></returns>
        public string BD_SearchTempEntry(string tableName)
        {
            _result = $@"
                          SELECT Top 1 a.*
                          FROM {tableName} a
                        ";
            return _result;
        }

        /// <summary>
        /// 室内装修工程单-装修工程下拉列表使用
        /// </summary>
        /// <returns></returns>
        public string CustInfoList()
        {
            _result = @"
                           SELECT a.Id,a.CustType
                           FROM dbo.T_BD_Cust a
                           WHERE a.Id<>1";
            return _result;
        }

        #endregion

        #region 单据相关

        /// <summary>
        /// 室内装修工程单-装修工程下拉列表使用
        /// </summary>
        /// <returns></returns>
        public string Order_Adorn_SearchDropDownList()
        {
            _result = @"
                                  SELECT a.HTypeid,a.HtypeName
                                  FROM dbo.T_BD_HTypeEntry a
                                  inner join dbo.T_BD_HType b on a.id=b.id
                                  where b.HType='装修工程'";
            return _result;
        }

        /// <summary>
        /// 室内装修工程单-根据表头ID读取表体信息(单据状态为R时使用) (作用:初始化GridView内容)
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string Order_SearchDtl(string functionname, int pid)
        {
            switch (functionname)
            {
                case "AdornOrder":
                    _result = $@"
                             SELECT a.id,a.Treeid,a.adornid,a.HTypeid 工程类别ID,a.HTypeProjectName 项目名称,
                                    a.Unit 单位名称,a.quantities 工程量,a.FinalPrice 综合单价,a.Ren_Cost 人工费用,a.Fu_Cost 辅材费用,
	                                a.Price 单价,a.Temp_Price 临时材料单价,a.Amount 合计,a.FRemark 备注,a.InputUser 录入人,a.InputDt 录入日期
                             FROM dbo.T_PRO_AdornEntry a
                             WHERE a.Id='{pid}'
                          ";
                    break;
                case "MaterialOrder":
                    _result = $@"
                               SELECT a.Id,a.TreeId,a.EntryID,a.MaterialId 材料ID,MaterialName 材料名称,
	                                a.Unit 单位名称,a.quantities 工程量,a.FinalPrice  综合单价,a.Ren_Cost 人工费用,
	                                a.Fu_Cost 辅材费用,a.Price 单价,a.Temp_Price 临时材料单价,
	                                a.Amount 合计,a.FRemark 备注,a.InputUser 录入人,a.InputDt 录入日期                       
                               FROM dbo.T_PRO_MaterialEntry a
                               WHERE a.Id='{pid}'
                                ";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 室内装修工程单-根据表头ID读取表头信息(单据状态为R时使用)
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string Order_TreeView(string functionname,int pid)
        {
            switch (functionname)
            {
                case "AdornOrder":
                    _result = $@"
                            SELECT a.Id,a.Treeid,a.ParentId,a.TypeName
                            FROM dbo.T_PRO_AdornTree a
                            WHERE a.Id='{pid}'
                           ";
                    break;
                case "MaterialOrder":
                    _result = $@"
                            SELECT a.Id,a.Treeid,a.ParentId,a.MaterialType
                            FROM dbo.T_PRO_MaterialTree a
                            WHERE a.Id='{pid}'
                           ";
                    break;
            }

            return _result;
        }

        /// <summary>
        /// 根据功能名称查询id=1时,各表头记录有没有值(室内装修工程 及 室内主材单使用)
        /// </summary>
        /// <returns></returns>
        public string Order_SearchNum(string functionname)
        {
            switch (functionname)
            {
                case "AdornOrder":
                    _result = "select a.* from dbo.T_PRO_AdornTree a where id=1";
                    break;
                case "MaterialOrder":
                    _result = "select a.* from T_PRO_MaterialTree a where id=1";
                    break;
            }

            return _result;
        }

        /// <summary>
        /// 根据功能名称插入表头信息
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public string Order_InsertTree(string factionName, int id, int pid, string treeName)
        {
            switch (factionName)
            {
                case "AdornOrder":
                    _result = $@"
                                  INSERT INTO dbo.T_PRO_AdornTree( Id,ParentId, TypeName )
                                  VALUES  ({id},{pid},'{treeName}')";
                    break;
                case "MaterialOrder":
                    _result = $@"
                                  INSERT INTO dbo.T_PRO_MaterialTree( Id,ParentId, MaterialType )
                                  VALUES  ({id},{pid},'{treeName}')
                                ";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 根据功能名称更新节点信息(室内装修工程 及 室内主材单使用)
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="treeName">需要更新的节点名称</param>
        /// <param name="id">原节点ID</param>
        /// <returns></returns>
        public string Order_UpdateTree(string factionName, string treeName, int id)
        {
            switch (factionName)
            {
                case "AdornOrder":
                    _result = $@"UPDATE dbo.T_PRO_AdornTree SET TypeName='{treeName}' WHERE Treeid='{id}'";
                    break;
                case "MaterialOrder":
                    _result = $@"UPDATE dbo.T_PRO_MaterialTree SET MaterialType='{treeName}' WHERE Treeid='{id}'";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 获取最大的单据ID (室内装修工程 及 室内主材单使用)
        /// </summary>
        /// <returns></returns>
        public string Get_OrderMaxid(string tablename)
        {
            _result = $@"
                         SELECT ISNULL(MAX(id),0)+1
                         FROM dbo.{tablename}";
            return _result;
        }

        /// <summary>
        /// 根据主键ID获取单据表相关信息
        /// </summary>
        /// <param name="factionname"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string Get_OrderInfo(string factionname, int pid)
        {
            switch (factionname)
            {
                case "AdornOrder":
                    _result = $@"
                                    SELECT a.OrderNo '单据名称',b.CustName '客户名称',b.HTypeName '房屋类型名称',b.SpareAdd '装修地址',a.Fstatus '审核状态'
                                    FROM dbo.T_PRO_Adorn a
                                    INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                                    WHERE a.id='{pid}'
                                ";
                    break;
                case "MaterialOrder":
                    _result = $@"
                                    SELECT a.OrderNo '单据名称',b.CustName '客户名称',b.HTypeName '房屋类型名称',b.SpareAdd '装修地址',a.Fstatus '审核状态'
                                    FROM dbo.T_PRO_Material a
                                    INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                                    WHERE a.id='{pid}'
                                ";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 删除记录 包括表头及表体信息(室内装修工程 室内主材)
        /// </summary>
        /// <param name="factionName">功能名称</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public string OrderInfo_Del(string factionName, int id)
        {
            switch (factionName)
            {
                case "AdornOrderHead":
                    _result =$@"delete from dbo.T_PRO_AdornTree where Treeid='{id}';
                               delete from dbo.T_PRO_AdornEntry  where Treeid='{id}'";
                    break;
                case "MaterialOrderHead":
                    _result =$@"delete from dbo.T_PRO_MaterialTree where Treeid='{id}';
                                delete from dbo.T_PRO_MaterialEntry where Treeid='{id}'";
                    break;
                //删除“室内装修工程”表体信息
                case "AdornOrder":
                    _result = $@"DELETE FROM dbo.T_PRO_AdornEntry WHERE adornid='{id}'";
                    break;
                //删除"室内主材"表体信息
                case "MaterialOrder":
                    _result = $@"DELETE FROM dbo.T_PRO_MaterialEntry WHERE EntryID='{id}'";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 根据获取树菜单节点刷新表体内容
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="id">主键ID</param>
        /// <param name="treeid">树菜单ID treeid 当为-1时,表体读取全部记录</param>
        /// <param name="dropdownlistid">室内装修工程下拉框使用</param>
        /// <returns></returns>
        public string OrderInfo_dtl(string factionName, int id, int treeid,int dropdownlistid)
        {
            switch (factionName)
            {
                case "AdornOrder":
                    //为-1表示读取ALL
                    if (treeid == -1)
                    {
                        _result = $@"SELECT a.id,a.Treeid,a.adornid,a.HTypeid 工程类别ID,a.HTypeProjectName 项目名称,
                                            a.Unit 单位名称,a.quantities 工程量,a.FinalPrice 综合单价,a.Ren_Cost 人工费用,a.Fu_Cost 辅材费用,
                                            a.Price 单价,a.Temp_Price 临时材料单价,a.Amount 合计,a.FRemark 备注,a.InputUser 录入人,a.InputDt 录入日期
                                    FROM dbo.T_PRO_AdornEntry a
                                    WHERE a.Id='{id}' AND a.HTypeid='{dropdownlistid}'";
                    }
                    else
                    {
                        _result = $@"
                                        SELECT a.id,a.Treeid,a.adornid,a.HTypeid 工程类别ID,a.HTypeProjectName 项目名称,
                                               a.Unit 单位名称,a.quantities 工程量,a.FinalPrice 综合单价,a.Ren_Cost 人工费用,a.Fu_Cost 辅材费用,
                                               a.Price 单价,a.Temp_Price 临时材料单价,a.Amount 合计,a.FRemark 备注,a.InputUser 录入人,a.InputDt 录入日期
                                        FROM dbo.T_PRO_AdornEntry a
                                        WHERE a.Id='{id}' AND a.Treeid='{treeid}' AND a.HTypeid='{dropdownlistid}'
                                    ";
                    }
                    break;
                case "MaterialOrder":
                    //为-1表示读取ALL
                    if (treeid == -1)
                    {
                        _result = $@"SELECT a.id,a.TreeId,a.EntryID,a.MaterialId 材料ID,a.MaterialName 材料名称,a.Unit 单位名称,
                                            a.quantities 工程量,a.FinalPrice 综合单价,a.Ren_Cost 人工费用,a.Fu_Cost 辅材费用,a.Price 单价,
	                                        a.Temp_Price 临时材料单价,a.Amount 合计,a.FRemark 备注,a.InputUser 录入人,a.InputDt 录入日期
                                    FROM dbo.T_PRO_MaterialEntry a
                                    WHERE a.Id='{id}'";
                    }
                    else
                    {
                        _result = $@"SELECT a.id,a.TreeId,a.EntryID,a.MaterialId 材料ID,a.MaterialName 材料名称,a.Unit 单位名称,
                                            a.quantities 工程量,a.FinalPrice 综合单价,a.Ren_Cost 人工费用,a.Fu_Cost 辅材费用,a.Price 单价,
	                                        a.Temp_Price 临时材料单价,a.Amount 合计,a.FRemark 备注,a.InputUser 录入人,a.InputDt 录入日期
                                    FROM dbo.T_PRO_MaterialEntry a
                                    WHERE a.Id='{id}' AND a.TreeId='{treeid}'";
                    }
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 更新单据状态(审核与反审核时使用)
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="confirmid">审核ID 0:审核 1:反审核</param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public string ChangeOrderState(string functionName, int confirmid,int id)
        {
            switch (functionName)
            {
                case "AdornOrder":
                    _result = confirmid == 0 ? $@"UPDATE dbo.T_PRO_Adorn SET Fstatus='Y',FstatusDt=GETDATE() WHERE Id='{id}'" 
                                                    : $@"UPDATE dbo.T_PRO_Adorn SET Fstatus='N',FstatusDt=NULL WHERE Id='{id}'";
                    break;
                case "MaterialOrder":
                    _result = confirmid == 0 ? $@"UPDATE dbo.T_PRO_Material SET Fstatus='Y',FstatusDt=GETDATE() WHERE Id='{id}'"
                                                    : $@"UPDATE dbo.T_PRO_Material SET Fstatus='N',FstatusDt=NULL WHERE Id='{id}'";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 更新订单表体记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Order_UpdateEntry(string tableName)
        {
            switch (tableName)
            {
                case "T_PRO_AdornEntry":
                    _result = @"
                                     UPDATE dbo.T_PRO_AdornEntry SET HTypeProjectName=@HTypeProjectName,Unit=@Unit,quantities=@quantities,FinalPrice=@FinalPrice,
								                                     Ren_Cost=@Ren_Cost,Fu_Cost=@Fu_Cost,Price=@Price,Temp_Price=@Temp_Price,Amount=@Amount,
								                                     FRemark=@FRemark
                                     WHERE adornid=@adornid
                                ";
                    break;
                case "T_PRO_MaterialEntry":
                    _result = $@"
                                    UPDATE dbo.T_PRO_MaterialEntry SET MaterialId=@MaterialId,MaterialName=@MaterialName,Unit=@Unit,quantities=@quantities,
								                                       FinalPrice=@FinalPrice,Ren_Cost=@Ren_Cost,Fu_Cost=@Fu_Cost,Price=@Price,
								                                       Temp_Price=@Temp_Price,Amount=@Amount,FRemark=@FRemark
                                    WHERE EntryID=@EntryID
                                ";
                    break;

            }
            return _result;
        }

        #endregion

        #region 主窗体

        /// <summary>
        /// 主窗体(帐号权限)窗体下拉列表
        /// </summary>
        /// <returns></returns>
        public string Main_Admin_Dropdownlist(string functionName)
        {
            switch (functionName)
            {
                //主窗体使用
                //客户名称信息
                case "Customer":
                    _result = @"SELECT -1 Custid,'全部' CustName
                                UNION
                                SELECT a.Custid,a.CustName FROM dbo.T_BD_CustEntry a";
                    break;
                //单据创建年份
                case "OrderYear":
                    _result = @"SELECT 2019 Yearid,2019 YearName
                                UNION
                                SELECT year(getdate()) Yearid,YEAR(GETDATE()) YearName";
                    break;
                //单据类型
                case "OrderType":
                    _result = @"SELECT 1 OrderId,'室内装修工程单' OrderName
                                UNION
                                SELECT 2 OrderId,'室内主材单' OrderName";
                    break;
                //房屋类型
                case "HouseType":
                    _result = @"SELECT a.HTypeid,a.HtypeName 
                                FROM dbo.T_BD_HTypeEntry a
                                INNER JOIN dbo.T_BD_HType b ON a.id=b.Id
                                WHERE b.Id=2";
                    break;
                //审核状态
                case "ConfirmType":
                    _result = @"SELECT 'N' FStatus,'末审核' FStatusName
                                UNION
                                SELECT 'Y' FStatus,'已审核' FStatusName";
                    break;
                //帐号权限窗体使用
                //职员名称
                case "AccountUser":
                    _result = @"
                                    SELECT '-1' Userid,'全部' UserName
                                    union
                                    SELECT a.UserId,a.UserName
                                    FROM dbo.T_AD_User a
                                    WHERE a.CloseStatus!='Y'
                                    AND a.Fstatus='Y'
                                ";
                    break;
                //职员性别
                case "AccountSex":
                    _result = @"
                                    SELECT 0 Sexid,'全部' SexName
                                    union
                                    SELECT 1 Sexid,'男' SexName
                                    union
                                    SELECT 2 Sexid,'女' SexName
                               ";
                    break;
                //职员帐号关闭状态
                case "AccountCloseStatue":
                    _result = @"
                                    SELECT 'N' Closeid,'末关闭' CloseName
                                    union
                                    SELECT 'Y' Closeid,'已关闭' CloseName
                                ";
                    break;
                //角色名称
                case "Role":
                    _result = @"
                                    SELECT 0 id ,'全部' RoleName
                                    UNION
                                    SELECT a.id,a.RoleName FROM dbo.T_AD_Role a
                                ";
                    break;
                //功能大类名称
                case "FunType":
                    _result = @"
                                    SELECT a.Funid,a.FunName
                                    FROM dbo.T_AD_Fun a
                                    WHERE a.ParentId=0
                                ";
                    break;
            }
            return _result;
        }

        /// <summary>
        /// 主窗体查询内容-室内装修工程单
        /// </summary>
        /// <param name="custid"></param>
        /// <param name="yearid"></param>
        /// <param name="hTypeid"></param>
        /// <param name="confirmfstatus"></param>
        /// <param name="confirmdt"></param>
        /// <returns></returns>
        public string Main_Adorndtl(int custid,int yearid,int hTypeid,string confirmfstatus,DateTime confirmdt)
        {
            _result = confirmfstatus == "Y"
                ? (custid == -1
                    ? $@"SELECT a.Id,'AdornOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                    b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                    CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                    a.InputUser 单据录入人,a.InputDt 单据录入日期
                            FROM dbo.T_PRO_Adorn a
                            INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                            WHERE YEAR(a.InputDt)='{yearid}'	                  --年份
                            AND b.HTypeid='{hTypeid}'		                  --房屋类型ID
                            AND A.Fstatus='{confirmfstatus}'                  --审核状态
                            AND CONVERT(DATE,a.FstatusDt)=CONVERT(DATE,'{confirmdt}')       --审核日期(注:当审核状态为Y时使用)"
                    : $@"SELECT a.Id,'AdornOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                   b.Spare 装修地区,b.SpareAdd 装修地址, b.Cust_Add 客户通讯地址, b.Cust_Phone 客户联系方式,
                                       CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态, a.FstatusDt 审核日期,
                                       a.InputUser 单据录入人, a.InputDt 单据录入日期
                                FROM dbo.T_PRO_Adorn a
                                INNER JOIN dbo.T_BD_CustEntry b ON a.Custid = b.Custid
                                WHERE a.Custid = '{custid}'--客户ID
                                AND YEAR(a.InputDt) = '{yearid}'--年份
                                AND b.HTypeid = '{hTypeid}'--房屋类型ID
                                AND A.Fstatus = '{confirmfstatus}'--审核状态
                                AND CONVERT(DATE, a.FstatusDt) = CONVERT(DATE, '{confirmdt}')--审核日期(注: 当审核状态为Y时使用)")
                : (custid == -1
                    ? $@"SELECT a.Id,'AdornOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                    b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                    CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                    a.InputUser 单据录入人,a.InputDt 单据录入日期
                            FROM dbo.T_PRO_Adorn a
                            INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                            WHERE YEAR(a.InputDt)='{yearid}'	    --年份
                            AND b.HTypeid='{hTypeid}'		    --房屋类型ID
                            AND A.Fstatus='{confirmfstatus}'    --审核状态"
                    : $@"SELECT a.Id,'AdornOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                   b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                   CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                   a.InputUser 单据录入人,a.InputDt 单据录入日期
                            FROM dbo.T_PRO_Adorn a
                            INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                            WHERE a.Custid='{custid}'			--客户ID
                            AND YEAR(a.InputDt)='{yearid}'	    --年份
                            AND b.HTypeid='{hTypeid}'		    --房屋类型ID
                            AND A.Fstatus='{confirmfstatus}'    --审核状态");
            return _result;
        }

        /// <summary>
        /// 主窗体查询内容-室内主材单
        /// </summary>
        /// <param name="custid"></param>
        /// <param name="yearid"></param>
        /// <param name="hTypeid"></param>
        /// <param name="confirmfstatus"></param>
        /// <param name="confirmdt"></param>
        /// <returns></returns>
        public string Main_Material(int custid, int yearid, int hTypeid, string confirmfstatus, DateTime confirmdt)
        {
            _result = confirmfstatus == "Y"
                ? (custid == -1
                    ? $@"SELECT a.Id,'MaterialOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                   b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                   CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                   a.InputUser 单据录入人,a.InputDt 单据录入日期
                                FROM dbo.T_PRO_Material A
                                INNER JOIN dbo.T_BD_CustEntry B ON a.Custid=b.Custid
                                WHERE YEAR(a.InputDt)='{yearid}'	                           --年份
                                AND b.HTypeid='{hTypeid}'		                               --房屋类型ID
                                AND A.Fstatus='{confirmfstatus}'                               --审核状态
                                AND CONVERT(DATE,a.FstatusDt)=CONVERT(DATE,'{confirmdt}')      --审核日期(注:当审核状态为Y时使用)"
                    : $@"SELECT a.Id,'MaterialOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                   b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                   CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                   a.InputUser 单据录入人,a.InputDt 单据录入日期
                                FROM dbo.T_PRO_Material A
                                INNER JOIN dbo.T_BD_CustEntry B ON a.Custid=b.Custid
                                WHERE a.Custid='{custid}'			                           --客户ID
                                AND YEAR(a.InputDt)='{yearid}'	                               --年份
                                AND b.HTypeid='{hTypeid}'		                               --房屋类型ID
                                AND A.Fstatus='{confirmfstatus}'                               --审核状态
                                AND CONVERT(DATE,a.FstatusDt)=CONVERT(DATE,'{confirmdt}')      --审核日期(注:当审核状态为Y时使用)")
                : (custid == -1
                    ? $@"SELECT a.Id,'MaterialOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                    b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                    CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                    a.InputUser 单据录入人,a.InputDt 单据录入日期
                                FROM dbo.T_PRO_Material A
                                INNER JOIN dbo.T_BD_CustEntry B ON a.Custid=b.Custid
                                WHERE YEAR(a.InputDt)='{yearid}'	    --年份
                                AND b.HTypeid='{hTypeid}'		    --房屋类型ID
                                AND A.Fstatus='{confirmfstatus}'    --审核状态"
                    : $@"SELECT a.Id,'MaterialOrder' ordertype,a.OrderNo 单据编号,b.CustName 客户名称,b.HTypeName 房屋类型名称,
	                                    b.Spare 装修地区,b.SpareAdd 装修地址,b.Cust_Add 客户通讯地址,b.Cust_Phone 客户联系方式,
	                                    CASE a.Fstatus WHEN 'Y' THEN '已审核' ELSE '末审核' END 审核状态,a.FstatusDt 审核日期,
	                                    a.InputUser 单据录入人,a.InputDt 单据录入日期
                                FROM dbo.T_PRO_Material A
                                INNER JOIN dbo.T_BD_CustEntry B ON a.Custid=b.Custid
                                WHERE a.Custid='{custid}'			--客户ID
                                AND YEAR(a.InputDt)='{yearid}'	    --年份
                                AND b.HTypeid='{hTypeid}'		    --房屋类型ID
                                AND A.Fstatus='{confirmfstatus}'    --审核状态");
            return _result;
        }

        #endregion

        #region 帐号权限

        /// <summary>
        /// 帐号权限主窗体查询
        /// </summary>
        /// <returns></returns>
        public string Admindtl(int userid, int sexid, string closeid, string confirmstatus, DateTime dtTime)
        {
            //显示全部记录
            _result = userid == -1
                ? $@"SELECT a.UserId,a.UserName 职员名称,
                               CASE a.UserSex WHEN '1' THEN '男' ELSE '女' END  职员性别,
	                           a.UserInDt 入职日期,
	                           CASE WHEN a.CloseStatus='Y' THEN '已关闭' ELSE '末关闭' END 关闭状态,
	                           CASE WHEN a.Fstatus='Y' THEN '已审核' ELSE '末审核' END 审核状态,
	                           a.FstatusDt 审核日期
                        FROM dbo.T_AD_User a
                        WHERE a.UserSex='{sexid}'           --性别
                        AND a.Fstatus='{confirmstatus}'     --审核状态
                        AND a.CloseStatus='{closeid}'       --关闭状态
                        AND CONVERT(DATE,a.UserInDt)=CONVERT(DATE,'{dtTime}') --入职日期"
                : $@"SELECT a.UserId,a.UserName 职员名称,
                               CASE a.UserSex WHEN '1' THEN '男' ELSE '女' END  职员性别,
	                           a.UserInDt 入职日期,
	                           CASE WHEN a.CloseStatus='Y' THEN '已关闭' ELSE '末关闭' END 关闭状态,
	                           CASE WHEN a.Fstatus='Y' THEN '已审核' ELSE '末审核' END 审核状态,
	                           a.FstatusDt 审核日期
                        FROM dbo.T_AD_User a
                        WHERE a.UserId='{userid}'           --帐号ID
                        AND a.UserSex='{sexid}'             --性别
                        AND a.Fstatus='{confirmstatus}'     --审核状态
                        AND a.CloseStatus='{closeid}'       --关闭状态
                        AND CONVERT(DATE,a.UserInDt)=CONVERT(DATE,'{dtTime}') --入职日期";

            return _result;
        }

        /// <summary>
        /// 角色信息管理查询
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public string Admin_roledtl(int roleid)
        {
            _result = roleid == 0
                ? $@"SELECT A.Id,A.RoleName 角色名称,
                                   A.InputUser 创建人,A.InputDt 创建日期,
	                               CASE WHEN a.CloseStatus='Y' THEN '已关闭' ELSE '末关闭' END 关闭状态,
	                               CASE WHEN a.Fstatus='Y' THEN '已审核' ELSE '末审核' END 审核状态,
	                               a.FstatusDt 审核日期
                            FROM dbo.T_AD_Role A"
                : $@"SELECT A.Id,A.RoleName 角色名称,
                                   A.InputUser 创建人,A.InputDt 创建日期,
	                               CASE WHEN a.CloseStatus='Y' THEN '已关闭' ELSE '末关闭' END 关闭状态,
	                               CASE WHEN a.Fstatus='Y' THEN '已审核' ELSE '末审核' END 审核状态,
	                               a.FstatusDt 审核日期
                            FROM dbo.T_AD_Role A
                            WHERE a.Id='{roleid}'";
            return _result;
        }

        /// <summary>
        /// 功能权限明细查询
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="funtypeid"></param>
        /// <returns></returns>
        public string Admin_roleFundtl(int roleid ,int funtypeid )
        {
            _result = $@"SELECT a.EntryID,
	                            a.FunName 功能名称,
	                            CASE WHEN a.CanShow='Y' THEN '是' ELSE '否' END 是否显示,
	                            CASE WHEN a.CanBackConfirm='Y' THEN '是' ELSE '否' END 是否反审核,
	                            CASE WHEN a.CanDel='Y' THEN '是' ELSE '否' END 是否可删除,
	                            a.InputUser 权限创建人,a.InputDt 权限创建日期
                        FROM dbo.T_AD_RoleDtl a
                        WHERE a.Id='{roleid}'
                        AND a.Funid='{funtypeid}'";
            return _result;
        }

        /// <summary>
        /// 获取T_AD_Role角色表-表头信息
        /// </summary>
        /// <returns></returns>
        public string Get_RoleHead(int roleid)
        {
            _result = $@"SELECT * FROM dbo.T_AD_Role where id='{roleid}'";
            return _result;
        }

        /// <summary>
        /// 获取T_AD_Fun表明细功能信息
        /// </summary>
        /// <returns></returns>
        public string Get_RoleDtl(int parentid)
        {
            _result = $@"SELECT * FROM dbo.T_AD_Fun a WHERE a.ParentId={parentid}";
            return _result;
        }

        #endregion
    }
}
