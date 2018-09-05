using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;
using LMSDAL.DAL.Master.Guest;
using LMSDAL.Entity.Master.Guest;
using System.Text.RegularExpressions;

namespace LMS
{
    public partial class guestForm : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        private List<GuestEntity> guest = new List<GuestEntity>();
        public guestForm()
        {
            InitializeComponent();
            ddlBloodGroup.Items.Insert(0, "<--Please select-->");
            ddlBloodGroup.SelectedIndex = 0;
            ddlDept.Items.Insert(0, "<--Please select-->");
            ddlDept.SelectedIndex = 0;
            ddlSearch.Items.Insert(0, "All");
            ddlSearch.SelectedIndex = 0;
            bindGuest();
        }
        public void bindGuest()
        {
            guestDal guestDal = new guestDal();
            guest = guestDal.GetGuests();
            grdGuest.DataSource = guest;
        }
        public void clear()
        {
            txtGuestName.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            ddlDept.SelectedItem = null;
            txtFathersName.Text = string.Empty;
            dtpDob.Value = DateTime.UtcNow;
            ddlBloodGroup.SelectedItem = null;
            txtQualification.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtPeresentAdddress.Text = string.Empty;
            txtPermanentAddress.Text = string.Empty;
            txtAadharNumber.Text = string.Empty;
            txtYearOfAppointment.Text = string.Empty;
            pictureBox.Image = null;
            txtGuestName.Focus();
            
        }
        private object ToInt32(string text)
        {

            try
            {
                return Convert.ToInt32(text);
            }
            catch (Exception ex)
            {
                lblId.Text = "0";
                return 0;
            }

        }
        private void savenew_Click(object sender, EventArgs e)
        {
            if(txtGuestName.Text==""||txtDesignation.Text==""||txtFathersName.Text==""||txtContactNo.Text==""||
                txtQualification.Text==""||txtPeresentAdddress.Text==""||txtPermanentAddress.Text==""||txtAadharNumber.Text==""
                ||txtYearOfAppointment.Text==""||ddlBloodGroup.SelectedItem==null||ddlDept.SelectedItem==null
                ||pictureBox.Image==null)
            {
                MessageBox.Show(" Plese fill all the details!!\n all fields are mendatory to fill!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGuestName.Focus();
                return;
            }
            if (!Regex.Match(txtGuestName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid name!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGuestName.Focus();
                return;
            }
            if (!Regex.Match(txtDesignation.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid designation!! Please enter alphabets only like Hr,Developer etc", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDesignation.Focus();
                return;
            }
            if (!Regex.Match(txtFathersName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid fathername!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFathersName.Focus();
                return;
            }
            if (!Regex.Match(txtQualification.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid qualification!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQualification.Focus();
                return;
            }
            if (!Regex.Match(txtPeresentAdddress.Text, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$").Success)
            {
                MessageBox.Show("Invalid PresentAddress!! Please enter house no too", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPeresentAdddress.Focus();
                return;
            }
            if (!Regex.Match(txtPermanentAddress.Text, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$").Success)
            {
                MessageBox.Show("Invalid permanentAddress!! Please enter house no too", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPermanentAddress.Focus();
                return;
            }
            if (!Regex.Match(txtContactNo.Text, @"^\d{10}$").Success)
            {
                MessageBox.Show("Invalid Contact no!! Please enter 10 digit contact No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Focus();
                return;
            }
            if (!Regex.Match(txtAadharNumber.Text, @"^\d{12}$").Success)
            {
                MessageBox.Show("Invalid Aadhar no!! Please enter 12 digit aadhar No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAadharNumber.Focus();
                return;
            }
            if (!Regex.Match(txtYearOfAppointment.Text, @"^\d{4}$").Success)
            {
                MessageBox.Show("Invalid year enter numeric year like 1998!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtYearOfAppointment.Focus();
                return;
            }
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Image not Selected,please Select!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnBrowse.Focus();
                return;
            }
            else
            {

                con.Open();
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    SqlCommand cmd = new SqlCommand("usp_guest_insert_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gid", ToInt32(lblId.Text));
                    cmd.Parameters.AddWithValue("@guestName", txtGuestName.Text);
                    cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@department", ddlDept.SelectedItem);
                    cmd.Parameters.AddWithValue("@fatherName", txtFathersName.Text);
                    cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dtpDob.Value));
                    cmd.Parameters.AddWithValue("@bloodGroup", ddlBloodGroup.SelectedItem);
                    cmd.Parameters.AddWithValue("@contactNo", txtContactNo.Text);
                    cmd.Parameters.AddWithValue("@qualification", txtQualification.Text);
                    cmd.Parameters.AddWithValue("@Presentaddress", txtPeresentAdddress.Text);
                    cmd.Parameters.AddWithValue("@Permanentaddress", txtPermanentAddress.Text);
                    cmd.Parameters.AddWithValue("@aadharNo", txtAadharNumber.Text);
                    cmd.Parameters.AddWithValue("@appointYear", Convert.ToInt32(txtYearOfAppointment.Text));
                    cmd.Parameters.AddWithValue("@photo", photo_aray);
                    cmd.ExecuteNonQuery();
                    if (lblId.Text == "0")
                        MessageBox.Show("Details saved Successfull!!");
                    else
                        MessageBox.Show("Details Updated Successfully!!");
                    clear();
                    bindGuest();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
            DialogResult res = opfd.ShowDialog();
            if (res == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(opfd.FileName);
            }
        }

        private void grdGuest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //clear();
            int id = Convert.ToInt32(grdGuest.CurrentRow.Cells["gid"].Value);
            GuestEntity gueset = guest.Where(x => x.gid == id).FirstOrDefault();
            lblId.Text = gueset.gid.ToString();
            txtGuestName.Text = gueset.guestname;
            txtDesignation.Text = gueset.designation;
            ddlDept.ResetText();
            ddlDept.SelectedItem = gueset.department;
            txtFathersName.Text = gueset.fatherName;
            dtpDob.Value = gueset.dob.Date;
            ddlBloodGroup.ResetText();
            ddlBloodGroup.SelectedItem = gueset.bloodGroup;
            txtContactNo.Text = gueset.contactNo;
            txtQualification.Text = gueset.qualification;
            txtPeresentAdddress.Text = gueset.presentaddress;
            txtPermanentAddress.Text = gueset.permanentaddress;
            txtAadharNumber.Text = gueset.aadharNo;
            txtYearOfAppointment.Text = gueset.appointYear.ToString();
            MemoryStream ms = new MemoryStream((byte[])gueset.Photo);
            pictureBox.Image = new Bitmap(ms);
        }

        private void close_Click(object sender, EventArgs e)
        {
            // System.Windows.Forms.Application.ExitThread();
            if (MessageBox.Show("Do you want to close your Application?", "My Application",
         MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
            
        }

        private void saveclose_Click(object sender, EventArgs e)
        {
            savenew_Click(sender, e);
            close_Click(sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           DataTable dt = new DataTable();
            
            if (ddlSearch.SelectedIndex == 1)
            {
                if (Regex.IsMatch(txtSearch.Text, @"^[a-zA-Z\b]+$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select gid, guestname,designation,dob,qualification,presentaddress,photo from guest where guestname like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdGuest.DataSource = dt;
                }
                else
                {
                    bindGuest();
                }
            }
            else if (ddlSearch.SelectedIndex == 2)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select gid, guestname,fathername,dob,department,aadharno,presentaddress,photo from guest where gid like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdGuest.DataSource = dt;
                }
                else
                {
                    bindGuest();
                }
            }
            else if (ddlSearch.SelectedIndex == 3)
            {
                if (Regex.IsMatch(txtSearch.Text, @"^[a-zA-Z\b]+$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select gid,department, guestname,designation,aadharno,presentaddress,photo from guest where department like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdGuest.DataSource = dt;
                }
                else
                {
                    bindGuest();
                }
            }
        }
    }
}
