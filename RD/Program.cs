using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //Application.Run(new Login());

            var mutex = new System.Threading.Mutex(false, "ThisOnlyRunOnce");

            //主要用这个来监控是否第一次打开登录窗体,若不是的话,就提示"程序已启动"
            var running = !mutex.WaitOne(0, false);

            if (!running)
            {
                var login = new Login();
                if (DialogResult.Cancel == login.ShowDialog())
                {
                    return;
                }
                var main = new Main();
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show("程序已启动.");
            }
        }
    }
}
