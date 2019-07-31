using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class frmPhieuThuTien : Form
    {
        PhieuThuTien pt = new PhieuThuTien();
        public frmPhieuThuTien()
        {
            InitializeComponent();
        }

        private void FrmPhieuThuTien_Load(object sender, EventArgs e)
        {
            HienThiMaNhanVien();
            HienThiPhieuThu();
            setNull();
            setButton(true);
            setTextBox(false);
            txtMaDocGia.Enabled = false;
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

        void setTextBox(bool val)
        {
            txtSoTienThu.Enabled = val;
            cboMaNVThu.Enabled = val;
            txtSoTienNo.Enabled = val;
        }

        void HienThiPhieuThu()
        {
            DataTable dt = pt.LayDSPhieuThu();
            dgvPhieuThu.DataSource = dt;
        }
        void HienThiMaNhanVien()
        {
            DataTable dt = pt.LayDSNhanVien();
            cboMaNVThu.DataSource = dt;
            cboMaNVThu.DisplayMember = "MaNhanVien";
            cboMaNVThu.ValueMember = "MaNhanVien";
        }

        void HienThiTienNo()
        {
             if (CheckID())
            {
                txtSoTienNo.Text = pt.LayTienNo(int.Parse(txtMaDocGia.Text)).ToString();
                setTextBox(true);
            }
        }
        void setNull()
        {
            txtMaDocGia.Text = "";
            txtSoTienNo.Text = "0";
            txtSoTienThu.Text = "0";
            cboMaNVThu.Text = "";
        }

        public bool CheckID()
        {
            if (string.IsNullOrEmpty(txtMaDocGia.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã đọc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDocGia.Focus();
                return false;
            }
            
            if (pt.CheckPhieuThu(int.Parse(txtMaDocGia.Text), false) == false)
            {
                MessageBox.Show("Không tìm thấy mã đọc giả trên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDocGia.Focus();
                txtMaDocGia.Text = "";
                return false;
            }
            if (pt.CheckPhieuThu(int.Parse(txtMaDocGia.Text), true) == false)
            {
                MessageBox.Show("Đọc giả trên không có tiền nợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDocGia.Focus();
                txtMaDocGia.Text = "";
                return false;
            }

            return true;
        }

        public bool CheckData()
        {
            if (float.Parse(txtSoTienThu.Text.ToString()) == 0 || string.IsNullOrEmpty(txtSoTienThu.Text))
            {
                MessageBox.Show("Bạn chưa nhập số tiền thu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoTienThu.Focus();
                txtSoTienThu.Text = "0";
                return false;
            }
            if (string.IsNullOrEmpty(cboMaNVThu.Text))
            {
                MessageBox.Show("Bạn chưa chọn mã nhân viên thu tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaNVThu.Focus();
                return false;
            }
            if (float.Parse(txtSoTienThu.Text.ToString()) > float.Parse(txtSoTienNo.Text.ToString()))
            {
                MessageBox.Show("Số tiền trả vượt quá số tiền nợ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoTienThu.Focus();
                txtSoTienThu.Text = "0";
                return false;
            }
            return true;
        }

        

        private void TxtSoTienThu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        private void DgvPhieuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtMaDocGia.Text = dgvPhieuThu.Rows[index].Cells["MaDocGia"].Value.ToString();
                txtSoTienNo.Text = dgvPhieuThu.Rows[index].Cells["SoTienNo"].Value.ToString();
                txtSoTienThu.Text = dgvPhieuThu.Rows[index].Cells["SoTienThu"].Value.ToString();
                cboMaNVThu.Text = dgvPhieuThu.Rows[index].Cells["MaNhanVien"].Value.ToString();
            }
        }

        public bool themmoi;

        private void BtnThem_Click(object sender, EventArgs e)
        {
            txtMaDocGia.Enabled = true;
            if (CheckData())
            {
                themmoi = true;
                setButton(false);
                txtMaDocGia.Focus();
            }
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (themmoi)
            {
                pt.ThemPhieuThu(float.Parse(txtSoTienNo.Text), float.Parse(txtSoTienThu.Text), int.Parse(txtMaDocGia.Text), cboMaNVThu.SelectedValue.ToString());
                MessageBox.Show("Thêm mới thành công!");
            }

            HienThiPhieuThu();
            setNull();
            setButton(true);
        }

        private void TxtMaDocGia_Leave(object sender, EventArgs e)
        {
            HienThiTienNo();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            bool flag = dgvPhieuThu.CurrentRow.Selected;
            if (flag && CheckData())
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn cập nhật dòng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    themmoi = false;
                    setButton(false);
                }
            }
            else
            {
                MessageBox.Show("Bạn phải lựa chọn mẩu tin cần cập nhật!", "Lưu ý!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TxtSoTienThu_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoTienNo.Text))
            {
                if (float.Parse(txtSoTienThu.Text.ToString()) > float.Parse(txtSoTienNo.Text.ToString()))
                {
                    MessageBox.Show("Số tiền trả vượt quá số tiền nợ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoTienThu.Focus();
                    txtSoTienThu.Text = "0";
                }
            }
        }

        private void TxtMaDocGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        private void TxtMaDocGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HienThiTienNo();
            }
        }
    }
}
