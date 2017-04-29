using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeFit.Models
{
    public class Chart
    {
        public List<chartItem> listChart { get; set; }
    }
    public class chartItem
    {
        public DateTime date { get; set; }
        public double visits { get; set; }

    }
}
