using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetterstation_Projekt.API
{
    public class OwnWeatherInsideAPIResponse
    {
        public double temperature { get; set; }
        public double humidity { get; set; }
        public double co2 { get; set; }
    }
}
