using NapaoES.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NapaoES.Main
{
    public partial class LibrarianMainForm : Form
    {
        public LibrarianMainForm()
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
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            OpenChildForm(new EditBooksForm(), sender);
        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FacultyListForm(), sender);
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentListForm(), sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogIn frmLogIn = new frmLogIn();
            frmLogIn.Show();
            this.Close();
        }
    }
}
