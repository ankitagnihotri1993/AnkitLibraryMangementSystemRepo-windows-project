using LMSDAL.Entity.Master.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LMSDAL.DAL.Master.Student
{
   public class studentDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        public List<StudentEntity> GetStudents()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("usp_stu_get", con);
            da.Fill(dt);

            List<StudentEntity> studentList = new List<StudentEntity>();
            studentList = (from DataRow dr in dt.Rows
                       select new StudentEntity()
                       {
                           sid = Convert.ToInt32(dr["sid"]),
                           session = dr["session"].ToString(),
                           qualification = dr["qualification"].ToString(),
                           studenttype = dr["studenttype"].ToString(),
                           studentName = dr["studentName"].ToString(),
                           fatherName = dr["fatherName"].ToString(),
                           dob = Convert.ToDateTime( dr["dob"]),
                           course = dr["course"].ToString(),
                           Photo = (byte[])dr["Photo"],
                           department = dr["department"].ToString(),
                           coursetype = dr["coursetype"].ToString(),
                           yearOrsemester = dr["yearOrsemester"].ToString(),
                           rollno = Convert.ToInt32( dr["rollno"]),
                           maternalstatus = dr["maternalstatus"].ToString(),
                           caste = dr["caste"].ToString(),
                           contactno = dr["contactno"].ToString(),
                           gender = dr["gender"].ToString(),
                           bloodgroup = dr["bloodgroup"].ToString(),
                           aadharno = dr["aadharno"].ToString(),
                           email = dr["email"].ToString(),
                           physicalDisable = dr["physicalDisable"].ToString(),
                           presentaddress = dr["presentaddress"].ToString(),
                           permanentaddress = dr["permanentaddress"].ToString(),
                       }).ToList();

            return studentList;
        }
    }
}
