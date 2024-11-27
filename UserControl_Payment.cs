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
    public partial class UserControl_Payment : UserControl
    {
        public UserControl_Payment()
        {
            InitializeComponent();
            LoadPayments();

        }
        public void LoadPayments()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True"; // Replace with your actual connection string
            string query = "SELECT PaymentId, OrderId, CustomerId, Amount, Status, Method, PaymentDate FROM Payment";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Bind the data to the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

            private void InsertPayment(int orderId, int customerId, decimal amount, string status, string method)
        {
            // Connection string to your SQL database (update with your actual connection details)
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True";

            // SQL insert query
            string query = "INSERT INTO Payment (OrderId, CustomerId, Amount, Status, Method) " +
                           "VALUES (@OrderId, @CustomerId, @Amount, @Status, @Method)";

            // Use ADO.NET to execute the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Method", method);

                        // Execute the insert command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the insert was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Payment added successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Error while adding payment.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                LoadPayments();

            }
        }

        public void UpdatePayment(int paymentId, decimal amount, string status, string method)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True"; // Replace with your actual connection string
            string query = "UPDATE Payment SET Amount = @Amount, Status = @Status, Method = @Method WHERE PaymentId = @PaymentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue("@PaymentId", paymentId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Method", method);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Payment updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No payment found with the specified PaymentId.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    LoadPayments();

                }
            }
        }

        private void DeletePayment(int paymentId)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True"; // Replace with your actual connection string
            string query = "DELETE FROM Payment WHERE PaymentId = @PaymentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the PaymentId parameter
                    command.Parameters.AddWithValue("@PaymentId", paymentId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Payment deleted successfully!");
                            LoadPayments(); // Refresh the DataGridView after deletion
                        }
                        else
                        {
                            MessageBox.Show("No payment found with the specified PaymentId.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            int orderId = int.Parse(orderid1.Text);           // Assuming OrderId is a number
            int customerId = int.Parse(customerid1.Text);     // Assuming CustomerId is a number
            decimal amount = decimal.Parse(amount1.Text);     // Amount as decimal
            string status = comboBox1.SelectedItem.ToString();  // Payment status (e.g., 'Completed', 'Pending', 'Failed')
            string method = comboBox2.SelectedItem.ToString();  // Payment method (e.g., 'Credit Card', 'PayPal', etc.)

            // Insert the payment record into the Payment table
            InsertPayment(orderId, customerId, amount, status, method);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int paymentId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Adjust index if necessary
                decimal amount = decimal.Parse(amount1.Text);     // Assuming you have a way to get this
                string status = comboBox1.SelectedItem.ToString();  // Assuming comboBox1 has a selected item
                string method = comboBox2.SelectedItem.ToString();  // Assuming comboBox2 has a selected item

                UpdatePayment(paymentId, amount, status, method);
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
                // Assuming the first column contains the PaymentId
                int paymentId = (int)dataGridView1.SelectedRows[0].Cells[0].Value; // Adjust index if necessary
                DeletePayment(paymentId);
            }
            else
            {
                MessageBox.Show("Please select a payment to delete.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Assuming the columns are in the order: PaymentId, OrderId, CustomerId, Amount, Status, Method, PaymentDate
                // Populate the text fields and combo boxes with the selected row data
                orderid1.Text = selectedRow.Cells["OrderId"].Value.ToString(); // Use column name or index
                customerid1.Text = selectedRow.Cells["CustomerId"].Value.ToString(); // Use column name or index
                amount1.Text = selectedRow.Cells["Amount"].Value.ToString(); // Use column name or index
                comboBox1.SelectedItem = selectedRow.Cells["Status"].Value.ToString(); // Use column name or index
                comboBox2.SelectedItem = selectedRow.Cells["Method"].Value.ToString(); // Use column name or index
            }
        }
    }
}
