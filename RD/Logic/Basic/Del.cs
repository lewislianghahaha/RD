using System;
using System.Data;
using RD.DB.Generate;

namespace RD.Logic.Basic
{
    /// <summary>
    /// 删除指定的记录
    /// </summary>
    public class Del
    {
        GenerateDt gerDt=new GenerateDt();

        /// <summary>
        /// 根据所选节点删除相关表头及表体记录(基础信息库使用)
        /// </summary>
        /// <param name="factionName"></param>
        /// <param name="pid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DelBD_Record(string factionName,int pid,DataTable dt)
        {
            var result = true;
            try
            {
                result = gerDt.DelBD_Rd(factionName, pid,dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
