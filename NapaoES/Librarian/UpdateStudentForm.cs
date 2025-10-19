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

    public partial class UpdateStudentForm : Form
    {
        Firesharp firebase = new Firesharp();
        public string IDNumber { get; set; }
        public string StudentName { get; set; }
        public string GradeLevel { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        StudentListForm studentListForm;
        public UpdateStudentForm(StudentListForm frm)
        {
            InitializeComponent();
            studentListForm = frm;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            StudentsModel students = new StudentsModel()
            {
                IDNumber= txtStudentID.Text,
                StudentName = txtStudentName.Text,
                GradeLevel = txtGradeLevel.Text,
                PhoneNumber = txtPhoneNo.Text,
                Password = txtPassword.Text,
                Category = "Student"
            };
            firebase.Response = await firebase.Client.UpdateAsync($"Students/{txtStudentID.Text}", students);
            studentListForm.LoadStudents();
            MessageBox.Show("Student Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStudentForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            txtStudentID.Text = IDNumber;
            txtStudentName.Text = StudentName;
            txtGradeLevel.Text = GradeLevel;
            txtPhoneNo.Text = PhoneNumber;
            txtPassword.Text = Password;
        }

    }
}
