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
    public partial class HDMT : Form
    {
        SqlConnection conn = new SqlConnection();
        Ham ham = new Ham();

        public HDMT()
        {
            InitializeComponent();
        }

        private void HDMT_Load(object sender, EventArgs e)
        {
            ham.Ketnoi(conn);
            string sql = "SELECT HOPDONGMUATHUE.STT_HDMT, HOPDONGMUATHUE.NgayLap, HOPDONGMUATHUE.GiaTriHopDong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, KHACHHANG.MaKH, KHACHHANG.HoTen, KHACHHANG.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGMUATHUE \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGMUATHUE.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN KHACHHANG ON HOPDONGMUATHUE.MaKH = KHACHHANG.MaKH \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGMUATHUE.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDMT, sql, conn);
            ham.LoadComb(tensp, "SELECT MaSP, TenSP FROM SANPHAM", conn, "TenSP", "MaSP");
            ham.LoadComb(tenlsp, "SELECT MaLSP, TenLSP FROM LOAISANPHAM", conn, "TenLSP", "MaLSP");
            ham.LoadComb(ttcc, "SELECT MaTTCC, TTCC FROM THONGTINCUNGCAP", conn, "TTCC", "MaTTCC");
            ham.LoadComb(mnv, "SELECT MaNV, HoTen FROM NHANVIEN", conn, "MaNV", "HoTen");

            stthdmt.Enabled = false;
            mkh.Enabled = false;
            htnv.Enabled = false;
            sdtnv.Enabled = false;
        }

        private void DTGVHDMT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_sua.Enabled = true;
            btn_xoa.Enabled = true;
            btn_them.Enabled = false;

            stthdmt.Text = DTGVHDMT.Rows[e.RowIndex].Cells[0].Value.ToString();
            nlhdmt.Text = DTGVHDMT.Rows[e.RowIndex].Cells[1].Value.ToString();
            gthd.Text = DTGVHDMT.Rows[e.RowIndex].Cells[2].Value.ToString();
            tensp.Text = DTGVHDMT.Rows[e.RowIndex].Cells[3].Value.ToString();
            tenlsp.Text = DTGVHDMT.Rows[e.RowIndex].Cells[4].Value.ToString();
            ttcc.Text = DTGVHDMT.Rows[e.RowIndex].Cells[5].Value.ToString();
            gtsp.Text = DTGVHDMT.Rows[e.RowIndex].Cells[6].Value.ToString();
            mkh.Text = DTGVHDMT.Rows[e.RowIndex].Cells[7].Value.ToString();
            htkh.Text = DTGVHDMT.Rows[e.RowIndex].Cells[8].Value.ToString();
            sdtkh.Text = DTGVHDMT.Rows[e.RowIndex].Cells[9].Value.ToString();
            mnv.Text = DTGVHDMT.Rows[e.RowIndex].Cells[10].Value.ToString();
            htnv.Text = DTGVHDMT.Rows[e.RowIndex].Cells[11].Value.ToString();
            sdtnv.Text = DTGVHDMT.Rows[e.RowIndex].Cells[12].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            //stthdmt
            string sttlonnhat = "SELECT MAX(STT_HDMT) FROM HOPDONGMUATHUE";
            SqlCommand comd_stt = new SqlCommand(sttlonnhat, conn);
            SqlDataReader reader_stt = comd_stt.ExecuteReader();

            if (reader_stt.Read())
            {
                int max = Convert.ToInt32(reader_stt.GetValue(0).ToString()) + 1;
                stthdmt.Text = "" + max;
                stthdmt.Enabled = false;
            }
            reader_stt.Close();

            //sttkh
            string mkhlonnhat = "SELECT MAX(SUBSTRING(MaKH, 3, 2)) FROM KHACHHANG";
            SqlCommand comd_kh = new SqlCommand(mkhlonnhat, conn);
            SqlDataReader reader_kh = comd_kh.ExecuteReader();

            if (reader_kh.Read())
            {
                int max = Convert.ToInt32(reader_kh.GetValue(0).ToString()) + 1;
                if (max < 10)
                {
                    mkh.Text = "KH0" + max;
                }
                else
                {
                    mkh.Text = "KH" + max;
                }

                mkh.Enabled = false;
            }
            reader_kh.Close();

            btn_sua.Enabled = false;

            tensp.Text = "Chọn sản phẩm";
            tenlsp.Text = "Chọn loại sản phẩm";
            ttcc.Text = "Chọn thông tin";

            string stt_hdmt = stthdmt.Text;
            string ngaylap = nlhdmt.Value.ToString(); ;
            string giatrihd = gthd.Text;
            string giatri = gtsp.Text;
            string makh = mkh.Text;
            string htenkh = htkh.Text;
            string sdt_kh = sdtkh.Text;
            string htennv = htnv.Text;
            string sdt_nv = sdtnv.Text;

            string sql_cdt = "INSERT INTO KHACHHANG(MaKH, HoTen, SDT) VALUES ('" + makh + "', N'" + htenkh + "', '" + sdt_kh + "')";
            SqlCommand comd_cdt = new SqlCommand(sql_cdt, conn);
            ham.CapNhat(sql_cdt, conn);

            string sql_hdkg = "INSERT INTO HOPDONGMUATHUE(STT_HDMT, NgayLap, GiaTriHopDong, MaNV, MaKH, MaSP) VALUES ('" + stt_hdmt + "', '" + ngaylap + "', '" + giatrihd + "', '" + mnv.Text + "', '" + makh + "', '" + tensp.SelectedValue.ToString() + "')";
            SqlCommand comd_hdkg = new SqlCommand(sql_hdkg, conn);
            ham.CapNhat(sql_hdkg, conn);

            string sql = "SELECT HOPDONGMUATHUE.STT_HDMT, HOPDONGMUATHUE.NgayLap, HOPDONGMUATHUE.GiaTriHopDong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, KHACHHANG.MaKH, KHACHHANG.HoTen, KHACHHANG.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGMUATHUE \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGMUATHUE.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN KHACHHANG ON HOPDONGMUATHUE.MaKH = KHACHHANG.MaKH \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGMUATHUE.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDMT, sql, conn);
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string sql_hdmt = "UPDATE HOPDONGMUATHUE SET NgayLap = '" + nlhdmt.Value.ToString() + "', GiaTriHopDong = '"+ gthd.Text + "' WHERE STT_HDMT = '" + stthdmt.Text + "'";
            SqlCommand comd_hdmt = new SqlCommand(sql_hdmt, conn);
            ham.CapNhat(sql_hdmt, conn);

            string sql = "SELECT HOPDONGMUATHUE.STT_HDMT, HOPDONGMUATHUE.NgayLap, HOPDONGMUATHUE.GiaTriHopDong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, KHACHHANG.MaKH, KHACHHANG.HoTen, KHACHHANG.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGMUATHUE \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGMUATHUE.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN KHACHHANG ON HOPDONGMUATHUE.MaKH = KHACHHANG.MaKH \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGMUATHUE.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDMT, sql, conn);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string sql_hdmt = "DELETE FROM HOPDONGMUATHUE WHERE STT_HDMT = '" + stthdmt.Text + "'";
            SqlCommand comd_hdmt = new SqlCommand(sql_hdmt, conn);
            ham.CapNhat(sql_hdmt, conn);

            string sql_kh = "DELETE FROM KHACHHANG WHERE MaKH = '" + mkh.Text + "'";
            SqlCommand comd_kh = new SqlCommand(sql_kh, conn);
            ham.CapNhat(sql_kh, conn);

            string sql_nv = "DELETE FROM NHANVIEN WHERE MaNV = '" + mnv.Text + "'";
            SqlCommand comd_nv = new SqlCommand(sql_nv, conn);
            ham.CapNhat(sql_nv, conn);

            string sql = "SELECT HOPDONGMUATHUE.STT_HDMT, HOPDONGMUATHUE.NgayLap, HOPDONGMUATHUE.GiaTriHopDong,SANPHAM.TenSP,LOAISANPHAM.TenLSP, THONGTINCUNGCAP.TTCC, GIATRISANPHAM.GiaTri, KHACHHANG.MaKH, KHACHHANG.HoTen, KHACHHANG.SDT, NHANVIEN.MaNV, NHANVIEN.HoTen, NHANVIEN.SDT \r\n" +
                "FROM HOPDONGMUATHUE \r\n" +
                "INNER JOIN SANPHAM ON HOPDONGMUATHUE.MaSP = SANPHAM.MaSP \r\n" +
                "INNER JOIN LOAISANPHAM ON SANPHAM.MaLSP = LOAISANPHAM.MaLSP \r\n" +
                "INNER JOIN THONGTINCUNGCAP ON THONGTINCUNGCAP.MaTTCC = LOAISANPHAM.MaTTCC \r\n" +
                "INNER JOIN GIATRISANPHAM ON GIATRISANPHAM.MaTTCC = THONGTINCUNGCAP.MaTTCC \r\n" +
                "INNER JOIN KHACHHANG ON HOPDONGMUATHUE.MaKH = KHACHHANG.MaKH \r\n" +
                "INNER JOIN NHANVIEN ON HOPDONGMUATHUE.MaNV = NHANVIEN.MaNV";
            ham.HienThiDLDG(DTGVHDMT, sql, conn);
        }

        private void btn_datlai_Click(object sender, EventArgs e)
        {
            stthdmt.ResetText();
            nlhdmt.ResetText();
            gthd.ResetText();
            tensp.ResetText();
            tenlsp.ResetText();
            gtsp.ResetText();
            ttcc.ResetText();
            mkh.ResetText();
            htkh.ResetText();
            sdtkh.ResetText();
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
