using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDAL.Entity.Master.Guest
{
  public  class GuestEntity
    {
        public int gid { get; set; }
        public string guestname { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string fatherName { get; set; }
        public DateTime dob { get; set; }
        public string bloodGroup { get; set; }
        public string contactNo { get; set; }
        public string qualification { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string aadharNo { get; set; }
        public int appointYear { get; set; }
        public byte[] Photo { get; set; }
    }
}
