using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.SearchModels
{
    public class PatientSearchModel
    {
        public string? FullName { get; set; }
        public bool bFullName { get; set; } = false;

        public string? PhoneNr { get; set; }
        public bool bPhoneNr { get; set; } = false;

        public int DoctorId { get; set; }
        public bool bDoctorId { get; set; } = false;

        public int MinAge { get; set; }
        public bool bMinAge { get; set; } = false;

        public int MaxAge { get; set; }
        public bool bMaxAge { get; set; } = false;

        public DateTime VisitDate { get; set; }
        public bool bVisitDate { get; set; } = false;

        public DateTime VisitDateFrom { get; set; }
        public bool bVisitDateFrom { get; set; } = false;

        public DateTime VisitDateTo { get; set; }
        public bool bVisitDateTo { get; set; } = false;

    }
}
