using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Eshop
{
    public partial class UserControl_Category : UserControl
    {
        private int categoryId = -1;
        public UserControl_Category()
        {
            InitializeComponent();
            LoadCategories();
        }
        private void LoadCategories()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "SELECT Id, Name, Status FROM Category";

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

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox2.Text;
            bool status = comboBox1.SelectedItem.ToString() == "Active";

            if (categoryId == -1)
            {
                AddCategory(name, status);
            }
            else
            {
                UpdateCategory(categoryId, name, status);
            }

        }
        private void AddCategory(string name, bool status)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "INSERT INTO Category (Name,Status) VALUES (@Name,@Status)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Status", status ? 1 : 0);

                    try
                    {
                        connection.Open();

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Category Added Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Failed to Add Category");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error Occurred:" + ex.Message);
                    }
                }
            }
        }
        private void UpdateCategory(int id, string name, bool status)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "UPDATE Category SET Name = @Name, Status = @Status WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Id", id);
                   
                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Category Updated Successfully");
                            categoryId = -1; // Reset after update
                        }
                        else
                        {
                            MessageBox.Show("Failed to Update Category");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error Occurred: " + ex.Message);
                    }
                }
            }
        }
        public void LoadCategoryForUpdate(int id, string name, bool status)
        {
            categoryId = id;
            TextBox2.Text = name;
            comboBox1.SelectedItem = status ? "Active" : "Inactive";
        }

        private void DeleteCategory(int id)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";
            string query = "DELETE FROM Category WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Category Deleted Successfully");
                            LoadCategories(); // Refresh the category list
                        }
                        else
                        {
                            MessageBox.Show("Failed to Delete Category");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error Occurred: " + ex.Message);
                    }
                }
            }
        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the category ID from the selected row
                int categoryId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Get values from your input controls
                string name = TextBox2.Text; // Assuming this is where you enter the category name
                bool status = comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Active";

                // Ensure a category is selected
                if (categoryId != -1)
                {
                    UpdateCategory(categoryId, name, status); // Call update method
                    MessageBox.Show("Category updated successfully!");
                }
                else
                {
                    MessageBox.Show("Please select a category to update.");
                }
            }
            else
            {
                MessageBox.Show("Please select a category to update.");
            }
        }

        private void dataGridViewCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve the category ID from the selected row
                int categoryId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Call the delete method
                DeleteCategory(categoryId);
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Retrieve values from the selected row
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                string name = selectedRow.Cells["Name"].Value.ToString();
                bool status = Convert.ToBoolean(selectedRow.Cells["Status"].Value);

                // Populate the input fields
                categoryId = id; // Save the selected category ID
                TextBox2.Text = name; // Set the name in the TextBox
                comboBox1.SelectedItem = status ? "Active" : "Inactive"; // Set the status in the ComboBox
            }
        }
    }
}
