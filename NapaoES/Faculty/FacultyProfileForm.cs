using NapaoES.Books;
using NapaoES.Database;
using NapaoES.FreedomWall;
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

namespace NapaoES.Faculty
{
    public partial class FacultyProfileForm : Form
    {
        Firesharp firebase = new Firesharp();
        public string FacultyID { get; set; }
        public FacultyProfileForm()
        {
            InitializeComponent();
        }
        private Form activeForm;
        private void OpenChildForm(Form childForm, Object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.pnlDesktopPane.Controls.Add(childForm);
            this.pnlDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private async void getUserInfo()
        {
            firebase.Response = await firebase.Client.GetAsync($"User_Accounts/{FacultyID}");
            FacultyModel faculty = firebase.Response.ResultAs<FacultyModel>();
            lblFacultyID.Text = FacultyID;
            lblFacultyName.Text = faculty.TeachersName;
        }
        private void FacultyProfileForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            getUserInfo();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            frmLogIn Form_LogIn = new frmLogIn();
            Form_LogIn.Show();
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmFreedomWall frmFreedom_Wall = new frmFreedomWall();
            frmFreedom_Wall.UserID = FacultyID;
            OpenChildForm(frmFreedom_Wall, sender);

        }

        private void lblInfo_Click(object sender, EventArgs e)
        {
            

        }

        private void lblBooks_Click(object sender, EventArgs e)
        {
            BooksListForm booksListForm = new BooksListForm();
            booksListForm.Show();
        }

    }
}
