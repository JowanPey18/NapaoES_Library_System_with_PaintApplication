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
    public partial class AddFacultyForm : Form
    {
        Firesharp firebase = new Firesharp();
        FacultyListForm facultyListForm;
        public AddFacultyForm(FacultyListForm frm)
        {
            InitializeComponent();
            facultyListForm = frm;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            firebase.Response = await firebase.Client.GetAsync($"User_Accounts/{txtTeacherID.Text}");
            if (firebase.Response.Body.ToString() != "null")
            {
                MessageBox.Show("Faculty Already Exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FacultyModel Teachers = new FacultyModel()
                {
                    IDNumber = txtTeacherID.Text,
                    TeachersName = txtTeacherName.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Password = txt_Pass.Text,
                    FacultyCategory = cmbCategory.Text,
                    Category = "Teacher"
                };
                firebase.Response = await firebase.Client.SetAsync($"User_Accounts/{txtTeacherID.Text}", Teachers);
                firebase.Response = await firebase.Client.SetAsync($"Faculty/{txtTeacherID.Text}", Teachers);
                facultyListForm.LoadFaculty();

                MessageBox.Show("Faculty Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtTeacherID.Text = $"TCH{otp}";
        }
        private void AddFacultyForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            generateID_Number();
        }

    }
}
    
