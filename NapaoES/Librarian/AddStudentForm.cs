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
    public partial class AddStudentForm : Form

    {
        Firesharp firebase = new Firesharp();
        StudentListForm studentListForm;

        public AddStudentForm(StudentListForm frm)
        {
            InitializeComponent();
            studentListForm = frm;
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            firebase.Response = await firebase.Client.GetAsync($"User_Accounts/{txtStudentID.Text}");
            if (firebase.Response.Body.ToString() != "null")
            {
                MessageBox.Show("Student Already Exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                StudentsModel students = new StudentsModel()
                {
                    IDNumber = txtStudentID.Text,
                    StudentName = txtStudentName.Text,
                    GradeLevel = txtGradeLevel.Text,
                    PhoneNumber = txtPhoneNo.Text,
                    Password = txtPassword.Text,
                    Category = "Student"
                };
                firebase.Response = await firebase.Client.SetAsync($"User_Accounts/{txtStudentID.Text}", students);
                firebase.Response = await firebase.Client.SetAsync($"Students/{txtStudentID.Text}", students);
                studentListForm.LoadStudents();
               
                MessageBox.Show("Student Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            txtStudentID.Text = $"STUD{otp}";
        }
        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            generateID_Number();
        }

    }
}

