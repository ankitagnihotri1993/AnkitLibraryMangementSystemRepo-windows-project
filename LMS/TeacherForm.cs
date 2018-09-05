using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;
using LMSDAL.DAL.Master.Teacher;
using LMSDAL.Entity.Master.Teacher;
using LMSDAL;
using System.Text.RegularExpressions;

namespace LMS
{
    public partial class TeacherForm : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        public int id;
        private List<teacherEntity> teachcer = new List<teacherEntity>();
        public TeacherForm()
        {
            InitializeComponent();
            ddlBloodGroup.Items.Insert(0, "<--Please select-->");
            ddlBloodGroup.SelectedIndex = 0;
            ddlDepartment.Items.Insert(0, "<--Please select-->");
            ddlDepartment.SelectedIndex = 0;
            ddlReligon.Items.Insert(0, "<--Please select-->");
            ddlReligon.SelectedIndex = 0;
            ddlSerach.Items.Insert(0, "All");
            ddlSerach.SelectedIndex = 0;
            rdoProfessor.Focus();
            bindTeacher();
        }
        public void clear()
        {
            txtName.Text = string.Empty;
            txtFatherHusbandName.Text = string.Empty;
            dtpDob.Value = DateTime.UtcNow;
            ddlBloodGroup.SelectedItem = null;
            txtAppointYear.Text = string.Empty;
            ddlDepartment.SelectedItem = null;
            ddlReligon.SelectedItem = null;
            txtSpecialization.Text = string.Empty;
            txtQualification.Text = string.Empty;
            txtPresentAddress.Text = string.Empty;
            txtPermanentAddress.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtPanNo.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAadhar.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtExperience.Text = string.Empty;
            lblid.Text = string.Empty;
            pictureBoxTeacher.Image = null;
            chkPg.Checked = false;
            chkUg.Checked = false;
            rdoProfessor.Checked = false;
            rdoAssoProfessor.Checked = false;
            rdoAsstProfessor.Checked = false;
            rdoPermanent.Checked = false;
            rdoTemporGuest.Checked = false;
            rdoProfessor.Focus();
        }
        private object ToInt32(string text)
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
        public void bindTeacher()
        {
            teacherDal teacherDal = new teacherDal();
            teachcer = teacherDal.getTeacher();
            grdTeacher.DataSource = teachcer;
        }
        private void btnSaveAndNewTeacher_Click(object sender, EventArgs e)
        {


            if (txtName.Text == "" || txtFatherHusbandName.Text == "" ||
                dtpDob.Value == null || ddlBloodGroup.SelectedItem == null || txtAppointYear.Text == "" || ddlDepartment.SelectedItem == null ||
                ddlReligon.SelectedItem == null || txtSpecialization.Text == "" || txtQualification.Text == "" || txtPresentAddress.Text == "" ||
                txtPermanentAddress.Text == "" || txtTelephone.Text == "" || txtMobile.Text == "" | txtPanNo.Text == "" || txtAadhar.Text == "" ||
                txtEmail.Text == "" || txtExperience.Text == "" || pictureBoxTeacher.Image == null)
            {
                MessageBox.Show(" Plese fill all the details!!\n all fields are mendatory to fill!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            else if ((rdoPermanent.Checked == false || rdoTemporGuest.Checked == false) && (chkPg.Checked == false && chkUg.Checked == false)
                && (rdoProfessor.Checked == false || rdoAssoProfessor.Checked == false ||
                rdoAsstProfessor.Checked == false))
            {
                MessageBox.Show("Nothing to save\nPlese fill all the details!!\n all fields are mendatory to fill!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdoProfessor.Focus();
                return;
            }
            if (!Regex.Match(txtName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid name!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            if (!Regex.Match(txtFatherHusbandName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid father/husband name!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFatherHusbandName.Focus();
                return;
            }
            if (!Regex.Match(txtSpecialization.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid specialization!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSpecialization.Focus();
                return;
            }
            if (!Regex.Match(txtQualification.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid qualification!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQualification.Focus();
                return;
            }
            if (!Regex.Match(txtPresentAddress.Text, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$").Success)
            {
                MessageBox.Show("Invalid PresentAddress!! Please enter house no too", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPresentAddress.Focus();
                return;
            }
            if (!Regex.Match(txtPermanentAddress.Text, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$").Success)
            {
                MessageBox.Show("Invalid permanentAddress!! Please enter house no too", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPermanentAddress.Focus();
                return;
            }
            if (!Regex.Match(txtEmail.Text, @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$").Success)
            {
                MessageBox.Show("Invalid Email!! Please enter correct email format only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            if (!Regex.Match(txtMobile.Text, @"^\d{10}$").Success)
            {
                MessageBox.Show("Invalid Mobile no!! Please enter correct No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobile.Focus();
                return;
            }
            if (!Regex.Match(txtTelephone.Text, @"^\d{10}$").Success)
            {
                MessageBox.Show("Invalid telephone no!! Please enter 10 digit No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelephone.Focus();
                return;
            }
            if (!Regex.Match(txtAadhar.Text, @"^\d{12}$").Success)
            {
                MessageBox.Show("Invalid Aadhar no!! Please enter 12 digit aadhar No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAadhar.Focus();
                return;
            }

            if (!Regex.Match(txtExperience.Text, "^([1-9]?[1-9]|30)$").Success)
            {
                MessageBox.Show("Invalid experience card no!! Please enter numeric digits 1 to 30 yr only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtExperience.Focus();
                return;
            }
            else
            {

                con.Open();
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBoxTeacher.Image.Save(ms, ImageFormat.Jpeg);
                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);
                    string val = rdoProfessor.Checked ? "Professor" : rdoAssoProfessor.Checked ? "AssoProfessor" : "AsstProfessor";
                    string exp = string.Empty;
                    if (chkUg.Checked == true) { exp = "ug,"; }
                    if (chkPg.Checked == true) { exp = exp + "pg,"; }
                   // lblid.Text = string.Empty;
                    SqlCommand cmd = new SqlCommand("usp_teacher_insert_update", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tid", ToInt32(lblid.Text));
                    cmd.Parameters.AddWithValue("@teachinggroup", val);
                    cmd.Parameters.AddWithValue("@Teachingtype", rdoPermanent.Checked ? "Permanent" : "Temporary/Guest");
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@FatherOrHusbandName", txtFatherHusbandName.Text);
                    cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dtpDob.Value));
                    cmd.Parameters.AddWithValue("@bloodgroup", ddlBloodGroup.SelectedItem);
                    cmd.Parameters.AddWithValue("@Appointmentyear", Convert.ToInt32(txtAppointYear.Text));
                    cmd.Parameters.AddWithValue("@department", ddlDepartment.SelectedItem);
                    cmd.Parameters.AddWithValue("@Religion", ddlReligon.SelectedItem);
                    cmd.Parameters.AddWithValue("@specialization", txtSpecialization.Text);
                    cmd.Parameters.AddWithValue("@qualification", txtQualification.Text);
                    cmd.Parameters.AddWithValue("@Presentaddress", txtPresentAddress.Text);
                    cmd.Parameters.AddWithValue("@Permanentaddress", txtPermanentAddress.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@panno", txtPanNo.Text);
                    cmd.Parameters.AddWithValue("@aadhar", txtAadhar.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@ExperienceType", exp);
                    cmd.Parameters.AddWithValue("@totalexp", Convert.ToInt32(txtExperience.Text));
                    cmd.Parameters.AddWithValue("@photo", photo_aray);
                    cmd.ExecuteNonQuery();
                    if (lblid.Text =="0")
                        MessageBox.Show("Details saved Successfull!!");
                    else
                        MessageBox.Show("Details Updated Successfully!!");
                    clear();
                    bindTeacher();
                    
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
        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
            DialogResult res = opfd.ShowDialog();
            if (res == DialogResult.OK)
            {
                pictureBoxTeacher.Image = Image.FromFile(opfd.FileName);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)

        {
            DataTable dt = new DataTable();
            //if(ddlSerach.SelectedIndex==0)
            //{
            //    MessageBox.Show("invalid search", "choose any tab from search button except <--please select--> ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ddlSerach.Focus();
            //    return;
            //}
            if (ddlSerach.SelectedIndex == 1)
            {
                if (Regex.IsMatch(txtSearch.Text, @"^[a-zA-Z\b]+$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select tid, name,fatherorhusbandname,email,aadhar,photo from teacher where name like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdTeacher.DataSource = dt;
                }
                else
                {
                    bindTeacher();
                }
            }
            else if (ddlSerach.SelectedIndex == 2)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select tid, name,fatherorhusbandname,email,aadhar,photo from teacher where tid like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdTeacher.DataSource = dt;
                }
                else
                {
                    bindTeacher();
                }
            }
            else if (ddlSerach.SelectedIndex == 3)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select tid, name,fatherorhusbandname,email,aadhar,photo from teacher where aadhar like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdTeacher.DataSource = dt;
                }
                else
                {
                    bindTeacher();
                }
            }
        }

        private void grdTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clear();
            int id = Convert.ToInt32(grdTeacher.CurrentRow.Cells["tid"].Value);
            teacherEntity teacher = teachcer.Where(x => x.tid == id).FirstOrDefault();
            if (teacher.teachinggroup == "rdoProfessor") { rdoProfessor.Checked = true; }
            else if (teacher.teachinggroup == "rdoAssoProfessor") { rdoAssoProfessor.Checked = true; }
            else { rdoAsstProfessor.Checked = true; }
            if (teacher.Teachingtype == "Permanent") { rdoPermanent.Checked = true; }
            else { rdoTemporGuest.Checked = true; }
            if (teacher.ExperienceType.TrimEnd(',') == "ug,pg") { chkUg.Checked = true; chkPg.Checked = true; }
            if (teacher.ExperienceType.TrimEnd(',') == "ug") { chkUg.Checked = true; }
            if (teacher.ExperienceType.TrimEnd(',') == "pg") { chkPg.Checked = true; }
            MemoryStream ms = new MemoryStream((byte[])teacher.photo);
            pictureBoxTeacher.Image = new Bitmap(ms);
            txtName.Text = teacher.name;
            txtFatherHusbandName.Text = teacher.FatherOrHusbandName;
            dtpDob.Value = teacher.dob.Date;
            ddlBloodGroup.ResetText();
            ddlBloodGroup.SelectedItem = teacher.bloodgroup;
            txtAppointYear.Text = teacher.Appointmentyear.ToString();
            ddlDepartment.ResetText();
            ddlDepartment.SelectedItem = (teacher.department);
            ddlReligon.ResetText();
            ddlReligon.SelectedItem = teacher.Religion;
            txtSpecialization.Text = teacher.specialization;
            txtQualification.Text = teacher.qualification;
            txtPresentAddress.Text = teacher.Presentaddress;
            txtPermanentAddress.Text = teacher.Permanentaddress;
            txtTelephone.Text = teacher.telefone.ToString();
            txtPanNo.Text = teacher.panno.ToString();
            txtMobile.Text = teacher.mobile.ToString();
            txtAadhar.Text = teacher.aadhar.ToString();
            txtEmail.Text = teacher.email;
            txtExperience.Text = teacher.totalexp.ToString();
            lblid.Text = teacher.tid.ToString();
        }

        private void btnCloseTeacher_Click(object sender, EventArgs e)
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

        private void btnSaveAndCloseTeacher_Click(object sender, EventArgs e)
        {
            btnSaveAndNewTeacher_Click(sender, e);
            btnCloseTeacher_Click(sender,e);
        }
    }
}
