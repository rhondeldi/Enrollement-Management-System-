using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EMS
{
    public partial class addstudent : UserControl
    {
        public addstudent()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                // Image filters
                open.Filter = "Image Files (.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.ico)|*.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.ico";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    button2.Controls.Clear();
                    pictureBox1.Image = new Bitmap(open.FileName);

                    // Update imagePath based on the OpenFileDialog result
                    string imagePath = open.FileName;

                    // Use imagePath in your application logic, e.g., save to database, pass to file manager, etc.
                    // Set the Tag property of the button to store the image path
                    button2.Tag = imagePath;
                }
            }
            }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string fname = textBox1.Text;
            string lname = textBox2.Text;
            string year = comboBox2.Text;
            string course = comboBox1.Text;
            string section = comboBox3.Text;
            string studentnum = textBox5.Text;
            string contactnum = textBox6.Text;

            // Validate student number format
            if (!IsValidStudentNumber(studentnum))
            {
                MessageBox.Show("Student number must consist of exactly 9 digits.");
                textBox5.Clear(); // Clear the textbox
                textBox5.Focus(); // Set focus back to the textbox
                return; // Exit the method
            }

            // Validate contact number format
            if (!IsValidContactNumber(contactnum))
            {
                MessageBox.Show("Contact number must consist of exactly 11 digits starting with '09'.");
                textBox6.Clear(); // Clear the textbox
                textBox6.Focus(); // Set focus back to the textbox
                return; // Exit the method
            }

            // Add validation for first name and last name
            if (string.IsNullOrWhiteSpace(fname) || string.IsNullOrWhiteSpace(lname) || fname.All(char.IsDigit) || lname.All(char.IsDigit))
            {
                MessageBox.Show("First name and last name cannot be empty or consist only of numbers.");
                return;
            }

            try
            {
                byte[] imageData = null; // Initialize imageData

                // Check if an image was uploaded
                if (button2.Tag != null && File.Exists(button2.Tag.ToString()))
                {
                    // Read the image file and store it as bytes
                    using (FileStream fs = new FileStream(button2.Tag.ToString(), FileMode.Open, FileAccess.Read))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, (int)fs.Length);
                    }
                }
                else
                {
                    // Handle the case where no image was uploaded
                    MessageBox.Show("Please upload an image.");
                    return;
                }

                // Update your connection string to match your database configuration
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        // Check if the student already exists
        string checkQuery = "SELECT COUNT(*) FROM students_tbl WHERE (fname = @fname AND lname = @lname) OR studentnum = @studentnum";
        using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
        {
            checkCommand.Parameters.AddWithValue("@fname", fname);
            checkCommand.Parameters.AddWithValue("@lname", lname);
            checkCommand.Parameters.AddWithValue("@studentnum", studentnum);

            int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
            if (existingCount > 0)
            {
                MessageBox.Show("A student with the same first name, last name, and student number already exists.");
                // You can optionally clear the input fields or take any other action here
                return; // Exit the method
            }
        }

        string userQuery = @"INSERT INTO `students_tbl` (`fname`, `lname`, `year`, `section`, `contactnum`, `course`, `studentnum`, `username`, `image_path`) 
                             VALUES (@fname, @lname, @year, @section, @contactnum, @course, @studentnum, @username, @imagePath);
                             SELECT LAST_INSERT_ID();";

        using (MySqlCommand command = new MySqlCommand(userQuery, connection))
        {
            // Use parameterized queries to prevent SQL injection
            command.Parameters.AddWithValue("@fname", fname);
            command.Parameters.AddWithValue("@lname", lname);
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@section", section);
            command.Parameters.AddWithValue("@contactnum", contactnum);
            command.Parameters.AddWithValue("@course", course);
            command.Parameters.AddWithValue("@studentnum", studentnum);
            command.Parameters.AddWithValue("@username", Session.UserName);
            // Add parameter for image path
            command.Parameters.AddWithValue("@imagePath",imageData);

            // Retrieve last inserted ID
            object lastId = command.ExecuteScalar();
            if (lastId != null)
            {
                int insertedId = Convert.ToInt32(lastId);
                // You can use the insertedId if needed
            }
        }
    }
             MessageBox.Show("Registration Complete!");
                // Clear all text boxes and combo boxes
                ClearControls(this.Controls);
                pictureBox1.Image = null;
                // Set focus on the first text box
                textBox1.Focus();
            }
            catch (Exception ex)
                        {
                            // Handle exception
                             MessageBox.Show("Error: " + ex.Message);
                         }


        
        }
        // Method to clear all text boxes and combo boxes
        private void ClearControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }
                else if (control is PictureBox)
                {
                    ((PictureBox)control).Image = null;
                }
                else if (control.HasChildren)
                {
                    ClearControls(control.Controls);
                }
            }
        }
        // Method to validate student number format
        private bool IsValidStudentNumber(string studentNumber)
        {
            return !string.IsNullOrEmpty(studentNumber) && Regex.IsMatch(studentNumber, @"^\d{9}$");
        }

        // Method to validate contact number format
        private bool IsValidContactNumber(string contactNumber)
        {
            return !string.IsNullOrEmpty(contactNumber) && Regex.IsMatch(contactNumber, @"^09\d{9}$");
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
