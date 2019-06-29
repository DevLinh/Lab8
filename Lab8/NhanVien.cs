using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class NhanVien
    {
        Database db;
        public NhanVien()
        {
            db = new Database();
        }
        // Khởi tạo phương thức nhận về danh sách nhân viên, kết quả trả về là Datatable
        public DataTable LayDSNhanVien()
        {
            string sqlStr = "SELECT MaNhanVien, HoTenNhanVien, NgaySinh, DiaChi, DienThoai, TenBangCap FROM NHANVIEN N, BANGCAP B WHERE N.MaBangCap = B.MaBangCap"; ;
            DataTable dt = db.Execute(sqlStr);
            return dt;
        }

        public DataTable LayBangCap()
        {
            string sqlStr = "SELECT * FROM BANGCAP";
            DataTable dt = db.Execute(sqlStr);
            return dt;
        }

        public void XoaNhanVien(string index_nv)
        {
            string sqlStr = "DELETE FROM NHANVIEN WHERE MaNhanVien = " + index_nv;
            db.ExecuteNonQuery(sqlStr);
        }

        public void ThemNhanVien(string ten, string ngaysinh, string diachi, string dienthoai, string index_bc)
        {
            string sqlStr = string.Format("INSERT INTO NHANVIEN VALUES (N'{0}', '{1}', N'{2}', '{3}', '{4}')", ten,ngaysinh,diachi,dienthoai,index_bc);
            db.ExecuteNonQuery(sqlStr);
        }

        public void CapNhatNhanVien(string index_nv, string hoten, string ngaysinh, string diachi, string dienthoai, string index_bc)
        {
            string sqlStr = string.Format("UPDATE NHANVIEN SET HoTenNhanVien = N'{0}', NgaySinh = '{1}', DiaChi = N'{2}', DienThoai = '{3}', MaBangCap = '{4}' WHERE MaNhanVien = {5}", hoten, ngaysinh, diachi, dienthoai, index_bc, index_nv);
            db.ExecuteNonQuery(sqlStr);
        }
    }
}
