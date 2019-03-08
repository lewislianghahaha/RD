using System.Data;
using RD.Logic.Basic;
using RD.Logic.ChangePwd;

namespace RD.Logic
{
    public class Task
    {
        ChangeData change=new ChangeData();
        Search search=new Search();

        private int _taskid;             //记录中转ID
        private string _accountName;     //记录帐号名称
        private string _accountPwd;      //记录帐号密码
        private int _functionId;         //记录"基础信息库"内的功能ID(创建:0 查询:1 保存:2 审核:3)
        private string _functinName;     //功能名称(确定是使用那个表系列)
        private string _functionType;    //记录表格类型ID(T:0 表体:1)

        private DataTable _resultTable;  //返回DT类型
        private bool _resultMark;        //返回是否成功标记

        /// <summary>
        /// 中转ID
        /// </summary>
        public int TaskId { set { _taskid = value; } }

        /// <summary>
        /// 接收帐号名称
        /// </summary>
        public string AccountName { set { _accountName = value; } }

        /// <summary>
        /// 接收帐号密码
        /// </summary>
        public string AccountPwd { set { _accountPwd = value; } }

        /// <summary>
        /// 记录"基础信息库"内的功能ID(创建:0 查询:1 保存:2 审核:3)
        /// </summary>
        public int FunctionId { set { _functionId = value; } }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { set { _accountName = value; } }

        /// <summary>
        /// 表格类型(T:表头 G:表体)
        /// </summary>
        public string FunctionType { set { _functionType = value; } }


        /// <summary>
        ///返回DataTable至主窗体
        /// </summary>
        public DataTable RestulTable => _resultTable;

        /// <summary>
        /// 返回结果标记
        /// </summary>
        public bool ResultMark => _resultMark;

        public void StartTask()
        {
            switch (_taskid)
            {
                //帐号密码修改
                case 0:
                    ChangeRecord(_accountName,_accountPwd);
                    break;
                //基础信息库
                case 1:
                    BasicInfo(_functionId,_functinName,_functionType);
                    break;
                //室内装修工程单
                case 2:

                    break;
                //主材单
                case 3:

                    break;
                //导出EXCEL
                case 4:

                    break;
                //打印
                case 5:

                    break;
                //帐户信息功能设定(帐号为:Admin时使用)
                case 6:

                    break;
                //查询功能(Main窗体使用)
                case 7:

                    break;
            }
        }

        /// <summary>
        /// 帐号密码修改
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userNewPwd"></param>
        private void ChangeRecord(string userName,string userNewPwd)
        {
            _resultMark = change.ChangeRecord(userName,userNewPwd);
        }

        /// <summary>
        /// 基础信息库
        /// </summary>
        /// <param name="functionId">功能ID(创建:0 查询:1 保存:2 审核:3)</param>
        /// <param name="functionName">功能名</param>
        /// <param name="functionType">表格类型(注:读取时使用) T:表头 G:表体</param>
        private void BasicInfo(int functionId,string functionName,string functionType)
        {
            switch (functionId)
            {
                //创建
                case 0:

                    break;
                //查询
                case 1:
                    _resultTable=search.GetData(functionName, functionType);
                    break;
                //保存
                case 2:

                    break;
                //审核
                case 3:

                    break;
            }
        }

    }
}
