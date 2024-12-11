using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string affiliation = cbAfilliation.Text;

            MainForm newform = new MainForm();

            if (affiliation == "Store Manager") // If Store Manager
            {
                if (username == "admin" && password == "admin123")
                {
                    MessageBox.Show("Welcome " + username, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    newform.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password, Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (affiliation == "Employee") // If Employee
            {
                if (username == "employee" && password == "employee123")
                {
                    MessageBox.Show("Welcome " + username, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    newform.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password, Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // Other methods and controls (InitializeComponent, etc.) here
    }
}
