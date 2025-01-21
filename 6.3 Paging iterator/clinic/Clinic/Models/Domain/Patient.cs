using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models.Domain
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNr { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}
