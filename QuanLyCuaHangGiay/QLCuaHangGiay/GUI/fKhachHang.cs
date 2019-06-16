using System;
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
    public partial class fKhachHang : MetroFramework.Forms.MetroForm
    {
        public fKhachHang()
        {
            InitializeComponent();
            Load();

        }
        public string baseAddress = "http://localhost:63920/api/";
        List<KhachHang_DTO> listKH = null;

        private void Load()
        {
            listKH = loadKH();
            dgvKhachHang.DataSource = listKH;
            AddBinding();
        }
        void AddBinding()
        {
            txtMaKH.Text = dgvKhachHang.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells["TenKH"].Value.ToString();
            txtSDT.Text = dgvKhachHang.CurrentRow.Cells["SDT"].Value.ToString();
        }
        private List<KhachHang_DTO> loadKH()
        {
            List<KhachHang_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("KhachHang");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhachHang_DTO>>();
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

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int MaKH = Convert.ToInt32(txtMaKH.Text);
            string Hoten = txtTenKH.Text;

            int SDT = Convert.ToInt32(txtSDT.Text);

            KhachHang_DTO KH = new KhachHang_DTO(MaKH, Hoten, SDT);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<KhachHang_DTO>("KhachHang", KH);

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
            int MaKH = Convert.ToInt32(txtMaKH.Text);
            string HoTen = txtTenKH.Text;
            int SDT = Convert.ToInt32(txtSDT.Text);

            KhachHang_DTO KH = new KhachHang_DTO(MaKH, HoTen, SDT);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<KhachHang_DTO>("KhachHang", KH);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm thành công");
                }

                Load();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Nếu xóa khách hàng sẽ ảnh hưởng đến thông tin hóa đơn .Bạn có thật sự muốn xóa khách hàng có mã là: " + txtMaKH.Text, "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                int id = Convert.ToInt32(txtMaKH.Text);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);

                    var url = baseAddress + "KhachHang/" + id;
                    //HTTP PUT
                    var postTask = client.DeleteAsync(url);

                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Xóa khách hàng không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Load();
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string TenKH = txtTimKiem.Text;
            List<KhachHang_DTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET  api/NhanVien?TenNV={TenNV}
                var responseTask = client.GetAsync($"KhachHang?TenKH={TenKH}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhachHang_DTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listKH = list;
            dgvKhachHang.DataSource = listKH;
        }
    }
}
