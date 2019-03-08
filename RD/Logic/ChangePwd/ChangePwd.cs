using RD.DB.Generate;

namespace RD.Logic.ChangePwd
{
    public class ChangeData
    {
        GenerateDt _generateDt=new GenerateDt();

        /// <summary>
        /// 修改帐户密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        public bool ChangeRecord(string username,string userpwd)
        {
            var result = _generateDt.ChangeUserPwd(username, userpwd);
            return result;
        }



    }
}
