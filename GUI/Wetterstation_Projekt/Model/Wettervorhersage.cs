using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetterstation_Projekt.Assist;

namespace Wetterstation_Projekt.Model
{
    public class Wettervorhersage : NotifyPropertyChanged
    {
        private string datum;
        private double minTemperatur;
        private double maxTemperatur;
        private int statusCode;

        public string Datum
        {
            get => datum;
            set
            {
                if (datum != value)
                {
                    datum = value;
                    RaisePropertyChanged(nameof(Datum));
                }
            }
        }

        public double MinTemperatur
        {
            get => minTemperatur;
            set
            {
                if (minTemperatur != value)
                {
                    minTemperatur = value;
                    RaisePropertyChanged(nameof(MinTemperatur));
                }
            }
        }

        public double MaxTemperatur
        {
            get => maxTemperatur;
            set
            {
                if (maxTemperatur != value)
                {
                    maxTemperatur = value;
                    RaisePropertyChanged(nameof(MaxTemperatur));
                }
            }
        }

        public int StatusCode
        {
            get => statusCode;
            set
            {
                if (statusCode != value)
                {
                    statusCode = value;
                    RaisePropertyChanged(nameof(StatusCode));
                }
            }
        }
    }
}
