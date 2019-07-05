using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class frmDocGia: Form
    {
        public frmDocGia()
        {
            InitializeComponent();
            txtHoten.Focus();
        }
        DocGia dg = new DocGia();

        void HienThiDocGia()
        {
            DataTable dt = dg.LayDSDocGia();
            dgvDocGia.DataSource = dt;
        }

        void setButton(bool value)
        {
            btnLuu.Enabled = !value;
            btnHuy.Enabled = !value;
            btnThem.Enabled = value;
            btnXoa.Enabled = value;
            btnSua.Enabled = value;
            btnThoat.Enabled = value;
        }

        private void FrmDocGia_Load(object sender, EventArgs e)
        {
            setButton(true);
            HienThiDocGia();
        }

        private void DgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if ( index >= 0)
            {
                txtHoten.Text = dgvDocGia.Rows[index].Cells["HoTenDocGia"].Value.ToString();
                txtDiaChi.Text = dgvDocGia.Rows[index].Cells["DiaChi"].Value.ToString();
                txtEmail.Text = dgvDocGia.Rows[index].Cells["Email"].Value.ToString();
                txtTienNo.Text = dgvDocGia.Rows[index].Cells["TienNo"].Value.ToString();
                dtpNgaySinh.Text = dgvDocGia.Rows[index].Cells["NgaySinh"].Value.ToString();
                dtpNgayLap.Text = dgvDocGia.Rows[index].Cells["NgayLapThe"].Value.ToString();
                dtpNgayHet.Text = dgvDocGia.Rows[index].Cells["NgayHetHan"].Value.ToString();
            }
        }

        void setNull()
        {
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtHoten.Text = "";
            txtTienNo.Text = "0";
        }

        private void TxtInputOnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        public bool CheckData()
        {
            if (string.IsNullOrEmpty(txtHoten.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoten.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Bạn chưa nhập email !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            
            if(!new EmailAddressAttribute().IsValid(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTienNo.Text))
            {
                MessageBox.Show("Bạn chưa nhập tiền nợ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTienNo.Focus();
                return false;
            }

            return true;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            bool flag = dgvDocGia.CurrentRow.Selected;
            if(flag)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa dòng này", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    dg.XoaDocGia(dgvDocGia.CurrentRow.Cells[0].Value.ToString());
                    dgvDocGia.Rows.RemoveAt(this.dgvDocGia.CurrentRow.Index);
                    setNull();
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn dòng cần xóa!", "Thông báo");
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        public bool themmoi;
        private void BtnThem_Click(object sender, EventArgs e)
        {
            bool flag = dgvDocGia.CurrentRow.Selected;
            if (flag)
                setNull();
            else
            {
                if (CheckData())
                {
                    themmoi = true;
                    setButton(false);
                    txtHoten.Focus();
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            bool flag = dgvDocGia.CurrentRow.Selected;
            if (flag)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn cập nhật dòng này!", "Thông báo báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    themmoi = false;
                    setButton(false);
                }
            }
            else
                MessageBox.Show("Bạn phải lựa chọn mẩu tin cần cập nhật!", "Lưu ý!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string ngaysinh = string.Format("{0:MM/dd/yyyy}", dtpNgaySinh.Value);
            string ngaylap = string.Format("{0:MM/dd/yyyy}", dtpNgayLap.Value);
            string ngayhet = string.Format("{0:MM/dd/yyyy}", dtpNgayHet.Value);
            if (themmoi)
            {
                dg.ThemDocGia(txtHoten.Text, ngaysinh, txtDiaChi.Text, txtEmail.Text, ngaylap, ngayhet, float.Parse(txtTienNo.Text));
                MessageBox.Show("Thêm mới thành công!");
            } 
            else
            {
                dg.CapNhatDocGia(dgvDocGia.SelectedRows[0].Cells[0].Value.ToString(), txtHoten.Text, ngaysinh, txtDiaChi.Text, txtEmail.Text, ngaylap, ngayhet, float.Parse(txtTienNo.Text));
                MessageBox.Show("Cập nhật thành công!");
            }
            HienThiDocGia();
            setNull();
            setButton(true);
        }
    }
}
