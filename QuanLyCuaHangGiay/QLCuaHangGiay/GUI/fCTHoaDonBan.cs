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
    public partial class fCTHoaDonBan : MetroFramework.Forms.MetroForm
    {
        public fCTHoaDonBan()
        {
            InitializeComponent();
        }
        public string baseAddress = "http://localhost:63920/api/";
        List<HoaDonBan_DTO> listGiay = null;
        private void Load()
        {
            listGiay = LoadCTHD();
            dgvCTHD.DataSource = listGiay;
            AddBinding();
        }
        void AddBinding()
        {
            txtMaHD.Text = dgvCTHD.CurrentRow.Cells["MaHD"].Value.ToString();
            txtMaGiay.Text = dgvCTHD.CurrentRow.Cells["MaGiay"].Value.ToString();
            txtSoLuong.Text = dgvCTHD.CurrentRow.Cells["SoLuong"].Value.ToString();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            //temCTHD = dgv.CurrentRow.Cells["MaGiay"].Value.ToString();
            //if (tempCTKM == null)
            //{
            //    MessageBox.Show("Chọn ít nhất 1 chi tiết khuyến mãi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết khuyến mãi là: " + tempCTKM, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (confirm == DialogResult.Yes)
            //    {
            //        listCTKhuyenMai.RemoveAt(tempIndex);
            //        listCT.RemoveAt(tempIndex);
            //        tempIndex = 0;
            //        tempCTKM = null;
            //        dgvDSKM.DataSource = listCTKhuyenMai;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
        }

        private void btnThemCTKM_Click(object sender, EventArgs e)
        {
            int MaHD = Convert.ToInt32(txtMaHD.Text);
            string MaGiay = txtMaGiay.Text;
            string SoLuong = txtSoLuong.Text;



            CTHoaDonBan_DTO CTHD = new CTHoaDonBan_DTO(MaHD, MaGiay,SoLuong);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<CTHoaDonBan_DTO>("CTHoaDonBan", CTHD);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm thành công");
                }

                Load();
            }
        }
    }
}
