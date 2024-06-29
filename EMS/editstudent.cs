using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMS
{
    public partial class editstudent : Form
    {
        public string Id;
        public editstudent()
        {
            InitializeComponent();

        }
        public editstudent(string studentId, string fname, string lname, string section, string contact) 
        {
            InitializeComponent();
            this.Id = studentId;
            textBox1.Text = fname;
            textBox2.Text = lname;
            comboBox1.Text = section;
            textBox6.Text = contact;
        }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Retrieve updated information from text boxes
            string updatedFname = textBox1.Text;
            string updatedLname = textBox2.Text;
            string updatedSection = comboBox1.Text;
            string updatedContact = textBox6.Text;

            // Check for null or empty values
            if (string.IsNullOrEmpty(updatedFname) || string.IsNullOrEmpty(updatedLname) ||
                string.IsNullOrEmpty(updatedSection) || string.IsNullOrEmpty(updatedContact))
            {
                MessageBox.Show("All fields must be filled.");
                return;
            }

            // Call a method to update the student's information in the database
            UpdateStudentInformation(this.Id, updatedFname, updatedLname, updatedSection, updatedContact);

            // Optionally, close the form after updating
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateStudentInformation(string studentId, string fname, string lname, string section, string contact)
        {
            try
            {
                // Check if a student with the same first name and last name already exists
                bool studentExists = CheckIfStudentExists(fname, lname, studentId);

                if (studentExists)
                {
                    MessageBox.Show("A student with the same first name and last name already exists.");
                    return;
                }

                // Construct the SQL UPDATE query
                string updateQuery = "UPDATE students_tbl SET fname = @fname, lname = @lname, section = @section, contactnum = @contact WHERE studentid = @studentId";

                // Create a connection and command objects
                using (MySqlConnection connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;"))
                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@fname", fname);
                    command.Parameters.AddWithValue("@lname", lname);
                    command.Parameters.AddWithValue("@section", section);
                    command.Parameters.AddWithValue("@contact", contact);
                    command.Parameters.AddWithValue("@studentId", studentId);

                    // Open the connection and execute the command
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student information updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("Error updating student information: " + ex.Message);
            }
        }
        private bool CheckIfStudentExists(string fname, string lname, string studentId)
        {
            try
            {
                // Construct the SQL SELECT query to check if the student exists
                string selectQuery = "SELECT COUNT(*) FROM students_tbl WHERE fname = @fname AND lname = @lname AND studentid != @studentId";

                // Create a connection and command objects
                using (MySqlConnection connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;"))
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@fname", fname);
                    command.Parameters.AddWithValue("@lname", lname);
                    command.Parameters.AddWithValue("@studentId", studentId);

                    // Open the connection and execute the command
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("Error checking if student exists: " + ex.Message);
                return false;
            }
        }
    }
}
