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

//New user page code 

namespace codeLock
{
    

    public partial class newusrPG : Form
    {
        
        private SqlConnection connection;
        private string conncetionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Programming\C_Sharp\codeLock\codeLock\loginList.mdf;Integrated Security = True");

        public newusrPG()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Goes back to the splash page 
            this.Hide();
            splashPG form3 = new splashPG();
            form3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //This checks the database for the users information to verify whether it has been used before or not
            string username = usrtextBox.Text.Trim();
            string password = passtextBox.Text.Trim();
            string confPass = confPasstextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confPass))
            {
                MessageBox.Show("Invalid.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Proceed with further actions (e.g., open another form)
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Form4_Load(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            //This section clears the inputs from the user that was inputted into the form
            usrtextBox.Clear();
            passtextBox.Clear();
            confPasstextBox.Clear();
        }

        private void passtextBox_TextChanged(object sender, EventArgs e)
        {
            //Hash the Password information to make it non visable 
            passtextBox.PasswordChar = '*';

        }

        private void confPasstextBox_TextChanged(object sender, EventArgs e)
        {
            //Hash the Password information to make it non visable 
            confPasstextBox.PasswordChar = '*';
        }
    }
}
