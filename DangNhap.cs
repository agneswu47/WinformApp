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
    public partial class Dangnhap : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham ham = new Ham();

        public Dangnhap()
        {
            InitializeComponent();
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {
            ham.Ketnoi(conn);
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            string tendn = mnv.Text;
            string passdn = this.mk.Text;
            string sql_dn = "Select MaNV , Password from USERS where MaNV = '" + tendn + "' and Password = '" + passdn + "'";
            SqlCommand comd = new SqlCommand(sql_dn, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                TrangChu trangchu = new TrangChu(tendn);
                trangchu.Show();
            }
            else
            {
                MessageBox.Show("Dang Nhap That Bai");
            }
            reader.Close();
        }

        private void mnv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mnv.Focus();
            }
        }

        private void mk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mk.Focus();
            }
        }

        private void btn_datlai_Click(object sender, EventArgs e)
        {
            mnv.ResetText();
            mk.ResetText();
        }

        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (showpass.Checked == true)
            {
                mk.UseSystemPasswordChar = false;
            }
            else
            {
                mk.UseSystemPasswordChar = true;
            }
        }
    }
}
