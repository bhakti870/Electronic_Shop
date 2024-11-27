using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Eshop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string email = textbox1.Text;  // Get the entered username
            string password = textbox2.Text;  // Get the entered password

            // Check if email and password are correct
            if (email == "admin7" && password == "admin@2717")
            {
                // If the login is successful, set the dialog result to OK
                this.DialogResult = DialogResult.OK;
                this.Close();  // Close the login form
            }
            else
            {
                // Display error message if the credentials don't match
                MessageBox.Show("Enter a valid email and password", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
