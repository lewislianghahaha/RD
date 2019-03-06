using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RD
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            show();
        }

        private void show()
        {
            textBox1.Text = GlobalClasscs.User.StrUsrName;
        }

    }
}
