namespace RD.UI
{
    public class GlobalClasscs
    {
        public struct Account
        {
            public string StrUsrName;   //获取帐号名称
            public string StrUsrpwd;    //获取帐号密码
            public int Userid;          //获取T_AD_User.UserId
            public int ChoseTypeid;     //获取‘进入窗体模式’ID （0:主窗体 1:帐户信息管理窗体)
        };

        public struct BasicFrm
        {
            //用于获取所打开的基础信息库的那个功能ID
            public int BasicId;          
        };

        public static Account User;
        public static BasicFrm Basic;
    }
}
