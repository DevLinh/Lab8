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
    public partial class frmSach : Form
    {
        public frmSach()
        {
            InitializeComponent();
        }

        public bool themmoi = false;
        Sach s = new Sach();

        void HienThiSach()
        {
            DataTable dt = s.LayDSSach();
            dgvSach.DataSource = dt;
        }

        void setNull()
        {
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtNamXB.Text = "";
            txtNhaXB.Text = "";
            txtTacGia.Text = "";
        }

        void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnXoa.Enabled = val;
            btnSua.Enabled = val;
            btnThoat.Enabled = val;
            btnXoa.Enabled = val;
            btnLuu.Enabled = !val;
            btnHuy.Enabled = !val;
        }

        private void FrmSach_Load(object sender, EventArgs e)
        {
            setNull();
            setButton(true);
            HienThiSach();
        }

        private void DgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                txtTacGia.Text = dgvSach.Rows[index].Cells["TacGia"].Value.ToString();
                txtTenSach.Text = dgvSach.Rows[index].Cells["TenSach"].Value.ToString();
                txtNamXB.Text = dgvSach.Rows[index].Cells["NamXuatBan"].Value.ToString();
                txtTriGia.Text = dgvSach.Rows[index].Cells["TriGia"].Value.ToString();
                txtNhaXB.Text = dgvSach.Rows[index].Cells["NhaXuatBan"].Value.ToString();
                dateTimePicker.Text = dgvSach.Rows[index].Cells["NgayNhap"].Value.ToString();
            }
        }

        public bool CheckData()
        {
            if (string.IsNullOrEmpty(txtTenSach.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSach.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTacGia.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên tác giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTacGia.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNamXB.Text))
            {
                MessageBox.Show("Bạn chưa nhập năm xuất bản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNamXB.Focus();
                return false;
            }
            int parsedIntValue;
            if (!int.TryParse(txtNamXB.Text, out parsedIntValue))
            {
                MessageBox.Show("Chỉ nhập giá trị số năm vào đây!");
                txtNamXB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(dateTimePicker.Text))
            {
                MessageBox.Show("Bạn chưa nhập ngày nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNhaXB.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhà xuất bản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNhaXB.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTriGia.Text))
            {
                MessageBox.Show("Bạn chưa nhập trị giá sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTriGia.Focus();
                return false;
            }

            float parsedFloatValue;
            if(!float.TryParse(txtTriGia.Text,out parsedFloatValue))
            {
                MessageBox.Show("Chỉ nhập giá trị số vào đây!");
                txtTriGia.Focus();
                return false;
            }
            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if(CheckData())
            {
                themmoi = true;
                setButton(false);
                txtTenSach.Focus();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            bool flag = dgvSach.CurrentRow.Selected;
            if (flag && CheckData())
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

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            int flag = dgvSach.SelectedRows.Count;
            if (flag > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa dòng này!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    s.XoaSach(dgvSach.CurrentRow.Cells[0].Value.ToString());
                    dgvSach.Rows.RemoveAt(this.dgvSach.SelectedRows[0].Index);
                    setNull();
                }
            }
            else
                MessageBox.Show("Bạn phải chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK);
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string ngay = dateTimePicker.Value.ToString("MM/dd/yyyy");
            if(themmoi)
            {
                s.ThemSach(txtTenSach.Text, txtTacGia.Text, int.Parse(txtNamXB.Text), txtNhaXB.Text, float.Parse(txtTriGia.Text), ngay);
                MessageBox.Show("Thêm mới thành công!");
            }
            else
            {
                s.CapNhatSach(dgvSach.SelectedRows[0].Cells[0].Value.ToString(), txtTenSach.Text, txtTacGia.Text, int.Parse(txtNamXB.Text), txtNhaXB.Text, float.Parse(txtTriGia.Text), ngay);
                MessageBox.Show("Cập nhật thành công!");
            }
            HienThiSach();
            setNull();
            setButton(true);
        }
    }
}
