using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetterstation_Projekt.Assist;

namespace Wetterstation_Projekt.Model
{
    public class Wetterstation : NotifyPropertyChanged
    {
        private Wetter wetter;
        private IndoorStation indoorStation;

        public Wetter Wetter
        {
            get => wetter;
            set
            {
                if (wetter != value)
                {
                    wetter = value;
                    RaisePropertyChanged(nameof(Wetter));
                }
            }
        }

        public IndoorStation IndoorStation
        {
            get => indoorStation;
            set
            {
                if (indoorStation != value)
                {
                    indoorStation = value;
                    RaisePropertyChanged(nameof(IndoorStation));
                }
            }
        }

        public Wetterstation() 
        {
            Wetter = new Wetter();
            IndoorStation = new IndoorStation();
        }
    }
}
