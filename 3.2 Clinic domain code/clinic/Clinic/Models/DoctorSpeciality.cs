﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class DoctorSpeciality
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int SpecialityId { get; set; }
    }
}
