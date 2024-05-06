using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Splash screen code 

namespace codeLock
{
    public partial class splashPG : Form
    {
        public splashPG()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //This function exits the entire program
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginPG login = new loginPG();
            login.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
