using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LMSDAL.Entity.Master.Teacher;

namespace LMSDAL.DAL.Master.Teacher
{
   public class teacherDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        public List<teacherEntity>getTeacher()
        {     
            SqlDataAdapter da = new SqlDataAdapter("usp_teacher_get", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<teacherEntity> teacherList = new List<teacherEntity>();
            teacherList = (from DataRow dr in dt.Rows
                           select new teacherEntity()
                           {   tid=Convert.ToInt32( dr["tid"]),
                               teachinggroup = dr["teachinggroup"].ToString(),
                               Teachingtype = dr["Teachingtype"].ToString(),
                               name = dr["name"].ToString(),
                               FatherOrHusbandName = dr["FatherOrHusbandName"].ToString(),
                               dob = Convert.ToDateTime( dr["dob"]),
                               bloodgroup = dr["bloodgroup"].ToString(),
                               Appointmentyear = Convert.ToInt32( dr["Appointmentyear"]),
                               department = dr["department"].ToString(),
                               Religion = dr["Religion"].ToString(),
                               specialization = dr["specialization"].ToString(),
                               qualification = dr["qualification"].ToString(),
                               Presentaddress = dr["Presentaddress"].ToString(),
                               Permanentaddress = dr["Permanentaddress"].ToString(),
                               telefone =   dr["telefone"].ToString() ,
                               mobile = dr["mobile"].ToString(),
                               panno = dr["panno"].ToString(),
                               aadhar = dr["aadhar"].ToString(),
                               email = dr["email"].ToString(),
                               ExperienceType = dr["ExperienceType"].ToString(),
                               totalexp = Convert.ToInt32(dr["totalexp"]),
                               photo = (byte[]) dr["photo"],
                           }).ToList();
            return teacherList;
        }
    }
}
