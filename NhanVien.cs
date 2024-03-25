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
    public partial class htennv : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham ham = new Ham();
        string gioitinh;
        public htennv()
        {
            InitializeComponent();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            ham.Ketnoi(conn);
            string sql = "SELECT NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.NgaySinh, NHANVIEN.GioiTinh, NHANVIEN.SDT, PHONGBAN.MaPB, PHONGBAN.TenPB, CHUCVU.MaCV, CHUCVU.TenCV, LICHSUCONGTAC.ThoiGianLamViec \r\n" +
                "FROM NHANVIEN \r\n" +
                "INNER JOIN PHONGBAN ON NHANVIEN.MaPB = PHONGBAN.MaPB \r\n" +
                "INNER JOIN LICHSUCONGTAC ON LICHSUCONGTAC.MaNV = NHANVIEN.MaNV \r\n" +
                "INNER JOIN CHUCVU ON LICHSUCONGTAC.MaCV = CHUCVU.MaCV \r\n"+
                "INNER JOIN USERS ON NHANVIEN.MaNV = USERS.MaNV \r\n";

            ham.HienThiDLDG(DTGVNV, sql, conn);
            ham.LoadComb(tenpb, "SELECT MaPB, TenPB FROM PHONGBAN", conn, "TenPB", "MaPB");
            ham.LoadComb(tencv, "SELECT MaCV, TenCV FROM CHUCVU", conn, "TenCV", "MaCV");
            mnv.Enabled = false;
        }

        private void DTGVNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_sua.Enabled = true;
            btn_xoa.Enabled = true;
            btn_them.Enabled = false;

            mnv.Text = DTGVNV.Rows[e.RowIndex].Cells[0].Value.ToString();
            htnv.Text = DTGVNV.Rows[e.RowIndex].Cells[1].Value.ToString();
            ns.Text = DTGVNV.Rows[e.RowIndex].Cells[2].Value.ToString();
            string gioitinh = DTGVNV.Rows[e.RowIndex].Cells[3].Value.ToString();
            sdt.Text = DTGVNV.Rows[e.RowIndex].Cells[4].Value.ToString();
            tenpb.Text = DTGVNV.Rows[e.RowIndex].Cells[6].Value.ToString();
            tencv.Text = DTGVNV.Rows[e.RowIndex].Cells[8].Value.ToString();
            tglv.Text = DTGVNV.Rows[e.RowIndex].Cells[9].Value.ToString();

            if (gioitinh == "Nam")
            {
                nam.Checked = true;
                nu.Checked = false;
            }
            else
            {
                nu.Checked = true;
                nam.Checked = false;
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string malonnhat = "SELECT MAX(SUBSTRING(MaNV, 3, 2)) FROM NHANVIEN";
            SqlCommand comd = new SqlCommand(malonnhat, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    mnv.Text = "NV0" + max;
                }
                else
                {
                    mnv.Text = "NV" + max;
                }

                mnv.Enabled = false;
            }

            reader.Close();
            btn_sua.Enabled = false;

            tenpb.Text = "Chọn phòng ban";
            tencv.Text = "Chọn chức vụ";

            if (nam.Checked == true)
            {
                gioitinh = nam.Text;
            }
            else
            {
                gioitinh = nu.Text;
            }

            DateTime ngaysinhTime = ns.Value;
            DateTime hientaiTime = DateTime.Now;
            int compareResult_nkb = DateTime.Compare(ngaysinhTime.Date, hientaiTime.Date);

            if (compareResult_nkb < 0)
            {
                MessageBox.Show("Ngày sinh phải nhỏ hơn ngày hiện tại!\nNhập lại");
                ns.Focus();
                return;
            }

            string ma = mnv.Text;
            string hoten = htnv.Text;
            string nsnv = ns.Value.ToString();
            string sdtnv = sdt.Text;

            string sql_nv = "INSERT INTO NHANVIEN(MaNV, HoTen, NgaySinh, GioiTinh, SDT, MaPB) VALUES ('" + ma + "', N'" + hoten + "', '" + nsnv + "', N'" + gioitinh + "', '" + sdtnv + "', '" + tenpb.SelectedValue.ToString() + "')";
            SqlCommand comd_nv = new SqlCommand(sql_nv, conn);
            ham.CapNhat(sql_nv, conn);

            string sql_lsct = "INSERT INTO LICHSUCONGTAC(ThoiGianLamViec, MaPB, MaNV, MaCV) VALUES ('" + tglv.Value.ToString() + "', '" + tenpb.SelectedValue.ToString() + "', '" + mnv.Text + "', '" + tencv.SelectedValue.ToString() + "')";
            SqlCommand comd_lsct = new SqlCommand(sql_lsct, conn);
            ham.CapNhat(sql_lsct, conn);

            string sql_user = "INSERT INTO USERS(MaNV, Password) VALUES ('" + mnv.Text + "', '" + mk.Text + "')";
            SqlCommand comd_user = new SqlCommand(sql_user, conn);
            ham.CapNhat(sql_user, conn);

            string sql = "SELECT NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.NgaySinh, NHANVIEN.GioiTinh, NHANVIEN.SDT, PHONGBAN.MaPB, PHONGBAN.TenPB, CHUCVU.MaCV, CHUCVU.TenCV, LICHSUCONGTAC.ThoiGianLamViec \r\n" +
                "FROM NHANVIEN \r\n" +
                "INNER JOIN PHONGBAN ON NHANVIEN.MaPB = PHONGBAN.MaPB \r\n" +
                "INNER JOIN LICHSUCONGTAC ON LICHSUCONGTAC.MaNV = NHANVIEN.MaNV \r\n" +
                "INNER JOIN CHUCVU ON LICHSUCONGTAC.MaCV = CHUCVU.MaCV \r\n" +
                "INNER JOIN USERS ON NHANVIEN.MaNV = USERS.MaNV \r\n";

            ham.HienThiDLDG(DTGVNV, sql, conn);
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (nam.Checked == true)
            {
                gioitinh = nam.Text;
            }
            else
            {
                gioitinh = nu.Text;
            }

            string sql_nv = "UPDATE NHANVIEN SET Hoten = N'" + htnv.Text + "', NgaySinh = '" + ns.Value.ToString() + "', GioiTinh = N'" + gioitinh + "', SDT = '" + sdt.Text + "', MaPB = N'" + tenpb.SelectedValue.ToString() + "' WHERE MaNV ='" + mnv.Text + "'";
            SqlCommand comd_nv = new SqlCommand(sql_nv, conn);
            ham.CapNhat(sql_nv, conn);

            string sql_lsct = "UPDATE LICHSUCONGTAC SET ThoiGianLamViec = '" + tglv.Value.ToString() + "', MaCV = N'" + tencv.SelectedValue.ToString() + " 'WHERE MaNV = '" + mnv.Text + "'";
            SqlCommand comd_lsct = new SqlCommand(sql_lsct, conn);
            ham.CapNhat(sql_lsct, conn);

            string sql_us = "UPDATE USERS SET Password = '" + mk.Text + " 'WHERE MaNV = '" + mnv.Text + "'";
            SqlCommand comd_us = new SqlCommand(sql_us, conn);
            ham.CapNhat(sql_us, conn);

            string sql = "SELECT NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.NgaySinh, NHANVIEN.GioiTinh, NHANVIEN.SDT, PHONGBAN.MaPB, PHONGBAN.TenPB, CHUCVU.MaCV, CHUCVU.TenCV, LICHSUCONGTAC.ThoiGianLamViec \r\n" +
                "FROM NHANVIEN \r\n" +
                "INNER JOIN PHONGBAN ON NHANVIEN.MaPB = PHONGBAN.MaPB \r\n" +
                "INNER JOIN LICHSUCONGTAC ON LICHSUCONGTAC.MaNV = NHANVIEN.MaNV \r\n" +
                "INNER JOIN CHUCVU ON LICHSUCONGTAC.MaCV = CHUCVU.MaCV \r\n" +
                "INNER JOIN USERS ON NHANVIEN.MaNV = USERS.MaNV \r\n";
            ham.HienThiDLDG(DTGVNV, sql, conn);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql_del_pass = "DELETE FROM USERS WHERE MaNV = '" + mnv.Text + "'";
            SqlCommand comd_del_pass = new SqlCommand(sql_del_pass, conn);
            ham.CapNhat(sql_del_pass, conn);
            
            string sql_lsct = "DELETE FROM LICHSUCONGTAC WHERE MaNV =  '" + mnv.Text + "'";
            SqlCommand comd_lsct = new SqlCommand(sql_lsct, conn);
            ham.CapNhat(sql_lsct, conn);

            string sql_nv = "DELETE FROM NHANVIEN WHERE MaNV =  '" + mnv.Text + "'";
            SqlCommand comd_nv = new SqlCommand(sql_nv, conn);
            ham.CapNhat(sql_nv, conn);

            string sql = "SELECT NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.NgaySinh, NHANVIEN.GioiTinh, NHANVIEN.SDT, PHONGBAN.MaPB, PHONGBAN.TenPB, CHUCVU.MaCV, CHUCVU.TenCV, LICHSUCONGTAC.ThoiGianLamViec \r\n" +
                "FROM NHANVIEN \r\n" +
                "INNER JOIN PHONGBAN ON NHANVIEN.MaPB = PHONGBAN.MaPB \r\n" +
                "INNER JOIN LICHSUCONGTAC ON LICHSUCONGTAC.MaNV = NHANVIEN.MaNV \r\n" +
                "INNER JOIN CHUCVU ON LICHSUCONGTAC.MaCV = CHUCVU.MaCV \r\n" +
                "INNER JOIN USERS ON NHANVIEN.MaNV = USERS.MaNV \r\n";

            ham.HienThiDLDG(DTGVNV, sql, conn);
        }

        private void timkiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string tukhoa = timkiem.Text;
                string sql_tim = "SELECT NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.NgaySinh, NHANVIEN.GioiTinh, NHANVIEN.SDT, PHONGBAN.MaPB, PHONGBAN.TenPB, CHUCVU.MaCV, CHUCVU.TenCV, LICHSUCONGTAC.ThoiGianLamViec \r\n" +
                    "FROM NHANVIEN \r\n" +
                    "INNER JOIN PHONGBAN ON NHANVIEN.MaPB = PHONGBAN.MaPB \r\n" +
                    "INNER JOIN LICHSUCONGTAC ON LICHSUCONGTAC.MaNV = NHANVIEN.MaNV \r\n" +
                    "INNER JOIN CHUCVU ON LICHSUCONGTAC.MaCV = CHUCVU.MaCV \r\n" +
                    "AND (NHANVIEN.HoTen like N'%" + tukhoa + "%' \r\n" +
                    "OR NHANVIEN.NgaySinh like '%" + tukhoa + "%' \r\n" +
                    "OR NHANVIEN.GioiTinh like N'%" + tukhoa + "%' \r\n" +
                    "OR NHANVIEN.SDT like '%" + tukhoa + "%' \r\n" +
                    "OR PHONGBAN.TenPB like N'%" + tukhoa + "%'\r\n" +
                    "OR CHUCVU.TenCV like N'%" + tukhoa + "%' \r\n" +
                    "OR LICHSUCONGTAC.ThoiGianLamViec like '%" + tukhoa + "%')";
                ham.HienThiDLDG(DTGVNV, sql_tim, conn);
            }
         }

        private void btn_datlai_Click(object sender, EventArgs e)
        {
            mnv.ResetText();
            htnv.ResetText();
            ns.ResetText();
            sdt.ResetText();
            tenpb.ResetText();
            tencv.ResetText();
            tglv.ResetText();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ns_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
