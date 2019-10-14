using System;
using System.Windows.Forms;
using RD.UI;
using RD.UI.Admin;
using RD.UI.Order;

namespace RD
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MaterialOrderFrm1());

            //var mutex = new System.Threading.Mutex(false, "ThisOnlyRunOnce");

            ////主要用这个来监控是否第一次打开登录窗体,若不是的话,就提示"程序已启动"
            //var running = !mutex.WaitOne(0, false);

            //if (!running)
            //{
            //    var login = new Login();
            //    if (DialogResult.Cancel == login.ShowDialog())
            //    {
            //        return;
            //    }
            //    //判断若在LOGIN窗体内选择了'帐户信息管理窗体'窗体的话;就进入"帐户信息功能设定"窗体，反之进入主窗体
            //    if (GlobalClasscs.User.ChoseTypeid==1)   //GlobalClasscs.User.StrUsrName == "Admin" || 
            //    {
            //        var adminFrm = new AdminFrm();
            //        adminFrm.ShowDialog();
            //    }
            //    else
            //    {
            //        var main = new Main();
            //        main.ShowDialog();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("程序已启动.");
            //}
        }
    }
}
