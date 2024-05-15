using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

//Main app code page


namespace codeLock
{
    public partial class mainPG : Form
    {
        //This sets the background color of the App to Wood color
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int LPAR);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;


        public mainPG()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(move_window);

        }


        private void move_window(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void randomizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int length = 10; // specify the desired length of the random string
            string randomString = GenerateRandomString(length);
            MessageBox.Show(randomString, "Randomly Generated Password");
            listBox1.Items.Add($"Password: {randomString}");

        }
        private string GenerateRandomString(int length)
        {
            const string chars = "~!@#$%^&*()_+`1234567890-=qwertyuiop[]asdfghjkl;'zxcvbnm,./QWERTYUIOPASDFGHJKLZXCVBNM";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void LoadWeb(string url)
        {
            SuppressScriptErrors(webBrowser1, true); // Suppress script errors
            webBrowser1.Navigate(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(url))
            {
                LoadWeb(url);
                listBox2.Items.Add(url);
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid URL.") ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.google.com");
        }

        private void SuppressScriptErrors(WebBrowser browser, bool hide)
        {
            FieldInfo field = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
            {
                object axIWebBrowser2 = field.GetValue(browser);
                if (axIWebBrowser2 != null)
                {
                    axIWebBrowser2.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, axIWebBrowser2, new object[] { hide });
                }
            }
        }


        private void AdjustWebPageSize()
        {
            if (webBrowser1.Document != null)
            {
                webBrowser1.Document.Body.Style = "zoom: " + (webBrowser1.Width / (double)webBrowser1.Document.Body.ScrollRectangle.Width);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            AdjustWebPageSize();
        }
    }
}
