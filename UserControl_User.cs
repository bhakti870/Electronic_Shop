using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Eshop
{
    public partial class UserControl_User : UserControl
    {
        private int UserId = -1;


        public UserControl_User()
        {
            InitializeComponent();
            LoadData();

        }
        private void LoadData()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "SELECT Id, Name, email,password,phonenumber FROM [User]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable; // Bind data to DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error Occurred: " + ex.Message);
                    }
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string name = name1.Text; // Replace with your actual TextBox names
            string email = email1.Text; // Replace with your actual TextBox names
            string password = password1.Text; // Replace with your actual TextBox names
            string phoneNumber = phoneNumber1.Text; // Replace with your actual TextBox names

           
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "INSERT INTO [User] (Name, Email, Password, PhoneNumber) VALUES (@Name, @Email, @Password, @PhoneNumber)"; // Use brackets to escape the table name

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameter values
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password); // Consider hashing the password
                    command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber) ? (object)DBNull.Value : phoneNumber); // Optional

                    try
                    {
                        connection.Open(); // Open the database connection
                        int result = command.ExecuteNonQuery(); // Execute the insert command
                        MessageBox.Show(result > 0 ? "User Added Successfully" : "Failed to Add User");
                        LoadData();
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Database Error: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error Occurred: " + ex.Message);
                    }
                }
            }
        }
        private void UpdateCategory(int id, string name, string email, string password, string phonenumber)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "UPDATE [User] SET Name = @Name, Email = @Email, Password = @Password, PhoneNumber = @PhoneNumber WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Show the parameters before executing
                   // MessageBox.Show("UserId: " + UserId);  // Debugging the value

                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("User Updated Successfully");
                            LoadData();  // Reload the data after update
                            UserId = -1; // Reset after update
                        }

                        else
                        {
                            MessageBox.Show("Failed to Update User. Check the Id and Data.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error Occurred: " + ex.Message);
                    }
                }
            }
        }


        public void LoadCategoryForUpdate(int id, string name, string email, string password, string phonenumber)
        {
            UserId = id;
            name1.Text = name;
            email1.Text = email;
            password1.Text = password;
            phoneNumber1.Text = phonenumber;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the User ID from the selected row
                UserId = Convert.ToInt32(selectedRow.Cells["Id"].Value); // Assign to UserId directly

                // Get values from your input controls
                string name = name1.Text;
                string email = email1.Text;
                string password = password1.Text;
                string phonenumber = phoneNumber1.Text;

                // Ensure a user is selected
                if (UserId != -1)
                {
                    // Call update method and pass UserId to update user
                    UpdateCategory(UserId, name, email, password, phonenumber);
                }
                else
                {
                    MessageBox.Show("Please select a user to update.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the User ID from the selected row
                int userId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Ask for confirmation before deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Connection string
                    string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";

                    // Delete query
                    string query = "DELETE FROM [User] WHERE Id = @Id";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", userId);

                            try
                            {
                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("User deleted successfully.");
                                    LoadData(); // Refresh the data grid view after deletion
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete user. User ID not found.");
                                }
                            }
                            catch (SqlException sqlEx)
                            {
                                MessageBox.Show("Database Error: " + sqlEx.Message);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An Error Occurred: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Populate the text fields with the data from the selected row
                UserId = Convert.ToInt32(selectedRow.Cells["Id"].Value); // Retrieve UserId from the selected row
                name1.Text = selectedRow.Cells["Name"].Value.ToString(); // Assuming your TextBox for name is name1
                email1.Text = selectedRow.Cells["Email"].Value.ToString(); // Assuming your TextBox for email is email1
                password1.Text = selectedRow.Cells["Password"].Value.ToString(); // Assuming your TextBox for name is name1
                phoneNumber1.Text = selectedRow.Cells["PhoneNumber"].Value.ToString();
            }
        }
    }
}
