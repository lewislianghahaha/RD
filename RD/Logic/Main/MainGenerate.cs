using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;
using RD.DB.Generate;

namespace RD.Logic.Main
{
    //主窗体审核功能
    public class MainGenerate
    {
        GenerateDt generateDt = new GenerateDt();

        /// <summary>
        /// 单据审核（反审核）操作
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="confirmid"></param>
        /// <param name="datarow"></param>
        /// <returns></returns>
        public bool Main_ConfirmOrderDtl(string functionName ,int confirmid , DataGridViewSelectedRowCollection datarow )
        {
            var result = true;
            result = generateDt.Main_ConfirmOrderDtl(functionName, confirmid, datarow);
            return result;
        }

    }
}
