using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetterstation_Projekt.API
{
    public class OwnWeatherAPIResponse
    {
        public double temperature { get; set; }

        public double humidity { get; set; }

        public float pressure { get; set; }
    }
}
