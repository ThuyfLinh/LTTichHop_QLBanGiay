using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangGiay_Data.DTO
{
    public class DangNhapDTO
    {
        private int idnv;
        private string name;
        private string pass;
        private int phanquyen;
     
        public int IDNV { get => idnv; set => idnv = value; }
        public string Name { get => name; set => name = value; }
        public string Pass { get => pass; set => pass = value; }
        public int PhanQuyen { get => phanquyen; set => phanquyen = value; }

        public DangNhapDTO()
        {

        }

        public DangNhapDTO(string Name, string Pass)
        {
            this.name = Name;
            this.pass = Pass;
        }

        public DangNhapDTO(DataRow row)
        {
            this.name = row["Name"].ToString();
            this.pass = row["Pass"].ToString();
        }
    }
}
