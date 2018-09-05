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
using LMSDAL.DAL.Master.Student;
using LMSDAL.Entity.Master.Student;
using System.Text.RegularExpressions;

namespace LMS
{
    public partial class studentForm : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        private List<StudentEntity> student = new List<StudentEntity>();
        public studentForm()
        {
            InitializeComponent();

            ddlBloodGroup.Items.Insert(0, "<--Please select-->");
            ddlBloodGroup.SelectedIndex = 0;
            ddlMatrnalStatus.Items.Insert(0, "<--Please select-->");
            ddlMatrnalStatus.SelectedIndex = 0;
            ddlCaste.Items.Insert(0, "<--Please select-->");
            ddlCaste.SelectedIndex = 0;
            ddlSearch.Items.Insert(0, "All");
            ddlSearch.SelectedIndex = 0;
            bindStudent();
        }
        public void clear()
        {
            rdoUg.Checked = false;
            rdoPg.Checked = false;
            rdoTraditional.Checked = false;
            rdoVocational.Checked = false;
            rdoAnnual.Checked = false;
            rdoSemester.Checked = false;
            rdoMale.Checked = false;
            rdoFemale.Checked = false;
            rdoYes.Checked = false;
            rdoNo.Checked = false;
            txtSession.Text = string.Empty;
            txtStudentaName.Text = "";
            txtFatherName.Text = "";
            dobDtp.Value = DateTime.UtcNow;
            pictureBox1.Image = null;
            txtCourse.Text = "";
            txtDepartment.Text = string.Empty;
            txtYearandSem.Text = string.Empty;
            txtRollno.Text = string.Empty;
            ddlMatrnalStatus.SelectedItem = null;
            ddlCaste.SelectedItem = null;
            txtContactno.Text = string.Empty;
            ddlBloodGroup.SelectedItem = null;
            txtAadhar.Text = string.Empty;
            txtmail.Text = string.Empty;
            txtPresentAddress.Text = string.Empty;
            txtpermanentAddress.Text = string.Empty;
            lblid.Text = string.Empty;
            txtStudentaName.Focus();
        }
        public void bindStudent()
        {
            studentDal studenDal = new studentDal();
            student = studenDal.GetStudents();
            grdStudent.DataSource = student;
        }
        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (txtSession.Text == "" || txtStudentaName.Text == "" || txtFatherName.Text == "" || dobDtp.Value == null || txtCourse.Text == "" ||
                txtDepartment.Text == "" || txtYearandSem.Text == "" || txtRollno.Text == "" || ddlMatrnalStatus.SelectedItem == null || ddlCaste.SelectedItem == null
                || txtContactno.Text == "" || txtAadhar.Text == "" || txtmail.Text == "" || pictureBox1.Image == null || txtPresentAddress.Text == ""
                || txtpermanentAddress.Text == "" || ddlBloodGroup.SelectedItem == null)
            {
                MessageBox.Show("Plese fill all the details!! all fields are mendatory to fill!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSession.Focus();
                return;
            }
            if ((rdoUg.Checked == false && rdoPg.Checked == false) && (rdoTraditional.Checked == false && rdoVocational.Checked == false)
                && (rdoAnnual.Checked == false && rdoSemester.Checked == false) && (rdoMale.Checked == false && rdoFemale.Checked == false)
                && (rdoYes.Checked == false && rdoNo.Checked == false))

            {
                MessageBox.Show("Plese fill all the details!! all fields are mendatory to fill!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdoUg.Focus();
                return;
            }
            if (!Regex.Match(txtSession.Text, @"[0-9-]").Success)
            {
                MessageBox.Show("Invalid session!!Enter session like 2018-2022", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSession.Focus();
                return;
            }
            if (!Regex.Match(txtStudentaName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid name!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentaName.Focus();
                return;
            }
            if (!Regex.Match(txtFatherName.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid fathername!! Please enter alphabets only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFatherName.Focus();
                return;
            }
            if (!Regex.Match(txtCourse.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid course!! Please enter course like BCA,MCa", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCourse.Focus();
                return;
            }
            if (!Regex.Match(txtDepartment.Text, "^[a-zA-Z][A-Za-z s]+$").Success)
            {
                MessageBox.Show("Invalid department!! enter departments like CS, It,Civil etc", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepartment.Focus();
                return;
            }
            if (!Regex.Match(txtAadhar.Text, @"^\d{12}$").Success)
            {
                MessageBox.Show("Invalid Aadhar no!! Please enter correct No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAadhar.Focus();
                return;
            }
            if (!Regex.Match(txtContactno.Text, @"^\d{10}$").Success)
            {
                MessageBox.Show("Invalid contact no!! Please enter correct No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactno.Focus();
                return;
            }
            if (!Regex.Match(txtRollno.Text, "^[0-9]+$").Success)
            {
                MessageBox.Show("Invalid Roll no!! Please enter correct Roll No", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRollno.Focus();
                return;
            }
            if (!Regex.Match(txtmail.Text, @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$").Success)
            {
                MessageBox.Show("Invalid Email!! Please enter correct email format only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmail.Focus();
                return;
            }
            if (!Regex.Match(txtYearandSem.Text, @"^\d{1}$").Success)
            {
                MessageBox.Show("Invalid year or sem!! Please enter correct year/sem ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtYearandSem.Focus();
                return;
            }
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Image not Selected,please Select!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnbrowse.Focus();
                return;
            }
            try
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                //lblid.Text = string.Empty;
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_stu_insert_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sid", ToInt32(lblid.Text));
                cmd.Parameters.AddWithValue("@session",txtSession.Text);
                cmd.Parameters.AddWithValue("@qualification", rdoUg.Checked ? "UG" : "PG");
                cmd.Parameters.AddWithValue("@studenttype", rdoTraditional.Checked ? "TraditionalStudent" : "VocationalStudent");
                cmd.Parameters.AddWithValue("@studentName", txtStudentaName.Text);
                cmd.Parameters.AddWithValue("@fatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dobDtp.Value));
                cmd.Parameters.AddWithValue("@photo", photo_aray);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);
                cmd.Parameters.AddWithValue("@department", txtDepartment.Text);
                cmd.Parameters.AddWithValue("@coursetype", rdoAnnual.Checked ? "Annual" : "Semester");
                cmd.Parameters.AddWithValue("@yearOrsemester", txtYearandSem.Text);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtRollno.Text));
                cmd.Parameters.AddWithValue("@maternalstatus", ddlMatrnalStatus.SelectedItem);
                cmd.Parameters.AddWithValue("@caste", ddlCaste.SelectedItem);
                cmd.Parameters.AddWithValue("@contactno", txtContactno.Text);
                cmd.Parameters.AddWithValue("@gender", rdoMale.Checked ? "Male" : "Female");
                cmd.Parameters.AddWithValue("@bloodgroup", ddlBloodGroup.SelectedItem);
                cmd.Parameters.AddWithValue("@aadharno", txtAadhar.Text);
                cmd.Parameters.AddWithValue("@email", txtmail.Text);
                cmd.Parameters.AddWithValue("@physicalDisable", rdoYes.Checked ? "Yes" : "No");
                cmd.Parameters.AddWithValue("@presentaddress", txtPresentAddress.Text);
                cmd.Parameters.AddWithValue("@permanentaddress", txtpermanentAddress.Text);
                cmd.ExecuteNonQuery();
                if (lblid.Text=="0")
                    MessageBox.Show("Details saved Successfull!!");
                else
                    MessageBox.Show("Details Updated Successfully!!");
                con.Close();
                clear();
                bindStudent();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
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

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog();
            opfd.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
            DialogResult res = opfd.ShowDialog();
            if (res == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opfd.FileName);
            }
        }
        

        private void grdStudent_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            clear();
            // StudentEntity stu = grdStudent.Rows[e.RowIndex].DataBoundItem as StudentEntity;
            int id = ToInt32(grdStudent.CurrentRow.Cells["sid"].Value);
            StudentEntity stu = student.Where(x => x.sid == id).FirstOrDefault();
            if (stu.qualification == "UG") { rdoUg.Checked = true; }
            else { rdoPg.Checked = true; }
            if (stu.studenttype == "TraditionalStudent") { rdoTraditional.Checked = true; }
            else { rdoVocational.Checked = true; }
            if (stu.coursetype == "Annual") { rdoAnnual.Checked = true; }
            else { rdoSemester.Checked = true; }
            if (stu.gender == "Male") { rdoMale.Checked = true; }
            else { rdoFemale.Checked = true; }
            if (stu.physicalDisable == "Yes") { rdoYes.Checked = true; }
            else { rdoNo.Checked = true; }
            txtSession.Text = stu.session;
            txtStudentaName.Text = stu.studentName;
            txtFatherName.Text = stu.fatherName;
            dobDtp.Value = stu.dob.Date;
            MemoryStream ms = new MemoryStream((byte[])stu.Photo);
            pictureBox1.Image = new Bitmap(ms);
            txtCourse.Text = stu.course;
            txtDepartment.Text = stu.department;
            txtYearandSem.Text = stu.yearOrsemester;
            txtRollno.Text = stu.rollno.ToString();
            ddlMatrnalStatus.ResetText();
            ddlMatrnalStatus.SelectedItem = stu.maternalstatus;
            ddlCaste.ResetText();
            ddlCaste.SelectedItem = stu.caste;
            txtContactno.Text = stu.contactno.ToString();
            ddlBloodGroup.ResetText();
            ddlBloodGroup.SelectedItem = stu.bloodgroup;
            txtAadhar.Text = stu.aadharno.ToString();
            txtmail.Text = stu.email;
            txtPresentAddress.Text = stu.presentaddress;
            txtpermanentAddress.Text = stu.permanentaddress;
            lblid.Text = stu.sid.ToString();
            //bindStudent();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //if (ddlSearch.SelectedIndex == 0)
            //{
            //    MessageBox.Show("invalid search", "choose any tab from search button except <--please select--> ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    ddlSearch.Focus();
            //    return;
            //}
            if (ddlSearch.SelectedIndex == 1)
            {
                if (Regex.IsMatch(txtSearch.Text, @"^[a-zA-Z\b]+$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select sid, studentname,fathername,email,aadharno,photo from student where studentname like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdStudent.DataSource = dt;
                }
                else
                {
                    bindStudent();
                }
            }
            else if (ddlSearch.SelectedIndex == 2)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select sid, studentname,fathername,email,dob,photo from student where sid like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdStudent.DataSource = dt;
                }
                else
                {
                    bindStudent();
                }
            }
            else if (ddlSearch.SelectedIndex == 3)
            {
                if (Regex.IsMatch(txtSearch.Text, "^[0-9\b]*$"))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select sid, studentname,fathername,email,aadharno,photo from student where aadharno like '" + txtSearch.Text + "%' ", con);
                    da.Fill(dt);
                    grdStudent.DataSource = dt;
                }
                else
                {
                    bindStudent();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
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

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            btnSaveAndNew_Click(sender,e);
            btnClose_Click(sender,e);
        }
    }
}
