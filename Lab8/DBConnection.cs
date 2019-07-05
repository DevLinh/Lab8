using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class DBConnection
    {
        SqlConnection sqlConn;
        SqlDataAdapter da;
        DataSet ds;

        // Khởi tạo Constructor DBConnection để tạo kết nối
        public string srvName = "VIVOBOOKS\\SQLEXPRESS";
        public string dbName = "QLTHUVIEN";

        public DBConnection()
        {
            string strConn = "Data Source = " + srvName + ";database = " + dbName + ";Integrated Security = True";
            sqlConn = new SqlConnection(strConn);
        }

        // Khởi tạo phương thức thực hiện truy vấn dữ liệu thông qua QUERY làm tham số đầu vào và kết quả trả về là một DataTable
        public DataTable Excute(string query)
        {
            da =  new SqlDataAdapter(query, sqlConn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }
        // KHởi tạo phương thức thực hiện thao tác không trả về bao gồm Insert, Update, Delete
        public void ExcuteNonQuery(string query)
        {
            SqlCommand sqlCmd = new SqlCommand(query, sqlConn);
            //B1: Mở kết nối
            sqlConn.Open();
            //B2: Thực thi dòng lệnh
            sqlCmd.ExecuteNonQuery();
            //B3: Đóng kết nối
            sqlConn.Close();
        }

    }
}
