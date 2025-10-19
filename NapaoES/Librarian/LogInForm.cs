using NapaoES.Database;
using NapaoES.Faculty;
using NapaoES.Forms;
using NapaoES.Librarian;
using NapaoES.Main;
using NapaoES.Model;
using NapaoES.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NapaoES
{
    public partial class frmLogIn : Form
    {
        Firesharp firebase = new Firesharp();
        string _IDNumber;
        string _Password;
        string _Category;
        public frmLogIn()
        {
            InitializeComponent();
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            firebase.Response = await firebase.Client.GetAsync($"User_Accounts/");
            if (firebase.Response.Body.ToString() != "null")
            {
                Dictionary<string, AccountsModel> getUser = firebase.Response.ResultAs<Dictionary<string, AccountsModel>>();
                foreach (var User in getUser)
                {
                    if (txtUserID.Text.Equals(User.Value.IDNumber) && txtPass.Text.Equals(User.Value.Password) && User.Value.Category.Equals("Librarian"))
                    {
                        _IDNumber = User.Value.IDNumber;
                        _Password = User.Value.Password;
                        _Category = User.Value.Category;
                        break;
                    }
                    else if (txtUserID.Text.Equals(User.Value.IDNumber) && txtPass.Text.Equals(User.Value.Password) && User.Value.Category.Equals("Student"))
                    {
                        _IDNumber = User.Value.IDNumber;
                        _Password = User.Value.Password;
                        _Category = User.Value.Category;
                        break;
                    }
                    else if (txtUserID.Text.Equals(User.Value.IDNumber) && txtPass.Text.Equals(User.Value.Password) && User.Value.Category.Equals("Teacher"))
                    {
                        _IDNumber = User.Value.IDNumber;
                        _Password = User.Value.Password;
                        _Category = User.Value.Category;
                        break;
                    }
                }
                if (txtUserID.Text.Equals(_IDNumber) && txtPass.Text.Equals(_Password) && _Category.Equals("Librarian"))
                {
                    LibrarianMainForm librarianMainForm = new LibrarianMainForm();
                    librarianMainForm.Show();
                    this.Hide();
                }
                else if (txtUserID.Text.Equals(_IDNumber) && txtPass.Text.Equals(_Password) && _Category.Equals("Student"))
                {
                    StudentProfileForm studentProfileForm = new StudentProfileForm();
                    studentProfileForm.StudentID = txtUserID.Text;
                    studentProfileForm.Show();
                    this.Hide();
                }
                else if (txtUserID.Text.Equals(_IDNumber) && txtPass.Text.Equals(_Password) && _Category.Equals("Teacher"))
                {
                    FacultyProfileForm facultyProfileForm = new FacultyProfileForm();
                    facultyProfileForm.FacultyID = txtUserID.Text;
                    facultyProfileForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username or Password is Incorrect", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            firebase.Response = await firebase.Client.GetAsync($"User_Accounts/");
            if (firebase.Response.Body.ToString() == "null")
            {
                RegisterLibrarianForm registerLibrarianForm = new RegisterLibrarianForm();
                registerLibrarianForm.ShowDialog();
            }
        }
    }
}
