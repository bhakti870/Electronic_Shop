using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Eshop
{
    public partial class UserControl_Product : UserControl
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";

        public UserControl_Product()
        {
            InitializeComponent();
           LoadCategories();
            LoadProductData();

        }
        private List<int> categoryIds = new List<int>(); // List to hold category IDs

        private void LoadCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, Name FROM Category"; // Adjust based on your Category table structure

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Store the category ID
                            categoryIds.Add((int)reader["Id"]);
                            // Add only the category name to the ComboBox
                            ComboBox1.Items.Add(reader["Name"].ToString());
                        }
                    }
                }
            }
        }
        private void LoadProductData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, Name, Rate, Quantity, CategoryId, Status, Image FROM Product"; // Adjust based on your Product table structure

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable; // Bind the DataTable to the DataGridView

                        // Check if any data is loaded
                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No data found in the Product table.");
                        }
                    }
                }
            }
        }





        private void categoryComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = (dynamic)ComboBox1.Items[e.Index];
            e.DrawBackground();
            e.Graphics.DrawString(item.Text, e.Font, Brushes.Black, e.Bounds);
            e.DrawFocusRectangle();
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void UserControl_Product_Load(object sender, EventArgs e)
        {
            LoadProductData();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text; // Adjust the name of your TextBox
            float rate = float.Parse(rate1.Text); // Adjust the name of your TextBox
            int quantity = int.Parse(quantity1.Text); // Adjust the name of your TextBox

            // Get the selected index of the ComboBox
            int selectedIndex = ComboBox1.SelectedIndex;

            // Check if a valid item is selected and retrieve the category ID from the list
            int categoryId = (selectedIndex >= 0) ? categoryIds[selectedIndex] : 0; // Default to 0 if nothing is selected

            bool status = ComboBox2.SelectedItem.ToString() == "Active";
            string image = PictureBox1.Text; // Save the image path to a TextBox for later use

            InsertProduct(name, rate, quantity, categoryId, status, image);
        }
       
       

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    PictureBox1.Image = Image.FromFile(imagePath); // Display image
                    PictureBox1.Text = imagePath; // Save the image path to a TextBox for later use
                }
            }
        }

        private bool DoesProductExist(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Product WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", productId);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void InsertProduct(string name, float rate, int quantity, int categoryId, bool status, string image)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Product (Name, Rate, Quantity, CategoryId, Status, Image) " +
                             "VALUES (@Name, @Rate, @Quantity, @CategoryId, @Status, @Image)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Rate", rate);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@CategoryId", (object)categoryId ?? DBNull.Value); // Handle nullable
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Image", image);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} product(s) added successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void UpdateProduct(int productId)
        {
            // Get values from the form fields
            string name = TextBox1.Text; // Assuming TextBox1 is for the product name
            float rate = float.Parse(rate1.Text); // Assuming rate1 is the TextBox for the rate
            int quantity = int.Parse(quantity1.Text); // Assuming quantity1 is the TextBox for quantity

            // Get the selected category ID from ComboBox1
            int selectedIndex = ComboBox1.SelectedIndex;
            int categoryId = (selectedIndex >= 0) ? categoryIds[selectedIndex] : 0; // Default to 0 if nothing is selected

            // Get the status from ComboBox2 (assuming it's a ComboBox for status)
            bool status = ComboBox2.SelectedItem.ToString() == "Active";

            // Get the image path from PictureBox1 (assuming PictureBox1 is used for displaying the image)
            string image = PictureBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Product SET Name = @Name, Rate = @Rate, Quantity = @Quantity, " +
                             "CategoryId = @CategoryId, Status = @Status, Image = @Image WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", productId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Rate", rate);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@CategoryId", (object)categoryId ?? DBNull.Value); // Handle nullable
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Image", image);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) updated successfully!");
                        LoadProductData(); // Refresh the grid after update
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }



        private void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Product WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", productId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No product found with the given ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadProductData();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming the first column contains the Order ID
                int ProductId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Adjust index if necessary
                UpdateProduct(ProductId);
            }
            else
            {
                MessageBox.Show("Please select an order to update.");
            }
        }



        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get the product ID from the selected row
                int productId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // Call the delete method
                DeleteProduct(productId);

                // Refresh the DataGridView to show updated data
                LoadProductData();  // Refresh the data after deletion
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Populate the input fields with data from the selected row
                TextBox1.Text = selectedRow.Cells["Name"].Value.ToString(); // Assuming the column name for product name is "Name"
                rate1.Text = selectedRow.Cells["Rate"].Value.ToString();   // Assuming the column name for product rate is "Rate"
                quantity1.Text = selectedRow.Cells["Quantity"].Value.ToString(); // Assuming the column name for quantity is "Quantity"

                // Set the selected category in ComboBox1
                int categoryId = (int)selectedRow.Cells["CategoryId"].Value; // Assuming the column name for category ID is "CategoryId"
                int index = categoryIds.IndexOf(categoryId);
                ComboBox1.SelectedIndex = index; // Set the ComboBox to the correct category

                // Set the status in ComboBox2
                bool status = (bool)selectedRow.Cells["Status"].Value; // Assuming the column name for status is "Status"
                ComboBox2.SelectedItem = status ? "Active" : "Inactive"; // Assuming "Active" and "Inactive" are your ComboBox items

                // Optionally set the image in PictureBox1
                string imagePath = selectedRow.Cells["Image"].Value.ToString(); // Assuming the column name for image is "Image"
                if (!string.IsNullOrEmpty(imagePath))
                {
                    PictureBox1.Image = Image.FromFile(imagePath); // Load the image into the PictureBox
                    PictureBox1.Text = imagePath; // Set the image path to the PictureBox text
                }
            }
        }
    }
}

