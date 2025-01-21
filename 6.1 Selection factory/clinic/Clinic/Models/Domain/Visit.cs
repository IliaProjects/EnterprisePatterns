using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.Domain
{
    public class Visit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; } 
    }
}
