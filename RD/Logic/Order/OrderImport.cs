using System;
using RD.DB.Import;

namespace RD.Logic.Order
{
    //室内装修工程单 以及 室内主材单导入数据时使用
    public class OrderImport
    {
        ImportDt importDt=new ImportDt();

        /// <summary>
        ///  根据功能名称插入对应的表(树形节点插入使用)
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="id">上上级节点ID(室内装修工程单 及 室内主材单使用)</param>
        /// <param name="pid">上级节点ID</param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public bool InsertTreeRd(string functionName, int id,int pid, string treeName)
        {
            var result = true;
            try
            {
                result = importDt.InsertTreeRecord(functionName,id,pid, treeName);
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
        public bool UpdateTreeRd(string functionName,int pid, string treeName)
        {
            var result = true;
            try
            {
                result = importDt.UpdateTreeRecord(functionName,pid, treeName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
