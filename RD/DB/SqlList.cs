﻿namespace RD.DB
{
    public class SqlList
    {
        //根据SQLID返回对应的SQL语句  
        private string _result;         

        /// <summary>
        /// "基础信息库"SQL列表
        /// </summary>
        /// <param name="sqlid">SQL中转ID</param>
        /// <param name="parentId">表头ID</param>
        /// <returns></returns>
        public string BD_SQLList(string sqlid,string parentId)
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
                    _result =$@"
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
                case "Customer":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=1";
                    break;
                case "Supplier":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=2";
                    break;
                case "Material":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=3";
                    break;
                case "House":
                    _result = "select a.ColId,a.ColName from T_BD_FunList a where a.FunId=4";
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
        public string BD_InsertTree(string factionName,int pid, string treeName)
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
        public string BD_UpdateTree(string factionName,string treeName,int id)
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
        public string PRO_Adorn_SearchDropDownList()
        {
            var result = @"
                                  SELECT a.HTypeid,a.HtypeName
                                  FROM dbo.T_BD_HTypeEntry a
                                  inner join dbo.T_BD_HType b on a.id=b.id
                                  where b.HType='装修工程'";
            return result;
        }

        /// <summary>
        /// 室内装修工程单-根据表头ID读取表体信息(单据状态为R时使用)
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string Pro_Adorn_SearchDtl(int pid)
        {
            var result = $@"
                             SELECT a.id,a.Treeid,a.adornid,a.HTypeid,a.HTypeProjectName,a.Unit,a.quantities,a.FinalPrice,a.Ren_Cost,a.Fu_Cost,
	                               a.Price,a.Amount,a.FRemark,a.InputUser,a.InputDt
                             FROM dbo.T_PRO_AdornEntry a
                             WHERE a.Id='{pid}'
                          ";
            return result;
        }

        /// <summary>
        /// 室内装修工程单-根据表头ID读取表头信息(单据状态为R时使用)
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string Pro_Adorn_SearchTreeView(int pid)
        {
            var result = $@"
                            SELECT a.Treeid,a.ParentId,a.TypeName
                            FROM dbo.T_PRO_AdornTree a
                            WHERE a.Id='{pid}'
                           ";
            return result;
        }

        /// <summary>
        /// 根据功能名称查询id=1时,各表头记录有没有值(室内装修工程 及 室内主材单使用)
        /// </summary>
        /// <returns></returns>
        public string Pro_SearchNum(string factionName)
        {
            switch (factionName)
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
        public string Pro_InsertTree(string factionName, int id,int pid, string treeName)
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
        public string Pro_UpdateTree(string factionName, string treeName, int id)
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
        public string Get_OrderInfo(string factionname,int pid)
        {
            switch (factionname)
            {
                case "Pro_Adorn":
                    _result = $@"
                                    SELECT a.OrderNo '单据名称',b.CustName '客户名称',b.HTypeName '房屋类型名称',b.SpareAdd '装修地址' 
                                    FROM dbo.T_PRO_Adorn a
                                    INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                                    WHERE a.id='{pid}'
                                ";
                    break;
                case "Pro_Material":
                    _result = $@"
                                    SELECT a.OrderNo '单据名称',b.CustName '客户名称',b.HTypeName '房屋类型名称',b.SpareAdd '装修地址' 
                                    FROM dbo.T_PRO_Material a
                                    INNER JOIN dbo.T_BD_CustEntry b ON a.Custid=b.Custid
                                    WHERE a.id='{pid}'
                                ";
                    break;
            }
            return _result;
        }

    }
}
