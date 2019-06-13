using System.Configuration;

namespace RD.DB
{
    public class Conn
    {
        public string GetConnectionString()
        {
            //读取App.Config配置文件中的Connstring节点    
            var pubs = ConfigurationManager.ConnectionStrings["Connstring"];
            var strcon = pubs.ConnectionString;
            //var consplit = pubs.ConnectionString.Split(';');

            //var strcon = string.Format(consplit[0], "192.168.1.250") + ";" + string.Format(consplit[1], "RD") + ";" +
            //             consplit[2] + ";" + string.Format(consplit[3], "sa") + ";" +
            //             string.Format(consplit[4], "8990489he") + ";" + consplit[5] + ";" + consplit[6] + ";" + consplit[7];

            //var conn = new SqlConnection(strcon);
            //return conn;
            return strcon;
        }
    }
}
