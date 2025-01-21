using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNr { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
