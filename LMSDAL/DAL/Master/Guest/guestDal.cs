using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LMSDAL.Entity.Master.Guest;

namespace LMSDAL.DAL.Master.Guest
{
  public  class guestDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        public List<GuestEntity> GetGuests()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("usp_guest_get", con);
            da.Fill(dt);

            List<GuestEntity> guestList = new List<GuestEntity>();
            guestList = (from DataRow dr in dt.Rows
                           select new GuestEntity()
                           {
                               gid = Convert.ToInt32(dr["gid"]),
                               guestname = dr["guestname"].ToString(),                               
                               designation = dr["designation"].ToString(),
                               department = dr["department"].ToString(),
                               fatherName = dr["fatherName"].ToString(),
                               dob = Convert.ToDateTime(dr["dob"]),
                               bloodGroup = dr["bloodGroup"].ToString(),
                               contactNo = dr["contactNo"].ToString(),
                               qualification = dr["qualification"].ToString(),
                               presentaddress = dr["presentaddress"].ToString(),
                               permanentaddress = dr["permanentaddress"].ToString(),
                               aadharNo = dr["aadharNo"].ToString(),
                               appointYear = Convert.ToInt32( dr["appointYear"]),
                               Photo = (byte[])dr["Photo"],
                           }).ToList();

            return guestList;
        }
    }
}

