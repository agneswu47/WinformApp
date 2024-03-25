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
using System.IO;
using System.Security.Policy;

namespace QLCTBDS
{
    public partial class HDKG : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham ham = new Ham();

        public HDKG()
        {
            InitializeComponent();
        }

        private void HDKG_Load(object sender, EventArgs e)
        {
            ham.Ketnoi(conn);
            string sql = "SELECT HOPDONGKYGUI.STT_HDKG, HOPDONGKYGUI.NgayLap, HOPDONGKYGUI.MucHoaHong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, CHUDAUTU.MaCDT, CHUDAUTU.HoTen, CHUDAUTU.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGKYGUI \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGKYGUI.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN CHUDAUTU ON HOPDONGKYGUI.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGKYGUI.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDKG, sql, conn);
            ham.LoadComb(tensp, "SELECT MaSP, TenSP FROM SANPHAM", conn, "TenSP", "MaSP");
            ham.LoadComb(tenlsp, "SELECT MaLSP, TenLSP FROM LOAISANPHAM", conn, "TenLSP", "MaLSP");
            ham.LoadComb(ttcc, "SELECT MaTTCC, TTCC FROM THONGTINCUNGCAP", conn, "TTCC", "MaTTCC");
            ham.LoadComb(mnv, "SELECT MaNV, HoTen FROM NHANVIEN", conn, "MaNV", "HoTen");

            stthdkg.Enabled = false;
            mcdt.Enabled = false;
            htnv.Enabled = false;
            sdtnv.Enabled = false;
        }

        private void DTGVHDKG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_sua.Enabled = true;
            btn_xoa.Enabled = true;
            btn_them.Enabled = false;

            stthdkg.Text = DTGVHDKG.Rows[e.RowIndex].Cells[0].Value.ToString();
            nlhdkg.Text = DTGVHDKG.Rows[e.RowIndex].Cells[1].Value.ToString();
            mhh.Text = DTGVHDKG.Rows[e.RowIndex].Cells[2].Value.ToString();
            tensp.Text = DTGVHDKG.Rows[e.RowIndex].Cells[3].Value.ToString();
            tenlsp.Text = DTGVHDKG.Rows[e.RowIndex].Cells[4].Value.ToString();
            ttcc.Text = DTGVHDKG.Rows[e.RowIndex].Cells[5].Value.ToString();
            gtsp.Text = DTGVHDKG.Rows[e.RowIndex].Cells[6].Value.ToString();
            mcdt.Text = DTGVHDKG.Rows[e.RowIndex].Cells[7].Value.ToString();
            htcdt.Text = DTGVHDKG.Rows[e.RowIndex].Cells[8].Value.ToString();
            sdtcdt.Text = DTGVHDKG.Rows[e.RowIndex].Cells[9].Value.ToString();
            mnv.Text = DTGVHDKG.Rows[e.RowIndex].Cells[10].Value.ToString();
            htnv.Text = DTGVHDKG.Rows[e.RowIndex].Cells[11].Value.ToString();
            sdtnv.Text = DTGVHDKG.Rows[e.RowIndex].Cells[12].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            //stthdkg
            string sttlonnhat = "SELECT MAX(STT_HDKG) FROM HOPDONGKYGUI";
            SqlCommand comd = new SqlCommand(sttlonnhat, conn);
            SqlDataReader reader = comd.ExecuteReader();

            if (reader.Read())
            {
                int max = Convert.ToInt32(reader.GetValue(0).ToString()) + 1;
                stthdkg.Text = "" + max;
                stthdkg.Enabled = false;
            }
            reader.Close();

            //machudautu
            string mcdtlonnhat = "SELECT MAX(SUBSTRING(MaCDT, 4, 3)) FROM CHUDAUTU";
            SqlCommand comd_cdtmax = new SqlCommand(mcdtlonnhat, conn);
            SqlDataReader reader_cdt = comd_cdtmax.ExecuteReader();

            if (reader_cdt.Read())
            {
                int max = Convert.ToInt32(reader_cdt.GetValue(0).ToString()) + 1;
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
            reader_cdt.Close();

            btn_sua.Enabled = false;

            tensp.Text = "Chọn sản phẩm";
            tenlsp.Text = "Chọn loại sản phẩm";
            ttcc.Text = "Chọn thông tin";

            string stt_hdkg = stthdkg.Text;
            string ngaylap = nlhdkg.Value.ToString(); ;
            string muchh = mhh.Text;
            string giatri = gtsp.Text;
            string macdt = mcdt.Text;
            string htencdt = htcdt.Text;
            string sdt_cdt = sdtcdt.Text;
            string htennv = htnv.Text;
            string sdt_nv = sdtnv.Text;

            string sql_cdt = "INSERT INTO CHUDAUTU(MaCDT, HoTen, SDT) VALUES ('" + macdt + "', N'" + htencdt + "', '" + sdt_cdt + "')";
            SqlCommand comd_cdt = new SqlCommand(sql_cdt, conn);
            ham.CapNhat(sql_cdt, conn);

            string sql_hdkg = "INSERT INTO HOPDONGKYGUI(STT_HDKG, NgayLap, MucHoaHong, MaCDT, MaNV, MaSP) VALUES ('" + stt_hdkg + "', '" + ngaylap + "', '" + muchh + "', '" + macdt + "', '" + mnv.Text + "','" + tensp.SelectedValue.ToString() + "')";
            SqlCommand comd_hdkg = new SqlCommand(sql_hdkg, conn);
            ham.CapNhat(sql_hdkg, conn);

            string sql = "SELECT HOPDONGKYGUI.STT_HDKG, HOPDONGKYGUI.NgayLap, HOPDONGKYGUI.MucHoaHong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, CHUDAUTU.MaCDT, CHUDAUTU.HoTen, CHUDAUTU.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                 "FROM HOPDONGKYGUI \r\n" +
                 "INNER JOIN SANPHAM ON HOPDONGKYGUI.MaSP = SANPHAM.MaSP \r\n" +
                 "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                 "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                 "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                 "INNER JOIN CHUDAUTU ON HOPDONGKYGUI.MaCDT = CHUDAUTU.MaCDT \r\n" +
                 "INNER JOIN NHANVIEN ON HOPDONGKYGUI.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDKG, sql, conn);
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sql_hdkg = "UPDATE HOPDONGKYGUI SET NgayLap = '" + nlhdkg.Value.ToString() + "', MucHoaHong = '" + mhh.Text + "' WHERE STT_HDKG = '" + stthdkg.Text + "'";
            SqlCommand comd_hdkg = new SqlCommand(sql_hdkg, conn);
            ham.CapNhat(sql_hdkg, conn);

            string sql = "SELECT HOPDONGKYGUI.STT_HDKG, HOPDONGKYGUI.NgayLap, HOPDONGKYGUI.MucHoaHong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, CHUDAUTU.MaCDT, CHUDAUTU.HoTen, CHUDAUTU.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGKYGUI \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGKYGUI.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN CHUDAUTU ON HOPDONGKYGUI.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGKYGUI.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDKG, sql, conn);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql_hdkg = "DELETE FROM HOPDONGKYGUI WHERE STT_HDKG = '" + stthdkg.Text + "'";
            SqlCommand comd_hdkg = new SqlCommand(sql_hdkg, conn);
            ham.CapNhat(sql_hdkg, conn);

            string sql_cdt = "DELETE FROM CHUDAUTU WHERE MaCDT = '" + mcdt.Text + "'";
            SqlCommand comd_cdt = new SqlCommand(sql_cdt, conn);
            ham.CapNhat(sql_cdt, conn);

            string sql_nv = "DELETE FROM NHANVIEN WHERE MaNV = '" + mnv.Text + "'";
            SqlCommand comd_nv = new SqlCommand(sql_nv, conn);
            ham.CapNhat(sql_nv, conn);

            string sql = "SELECT HOPDONGKYGUI.STT_HDKG, HOPDONGKYGUI.NgayLap, HOPDONGKYGUI.MucHoaHong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, CHUDAUTU.MaCDT, CHUDAUTU.HoTen, CHUDAUTU.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGKYGUI \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGKYGUI.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN CHUDAUTU ON HOPDONGKYGUI.MaCDT = CHUDAUTU.MaCDT \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGKYGUI.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDKG, sql, conn);
        }

        private void btn_datlai_Click(object sender, EventArgs e)
        {
            stthdkg.ResetText();
            nlhdkg.ResetText();
            mhh.ResetText();
            tensp.ResetText();
            tenlsp.ResetText();
            gtsp.ResetText();
            ttcc.ResetText();
            mcdt.ResetText();
            htcdt.ResetText();
            sdtcdt.ResetText();
            mnv.ResetText();
            htnv.ResetText();
            sdtnv.ResetText();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
