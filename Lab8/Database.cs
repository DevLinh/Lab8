using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Database
    {
        public SqlConnection sqlConn; // Khởi tạo đối tượng kết nối DB SqlConnection trong thư viện SqlClient
        SqlDataAdapter da; // Khởi tạo bộ điều phối dữ liệu (Data Adapter)
        DataSet ds; // Khởi tạo đói tượng chứa dữ liệu khi giao tiếp trong thư viện Data

        // Khởi tạo Constuctor Database tạo lập kết nối
        public string srvName = "VIVOBOOKS\\SQLEXPRESS"; // Chỉ định tên Server
        public string dbName = "QLTHUVIEN_DOAN"; // Chỉ định tên DB
        public string dbUID = "sa";
        public string dbPWD = "123den89";
        public Database()
        {
            string strConn = "Data source=" + srvName + ";database=" + dbName + ";uid=" + dbUID + ";pwd = " + dbPWD + ";";
            sqlConn = new SqlConnection(strConn);
        }

        // Khởi tạo phương thức thực hiện truy vấn dữ liệu thông qua QUERY là tham số đầu vào sqlStr, kết quả trả về là một table, dùng cho các truy vấn Select...
        public  DataTable Execute(string sqlStr)
        {
            da = new SqlDataAdapter(sqlStr, sqlConn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        // Khởi tạo phương thức thực hiện các thao tác không trả về bao gồm Insert, Delete, Update ...
        public void ExecuteNonQuery(string sqlStr)
        {
            SqlCommand sqlcmd = new SqlCommand(sqlStr, sqlConn);
            sqlConn.Open(); // Mở kết nối
            sqlcmd.ExecuteNonQuery(); // THực thi lệnh
            sqlConn.Close(); // Đóng kết nối
        }
    }
}
