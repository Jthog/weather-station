using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetterstation_Projekt.API
{
    public class Current
    {
        public double temp_c { get; set; }

        public Condition condition { get; set; }

        public double wind_kph { get; set; }

        public string wind_dir { get; set; }

        public double feelslike_c { get; set; }

        public int humidity { get; set; }

        public double pressure_mb { get; set; }

        public double uv { get; set; }

        public Air_Quality air_quality { get; set; }
    }
}
