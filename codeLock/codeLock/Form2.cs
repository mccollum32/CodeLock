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
using System.IO;

//Login page code 

namespace codeLock
{
    public partial class loginPG : Form
    {

        private SqlConnection connection;
        private string conncetionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Programming\C_Sharp\codeLock\codeLock\loginList.mdf;Integrated Security = True");


        public loginPG()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            usernameText.Clear();
            passText.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Loads the database into the project and gets the login/ pass info
            connection = new SqlConnection(conncetionString);
            try
            {
                // Open the connection
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            //This checks the database for the users information to verify whether it has been used before or not
            string username = usernameText.Text.Trim();
            string password = passText.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

            using (SqlConnection connection = new SqlConnection(conncetionString))
            {
                string query = "SELECT COUNT(*) FROM Logs WHERE username = @username AND password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Login successful.", "Welcome Back", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Proceed with further actions (e.g., open another form)
                        mainPG openMain = new mainPG();
                        this.Hide();
                        openMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Takes the user back to the 
            splashPG home = new splashPG();
            this.Hide();
            home.ShowDialog();
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                passText.UseSystemPasswordChar = false;
            }
            else
            {
                passText.UseSystemPasswordChar = true;
            }
        }


    }
}
