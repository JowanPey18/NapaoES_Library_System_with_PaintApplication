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

namespace NapaoES.Student
{
    public partial class StudentProfileForm : Form
    {
        Firesharp firebase = new Firesharp();
        public string StudentID { get; set; }
        public StudentProfileForm()
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
            firebase.Response = await firebase.Client.GetAsync($"User_Accounts/{StudentID}");
            StudentsModel student = firebase.Response.ResultAs<StudentsModel>();
            lblStudentID.Text = StudentID;
            lblStudentName.Text = student.StudentName;
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            frmLogIn Form_LogIn = new frmLogIn();
            Form_LogIn.Show();
            this.Close();
        }

        private void StudentProfileForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            getUserInfo();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmFreedomWall frmFreedom_Wall = new frmFreedomWall();
            frmFreedom_Wall.UserID = StudentID;
            OpenChildForm(frmFreedom_Wall, sender);
        }

        private void lblBooks_Click(object sender, EventArgs e)
        {
            BooksListForm booksListForm = new BooksListForm();
            booksListForm.Show();
        }
    }
}
