using System.Data.SqlClient;

namespace Eshop
{

    public partial class Dashboard : Form
    {
        Login login = new Login();
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\78__24SOECA21078_Nirali_Answersheet\\Electro\\Database1.mdf;Integrated Security=True"; // Update with your actual connection string
        private object dataRetrieval;

       /* public int GetTotalCustomers()
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
        }*/

        public Dashboard()
        {
           login.ShowDialog();
            InitializeComponent();
           // LoadTotals();
            /* userControl_Product1 = new UserControl_Product();
             userControl_Customer1 = new UserControl_Customer();
             userControl_Category1 = new UserControl_Category();
             userControl_Order1 = new UserControl_Order();
             userControl_Payment1 = new UserControl_Payment();
             userControl_User1 = new UserControl_User();*/



        }

       /* private void LoadTotals()
        {
            label1.Text = GetTotalCustomers().ToString(); // Call directly
            label2.Text = GetTotalProducts().ToString();  // Call directly
            label3.Text = GetTotalEmployees().ToString();
            label4.Text = GetTotalPayment().ToString();

        }*/
        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

     /*   //1_dashboard
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            userControl_Dashboard1.Visible = true;
            userControl_Product1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;
        }*/



        //2_customer
       /* private void guna2Button3_Click(object sender, EventArgs e)
        {
            userControl_Customer1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Product1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;

        }*/

        private void userControl_Customer1_Load(object sender, EventArgs e)
        {

        }

        //3_product
       /* private void guna2Button4_Click(object sender, EventArgs e)
        {
            userControl_Product1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;
        }



        //4_category
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            userControl_Category1.Visible = true;
            userControl_Product1.Visible = false;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;


        }*/

        private void userControl_Product1_Load(object sender, EventArgs e)
        {

        }

        //5_order
      /*  private void guna2Button5_Click(object sender, EventArgs e)
        {
            userControl_Order1.Visible = true;
            userControl_Product1.Visible = false;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;
        }*/

        //6_payment
     /*   private void guna2Button7_Click(object sender, EventArgs e)
        {
            userControl_Payment1.Visible = true;
            userControl_Product1.Visible = false;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_User1.Visible = false;

        }

        //7_user
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            userControl_User1.Visible = true;
            userControl_Product1.Visible = false;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
        }*/

        ////8_payment
        //private void guna2Button9_Click(object sender, EventArgs e)
        //{
        //    userControl_Product1.Visible = false;
        //    userControl_Dashboard1.Visible = false;
        //    userControl_Customer1.Visible = false;
        //}

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SetActiveUserControl(UserControl activeControl)
        {
            userControl_Dashboard1.Visible = activeControl == userControl_Dashboard1;
            userControl_Customer1.Visible = activeControl == userControl_Customer1;
            userControl_Product1.Visible = activeControl == userControl_Product1;
            userControl_Category1.Visible = activeControl == userControl_Category1;
            userControl_Order1.Visible = activeControl == userControl_Order1;
            userControl_Payment1.Visible = activeControl == userControl_Payment1;
            userControl_User1.Visible = activeControl == userControl_User1;
        }

        //1_dashboard
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            SetActiveUserControl(userControl_Dashboard1);


        }

        private void userControl_User1_Load(object sender, EventArgs e)
        {

        }

        //2_customer
        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            userControl_Customer1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Product1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;
        }

        //3_product
        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            userControl_Product1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;
        }
        //4_category
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            userControl_Category1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Product1.Visible = false;
            userControl_Order1.Visible = false ;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;
        }
        //5_order
        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            userControl_Order1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Product1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Payment1.Visible = false;
            userControl_User1.Visible = false;

        }
        //6_payment
        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            userControl_Payment1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Product1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_User1.Visible = false;
        }

        //7_user
        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            userControl_User1.Visible = true;
            userControl_Dashboard1.Visible = false;
            userControl_Customer1.Visible = false;
            userControl_Product1.Visible = false;
            userControl_Category1.Visible = false;
            userControl_Order1.Visible = false;
            userControl_Payment1.Visible = false;
        }

        private void userControl_User1_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void userControl_Customer1_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Hide();  

            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)  
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }
    }
}
