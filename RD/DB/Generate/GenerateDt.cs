using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.DB.Generate
{
    //注:此类包括了对功能的运算以及帐号密码修改的功能
    public class GenerateDt
    {
        #region 更新帐号密码

        private string _UpdateRecord = @"
                                            update T_AD_User set UserPassword='{1}' where UserName='{0}'
                                        ";

        #endregion

        Conn conn = new Conn();

        public bool ChangeUserPwd(string username,string userpwd)
        {
            var result = true;
            try
            {
                using (var sql = new SqlConnection(conn.GetConnectionString()))
                {
                    var sqlCommand = new SqlCommand(string.Format(_UpdateRecord, username, userpwd), sql);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                //throw new Exception(ex.Message);
                result = false;
            }
            return result;
        }



    }      
}
