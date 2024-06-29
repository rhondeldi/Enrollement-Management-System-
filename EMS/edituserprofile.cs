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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EMS
{
    public partial class edituserprofile : UserControl
    {
        public edituserprofile()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string oldPass = textBox2.Text;
            string newPass = textBox3.Text;
            string confirmPass = textBox4.Text;

            // Check if at least one of the fields is not empty
            bool anyFieldNotEmpty = !string.IsNullOrWhiteSpace(oldPass) || !string.IsNullOrWhiteSpace(newPass) || !string.IsNullOrWhiteSpace(confirmPass);

            // Check if the new password is the same as the old password
            bool sameAsOldPassword = newPass == oldPass;

            // Check if the password meets the requirement of more than 4 letters and more than 3 numbers
            int letterCount = newPass.Count(char.IsLetter);
            int digitCount = newPass.Count(char.IsDigit);

            // Check if the user wants to update the password and if the password meets the requirements
            bool updatePassword = anyFieldNotEmpty && !sameAsOldPassword && !string.IsNullOrWhiteSpace(newPass) && newPass == confirmPass && letterCount > 4 && digitCount > 3;

            // Ask the user if they want to change their password
            if (updatePassword && MessageBox.Show("Are you sure you want to change your password?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Make sure to use parameterized queries to prevent SQL injection
                        string userQuery = @"UPDATE user_tbl 
                                SET password = @newPass
                                WHERE id = @userId;";

                        using (var command = new MySqlCommand(userQuery, connection))
                        {
                            // Add parameters to the query
                            command.Parameters.AddWithValue("@newPass", newPass);
                            command.Parameters.AddWithValue("@userId", Session.UserId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Password successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Clear all text boxes
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Update failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (sameAsOldPassword)
            {
                MessageBox.Show("New password cannot be the same as the old password. Please fill in at least one field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!updatePassword)
            {
                MessageBox.Show("Please provide a valid password to update. The password must have more than 4 letters and more than 3 numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!anyFieldNotEmpty)
            {
                MessageBox.Show("Please fill in at least one field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void edituserprofile_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
