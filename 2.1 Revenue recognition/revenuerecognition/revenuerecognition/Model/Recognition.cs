using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace revenuerecognition.Model
{
    public class Recognition
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int Revenue { get; set; }
        public DateTime RecognitionDate { get; set; }
    }
}
