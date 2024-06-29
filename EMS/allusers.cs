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
    public partial class allusers : UserControl
    {
        public allusers()
        {
            InitializeComponent();
            try
            {
                string connectionString = "Server=localhost;Database=ems;Uid=admin;Pwd=admin;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string expenseQuery = @"SELECT id AS 'ID', full_name AS 'Full Name', username AS 'Username', email AS 'Email' FROM user_tbl";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        DataTable dataTable = new DataTable();

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);

                            // Check if data was retrieved
                            if (dataTable.Rows.Count > 0)
                            {
                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
