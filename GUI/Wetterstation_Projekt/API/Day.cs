using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetterstation_Projekt.API
{
    public class Day
    {
        public double maxtemp_c { get; set; }
        public double mintemp_c { get; set; }
        public Condition condition { get; set; }
    }
}
