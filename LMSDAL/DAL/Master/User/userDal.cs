using LMSDAL.Entity.Master.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDAL.DAL.Master.User
{
  public  class userDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        public List<userEntity> getUser()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("usp_user_get", con);
            da.Fill(dt);

            List<userEntity> userList = new List<userEntity>();
            userList = (from DataRow dr in dt.Rows
                           select new userEntity()
                           {
                               uid = Convert.ToInt32(dr["uid"]),
                               Userid =dr["userid"].ToString(),
                               Username = dr["name"].ToString(),
                               Password = dr["password"].ToString(),
                               Roleid =Convert.ToInt32( dr["roleid"]),
                               Rolename=dr["rolename"].ToString(),
                           }).ToList();

            return userList;
        }
    }
}
