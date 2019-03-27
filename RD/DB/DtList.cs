using System;
using System.Data;

namespace RD.DB
{
    //根据功能名创建空表
    public class DtList
    {
        /// <summary>
        /// 获取"客户信息管理"临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_CustEmptydt()
        {
            var dt=new DataTable();
            for (var i = 0; i < 9; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1: //CustName
                        dc.ColumnName = "客户名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2: //HTypeid
                        dc.ColumnName = "房屋类型ID"; 
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 3: //Spare
                        dc.ColumnName = "装修地区"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4://SpareAdd
                        dc.ColumnName = "装修地址"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5: //Cust_Add
                        dc.ColumnName = "客户通讯地址"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6: //Cust_Phone
                        dc.ColumnName = "客户联系方式"; 
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
        /// 获取"供应商信息管理"临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_SupplierEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 8; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1: //SupName
                        dc.ColumnName = "供应商名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2: //Address
                        dc.ColumnName = "通讯地址"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //ContactName
                        dc.ColumnName = "联系人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4://ContactPhone
                        dc.ColumnName = "联系方式"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5: //GoNum
                        dc.ColumnName = "工商登记号"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7://InputDt
                        dc.ColumnName = "录入日期"; 
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"材料信息管理"临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_MaterialEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 8; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1: //MaterialName
                        dc.ColumnName = "材料名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2: //MaterialSize
                        dc.ColumnName = "材料规格"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3: //Supid
                        dc.ColumnName = "材料供应商ID"; 
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 4://Unit
                        dc.ColumnName = "单位"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5: //Price
                        dc.ColumnName = "单价"; 
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 6: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7://InputDt
                        dc.ColumnName = "录入日期"; 
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }

        /// <summary>
        /// 获取"房屋类型及装修工程类别信息管理"临时表
        /// </summary>
        /// <returns></returns>
        public DataTable Get_HouseEmptydt()
        {
            var dt = new DataTable();
            for (var i = 0; i < 4; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case 1: //HtypeName
                        dc.ColumnName = "类型信息名称"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2: //InputUser
                        dc.ColumnName = "录入人"; 
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3://InputDt
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


    }
}
