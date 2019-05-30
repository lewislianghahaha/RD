using System;
using System.Windows.Forms;
using RD.DB.Generate;

namespace RD.Logic.Main
{
    public class MainDel
    {
        GenerateDt gerDt = new GenerateDt();

        /// <summary>
        /// 删除所选的单据记录
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="datarow"></param>
        /// <returns></returns>
        public bool Del_OrderRecord(string factionName, DataGridViewSelectedRowCollection datarow)
        {
            var result = true;
            try
            {
                result = gerDt.Del_OrderRecord(factionName,datarow);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
