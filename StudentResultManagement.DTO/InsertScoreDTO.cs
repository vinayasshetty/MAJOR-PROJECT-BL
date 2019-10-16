using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentResultManagement.DTO
{
    public class InsertScoreDTO
    {
        public string UniversitySeatNumber;

        public string SubjectId { get; set; }
        public string SubjectId1 { get; set; }
        public string SubjectId2 { get; set; }
        public string SubjectId3 { get; set; }

        public string SubjectId4 { get; set; }
        public string SubjectId5 { get; set; }
        public string SubjectId6 { get; set; }
        public string SubjectName { get; set; }

        public string BranchId { get; set; }

        public int SemesterNumber { get; set; }
    }
}
