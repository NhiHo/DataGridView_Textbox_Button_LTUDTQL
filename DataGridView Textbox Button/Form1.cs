using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataGridView_Textbox_Button
{
    public partial class Form1 : Form
    {
        string SQL;
        SqlConnection cn;
        SqlDataAdapter da;
        string connsstring = @"Data Source=NHIHO-PC;Initial Catalog=K48;Integrated Security=True";
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadDL()
        {
            cn = new SqlConnection(connsstring);
            SQL = "SELECT * FROM Khoa";
            da = new SqlDataAdapter(SQL, cn);
            ds = new DataSet("Khoa");
            da.Fill(ds, "Khoa");
            dgvKhoa.DataSource = ds;
            dgvKhoa.DataMember = "Khoa";
            cn.Close();
            cn.Dispose();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDL();
            
        }

        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = new DataGridViewRow();
                row = dgvKhoa.Rows[e.RowIndex];
                txtMaKhoa.Text = row.Cells[0].Value.ToString();
                txtTenKhoa.Text = row.Cells[1].Value.ToString();
                txtSDT.Text = row.Cells[2].Value.ToString();
                txtMaKhoa.Enabled = false;
                btnLuu.Enabled = false;
            }
            catch (Exception)
            { 

            }
        }


        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Clear();
            txtMaKhoa.Focus();
            txtTenKhoa.Clear();
            txtSDT.Clear();
            txtMaKhoa.Enabled = true;
            btnCapNhat.Enabled = true;
            btnLuu.Enabled = true;
            btnXoa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SQL = "INSERT INTO Khoa VALUES (N'"+txtMaKhoa.Text.ToString()+"',N'"+txtTenKhoa.Text.ToString()+"',N'"+txtSDT.Text.ToString()+"')";
            cn = new SqlConnection(connsstring);
            SqlCommand cmd = new SqlCommand(SQL, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            LoadDL();
            cn.Close();
            cn.Dispose();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                SQL = "UPDATE Khoa SET TenKhoa = N'" + txtTenKhoa.Text.ToString() + ", SDT = N'" + txtSDT.Text.ToString() + "' WHERE MaKhoa = N'" + txtMaKhoa.Text.ToString() + "')";
                cn = new SqlConnection(connsstring);
                SqlCommand cmd = new SqlCommand(SQL, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                LoadDL();
                cn.Close();
                cn.Dispose();
            }
            catch
            {
                MessageBox.Show("Có lỗi","Chú ý",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult k = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (k == DialogResult.OK)
            {
                SQL = "DELETE FROM Khoa WHERE MaKhoa = N'" + txtTenKhoa.Text + "'";
                cn = new SqlConnection(connsstring);
                SqlCommand cmd = new SqlCommand(SQL, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                LoadDL();
                cn.Close();
                cn.Dispose();
            }
        }


        


    }
}
