using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class PhieuThuTien
    {
        Database db;

        public PhieuThuTien()
        {
            db = new Database();
        }

        public DataTable LayDSPhieuThu()
        {
            string sqlStr = "SELECT * FROM PHIEUTHUTIEN";
            DataTable dt = db.Execute(sqlStr);
            return dt;
        }

        public DataTable LayDSNhanVien()
        {
            string sqlStr = "SELECT * FROM NHANVIEN";
            DataTable dt = db.Execute(sqlStr);
            return dt;
        }

        public float LayTienNo(int ma)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TienNo FROM DOCGIA WHERE MaDocGia = @ma";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.Connection = db.sqlConn;
            db.sqlConn.Open();
            float returnValue = float.Parse(cmd.ExecuteScalar().ToString());
            db.sqlConn.Close();
            return returnValue;
        }
        // hàm kiểm tra xem mã đọc giả được nhập vào có trong danh sách đọc giả hay không
        //tham số bool truyền vào , nếu true, ta kiểm tra xem đọc giả đó có tiền nợ (tiền nợ > 0> và ngược lại
        public bool CheckPhieuThu(int ma, bool check)
        {
            SqlCommand cmd = new SqlCommand();
            if (!check)
            {
                cmd.CommandText = "SELECT count(*) FROM DOCGIA WHERE MaDocGia = @ma";
            }
            else
            {
                cmd.CommandText = "SELECT count(*) FROM DOCGIA WHERE MaDocGia = @ma AND TienNo > 0";
            }
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ma", ma);
            cmd.Connection = db.sqlConn;
            db.sqlConn.Open();
            Int32 returnValue = (Int32)cmd.ExecuteScalar();
            db.sqlConn.Close();
            
            if (returnValue > 0)
                return true;
            else
                return false;
        }

        //Thêm phiếu thu, cập nhật lại số tiền nợ trong bảng đọc giả
        public void ThemPhieuThu(float tienno, float tienthu, int madocgia, string index_cb)
        {
            string sqlStr = string.Format("INSERT INTO PHIEUTHUTIEN VALUES ( {0}, {1}, {2}, '{3}' )", tienno, tienthu, madocgia, index_cb);
            db.ExecuteNonQuery(sqlStr);
            string sqlStr2 = string.Format("UPDATE DOCGIA SET TienNo = {0} - {1} WHERE MaDocGia = {2}", tienno, tienthu, madocgia);
            db.ExecuteNonQuery(sqlStr2);
        }
    }
}
