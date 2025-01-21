using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.SearchModels
{
    public class DoctorSearchModel
    {
        public string? FullName { get; set; }
        public bool bFullName { get; set; } = false;

        public Speciality[]? Specialities { get; set; }
        public bool bSpecialities { get; set; } = false;

        public int MinStageYears { get; set; }
        public bool bMinStageYears { get; set; } = false;

        public int MaxStageYears { get; set; }
        public bool bMaxStageYears { get; set; } = false;

        public DateTime VisitDate { get; set; }
        public bool bVisitDate { get; set; } = false;

        public DateTime VisitsFrom { get; set; }
        public bool bVisitsFrom { get; set; } = false;

        public DateTime VisitsTo { get; set; }
        public bool bVisitsTo { get; set; } = false;
    }
}
