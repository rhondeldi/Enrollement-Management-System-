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

namespace EMS
{
    public partial class allstudents : UserControl
    {
        public allstudents()
        {
            InitializeComponent();
            LoadData();
            LoadYears();
         
        }
        private void LoadData()
        {
            try
            {
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string expenseQuery = @"SELECT studentid AS 'ID', fname AS 'First Name', lname AS 'Last Name', year AS 'Year', section AS 'Section', contactnum AS 'Contact Number', course AS 'Course', studentnum AS 'Student Number', username AS 'Registrar' FROM students_tbl";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        DataTable dataTable = new DataTable();

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        // Clear existing columns before adding new ones
                        dataGridView1.Columns.Clear();

                        // Add edit and delete buttons
                        DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
                        editButtonColumn.HeaderText = "Edit";
                        editButtonColumn.Text = "Edit";
                        editButtonColumn.Name = "editButtonColumn";
                        editButtonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(editButtonColumn);

                        DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                        deleteButtonColumn.HeaderText = "Delete";
                        deleteButtonColumn.Text = "Delete";
                        deleteButtonColumn.Name = "deleteButtonColumn";
                        deleteButtonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(deleteButtonColumn);

                        dataGridView1.DataSource = dataTable;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
        private void LoadYears()
        {
            try
            {
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string yearQuery = "SELECT DISTINCT year FROM students_tbl";

                    using (var command = new MySqlCommand(yearQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["year"].ToString());
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Ensure the clicked cell is valid
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "editButtonColumn")
                {
                    // Handle edit button click
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Retrieve data from cells and store in variables
                    string studentID = selectedRow.Cells["ID"].Value.ToString();
                    string firstName = selectedRow.Cells["First Name"].Value.ToString();
                    string lastName = selectedRow.Cells["Last Name"].Value.ToString();
                    string year = selectedRow.Cells["Year"].Value.ToString();
                    string section = selectedRow.Cells["Section"].Value.ToString();
                    string contactNumber = selectedRow.Cells["Contact Number"].Value.ToString();
                    string course = selectedRow.Cells["Course"].Value.ToString();
                    string studentNumber = selectedRow.Cells["Student Number"].Value.ToString();
                    string registrar = selectedRow.Cells["Registrar"].Value.ToString();


                    editstudent edit = new editstudent(studentID, firstName, lastName, section, contactNumber);
                    edit.ShowDialog();

                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "deleteButtonColumn")
                {
                    // Handle delete button click
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    string studentID = selectedRow.Cells["ID"].Value.ToString();
                    string studentNumber = selectedRow.Cells["ID"].Value.ToString();

                    // Prompt the user for confirmation before deleting
                    DialogResult result = MessageBox.Show("Are you sure you want to delete student with ID: " + studentNumber + "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // If the user confirms deletion, proceed with delete operation
                    if (result == DialogResult.Yes)
                    {
                        // Call a method to delete the student's information from the database
                        DeleteStudentInformation(studentID);

                        // Refresh the DataGridView or remove the row from the DataGridView
                        dataGridView1.Rows.RemoveAt(e.RowIndex);

                        MessageBox.Show("Student with ID: " + studentNumber + " has been deleted.", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DeleteStudentInformation(string studentID)
        {
            try
            {
                // Construct the SQL DELETE query
                string deleteQuery = "DELETE FROM students_tbl WHERE studentid = @studentID";

                // Create a connection and command objects
                using (MySqlConnection connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;"))
                using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
                {
                    // Add parameter to the command
                    command.Parameters.AddWithValue("@studentID", studentID);

                    // Open the connection and execute the command
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student with ID: " + studentID + " has been deleted successfully.", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No rows were deleted.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("Error deleting student information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filter data based on selected year
            string selectedYear = comboBox1.SelectedItem.ToString();
            if (selectedYear != "<-None->")
            {
                FilterDataByYear(selectedYear);
            }
            else
            {
                // If no year selected, reload all data
                LoadData();
            }
        }

        private void FilterDataByYear(string year)
        {
            try
            {
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT studentid AS 'ID', fname AS 'First Name', lname AS 'Last Name', year AS 'Year', section AS 'Section', contactnum AS 'Contact Number', course AS 'Course', studentnum AS 'Student Number', username AS 'Registrar' FROM students_tbl WHERE year = '{year}'";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        DataTable dataTable = new DataTable();

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        dataGridView1.DataSource = dataTable;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
        }
