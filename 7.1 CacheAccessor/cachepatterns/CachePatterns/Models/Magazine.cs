using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachePatterns.Models
{
    public class Magazine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IssueNr { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
