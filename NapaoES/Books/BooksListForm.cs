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

namespace NapaoES.Books
{
    public partial class BooksListForm : Form
    {
        Firesharp firebase = new Firesharp();
        public BooksListForm()
        {
            InitializeComponent();
        }
        private async void LoadBooks()
        {
            dgvBooks.Rows.Clear();
            firebase.Response = await firebase.Client.GetAsync("Books");
            if (firebase.Response.Body.ToString() != "null")
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

        private void BooksListForm_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            LoadBooks();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool found_match = false;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Search box is empty.", "Search Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (DataGridViewRow row in dgvBooks.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtSearch.Text))
                    {
                        row.Selected = true;
                        dgvBooks.CurrentCell = row.Cells[1];
                        found_match = true;
                        break;
                    }
                }
                if (found_match != true)
                {
                    MessageBox.Show("Record does not exist.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        }
    }

