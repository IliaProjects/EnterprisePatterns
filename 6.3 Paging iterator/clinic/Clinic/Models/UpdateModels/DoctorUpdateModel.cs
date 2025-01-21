using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.UpdateModels
{
    public class DoctorUpdateModel
    {
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public bool bFullName { get; set; } = false;
        public DateTime Hired { get; set; }
        public bool bHired { get; set; } = false;
        public int SpecialityAddId { get; set; }
        public bool bSpecialityAdd { get; set; } = false;
        public int SpecialityRemoveId { get; set; }
        public bool bSpecialityRemove { get; set; } = false;
    }
}
