using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Sach
    {
        Database db;
        public Sach()
        {
            db = new Database();
        }
        // Phương thức lấy danh sách sách kết quả trả về là một datatable
        public DataTable LayDSSach()
        {
            string sqlStr = "SELECT * FROM SACH";
            DataTable dt = db.Execute(sqlStr);
            return dt;
        }

        public void XoaSach(string masach)
        {
            string sqlStr = "DELETE FROM SACH WHERE MaSach = " + masach;
            db.ExecuteNonQuery(sqlStr);
        }

        public void ThemSach(string ma, string ten, string tacgia, int namxb, string nxb, float trigia, string ngay)
        {
            string sqlStr = string.Format("INSERT INTO SACH VALUES ('{0}',N'{1}', N'{2}', {3}, N'{4}', {5}, '{6}')",ma, ten, tacgia, namxb, nxb, trigia, ngay);
            db.ExecuteNonQuery(sqlStr);
        }

        public void CapNhatSach(string ma, string tensach, string tacgia, int namxb, string nxb, float trigia, string ngay)
        {
            string sqlStr = string.Format("UPDATE SACH SET TenSach = N'{0}', TacGia = N'{1}', NamXuatBan = {2}, NhaXuatBan = N'{3}', TriGia = {4} , NgayNhap = '{5}' WHERE MaSach = {6}", tensach, tacgia, namxb, nxb, trigia, ngay, ma);
            db.ExecuteNonQuery(sqlStr);
        }
    }
}
