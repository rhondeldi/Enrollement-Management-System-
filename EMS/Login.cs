using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EMS
{
    public partial class Login : Form
    {
        public Login()
        {

            InitializeComponent();
        }


        private void label3_Click_1(object sender, EventArgs e)
        {

        }


        private void Login_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Check if username or password is empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Incomplete Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Clear the text boxes
                textBox1.Focus();
                textBox1.Clear();
                textBox2.Clear();
                return;
            }

            try
            {
                using (var connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;"))
                {
                    connection.Open();

                    string loginQuery = $@"SELECT id, username,full_name
                    FROM user_tbl
                    WHERE username = '{username}'
                    AND password = '{password}'";

                    using (var command = new MySqlCommand(loginQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            // Check if any rows are returned
                            if (reader.Read())
                            {
                                // Read values from the reader
                                int userId = reader.GetInt32("id");
                                string fetchedUsername = reader.GetString("username");
                                string fullname = reader.GetString("full_name");

                                // if valid user (successful login)
                                if (userId != 0)
                                {
                                    // Set current session values
                                    Session.IsLoggedIn = true;
                                    Session.UserId = userId;
                                    Session.UserName = fullname;
                                    this.Hide();

                                    Dashboard dashboard = new Dashboard();
                                    dashboard.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // Clear the password textbox
                                textBox1.Focus();
                                textBox2.Clear();
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }


        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            RegisterForm regform = new RegisterForm();
            regform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Display a message box asking for confirmation
            DialogResult result = MessageBox.Show("Are you sure you want to close this window?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user clicked 'Yes', close the form
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form = new Form1();
            form.Show();
        }
    }
}
