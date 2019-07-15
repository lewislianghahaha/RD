using System;
using System.Data;
using RD.DB.Export;

namespace RD.Logic.Export
{
    //将指定数据导出的
    public class ExportReportdt
    {
        ExportDt exportdt=new ExportDt();

        /// <summary>
        /// 根据指定条件获取对应单据的Dt
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable Get_exportdt(string functionname, int id)
        {
            var resultdt=new DataTable();
            try
            {
                resultdt = exportdt.Get_exportdt(functionname, id);
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
            }
            return resultdt;
        }
    }
}
