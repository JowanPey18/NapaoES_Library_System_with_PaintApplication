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

namespace NapaoES.FreedomWall
{
    public partial class frmFreedomWall : Form
    {
        Firesharp firebase = new Firesharp();
        public string UserID { get; set; }
        public frmFreedomWall()
        {
            InitializeComponent();
        }

        private void frmFreedomWall_Load(object sender, EventArgs e)
        {
            firebase.Connect();
            LoadPost();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetails.SelectedCells.Count > 0)
            {
                firebase.Response = await firebase.Client.DeleteAsync($"Post/{UserID}/{txtTitleOfThePost.Text}");
                LoadPost();
                MessageBox.Show("Post Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public async void LoadPost()
        {
            dgvDetails.Rows.Clear();
            firebase.Response = await firebase.Client.GetAsync($"Post/{UserID}");
            if (firebase.Response.Body.ToString() != "null")
            {
                Dictionary<string, FreedomWallModel> getPost = firebase.Response.ResultAs<Dictionary<string, FreedomWallModel>>();
                foreach (var Post in getPost)
                {
                    dgvDetails.Rows.Add(
                        Post.Value.DateandTime,
                        Post.Value.Title
                        );
                }
            }
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            firebase.Response = await firebase.Client.GetAsync($"Post/{UserID}/{txtTitleOfThePost.Text}");
            if (firebase.Response.Body.ToString() != "null")
            {
                MessageBox.Show("Post Not Uploaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FreedomWallModel freedomWallModel = new FreedomWallModel()
                {
                    Title = txtTitleOfThePost.Text,
                    Post = txtFreedomWall.Text,
                    Author = "Author",
                    DateandTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
                };
                firebase.Response = await firebase.Client.SetAsync($"Post/{UserID}/{txtTitleOfThePost.Text}", freedomWallModel);
                LoadPost();

                MessageBox.Show("Post Uploaded", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            FreedomWallModel freedomWallModel = new FreedomWallModel()
            {
                Title = txtTitleOfThePost.Text,
                Post = txtFreedomWall.Text,
                Author = "Author",
                DateandTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
            };
            firebase.Response = await firebase.Client.UpdateAsync($"Post/{UserID}/{txtTitleOfThePost.Text}", freedomWallModel);
            LoadPost();
            MessageBox.Show("Post Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void dgvDetails_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvDetails.SelectedRows.Count > 0)
            {
                int selectedrowindex = dgvDetails.SelectedCells[0].RowIndex;
                DataGridViewRow row = dgvDetails.Rows[selectedrowindex];


                txtTitleOfThePost.Text = row.Cells[1].Value.ToString();
                firebase.Response = await firebase.Client.GetAsync($"Post/{UserID}/{txtTitleOfThePost.Text}");
                FreedomWallModel freedomWallModel = firebase.Response.ResultAs<FreedomWallModel>();
                txtFreedomWall.Text = freedomWallModel.Post;
            }
        }

        private void btnPaint_Click(object sender, EventArgs e)
        {
            PaintApplication paintApplication = new PaintApplication();
            paintApplication.Show();
        }
    }
}

