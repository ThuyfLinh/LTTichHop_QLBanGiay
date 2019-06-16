using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangGiay.GUI
{
    public partial class fHoaDonBan : MetroFramework.Forms.MetroForm
    {
        public fHoaDonBan()
        {
            InitializeComponent();
            Load();
        }

        public string baseAddress = "http://localhost:63920/api/";
        List<HoaDonBan_DTO> listHD = null;
        private void Load()
        {
            listHD = loadHD();
            dgvHoaDonBan.DataSource = listHD;
            AddBinding();
        }
        private List<HoaDonBan_DTO> loadHD()
        {
            List<HoaDonBan_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("NhanVien");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<HoaDonBan_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            return list;
        }
        void AddBinding()
        {
            txtMaHD.Text = dgvHoaDonBan.CurrentRow.Cells["MaHD"].Value.ToString();
            txtTenKH.Text = dgvHoaDonBan.CurrentRow.Cells["TenKH"].Value.ToString();
            txtSDT.Text = dgvHoaDonBan.CurrentRow.Cells["SDT"].Value.ToString();
            txtTenNV.Text = dgvHoaDonBan.CurrentRow.Cells["TenNV"].Value.ToString();
            txtKM.Text = dgvHoaDonBan.CurrentRow.Cells["KM"].Value.ToString();
            dtNgay.Text = dgvHoaDonBan.CurrentRow.Cells["Ngay"].Value.ToString();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int MaHD = Convert.ToInt32(txtMaHD.Text);
            string Hoten = txtTenNV.Text;
            DateTime Ngaysinh = Convert.ToDateTime(dtNgay.Text);
            string SDT = txtSDT.Text;
            string TenKH = txtTenKH.Text;
            string KM = txtKM.Text;

            HoaDonBan_DTO HD = new HoaDonBan_DTO(MaHD, Hoten, TenKH, KM, Ngaysinh, SDT);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<HoaDonBan_DTO>("HoaDon", HD);

                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sửa thành công");
                }

                Load();
            }
        }

        private void btnThemKM_Click(object sender, EventArgs e)
        {
            int MaHD = Convert.ToInt32(txtMaHD.Text);
            string Hoten = txtTenNV.Text;
            DateTime Ngaysinh = Convert.ToDateTime(dtNgay.Text);
            string SDT = txtSDT.Text;
            string TenKH = txtTenKH.Text;
            string KM = txtKM.Text;

            HoaDonBan_DTO HD = new HoaDonBan_DTO(MaHD, Hoten, TenKH, KM, Ngaysinh, SDT);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<HoaDonBan_DTO>("HoaDonBan", HD);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm thành công");
                }

                Load();
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string TenKH = txtTimKiem.Text;
            List<HoaDonBan_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET  api/NhanVien?TenNV={TenNV}
                var responseTask = client.GetAsync($"KhachHang?TenKH={TenKH}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<HoaDonBan_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
        }
    }
}
