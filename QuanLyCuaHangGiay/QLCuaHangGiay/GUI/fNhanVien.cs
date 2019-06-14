﻿using QLCuaHangGiay_Data.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangGiay.GUI
{
    public partial class fNhanVien : MetroFramework.Forms.MetroForm 
    {
        public fNhanVien()
        {
            InitializeComponent();
            Load();
        }

        public string baseAddress = "http://localhost:63920/api/";
        List<NhanVienDTO> listNV = null;

        private void Load()
        {
            listNV = loadNV();
            dgvNhanVien.DataSource = listNV;
            AddBinding();
        }
        void AddBinding()
        {
            txtIDNV.Text = dgvNhanVien.CurrentRow.Cells["IDNV"].Value.ToString();
            txtHoTen.Text = dgvNhanVien.CurrentRow.Cells["TenNV"].Value.ToString();
            dtpNSNV.Text = dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();

        }
        private List<NhanVienDTO> loadNV()
        {
            List<NhanVienDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //HTTP GET
                var responseTask = client.GetAsync("NhanVien");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<NhanVienDTO>>();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            string Hoten = txtHoTen.Text;
            DateTime Ngaysinh = Convert.ToDateTime(dtpNSNV.Text);
            string Diachi = txtDiaChi.Text;

            NhanVienDTO NV = new NhanVienDTO(Hoten, Ngaysinh, Diachi);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<NhanVienDTO>("NhanVien", NV);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm thành công");
                }

                Load();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtIDNV.Text = "";
            txtHoTen.Text = "";
            dtpNSNV.Text = "";
            txtDiaChi.Text = "";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int MaNV = Convert.ToInt32(txtIDNV.Text);
            string Hoten = txtHoTen.Text;
            DateTime Ngaysinh = Convert.ToDateTime(dtpNSNV.Text);
            string Diachi = txtDiaChi.Text;

            NhanVienDTO NV = new NhanVienDTO(MaNV, Hoten, Ngaysinh, Diachi);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<NhanVienDTO>("NhanVien", NV);

                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sửa thành công");
                }

                Load();
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDNV.Text = dgvNhanVien.CurrentRow.Cells["IDNV"].Value.ToString();
            txtHoTen.Text = dgvNhanVien.CurrentRow.Cells["TenNV"].Value.ToString();
            dtpNSNV.Text = dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDNV.Text);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                var url = baseAddress + "NhanVien/" + id;
                //HTTP PUT
                var postTask = client.DeleteAsync(url);

                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa thành công");
                }

                Load();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string TenNV = txtSearch.Text;
            List<NhanVienDTO> list = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                //GET  api/NhanVien?TenNV={TenNV}
                var responseTask = client.GetAsync($"NhanVien?TenNV={TenNV}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<NhanVienDTO>>();
                    readTask.Wait();

                    list = readTask.Result;

                }
                else //web api sent error response 
                {
                    //log response status here..    

                }
            }
            listNV = list;
            dgvNhanVien.DataSource = listNV;
        }
    }
}
