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
    public partial class EditBooksForm : Form
    {
        Firesharp firebase = new Firesharp();

        public EditBooksForm()
        {
            InitializeComponent();
        }

        private async void ClearTextBoxes()
        {
            txtBookID.Clear();
            txtBookTitle.Clear();
            txtBookAuthor.Clear();
            txtBookQuantity.Clear();
        }

        private async void LoadBooks()
        {
            dgvBooks.Rows.Clear();
            firebase.Response = await firebase.Client.GetAsync("Books");
            if(firebase.Response.Body.ToString()!="null")
            {
                Dictionary<string, BooksModel> getBooks = firebase.Response.ResultAs<Dictionary<string, BooksModel>>();
                foreach (var Books in getBooks)
                {
                    dgvBooks.Rows.Add(
                        Books.Value.BookID,
                        Books.Value.Title,
                        Books.Value.Author,
                        Books.Value.Quantity
                        );
                }
            }
        }

        private void EditBooksForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            LoadBooks();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            frmLogIn Form_LogIn = new frmLogIn();
            Form_LogIn.Show();
            this.Close();
        }

        private async void btnAddBook_Click(object sender, EventArgs e)
        {
            firebase.Response = await firebase.Client.GetAsync($"Books/{txtBookID.Text}");
            if(firebase.Response.Body.ToString() !="null")
            {
                MessageBox.Show("Book Already Exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                BooksModel Book = new BooksModel()
                {
                    BookID = txtBookID.Text,
                    Title = txtBookTitle.Text,
                    Author = txtBookAuthor.Text,
                    Quantity = txtBookQuantity.Text
                };
                firebase.Response = await firebase.Client.SetAsync($"Books/{txtBookID.Text}", Book);
                LoadBooks();
                ClearTextBoxes();
                MessageBox.Show("Book Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnUpdateBook_Click(object sender, EventArgs e)
        {
            BooksModel Book = new BooksModel()
            {
                BookID = txtBookID.Text,
                Title = txtBookTitle.Text,
                Author = txtBookAuthor.Text,
                Quantity = txtBookQuantity.Text
            };
            firebase.Response = await firebase.Client.UpdateAsync($"Books/{txtBookID.Text}", Book);
            LoadBooks();
            MessageBox.Show("Book Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedCells.Count > 0)
            {
                firebase.Response = await firebase.Client.DeleteAsync($"Books/{txtBookID.Text}");
                LoadBooks();
                ClearTextBoxes();
                MessageBox.Show("Book Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvBooks_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvBooks.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgvBooks.SelectedCells[0].RowIndex;
                DataGridViewRow row = dgvBooks.Rows[selectedrowindex];

                txtBookID.Text = row.Cells[0].Value.ToString();
                txtBookTitle.Text = row.Cells[1].Value.ToString();
                txtBookAuthor.Text = row.Cells[2].Value.ToString();
                txtBookQuantity.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            StudentListForm studentListForm = new StudentListForm();
            studentListForm.Show();
        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            FacultyListForm facultyListForm = new FacultyListForm();
            facultyListForm.Show();
        }
    }
}

