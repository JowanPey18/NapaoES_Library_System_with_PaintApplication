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

namespace NapaoES.Forms
{
    public partial class UpdateFacultyForm : Form
    {
        Firesharp firebase = new Firesharp();
        public string TeachersID { get; set; }
        public string TeachersName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }

        FacultyListForm facultyListForm;
        public UpdateFacultyForm(FacultyListForm frm)
        {
            InitializeComponent();
            facultyListForm = frm;
        }

        private void UpdateFacultyForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            txtTeacherID.Text = TeachersID;
            txtTeacherName.Text = TeachersName;
            txtPhoneNumber.Text = PhoneNumber;
            txt_Pass.Text = Password;
            cmbCategory.Text = Category;

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            FacultyModel faculty = new FacultyModel()
            {
                IDNumber = txtTeacherID.Text,
                TeachersName = txtTeacherName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Password = txt_Pass.Text,
                FacultyCategory = cmbCategory.Text,
                Category = "Teacher"
            };
            firebase.Response = await firebase.Client.UpdateAsync($"Faculty/{txtTeacherID.Text}", faculty);
            facultyListForm.LoadFaculty();
            MessageBox.Show("Faculty Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
