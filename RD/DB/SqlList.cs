namespace RD.DB
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
        /// <param name="searchName">查询选择列名-查询框有值时使用</param>
        /// <param name="searchValue">查询所填值-查询框有值时使用</param>
        /// <returns></returns>
        public string BD_SQLList(string sqlid,string parentId,string searchName,string searchValue)
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
                                SELECT a.CustName AS '客户名称',a.HTypeid AS '房屋类型ID',
	                                   a.Spare AS '装修地区',a.SpareAdd AS '装修地址',a.Cust_Add AS '客户通讯地址',
                                       a.Cust_Phone AS '客户联系方式',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                FROM dbo.T_BD_CustEntry a";
                    break;
                //客户信息管理-表体(针对表体信息查询)
                case "2":
                    _result =$@"
                                SELECT a.CustName AS '客户名称',a.HTypeid AS '房屋类型ID',
	                                   a.Spare AS '装修地区',a.SpareAdd AS '装修地址',a.Cust_Add AS '客户通讯地址',
                                       a.Cust_Phone AS '客户联系方式',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                FROM dbo.T_BD_CustEntry a
                                where a.Custid='{parentId}'";
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
                    _result = @"SELECT a.Id,a.parentid,a.SupType FROM dbo.T_BD_Supplier a";
                    break;
                //供应商管理-表体(全部)
                case "4":
                    _result = @"
                                   SELECT a.SupName AS '供应商名称',a.Address AS '通讯地址',a.ContactName AS '联系人',
                                          a.ContactPhone AS '联系方式',a.GoNum AS '工商登记号',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                   FROM dbo.T_BD_SupplierEntry a
                               ";
                    break;
                //供应商管理-表体(针对表体信息查询)
                case "5":
                    _result = $@"
                                  SELECT a.SupName AS '供应商名称',a.Address AS '通讯地址',a.ContactName AS '联系人',
                                          a.ContactPhone AS '联系方式',a.GoNum AS '工商登记号',a.InputUser AS '录入人',a.InputDt AS '录入日期'
                                   FROM dbo.T_BD_SupplierEntry a
                                   where a.Supid='{parentId}'";
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
                    _result = @"SELECT a.Id,a.parentid,a.MaterialType FROM dbo.T_BD_Material a";
                    break;
                //材料信息管理-表体(全部)
                case "7":
                    _result = @"
                                  SELECT a.MaterialName as '材料名称',a.MaterialSize as '材料规格',a.Supid as '材料供应商ID',
                                         a.Unit as '单位',a.Price as '单价',
                                         a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_MaterialEntry a
                               ";
                    break;
                //材料信息管理-表体(针对表体信息查询)
                case "8":
                    _result = $@"
                                    SELECT a.MaterialName as '材料名称',a.MaterialSize as '材料规格',a.Supid as '材料供应商ID',
                                           a.Unit as '单位',a.Price as '单价',
                                           a.InputUser as '录入人',a.InputDt as '录入日期'
                                    FROM dbo.T_BD_MaterialEntry a
                                    where a.MaterialId='{parentId}'";   
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
                    _result = @"SELECT a.Id,a.parentid,a.HType FROM dbo.T_BD_HType a";
                    break;
                //房屋类型及装修工程类别信息管理(表体)
                case "10":
                    _result = @"
                                  SELECT a.HtypeName as '类型信息名称',a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_HTypeEntry a";
                    break;
                //房屋类型及装修工程类别信息管理(针对表体信息查询)
                case "11":
                    _result = $@"
                                  SELECT a.HtypeName as '类型信息名称',a.InputUser as '录入人',a.InputDt as '录入日期'
                                  FROM dbo.T_BD_HTypeEntry a
                                  where a.HTypeid='{parentId}'";
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
            }
            return _result;
        }
    }
}
