using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResultManagement.DTO
{
   public class StudentDetailDTO
    {
        public string UniversitySeatNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public long ContactNumber { get; set; }
        public long OptionalContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SemesterNumber { get; set; }
    }
}
