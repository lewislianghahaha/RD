using System;
using System.Data;

namespace RD.DB
{
    //作用:创建所需的临时表
    public class DtList
    {
        /// <summary>
        /// 获取"客户信息管理"表体临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CustEmptydt()
        {
            var dt=new DataTable();
            for (var i = 0; i < 11; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    //表头主键ID
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1:
                        dc.ColumnName = "Custid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 2: //CustName
                        dc.ColumnName = "客户名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //HTypeid
                        dc.ColumnName = "房屋类型id"; 
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 4: //HTypeName
                        dc.ColumnName = "房屋类型名称";
                        dc.DataType=Type.GetType("System.String");
                        break;
                    case 5: //Spare
                        dc.ColumnName = "装修地区"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6://SpareAdd
                        dc.ColumnName = "装修地址"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7: //Cust_Add
                        dc.ColumnName = "客户通讯地址"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 8: //Cust_Phone
                        dc.ColumnName = "客户联系方式"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 9: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 10://InputDt
                        dc.ColumnName = "录入日期"; 
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"供应商信息管理"表体临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_SupplierEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 9; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1:
                        dc.ColumnName = "Supid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 2: //SupName
                        dc.ColumnName = "供应商名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //Address
                        dc.ColumnName = "通讯地址"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4: //ContactName
                        dc.ColumnName = "联系人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5://ContactPhone
                        dc.ColumnName = "联系方式"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6: //GoNum
                        dc.ColumnName = "工商登记号"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 8://InputDt
                        dc.ColumnName = "录入日期"; 
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"材料信息管理"表体临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_MaterialEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 10; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1:
                        dc.ColumnName = "MaterialId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 2: //MaterialName
                        dc.ColumnName = "材料名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //MaterialSize
                        dc.ColumnName = "材料规格"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4: //Supid
                        dc.ColumnName = "材料供应商id"; 
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 5://SupName
                        dc.ColumnName = "材料供应商名称";
                        dc.DataType=Type.GetType("System.String");
                        break;
                    case 6://Unit
                        dc.ColumnName = "单位"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7: //Price
                        dc.ColumnName = "单价"; 
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 8: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 9://InputDt
                        dc.ColumnName = "录入日期"; 
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"房屋类型及装修工程类别信息管理"表体临时表 T_BD_HTypeEntry
        /// </summary>
        /// <returns></returns>
        public DataTable Get_HouseEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 5; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1:
                        dc.ColumnName = "HTypeid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 2: //HtypeName
                        dc.ColumnName = "类型信息名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4://InputDt
                        dc.ColumnName = "录入日期"; 
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"房屋类型及装修工程类别信息管理"-项目名称表体临时表 T_BD_HTypeProjectDtl
        /// </summary>
        /// <returns></returns>
        public DataTable Get_HouseProjectEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "HTypeid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1:
                        dc.ColumnName = "Projectid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 2: //PrjoectName
                        dc.ColumnName = "项目名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //Unit
                        dc.ColumnName = "单位";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4://Price
                        dc.ColumnName = "单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 5://InputUser
                        dc.ColumnName = "录入人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6://InputDt
                        dc.ColumnName = "录入日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 创建删除临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_TreeidTemp()
        {
            var dt=new DataTable();
            for (var i = 0; i < 1; i++)
            {
                var dc=new DataColumn();
                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"室内装修工程"表体临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_AdornEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 17; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    //表头主键ID
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //树菜单ID
                    //case 1:
                    //    dc.ColumnName = "TreeId";
                    //    dc.DataType = Type.GetType("System.Int32");
                    //    break;
                    //小类ID
                    case 1:
                        dc.ColumnName = "adornid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //HTypeid
                    case 2:
                        dc.ColumnName = "工程类别ID";
                        dc.DataType=Type.GetType("System.Int32");
                        break;
                    //TypeName
                    case 3:
                        dc.ColumnName = "大类名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //HtypeName
                    case 4:
                        dc.ColumnName = "装修工程类别";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //HTypeProjectName
                    case 5:
                        dc.ColumnName = "项目名称";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //Unit
                    case 6:
                        dc.ColumnName = "单位名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //quantities
                    case 7:
                        dc.ColumnName = "工程量";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                    //FinalPrice
                    case 8:
                        dc.ColumnName = "综合单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Ren_Cost
                    case 9:
                        dc.ColumnName = "人工费用";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Fu_Cost
                    case 10:
                        dc.ColumnName = "辅材费用";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Price
                    case 11:
                        dc.ColumnName = "单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Temp_Price
                    case 12:
                        dc.ColumnName = "临时材料单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Amount
                    case 13:
                        dc.ColumnName = "合计";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //FRemark
                    case 14:
                        dc.ColumnName = "备注";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 15:
                        dc.ColumnName = "录入人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 16:
                        dc.ColumnName = "录入日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"室内主材"表体临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ProMaterialEmtrydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 16; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    //表头主键ID
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //树菜单ID
                    //case 1:
                    //    dc.ColumnName = "TreeId";
                    //    dc.DataType = Type.GetType("System.Int32");
                    //    break;
                    //小类ID
                    case 1:
                        dc.ColumnName = "EntryID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //MaterialId
                    case 2:
                        dc.ColumnName = "材料ID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //MaterialType
                    case 3:
                        dc.ColumnName = "材料大类名称";
                        dc.DataType=Type.GetType("System.String");
                        break;
                    //MaterialName
                    case 4:
                        dc.ColumnName = "材料名称";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //Unit
                    case 5:
                        dc.ColumnName = "单位名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //quantities
                    case 6:
                        dc.ColumnName = "工程量";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                    //FinalPrice
                    case 7:
                        dc.ColumnName = "综合单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Ren_Cost
                    case 8:
                        dc.ColumnName = "人工费用";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Fu_Cost
                    case 9:
                        dc.ColumnName = "辅材费用";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Price
                    case 10:
                        dc.ColumnName = "单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Temp_Price
                    case 11:
                        dc.ColumnName = "临时材料单价";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //Amount
                    case 12:
                        dc.ColumnName = "合计";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    //FRemark
                    case 13:
                        dc.ColumnName = "备注";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 14:
                        dc.ColumnName = "录入人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 15:
                        dc.ColumnName = "录入日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"室内装修工程"表头临时表(生成单据时使用)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ProAdorndt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    //表头主键ID
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //单据编号
                    case 1:
                        dc.ColumnName = "OrderNo";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //客户ID
                    case 2:
                        dc.ColumnName = "Custid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //录入人
                    case 3:
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //录入日期
                    case 4:
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    //审核状态
                    case 5:
                        dc.ColumnName = "Fstatus";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //审核日期
                    case 6:
                        dc.ColumnName = "FstatusDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"室内主材单"表头临时表(生成单据时使用)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ProMaterialdt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    //表头主键ID
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //单据编号
                    case 1:
                        dc.ColumnName = "OrderNo";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //客户ID
                    case 2:
                        dc.ColumnName = "Custid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //录入人
                    case 3:
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //录入日期
                    case 4:
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    //审核状态
                    case 5:
                        dc.ColumnName = "Fstatus";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //审核日期
                    case 6:
                        dc.ColumnName = "FstatusDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"室内主材单"树菜单临时表(生成树菜单时使用)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ProMaterialTreedt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 4; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    //表头主键ID
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //树菜单ID
                    case 1:
                        dc.ColumnName = "Treeid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //(记录ALL父节点信息) 
                    case 2:
                        dc.ColumnName = "ParentId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //MaterialType
                    case 3:
                        dc.ColumnName = "材料大类名称";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取历史单据记录T_PRO_AdornEntry TypeInfoFrm.cs使用
        /// </summary>
        /// <returns></returns>
        public DataTable Get_HistoryAdornEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 8; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //HTypeid
                    case 0:
                        dc.ColumnName = "HTypeid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //adornid
                    case 1:
                        dc.ColumnName = "adornid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //HTypeProjectName
                    case 2:
                        dc.ColumnName = "项目名称";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //Unit
                    case 3:
                        dc.ColumnName = "单位";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Price
                    case 4:
                        dc.ColumnName = "单价";
                        dc.DataType = Type.GetType("System.Decimal"); 
                        break;
                    //InputUser
                    case 5:
                        dc.ColumnName = "单据录入人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 6:
                        dc.ColumnName = "单据录入日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    //OrderNo
                    case 7:
                        dc.ColumnName = "单据名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        #region 主窗体

        /// <summary>
        /// 主窗体-查询功能使用(AdminFrm.cs)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Maindtl()
        {
            var dt=new DataTable();
            for (var i = 0; i < 13; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //Id
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //ordertype
                    case 1:
                        dc.ColumnName = "ordertype";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //OrderNo
                    case 2:
                        dc.ColumnName = "单据编号";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //CustName
                    case 3:
                        dc.ColumnName = "客户名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //HTypeName
                    case 4:
                        dc.ColumnName = "房屋类型名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Spare
                    case 5:
                        dc.ColumnName = "装修地区";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //SpareAdd
                    case 6:
                        dc.ColumnName = "装修地址";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Cust_Add
                    case 7:
                        dc.ColumnName = "客户通讯地址";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Cust_Phone
                    case 8:
                        dc.ColumnName = "客户联系方式";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Fstatus
                    case 9:
                        dc.ColumnName = "审核状态";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //FstatusDt
                    case 10:
                        dc.ColumnName = "审核日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    //InputUser
                    case 11:
                        dc.ColumnName = "单据录入人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 12:
                        dc.ColumnName = "单据录入日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        #endregion

        #region 帐号权限

        /// <summary>
        /// 帐号权限主窗体查询使用(AccountAddFrm.cs)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Admindtl()
        {
            var dt = new DataTable();
            for (var i = 0; i < 12; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //UserId
                    case 0:
                        dc.ColumnName = "UserId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //UserPassword
                    case 1:
                        dc.ColumnName = "UserPassword";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserName
                    case 2:
                        dc.ColumnName = "职员名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserSex
                    case 3:
                        dc.ColumnName = "职员性别";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserContact
                    case 4:
                        dc.ColumnName = "联系方式";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserContact
                    case 5:
                        dc.ColumnName = "职员邮箱";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserInDt
                    case 6:
                        dc.ColumnName = "入职日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    //CloseStatus
                    case 7:
                        dc.ColumnName = "关闭状态";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 8:
                        dc.ColumnName = "创建人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 9:
                        dc.ColumnName = "创建日期";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    //Fstatus
                    case 10:
                        dc.ColumnName = "审核状态";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //FstatusDt
                    case 11:
                        dc.ColumnName = "审核日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 角色信息管理查询(RoleInfoFrm.cs)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Admin_roledtl()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //RoleId
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //RoleName
                    case 1:
                        dc.ColumnName = "角色名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 2:
                        dc.ColumnName = "创建人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 3:
                        dc.ColumnName = "创建日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    //CloseStatus
                    case 4:
                        dc.ColumnName = "关闭状态";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Fstatus
                    case 5:
                        dc.ColumnName = "审核状态";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //FstatusDt
                    case 6:
                        dc.ColumnName = "审核日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 功能权限明细查询(RoleInfoDtlFrm.cs)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Admin_roleFundtl()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //EntryID
                    case 0:
                        dc.ColumnName = "EntryID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //FunName
                    case 1:
                        dc.ColumnName = "功能名称";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //CanShow
                    case 2:
                        dc.ColumnName = "是否显示";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //CanBackConfirm
                    case 3:
                        dc.ColumnName = "是否反审核";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    //CanDel
                    case 4:
                        dc.ColumnName = "是否可删除";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 5:
                        dc.ColumnName = "权限创建人";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 6:
                        dc.ColumnName = "权限创建日期";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取T_AD_Role临时表(插入信息至T_AD_Role表内)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_T_AD_RoleTemp()
        {
            var dt = new DataTable();
            for (var i = 0; i < 8; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //Id
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //RoleName
                    case 1:
                        dc.ColumnName = "RoleName";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //InputUser
                    case 2:
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 3:
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    //CanALLMark
                    case 4:
                        dc.ColumnName = "CanALLMark";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //CloseStatus
                    case 5:
                        dc.ColumnName = "CloseStatus";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //Fstatus
                    case 6:
                        dc.ColumnName = "Fstatus";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //FstatusDt
                    case 7:
                        dc.ColumnName = "FstatusDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取T_AD_RoleDtl临时表(插入信息至T_AD_RoleDtl表内)
        /// </summary>
        /// <returns></returns>
        public DataTable Get_T_AD_RoleDtlTemp()
        {
            var dt = new DataTable();
            for (var i = 0; i < 9; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //Id
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //Funid
                    case 1:
                        dc.ColumnName = "Funid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //EntryID
                    case 2:
                        dc.ColumnName = "EntryID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //FunName
                    case 3:
                        dc.ColumnName = "FunName";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //CanShow
                    case 4:
                        dc.ColumnName = "CanShow";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //CanBackConfirm
                    case 5:
                        dc.ColumnName = "CanBackConfirm";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //CanDel
                    case 6:
                        dc.ColumnName = "CanDel";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 7:
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 8:
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取T_AD_User临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_T_AD_UserTemp()
        {
            var dt = new DataTable();
            for (var i = 0; i < 12; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //UserId
                    case 0:
                        dc.ColumnName = "UserId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //UserName
                    case 1:
                        dc.ColumnName = "UserName";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //UserPassword
                    case 2:
                        dc.ColumnName = "UserPassword";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserSex
                    case 3:
                        dc.ColumnName = "UserSex";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserContact
                    case 4:
                        dc.ColumnName = "UserContact";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserMail
                    case 5:
                        dc.ColumnName = "UserMail";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //UserInDt
                    case 6:
                        dc.ColumnName = "UserInDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                    //CloseStatus
                    case 7:
                        dc.ColumnName = "CloseStatus";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 8:
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 9:
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    //Fstatus
                    case 10:
                        dc.ColumnName = "Fstatus";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //FstatusDt
                    case 11:
                        dc.ColumnName = "FstatusDt";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取T_AD_UserDtl临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_T_AD_UserDtlTemp()
        {
            var dt = new DataTable();
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    //UserId
                    case 0:
                        dc.ColumnName = "UserId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //RoleId
                    case 1:
                        dc.ColumnName = "RoleId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //EntryID
                    case 2:
                        dc.ColumnName = "EntryID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //RoleName
                    case 3:
                        dc.ColumnName = "RoleName";
                        dc.DataType = Type.GetType("System.String"); 
                        break;
                    //AddId
                    case 4:
                        dc.ColumnName = "AddId";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputUser
                    case 5:
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    //InputDt
                    case 6:
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime"); 
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        #endregion

    }
}
