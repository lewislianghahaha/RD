using System;
using System.Data;
using RD.DB.Import;

namespace RD.Logic.Basic
{
    //作用:对"基础信息库"功能内的数据进行导入
    public class Import
    {
        ImportDt importDt=new ImportDt();

        /// <summary>
        /// 根据功能名称插入对应的表(树形节点插入使用)
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

        /// <summary>
        /// 对基础信息库内的表体进行保存(注:包括插入及更新操作)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="dt"></param>
        /// <param name="pid">上级主键ID</param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public bool Save_BaseEntry(string functionName, DataTable dt,int pid,string accountName)
        {
            var result = true;
            try
            {
                result = importDt.SavebaseEntryrd(functionName, dt,pid, accountName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
