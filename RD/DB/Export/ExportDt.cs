using System.Data;
using System.Data.SqlClient;
using RD.DB.Search;

namespace RD.DB.Export
{
    //导出报表使用
    public class ExportDt
    {
        SqlList sqlList=new SqlList();
        SearchDt serDt=new SearchDt();

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="functionname"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable Get_exportdt(string functionname, int id)
        {
            var sqlscript = string.Empty;
            var resultdt=new DataTable();

            switch (functionname)
            {
                case "AdornOrder":
                    sqlscript = sqlList.Get_AdornOrderdtl(id);
                    break;
                case "MaterialOrder":
                    sqlscript = sqlList.Get_MaterialOrderdtl(id);
                    break;
            }

            var sqlDataAdapter = new SqlDataAdapter(sqlscript, serDt.GetConn());
            sqlDataAdapter.Fill(resultdt);
            return resultdt;
        }
    }
}
