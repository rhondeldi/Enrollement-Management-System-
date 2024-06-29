using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            LoadCourses();

            panel3.Hide();
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

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login login = new Login();
            login.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login newlogin = new Login();
            newlogin.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch image path and other student info based on student number and course
                    string userQuery = @"SELECT image_path, fname, lname, year, section, contactnum, course, studentnum
                                     FROM students_tbl 
                                     WHERE studentnum = @studentNum AND course = @course";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@studentNum", textBox1.Text);
                        command.Parameters.AddWithValue("@course", comboBox1.SelectedItem?.ToString());

                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Check if the column is not null
                            if (!reader.IsDBNull(0))
                            {
                                panel3.Show();
                                // Read the BLOB data into a byte array
                                byte[] profilePicRaw = (byte[])reader["image_path"];

                                // Convert byte array to Image
                                MemoryStream ms = new MemoryStream(profilePicRaw);
                                Image image = Image.FromStream(ms);

                                // Set the SizeMode property to Zoom
                                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                                // Assign the image to the PictureBox
                                pictureBox2.Image = image;

                                // Display other student info in labels
                                textBox2.Text = reader["fname"].ToString() + " " + reader["lname"].ToString();
                                textBox6.Text =reader["year"].ToString();
                                textBox5.Text = reader["section"].ToString();
                                textBox7.Text = reader["contactnum"].ToString();
                                textBox4.Text = reader["course"].ToString();
                                textBox3.Text = reader["studentnum"].ToString();
                            }
                        }
                        else
                        {
                            // No student found with the given student number and course
                            MessageBox.Show("No student found with the provided information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCourses()
        {
            try
            {
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Fetch distinct courses from the database
                    string courseQuery = "SELECT DISTINCT course FROM students_tbl";

                    using (var command = new MySqlCommand(courseQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add courses to the ComboBox
                                comboBox1.Items.Add(reader["course"].ToString());
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
