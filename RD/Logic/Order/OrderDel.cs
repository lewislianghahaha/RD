using System;
using System.Data;
using RD.DB.Generate;

namespace RD.Logic.Order
{
    //作用:删除数据
    public class OrderDel
    {
        GenerateDt gerDt = new GenerateDt();

        /// <summary>
        /// 根据节点ID删除对应的记录(包括GridView表体记录)
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="pid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DelBD_Record(string factionName, int pid, DataTable dt)
        {
            var result = true;
            try
            {
                result = gerDt.DelBD_Rd(factionName, pid, dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
