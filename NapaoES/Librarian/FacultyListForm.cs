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
    public partial class FacultyListForm : Form
    {
        Firesharp firebase = new Firesharp();
        public FacultyListForm()
        {
            InitializeComponent();
        }

        private void btnAddFaculty_Click(object sender, EventArgs e)
        {

            AddFacultyForm addFacultyForm = new AddFacultyForm(this);
            addFacultyForm.ShowDialog();
        }

        public async void LoadFaculty()
        {
            dgvFacultyList.Rows.Clear();
            firebase.Response = await firebase.Client.GetAsync("Faculty");
            if (firebase.Response.Body.ToString() != "null")
            {
                Dictionary<string, FacultyModel> getFaculty = firebase.Response.ResultAs<Dictionary<string, FacultyModel>>();
                foreach (var Teachers in getFaculty)
                {
                    dgvFacultyList.Rows.Add(
                        Teachers.Value.IDNumber,
                        Teachers.Value.TeachersName,
                        Teachers.Value.PhoneNumber,
                        Teachers.Value.Password,
                        Teachers.Value.FacultyCategory
                        );
                }


            }
        }

        private void FacultyListForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            LoadFaculty();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateFacultyForm updateFacultyForm = new UpdateFacultyForm(this);
            if (dgvFacultyList.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvFacultyList.SelectedCells[0].RowIndex;
                DataGridViewRow row = dgvFacultyList.Rows[selectedrowindex];

                updateFacultyForm.TeachersID = row.Cells[0].Value.ToString();
                updateFacultyForm.TeachersName = row.Cells[1].Value.ToString();
                updateFacultyForm.PhoneNumber = row.Cells[2].Value.ToString();
                updateFacultyForm.Password = row.Cells[3].Value.ToString();
                updateFacultyForm.Category = row.Cells[4].Value.ToString();

                updateFacultyForm.ShowDialog();

            }
        }
    }
}
