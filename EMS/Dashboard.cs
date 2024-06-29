using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EMS
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();


            try
            {
                using (var connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;"))
                {
                    connection.Open();

                    string loginQuery = $@"SELECT username
                    FROM user_tbl
                    WHERE id = '{Session.UserId}'";

                    using (var command = new MySqlCommand(loginQuery, connection))
                    {
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string fullname = reader.GetString("username");
                            linkLabel1.Text = fullname;
                        }

                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }

            Home home = new Home();

            panel1.Controls.Add(home);
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

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click_2(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Display a message box asking for confirmation
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user clicked 'Yes', close the form
            if (result == DialogResult.Yes)
            // Clear session data
            {
                Session.IsLoggedIn = false;
                Session.UserId = 0;
                Session.UserName = null;

                // Close the current form (logout)
                this.Close();

                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Home home = new Home();
            home.Dock = DockStyle.Fill;
            panel1.Controls.Add(home);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            panel1.Controls.Clear();
            addstudent add = new addstudent();
            add.Dock = DockStyle.Fill;
            panel1.Controls.Add((addstudent) add);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            allstudents add = new allstudents();
            add.Dock = DockStyle.Fill;
            panel1.Controls.Add((allstudents)add);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            allusers add = new allusers();
            add.Dock = DockStyle.Fill;
            panel1.Controls.Add((allusers)add);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            userprofile add = new userprofile();
            add.Dock = DockStyle.Fill;
            panel1.Controls.Add((userprofile)add);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Display a message box asking for confirmation
            DialogResult result = MessageBox.Show("Are you sure you want to close this window?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the user clicked 'Yes', close the form
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Controls.Clear();
            userprofile add = new userprofile();
            add.Dock = DockStyle.Fill;
            panel1.Controls.Add((userprofile)add);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
