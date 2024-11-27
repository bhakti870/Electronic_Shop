using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Eshop
{
    public partial class UserControl_Dashboard : UserControl
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True"; // Update with your actual connection string
        Login login = new Login();
        private object dataRetrieval;

        public UserControl_Dashboard()
        {
            InitializeComponent();

             LoadTotals();



        }
        public int GetTotalCustomers()
        {
            return GetCount("SELECT COUNT(*) FROM [User]");
        }

        public int GetTotalProducts()
        {
            return GetCount("SELECT COUNT(*) FROM Product");
        }

        public int GetTotalEmployees()
        {
            return GetCount("SELECT COUNT(*) FROM orders");
        }
        public int GetTotalPayment()
        {
            return GetCount("SELECT COUNT(*) FROM Payment");
        }

        private int GetCount(string query)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                count = (int)command.ExecuteScalar();
            }
            return count;
        }
       
        private void LoadTotals()
        {
            label1.Text = GetTotalCustomers().ToString(); // Call directly
            label2.Text = GetTotalProducts().ToString();  // Call directly
            label3.Text = GetTotalEmployees().ToString();
            label4.Text = GetTotalPayment().ToString();

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserControl_Dashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
