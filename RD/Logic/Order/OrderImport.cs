using System;
using System.Data;
using RD.DB.Import;

namespace RD.Logic.Order
{
    //作用:导入数据
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

        /// <summary>
        /// 根据相关条件插入信息至T_PRO_Material 或 T_PRO_Adorn表内,并返回新插入的主键ID值
        /// </summary>
        /// <returns></returns>
        public int InsertOrderFirstDt(string functionName, int custid,string accountName)
        {
            var reslutid = 0;
            try
            {
                reslutid = importDt.InsertOrderFirstDt(functionName, custid, accountName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return reslutid;
        }

        /// <summary>
        /// (作用:对表体GridView进行导入) (注:包括插入及更新 及删除操作)
        /// </summary>
        /// <param name="functionName">功能名称(AdornOrder:室内装修工程 MaterialOrder:室内主材单)</param>
        /// <param name="dt">获取GridView内的DataTable</param>
        /// <param name="deldt">要进行删除的记录</param>
        /// <returns></returns>
        public bool Save_OrderEntry(string functionName,DataTable dt,DataTable deldt)
        {
            var result = true;
            try
            {
                result = importDt.SaveOrderEntry(functionName, dt, deldt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 将“材料信息管理”的表头信息插入至T_Pro_MaterialTree表内
        /// </summary>
        /// <param name="functionName">功能名称</param>
        /// <param name="pid">表头ID</param>
        /// <returns></returns>
        public bool InsertMaterialIntoDt(string functionName,int pid)
        {
            var result = true;
            try
            {
                result = importDt.InsertMaterialIntoDt(functionName,pid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
