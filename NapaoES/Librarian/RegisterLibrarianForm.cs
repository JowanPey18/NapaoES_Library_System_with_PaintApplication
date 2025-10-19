using NapaoES.Database;
using NapaoES.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NapaoES.Librarian
{
    public partial class RegisterLibrarianForm : Form
    {
        Firesharp firebase = new Firesharp();
        public RegisterLibrarianForm()
        {
            InitializeComponent();
        }

        private void RegisterLibrarian_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            generateID_Number();
        }
        public void generateID_Number()
        {
            string num = "1234567890";
            int len = num.Length;
            string otp = string.Empty;
            int otpDigit = 4; // Length 
            string finaldigit;

            int getindex;
            for (int i = 0; i < otpDigit; i++)
            {
                do
                {
                    getindex = new Random().Next(0, len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                } while (otp.IndexOf(finaldigit) != -1);

                otp += finaldigit;
            }
            txtUserID.Text = otp;
        }
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            AccountsModel accounts = new AccountsModel()
            {
                Username = txtName.Text,
                IDNumber = txtUserID.Text,
                Password = txtPass.Text
            };
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text)
                || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name and Password cannot be empty.", "Empty Fields",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                AccountsModel accountsModel = new AccountsModel()
                {
                    IDNumber = txtUserID.Text,
                    Username = txtName.Text,
                    Password = txtPass.Text,
                    Category = "Librarian"
                };
                firebase.Response = await firebase.Client.SetAsync($"User_Accounts/{txtUserID.Text}",accountsModel);
                MessageBox.Show("Librarian Account Registered.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
