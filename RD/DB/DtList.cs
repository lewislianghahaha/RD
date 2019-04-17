using System;
using System.Data;

namespace RD.DB
{
    //根据功能名创建空表
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
        /// 获取"房屋类型及装修工程类别信息管理"表体临时表
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
        /// 获取"房屋类型及装修工程类别信息管理"-项目名称表体临时表
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
                        dc.ColumnName = "Prjoectid";
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
                    case 1:
                        dc.ColumnName = "TreeId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //小类ID
                    case 2:
                        dc.ColumnName = "adornid";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //HTypeid
                    case 3:
                        dc.ColumnName = "工程类别ID";
                        dc.DataType=Type.GetType("System.Int32");
                        break;
                    //HTypeProjectName
                    case 4:
                        dc.ColumnName = "项目名称";
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
                    case 1:
                        dc.ColumnName = "TreeId";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //小类ID
                    case 2:
                        dc.ColumnName = "EntryID";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    //MaterialId
                    case 3:
                        dc.ColumnName = "材料ID";
                        dc.DataType = Type.GetType("System.Int32");
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

    }
}
