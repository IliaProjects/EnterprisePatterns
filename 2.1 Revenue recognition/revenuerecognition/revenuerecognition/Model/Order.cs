using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace revenuerecognition.Model
{
    public class Order
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public DateTime OrderTime { get; set; }
        public int totalRevenue { get; set; }
    }
}
