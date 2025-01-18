using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using Wetterstation_Projekt.Assist;
using Wetterstation_Projekt.Model;

namespace Wetterstation_Projekt.ViewModel
{
    public class WettervorhersageViewModel
    {
        private Wettervorhersage wettervorhersage = new Wettervorhersage();

        public int MaxTemperatur => (int)Math.Round(wettervorhersage.MaxTemperatur);

        public int MinTemperatur => (int)Math.Round(wettervorhersage.MinTemperatur);

        public string Wochentag
        {
            get 
            {
                DateTime datetime = DateTime.Parse(wettervorhersage.Datum);
                return datetime.ToString("dddd");
            } 
        }

        public string Bild
        {
            get
            {
                return WetterStatus.GetWetterStatus(false, wettervorhersage.StatusCode);
            }
        }

        public WettervorhersageViewModel(Wettervorhersage wettervorhersage)
        {
            this.wettervorhersage = wettervorhersage;
        }
    }
}
