using RD.DB.Generate;

namespace RD.Logic.Order
{
    //作用:审核 权限控制
    public class OrderGenerate
    {
        GenerateDt generateDt=new GenerateDt();

        /// <summary>
        /// 单据审核（反审核）操作
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="confirmid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool ConfirmOrderDtl(string functionName, int confirmid,int pid)
        {
            var result = true;
            result = generateDt.ConfirmOrderDtl(functionName, confirmid, pid);
            return result;
        }
    }
}
