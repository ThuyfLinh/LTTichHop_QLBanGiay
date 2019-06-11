using QLCuaHangGiay_Data.DAO;
using QLCuaHangGiay_Data.DTO;
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
    public partial class fKhuyenMai : MetroFramework.Forms.MetroForm
    {
        public fKhuyenMai()
        {
            InitializeComponent();
            Load();
        }
        public string baseAddress = "http://localhost:63920/api/";

        private void Load()
        {

            using (var client = new HttpClient())
            {
                HttpClient cli = new HttpClient();
                cli.BaseAddress = new Uri(baseAddress);
                HttpResponseMessage response = cli.GetAsync("KhuyenMai").Result;
                var emp = response.Content.ReadAsAsync<DataTable>().Result;
                dgvKhuyenMai.DataSource = emp;
                //HTTP GET
            }
            AddBinding();
        }
        
        
        void AddBinding()
        {
            txtIDKM.Text = dgvKhuyenMai.CurrentRow.Cells["IDKhuyenMai"].Value.ToString();
            txtTenCT.Text = dgvKhuyenMai.CurrentRow.Cells["TenCT"].Value.ToString();
            txtMoTa.Text = dgvKhuyenMai.CurrentRow.Cells["MoTa"].Value.ToString();
            txtChietKhau.Text = dgvKhuyenMai.CurrentRow.Cells["ChietKhau"].Value.ToString();
            dtpDenNgay.Text = dgvKhuyenMai.CurrentRow.Cells["NgayKT"].Value.ToString();
            dtpTuNgay.Text = dgvKhuyenMai.CurrentRow.Cells["NgayBD"].Value.ToString();
        }
       
       
        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void dgvKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDKM.Text = dgvKhuyenMai.CurrentRow.Cells["IDKhuyenMai"].Value.ToString();
            txtTenCT.Text = dgvKhuyenMai.CurrentRow.Cells["TenCT"].Value.ToString();
            txtMoTa.Text = dgvKhuyenMai.CurrentRow.Cells["MoTa"].Value.ToString();
            txtChietKhau.Text = dgvKhuyenMai.CurrentRow.Cells["ChietKhau"].Value.ToString();
            dtpDenNgay.Text = dgvKhuyenMai.CurrentRow.Cells["NgayKT"].Value.ToString();
            dtpTuNgay.Text = dgvKhuyenMai.CurrentRow.Cells["NgayBD"].Value.ToString();
        }

        private void btnThemKM_Click(object sender, EventArgs e)
        {
            
            string tenct = txtTenCT.Text;
            string mota = txtMoTa.Text;
            float chietkhau = float.Parse(txtChietKhau.Text);
            DateTime ngaybd = Convert.ToDateTime(dtpTuNgay.Text);
            DateTime ngaykt = Convert.ToDateTime(dtpDenNgay.Text);

            var KM = new KhuyenMaiDTO(tenct, mota, ngaybd, ngaykt, chietkhau);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<KhuyenMaiDTO>("KhuyenMai", KM);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Test");
                }

                Load();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDKM.Text);
            string tenct = txtTenCT.Text;
            string mota = txtMoTa.Text;
            float chietkhau = float.Parse(txtChietKhau.Text);
            DateTime ngaybd = Convert.ToDateTime(dtpTuNgay.Text);
            DateTime ngaykt = Convert.ToDateTime(dtpDenNgay.Text);

            KhuyenMaiDTO KM = new KhuyenMaiDTO(id, tenct, mota, ngaybd, ngaykt, chietkhau);

           
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri(baseAddress);

                var putTask = client.PutAsJsonAsync<KhuyenMaiDTO>("KhuyenMai", KM);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<KhuyenMaiDTO>();
                    readTask.Wait();

                }
                else
                {
                    Load();
                }
                
            }

        }
    }
}
