using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDAL.Entity.Master.Teacher
{
   public class teacherEntity
    {
        public int tid { get; set; }
        public string teachinggroup { get; set; }
        public string Teachingtype { get; set; }
        public string name { get; set; }
        public string FatherOrHusbandName { get; set; }
        public DateTime dob { get; set; }
        public string bloodgroup { get; set; }
        public int Appointmentyear { get; set; }
        public string department { get; set; }
        public string Religion { get; set; }
        public string specialization { get; set; }
        public string qualification { get; set; }
        public string Presentaddress { get; set; }
        public string Permanentaddress { get; set; }
        public string telefone { get; set; }
        public string mobile { get; set; }
        public string panno { get; set; }
        public string aadhar { get; set; }
        public string email { get; set; }
        public string ExperienceType { get; set; }
        public int totalexp { get; set; }
        public byte[] photo { get; set; }
    }
}
