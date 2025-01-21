using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.Domain
{
    public class Doctor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime Hired { get; set; }
        public virtual ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();
        public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
