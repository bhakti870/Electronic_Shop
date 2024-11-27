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

namespace Eshop
{
    public partial class UserControl_Order : UserControl
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";

        public UserControl_Order()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id,ProductId, CustomerId, Amount, Status, PaymentMethod, Address, Date FROM Orders"; // Adjust the SELECT statement as needed

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable ordersTable = new DataTable();
                        adapter.Fill(ordersTable);
                        dataGridView1.DataSource = ordersTable; // Replace dataGridViewOrders with the name of your DataGridView
                    }
                }
            }
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(productid.Text);
            int customerId = int.Parse(customerid.Text);
            decimal amount = decimal.Parse(amount1.Text);
            string status = status1.Text;
            string paymentMethod = payment.Text;
            string address = address1.Text;

            // Insert the data into the Orders table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Orders ( ProductId,CustomerId, Amount, Status, PaymentMethod, Address) " +
                             "VALUES (@ProductId, @CustomerId, @Amount, @Status, @PaymentMethod, @Address)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    command.Parameters.AddWithValue("@Address", address);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) added successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        private void UpdateOrder(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Orders SET ProductId = @ProductId,CustomerId = @CustomerId, Amount = @Amount, " +
                             "Status = @Status, PaymentMethod = @PaymentMethod, Address = @Address " +
                             "WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", orderId);
                    command.Parameters.AddWithValue("@ProductId", int.Parse(productid.Text));

                    command.Parameters.AddWithValue("@CustomerId", int.Parse(customerid.Text));
                    command.Parameters.AddWithValue("@Amount", decimal.Parse(amount1.Text));
                    command.Parameters.AddWithValue("@Status", status1.Text);
                    command.Parameters.AddWithValue("@PaymentMethod", payment.Text);
                    command.Parameters.AddWithValue("@Address", address1.Text);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) updated successfully!");
                        LoadOrders(); // Refresh the grid after update
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void DeleteOrder(int orderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Orders WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", orderId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) deleted successfully!");
                        LoadOrders(); // Refresh the grid after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming the first column contains the Order ID
                int orderId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Adjust index if necessary
                UpdateOrder(orderId);
            }
            else
            {
                MessageBox.Show("Please select an order to update.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // Assuming the columns are in this order
                customerid.Text = row.Cells["CustomerId"].Value.ToString();
                amount1.Text = row.Cells["Amount"].Value.ToString();
                status1.Text = row.Cells["Status"].Value.ToString();
                payment.Text = row.Cells["PaymentMethod"].Value.ToString();
                address1.Text = row.Cells["Address"].Value.ToString();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Assuming the first column contains the Order ID
                int orderId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Adjust index if necessary
                var confirmResult = MessageBox.Show("Are you sure to delete this order?",
                                                     "Confirm Delete!",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DeleteOrder(orderId);
                }
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Assuming the columns are named exactly as in the database
                // Update input fields with the values from the selected row
                productid.Text = row.Cells["ProductId"].Value.ToString();
                customerid.Text = row.Cells["CustomerId"].Value.ToString();
                amount1.Text = row.Cells["Amount"].Value.ToString();
                status1.Text = row.Cells["Status"].Value.ToString();
                payment.Text = row.Cells["PaymentMethod"].Value.ToString();
                address1.Text = row.Cells["Address"].Value.ToString();
            }
        }
    }

}
