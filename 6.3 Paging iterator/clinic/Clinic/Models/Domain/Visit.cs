using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public virtual Doctor Doctor { get; set; }
        [Required]
        public virtual Patient Patient { get; set; } 
    }
}
