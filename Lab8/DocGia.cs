using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class DocGia
    {
        Database db;
        public DocGia()
        {
            db = new Database();
        }

        public DataTable LayDSDocGia()
        {
            string sqlStr = "SELECT * FROM DOCGIA";
            DataTable dt = db.Execute(sqlStr);
            return dt;
        }

        public void XoaDocGia(string madg)
        {
            string sqlStr = string.Format("DELETE FROM DOCGIA WHERE MaDocGia =" + madg);
            db.ExecuteNonQuery(sqlStr);
        }

        public void ThemDocGia(string ten, string ngaysinh, string diachi, string email, string ngaylap, string ngayhet, float tienno)
        {
            string sqlStr = string.Format("INSERT INTO DOCGIA VALUES (N'{0}', '{1}', N'{2}', '{3}', '{4}', '{5}', {6} )", ten, ngaysinh, diachi, email, ngaylap, ngayhet, tienno);
            db.ExecuteNonQuery(sqlStr);
        }

        public void CapNhatDocGia(string index_dg, string ten, string ngaysinh, string diachi, string email, string ngaylap, string ngayhet, float tienno)
        {
            string sqlStr = string.Format("UPDATE DOCGIA SET HoTenDocGia = N'{0}', NgaySinh = '{1}', DiaChi = N'{2}', Email = '{3}', NgayLapThe = '{4}', NgayHetHan = '{5}', TienNo = {6} WHERE MaDocGia = {7}", ten, ngaysinh, diachi, email, ngaylap, ngayhet, tienno, index_dg);
            db.ExecuteNonQuery(sqlStr);
        }
    }
}
