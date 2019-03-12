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
            for (var i = 0; i < 8; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0: //客户名称
                        dc.ColumnName = "CustName";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1: //房屋类型ID
                        dc.ColumnName = "HTypeid";
                        dc.DataType = Type.GetType("System.int"); 
                        break;
                    case 2: //装修地区
                        dc.ColumnName = "Spare";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3://装修地址
                        dc.ColumnName = "SpareAdd";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4: //客户通讯地址
                        dc.ColumnName = "Cust_Add";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5: //客户联系方式
                        dc.ColumnName = "Cust_Phone";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6: //录入人
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 7://录入日期
                        dc.ColumnName = "InputDt";
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
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0: //供应商名称
                        dc.ColumnName = "SupName";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1: //通讯地址
                        dc.ColumnName = "Address";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2: //联系人
                        dc.ColumnName = "ContactName";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 3://联系方式
                        dc.ColumnName = "ContactPhone";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4: //工商登记号
                        dc.ColumnName = "GoNum";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 5: //录入人
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6://录入日期
                        dc.ColumnName = "InputDt";
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
            for (var i = 0; i < 7; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0: //材料名称
                        dc.ColumnName = "MaterialName";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1: //材料规格
                        dc.ColumnName = "MaterialSize";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2: //材料供应商ID
                        dc.ColumnName = "Supid";
                        dc.DataType = Type.GetType("System.int");
                        break;
                    case 3://单位
                        dc.ColumnName = "Unit";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 4: //单价
                        dc.ColumnName = "Price";
                        dc.DataType = Type.GetType("System.Decimal");
                        break;
                    case 5: //录入人
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 6://录入日期
                        dc.ColumnName = "InputDt";
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
            for (var i = 0; i < 3; i++)
            {
                var dc = new DataColumn();

                switch (i)
                {
                    case 0: //类型信息名称
                        dc.ColumnName = "HtypeName";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 1: //录入人
                        dc.ColumnName = "InputUser";
                        dc.DataType = Type.GetType("System.String");
                        break;
                    case 2://录入日期
                        dc.ColumnName = "InputDt";
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }
    }
}
