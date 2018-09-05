using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using LMSDAL.Entity.Master.User;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using LMSDAL.DAL.Master.User;
using System.Text.RegularExpressions;

namespace LMS
{
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        private List<userEntity> user = new List<userEntity>();
        public UserForm()
        {
            InitializeComponent();

            ddlSearch.Items.Insert(0, "All");
            ddlSearch.SelectedIndex = 0;
            bindUser();
            bindRole();
            grdUser.Columns["uid"].Visible = false;
            grdUser.Columns["password"].Visible = false;
            TxtPassword.PasswordChar = '\u25CF';
        }
        public void clear()
        {
            txtUserId.Text = string.Empty;
            txtUserName.Text = string.Empty;
            TxtPassword.Text = string.Empty;
            ddlRoleId.SelectedIndex = 0;
        }
        public void bindRole()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("usp_role_get", con);
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row[0] = 0;
            row[1] = "Please select";
            dt.Rows.InsertAt(row, 0);
            ddlRoleId.DisplayMember = "RoleName";
            ddlRoleId.ValueMember = "id";
            ddlRoleId.DataSource = dt;
        }
        
        public void bindUser()
        {
            userDal studenDal = new userDal();
            user = studenDal.getUser();
            grdUser.DataSource = user;
        }
        private int ToInt32(object text)
        {

            try
            {
                return Convert.ToInt32(text);
            }
            catch (Exception ex)
            {
                lblid.Text = "0";
                return 0;
            }

        }
        
        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text == "" || txtUserName.Text == "" || TxtPassword.Text == "" || ddlRoleId.SelectedIndex == 0)
            {
                MessageBox.Show("Please Fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Focus();
                return;
            }
            if (!Regex.Match(txtUserId.Text, "^[0-9]*$").Success)
            {
                MessageBox.Show("Invalid userid!! Please enter correct id", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Focus();
                return;
            }

            if (!Regex.Match(txtUserName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid name!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            
            userDal userDal = new userDal();
            List<userEntity> user = userDal.getUser();
            if (user.Where(c => c.Userid == txtUserId.Text).Count() > 0)
            {
                MessageBox.Show("Duplicate UserId\n please enter unique userid", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Focus();
                return;
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_user_insert_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", ToInt32(lblid.Text));
                    cmd.Parameters.AddWithValue("@userid", txtUserId.Text);
                    cmd.Parameters.AddWithValue("@name", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@password", TxtPassword.Text);
                    cmd.Parameters.AddWithValue("@roleid", ddlRoleId.SelectedValue);
                    cmd.ExecuteNonQuery();
                    // con.Close();
                    MessageBox.Show("details saved!!");
                    clear();
                    bindUser();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close your Application?", "My Application",
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
              if (ddlSearch.SelectedIndex == 1)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("usp_user_get  '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);

                    grdUser.DataSource = dt;
                }
                else
                {
                    bindUser();
                }
            }
           else if (ddlSearch.SelectedIndex == 2)
            {
                if (Regex.IsMatch(txtSearch.Text, @"^[a-zA-Z\b]+$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from users where name like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdUser.DataSource = dt;
                }
                else
                {
                    bindUser();
                }
            }
            else if (ddlSearch.SelectedIndex == 3)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from users where roleid like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdUser.DataSource = dt;
                }
            }
    }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            btnSaveAndNew_Click(sender, e);
            btnClose_Click(sender,e);
        }

        private void grdUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clear();
            int id = ToInt32(grdUser.CurrentRow.Cells["uid"].Value);
            userEntity useer = user.Where(x => x.uid == id).FirstOrDefault();
            txtUserId.Text = useer.uid.ToString();
            txtUserId.Enabled = false;
            lblid.Text = useer.Userid.ToString();
            txtUserName.Text = useer.Username;
            TxtPassword.Text = useer.Password;
            // ddlRoleId.ResetText();
            ddlRoleId.SelectedIndex = useer.Roleid;
        }
    }
}
