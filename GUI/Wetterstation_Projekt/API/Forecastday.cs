﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetterstation_Projekt.API
{
    public class Forecastday
    {
        public string date { get; set; }
        public Day day { get; set; }
        public Astro astro { get; set; }
    }
}
