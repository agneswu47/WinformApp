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
    public partial class SanPham : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham ham = new Ham();

        public SanPham()
        {
            InitializeComponent();
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            ham.Ketnoi(conn);
            string sql = "SELECT SANPHAM.MaSP, SANPHAM.TenSP, LOAISANPHAM.MaLSP, LOAISANPHAM.TenLSP, GIATRISANPHAM.GiaTri, THONGTINCUNGCAP.MaTTCC, THONGTINCUNGCAP.TTCC, CHUDAUTU.MaCDT, CHUDAUTU.HoTen \r\n" +
                "FROM SANPHAM \r\n" +
                "INNER JOIN CHUDAUTU ON SANPHAM.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC";
            ham.HienThiDLDG(DTGVSP, sql, conn);
            ham.LoadComb(tenlsp, "SELECT MaLSP, TenLSP FROM LOAISANPHAM", conn, "TenLSP", "MaLSP");
            msp.Enabled = false;
            mttcc.Enabled = false;
            mcdt.Enabled = false;
        }

        private void DTGVSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_sua.Enabled = true;
            btn_xoa.Enabled = true;
            btn_them.Enabled = false;
           
            msp.Text = DTGVSP.Rows[e.RowIndex].Cells[0].Value.ToString();
            tensp.Text = DTGVSP.Rows[e.RowIndex].Cells[1].Value.ToString();
            tenlsp.Text = DTGVSP.Rows[e.RowIndex].Cells[3].Value.ToString();
            gtsp.Text = DTGVSP.Rows[e.RowIndex].Cells[4].Value.ToString();
            mttcc.Text = DTGVSP.Rows[e.RowIndex].Cells[5].Value.ToString();
            ttcc.Text = DTGVSP.Rows[e.RowIndex].Cells[6].Value.ToString();
            mcdt.Text = DTGVSP.Rows[e.RowIndex].Cells[7].Value.ToString();
            hten.Text = DTGVSP.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            //sanpham
            string malonnhat = "SELECT MAX(SUBSTRING(MaSP, 3, 2)) FROM SANPHAM";
            SqlCommand comd = new SqlCommand(malonnhat, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    msp.Text = "SP0" + max;
                }
                else
                {
                    msp.Text = "SP" + max;
                }
                msp.Enabled = false;
            }
            reader.Close();

            //thongtincungcap
            string malonnhat3 = "SELECT MAX(SUBSTRING(MaTTCC, 3, 2)) FROM THONGTINCUNGCAP";
            SqlCommand comd3 = new SqlCommand(malonnhat3, conn);
            SqlDataReader reader3 = comd3.ExecuteReader();

            if (reader3.Read())
            {
                int max = Convert.ToInt32(reader3.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    mttcc.Text = "TT0" + max;
                }
                else
                {
                    mttcc.Text = "TT" + max;
                }
                mttcc.Enabled = false;
            }
            reader3.Close();

            //chudautu
            string malonnhat4 = "SELECT MAX(SUBSTRING(MaCDT, 4, 3)) FROM CHUDAUTU";
            SqlCommand comd4 = new SqlCommand(malonnhat4, conn);
            SqlDataReader reader4 = comd4.ExecuteReader();

            if (reader4.Read())
            {
                int max = Convert.ToInt32(reader4.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    mcdt.Text = "CDT0" + max;
                }
                else
                {
                    mcdt.Text = "CDT" + max;
                }
                mcdt.Enabled = false;
            }
            reader4.Close();

            btn_sua.Enabled = false;
            
            tenlsp.Text = "Chọn loại sản phẩm";

            string masp = msp.Text;
            string tsp = tensp.Text;
            string giatri = gtsp.Text;
            string mattcc = mttcc.Text;
            string tenttcc = ttcc.Text;
            string macdt = mcdt.Text;
            string hoten = hten.Text;
            
            string sql_ttcc = "INSERT INTO THONGTINCUNGCAP(MaTTCC, TTCC) VALUES ('" + mattcc + "', N'" + tenttcc + "')";
            SqlCommand comd_ttcc = new SqlCommand(sql_ttcc, conn);
            ham.CapNhat(sql_ttcc, conn);

            string sql_cdt = "INSERT INTO CHUDAUTU(MaCDT, HoTen) VALUES ('" + macdt + "', N'" + hoten + "')";
            SqlCommand comd_cdt = new SqlCommand(sql_cdt, conn);
            ham.CapNhat(sql_cdt, conn);

            string sql_sp = "INSERT INTO SANPHAM(MaSP, TenSP, MaLSP, MaCDT) VALUES ('" + masp + "', N'" + tsp + "', '" + tenlsp.SelectedValue.ToString() + "', '" + macdt + "')";
            SqlCommand comd_sp = new SqlCommand(sql_sp, conn);
            ham.CapNhat(sql_sp, conn);

            string sql_giatri = "INSERT INTO GIATRISANPHAM(GiaTri, MaTTCC) VALUES ('" + giatri + "', '" + mattcc + "')";
            SqlCommand comd_giatri = new SqlCommand(sql_giatri, conn);
            ham.CapNhat(sql_giatri, conn);

            string sql = "SELECT SANPHAM.MaSP, SANPHAM.TenSP, LOAISANPHAM.MaLSP, LOAISANPHAM.TenLSP, GIATRISANPHAM.GiaTri, THONGTINCUNGCAP.MaTTCC, THONGTINCUNGCAP.TTCC, CHUDAUTU.MaCDT, CHUDAUTU.HoTen \r\n" +
                "FROM SANPHAM \r\n" +
                "INNER JOIN CHUDAUTU ON SANPHAM.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC";

            ham.HienThiDLDG(DTGVSP, sql, conn);
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sql_tt = "UPDATE THONGTINCUNGCAP SET TTCC = N'" + ttcc.Text + "' WHERE MaTTCC = '" + mttcc.Text + "'";
            SqlCommand comd_tt = new SqlCommand(sql_tt, conn);
            ham.CapNhat(sql_tt, conn);

            string sql_suacdt = "UPDATE CHUDAUTU SET HoTen = N'" + hten.Text + "' WHERE MaCDT = '" + mcdt.Text + "'";
            SqlCommand comd_suacdt = new SqlCommand(sql_suacdt, conn);
            ham.CapNhat(sql_suacdt, conn);

            string sql_suasp = "UPDATE SANPHAM SET TenSP = N'" + tensp.Text + "' WHERE MaSP = '" + msp.Text + "'";
            SqlCommand comd_sp = new SqlCommand(sql_suasp, conn);
            ham.CapNhat(sql_suasp, conn);

            string sql_gt = "UPDATE GIATRISANPHAM SET GiaTri = '" + gtsp.Text + "' WHERE MaTTCC = '" + mttcc.Text + "'";
            SqlCommand comd_gt = new SqlCommand(sql_gt, conn);
            ham.CapNhat(sql_gt, conn);

            string sql = "SELECT SANPHAM.MaSP, SANPHAM.TenSP, LOAISANPHAM.MaLSP, LOAISANPHAM.TenLSP, GIATRISANPHAM.GiaTri, THONGTINCUNGCAP.MaTTCC, THONGTINCUNGCAP.TTCC, CHUDAUTU.MaCDT, CHUDAUTU.HoTen \r\n" +
                "FROM SANPHAM \r\n" +
                "INNER JOIN CHUDAUTU ON SANPHAM.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC";

            ham.HienThiDLDG(DTGVSP, sql, conn);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql_ttcc = "DELETE FROM THONGTINCUNGCAP WHERE MaTTCC = '" + ttcc.Text + "'";
            SqlCommand comd_ttcc = new SqlCommand(sql_ttcc, conn);
            ham.CapNhat(sql_ttcc, conn);

            string sql_sp = "DELETE FROM SANPHAM WHERE MaSP = '" + msp.Text + "'";
            SqlCommand comd_sp = new SqlCommand(sql_sp, conn);
            ham.CapNhat(sql_sp, conn);

            string sql_cdt = "DELETE FROM CHUDAUTU WHERE MaCDT = '" + mcdt.Text + "'";
            SqlCommand comd_cdt = new SqlCommand(sql_cdt, conn);
            ham.CapNhat(sql_cdt, conn);

            string sql = "SELECT SANPHAM.MaSP, SANPHAM.TenSP, LOAISANPHAM.MaLSP, LOAISANPHAM.TenLSP, GIATRISANPHAM.GiaTri, THONGTINCUNGCAP.MaTTCC, THONGTINCUNGCAP.TTCC, CHUDAUTU.MaCDT, CHUDAUTU.HoTen \r\n" +
                "FROM SANPHAM \r\n" +
                "INNER JOIN CHUDAUTU ON SANPHAM.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC";

            ham.HienThiDLDG(DTGVSP, sql, conn);
        }

        private void btn_datlai_Click(object sender, EventArgs e)
        {
            msp.ResetText();
            tensp.ResetText();
            tenlsp.ResetText();
            gtsp.ResetText();
            mttcc.ResetText();
            ttcc.ResetText();
            mcdt.ResetText();
            hten.ResetText();
            timkiem.ResetText();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timkiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string tukhoa = timkiem.Text;
                string sql_tim = "SELECT SANPHAM.MaSP, SANPHAM.TenSP, LOAISANPHAM.MaLSP, LOAISANPHAM.TenLSP, GIATRISANPHAM.GiaTri, THONGTINCUNGCAP.MaTTCC, THONGTINCUNGCAP.TTCC, CHUDAUTU.MaCDT, CHUDAUTU.HoTen \r\n" +
                    "FROM SANPHAM \r\n" +
                    "INNER JOIN CHUDAUTU ON SANPHAM.MaCDT = CHUDAUTU.MaCDT \r\n" +
                    "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                    "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                    "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                    "AND (SANPHAM.TenSP like N'%" + tukhoa + "%' \r\n" +
                    "OR LOAISANPHAM.TenLSP like N'%" + tukhoa + "%' \r\n" +
                    "OR GIATRISANPHAM.GiaTri like '%" + tukhoa + "%' \r\n" +
                    "OR THONGTINCUNGCAP.TTCC like N'%" + tukhoa + "%' \r\n" +
                    "OR CHUDAUTU.HoTen like N'%" + tukhoa + "%')";
                ham.HienThiDLDG(DTGVSP, sql_tim, conn);
            }
        }
    }
}
