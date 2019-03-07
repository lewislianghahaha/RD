using System.Data;
using RD.Logic.Change;

namespace RD.Logic
{
    public class Task
    {
        ChangeData change=new ChangeData();


        private int _taskid;             //记录中转ID
        private string _accountName;     //记录帐号名称
        private string _accountPwd;      //记录帐号密码



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
                //客户信息管理
                case 1:

                    break;
                //供应商信息管理
                case 2:

                    break;
                //材料信息管理
                case 3:

                    break;
                //房屋类型及装修工程类别信息管理
                case 4:

                    break;
                //室内装修工程单
                case 5:

                    break;
                //主材单
                case 6:

                    break;
                //导出EXCEL
                case 7:

                    break;
                //打印
                case 8:

                    break;
                //帐户信息功能设定(帐号为:Admin时使用)
                case 9:

                    break;
                //查询功能
                case 10:

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
            _resultMark =change.ChangeRecord(userName,userNewPwd);
        }



    }
}
