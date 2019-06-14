using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DAO
{
    public class DangNhapDAO
    {
        private static DangNhapDAO instance;

        public static DangNhapDAO Instance
        {
            get { if (instance == null) instance = new DangNhapDAO(); return instance; }
            private set { instance = value; }
        }

        public int DangNhap(string name, string pass)
        {
            string query = string.Format("SELECT COUNT(*) AS SoLuong  FROM NGUOIDUNG WHERE TENDANGNHAP = '{0}' AND MATKHAU = '{1}'", name, pass);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if ((int)data.Rows[0]["SoLuong"] > 0)
            {
                query = string.Format("SELECT PhanQuyen FROM DANGNHAP WHERE Name = '{0}' AND Pass = '{1}'", name, pass);
                return DataProvider.Instance.ExecuteNonQuery(query);
            }
            return 0;
        }
    }
}
