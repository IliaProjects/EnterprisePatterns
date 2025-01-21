using Clinic.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.UpdateModels
{
    internal class PatientUpdateModel
    {
        public int PatientId { get; set; }
        public string? FullName { get; set; }
        public bool bFullName { get; set; } = false;
        public string? PhoneNr { get; set; }
        public bool bPhoneNr { get; set; } = false;
        public DateTime BirthDate { get; set; }
        public bool bBirthDate { get; set; } = false;
        public Visit? VisitAdd { get; set; }
        public bool bVisitAdd { get; set; } = false;
        public int VisitRemoveId { get; set; }
        public bool bVisitRemove { get; set; } = false;

    }
}
