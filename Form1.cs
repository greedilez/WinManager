using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void disSpyware_Click(object sender, EventArgs e) => Program.DisableAntiSpyware();

        private void disUpdt_Click(object sender, EventArgs e) => Program.DisableUpdates();

        private void deskBldNum_Click(object sender, EventArgs e) => Program.DisableDesktopBuildNumberShowState();
    }
}
