using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lap3.Models
{
    public class Major
    {
        public Major() {
            Learners = new List<Learner>();
        }
        public int MajorID { get; set; }
        public string MajorName { get; set;}
        public virtual ICollection<Learner> Learners { get; set; }
    }
}