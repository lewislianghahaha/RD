using System;
using RD.DB.Import;

namespace RD.Logic.Basic
{
    //作用:对"基础信息库"功能内的数据进行导入
    public class Import
    {
        ImportDt importDt=new ImportDt();

        /// <summary>
        /// 根据功能名称插入对应的
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="pid"></param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public bool InsertTreeRd(string functionName,int pid,string treeName)
        {
            var result=true;
            try
            {
                result=importDt.InsertTreeRecord(functionName, pid, treeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 更新树形节点
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="pid"></param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public bool UpdateTreeRd(string functionName, int pid, string treeName)
        {
            var result = true;
            try
            {
                result = importDt.UpdateTreeRecord(functionName, pid, treeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }



    }
}
