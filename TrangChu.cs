using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLCTBDS
{
    public partial class TrangChu : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham ham = new Ham();
        public String userlogin;

        public TrangChu(string tendn)
        {
            InitializeComponent();
            label_hello.Text = tendn;
            userlogin = tendn;
        }

        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainpanel.Controls.Add(childForm);
            mainpanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            ham.Ketnoi(conn);
        }

        private void sanpham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SanPham());
            tieude.Text = sanpham.Text;
        }

        private void hdkg_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HDKG());
            tieude.Text = hdkg.Text;
        }

        private void hdmt_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HDMT());
            tieude.Text = hdmt.Text;
        }

        private void nhanvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new htennv());
            tieude.Text = nhanvien.Text;
        }
    }
}
