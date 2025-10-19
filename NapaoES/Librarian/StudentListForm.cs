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
    public partial class StudentListForm : Form
    {
        Firesharp firebase = new Firesharp();
        public StudentListForm()
        {
            InitializeComponent();
        }

        private void btnAddStudents_Click(object sender, EventArgs e)
        {
            AddStudentForm addStudentForm = new AddStudentForm(this);
            addStudentForm.ShowDialog();
        }

        public async void LoadStudents()
        {
            dgvStudentsList.Rows.Clear();
            firebase.Response = await firebase.Client.GetAsync("Students");
            if (firebase.Response.Body.ToString() != "null")
            {
                Dictionary<string, StudentsModel> getStudents = firebase.Response.ResultAs<Dictionary<string, StudentsModel>>();
                foreach (var Students in getStudents)
                {
                    dgvStudentsList.Rows.Add(
                        Students.Value.IDNumber,
                        Students.Value.StudentName,
                        Students.Value.GradeLevel,
                        Students.Value.PhoneNumber,
                        Students.Value.Password
                        );
                }
            }
        }

        private void StudentListForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            LoadStudents();
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStudentForm updateStudentForm = new UpdateStudentForm(this);
            if (dgvStudentsList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvStudentsList.SelectedCells[0].RowIndex;
                DataGridViewRow row = dgvStudentsList.Rows[selectedrowindex];

                updateStudentForm.IDNumber = row.Cells[0].Value.ToString();
                updateStudentForm.StudentName = row.Cells[1].Value.ToString();
                updateStudentForm.GradeLevel = row.Cells[2].Value.ToString();
                updateStudentForm.PhoneNumber = row.Cells[3].Value.ToString();
                updateStudentForm.Password = row.Cells[4].Value.ToString();

                updateStudentForm.ShowDialog();
            }

        }
    }
}
