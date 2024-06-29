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
using System.Web;
using System.Windows.Forms;

namespace EMS
{
    public partial class userprofile : UserControl
    {
        public userprofile()
        {
            InitializeComponent();

            try
            {
                using (var connection = new MySqlConnection("server=localhost;database=ems;uid=admin;pwd=admin;"))
                {
                    connection.Open();

                    string loginQuery = $@"SELECT id, full_name, username, email, user_registration
                               FROM user_tbl
                               WHERE id = {Session.UserId}";

                    using (var command = new MySqlCommand(loginQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            { 
                                int id = reader.GetInt32("id");
                                string username = reader.GetString("username");
                                string fullname = reader.GetString("full_name");
                                string email = reader.GetString("email");
                                string user_registration = reader["user_registration"].ToString();

                                // Assuming name, userid, label10, and label11 are the correct names of your label controls
                                name.Text = fullname;
                                userid.Text = id.ToString();
                                label10.Text = email;
                                label11.Text = username;
                                regdate.Text = user_registration;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            edituserprofile add = new edituserprofile();
            panel3.Controls.Add((edituserprofile)add);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void userprofile_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
