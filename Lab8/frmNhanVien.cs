using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class frmNhanVien : System.Windows.Forms.Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }
        public bool themmoi = false;
        NhanVien nv = new NhanVien();

        void HienThiNhanVien()
        {
            DataTable dt = nv.LayDSNhanVien();
            dgvNhanVien.DataSource = dt;
        }

        void setNull()
        {
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnXoa.Enabled = val;
            btnSua.Enabled = val;
            btnThoat.Enabled = val;
            btnLuu.Enabled = !val;
            btnHuy.Enabled = !val;
        }

        void HienThiBangCap()
        {
            DataTable dt = nv.LayBangCap();
            cboBangCap.DataSource = dt;
            cboBangCap.DisplayMember = "TenBangCap";
            cboBangCap.ValueMember = "MaBangCap";
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            setNull();
            setButton(true);
            HienThiNhanVien();
            HienThiBangCap();
        }

        public bool CheckData()
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("Bạn chưa nhập điện thoại nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(dateTimePicker.Text))
            {
                MessageBox.Show("Bạn chưa chọn ngày sinh nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cboBangCap.Text))
            {
                MessageBox.Show("Bạn chưa chọn bằng cấp nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboBangCap.Focus();
                return false;
            }
            return true;
        }
        
        private void DgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int index = e.RowIndex;
            if(index >= 0)
            {
                
                txtHoTen.Text = dgvNhanVien.Rows[index].Cells["HoTenNhanVien"].Value.ToString();
                txtDiaChi.Text = dgvNhanVien.Rows[index].Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dgvNhanVien.Rows[index].Cells["DienThoai"].Value.ToString();
                cboBangCap.Text = dgvNhanVien.Rows[index].Cells["TenBangCap"].Value.ToString();
                dateTimePicker.Text = dgvNhanVien.Rows[index].Cells["NgaySinh"].Value.ToString();
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                themmoi = true;
                setButton(false);
                txtHoTen.Focus();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            bool flag = dgvNhanVien.CurrentRow.Selected;
            if(flag && CheckData())
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

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đóng!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Dispose();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            int flag = dgvNhanVien.SelectedRows.Count;
            if (flag > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa dòng này!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    nv.XoaNhanVien(dgvNhanVien.CurrentRow.Cells[0].Value.ToString());
                    dgvNhanVien.Rows.RemoveAt(this.dgvNhanVien.SelectedRows[0].Index);
                    setNull();
                }
            }
            else
                MessageBox.Show("Bạn phải chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string ngay = String.Format("{0:MM/dd/yyyy}", dateTimePicker.Value);
            if (themmoi)
            {
                nv.ThemNhanVien(txtHoTen.Text, ngay, txtDiaChi.Text, txtDienThoai.Text, cboBangCap.SelectedValue.ToString());
                MessageBox.Show("Thêm mới thành công!");
            }
            else
            {
                nv.CapNhatNhanVien(dgvNhanVien.SelectedRows[0].Cells[0].Value.ToString(), txtHoTen.Text, ngay, txtDiaChi.Text, txtDienThoai.Text, cboBangCap.SelectedValue.ToString());
                MessageBox.Show("Cập nhật thành công!");
            }
            HienThiNhanVien();
            setNull();
            setButton(true);
        }
    }
}
