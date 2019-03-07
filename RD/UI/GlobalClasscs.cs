namespace RD.UI
{
    public class GlobalClasscs
    {
        public struct Account
        {
            public string StrUsrName;
            public string StrUsrpwd;
        };

        public struct BasicFrm
        {
            public int BasicId;          //用于获取所打开的基础信息库的那个功能ID
        };

        public static Account User;
        public static BasicFrm Basic;
    }
}
