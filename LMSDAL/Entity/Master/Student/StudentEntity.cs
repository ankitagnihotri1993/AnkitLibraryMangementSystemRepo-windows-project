using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDAL.Entity.Master.Student
{
  public class StudentEntity
    {//Get set property
        public int sid { get; set; }
        public string session { get; set; }
        public string qualification { get; set; }
        public string studenttype { get; set; }
        public string studentName { get; set; }
        public string fatherName { get; set; }
        public DateTime dob { get; set; }
        public byte[] Photo { get; set; }
        public string course { get; set; }
        public string department { get; set; }
        public string coursetype { get; set; }
        public string yearOrsemester { get; set; }
        public int rollno { get; set; }
        public string maternalstatus { get; set; }
        public string caste { get; set; }
        public string contactno { get; set; }
        public string gender { get; set; }
        public string bloodgroup { get; set; }
        public string aadharno { get; set; }
        public string email { get; set; }
        public string physicalDisable { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }

    }
}
