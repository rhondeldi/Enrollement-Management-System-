using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.RegularExpressions;

namespace EMS
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            this.Hide();
            login.Show();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            string fullname = textBox1.Text;
            string email = textBox2.Text;
            string username = textBox3.Text;
            string password = textBox4.Text;
            string confirm = textBox5.Text;

            // Capitalize the first letter of the full name
            fullname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fullname.ToLower());

            if (String.IsNullOrEmpty(fullname) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please Complete Your Info!");

                return;
            }
            if (!IsValidEmailFormat(email))
            {
                MessageBox.Show("Please enter a valid email address!");
                textBox2.Focus();
                return;
            }
                if (password.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters long!");
                return;
            }

            int digitCount = password.Count(char.IsDigit);
            if (digitCount < 2)
            {
                MessageBox.Show("Password must contain at least 2 numbers!");
                return;
            }
                if (password != confirm)
            {
                MessageBox.Show("Incorrect Password");

                return;

            }
            try
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;");

                connection.Open();

                // Check if the username or email already exists
                string checkQuery = $"SELECT COUNT(*) FROM user_tbl WHERE username = '{username}' OR email = '{email}'";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (userCount > 0)

                {
                    MessageBox.Show("User with this username or email already exists. Please choose a different one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                    textBox3.Clear();
                    textBox3.Focus();
                    return;
                }
                string userQuery = $@"INSERT INTO `user_tbl` (`full_name`, `username`, `password`,`email`) 
                                          VALUES ('{fullname}','{username}','{password}','{email}'); 
                                          SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(userQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Register Complete!");

                // Clear all textboxes
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

                // Navigate to the login form
                this.Hide();
                Login loginForm = new Login();
                loginForm.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
        private bool IsValidEmailFormat(string email)
        {
            // Regular expression pattern for validating email address format
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            Login login = new Login();
            login.Show();   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();

            Form1 form = new Form1();   
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
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
    }
}
