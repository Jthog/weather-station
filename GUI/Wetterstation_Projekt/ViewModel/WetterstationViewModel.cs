using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wetterstation_Projekt.Assist;
using Wetterstation_Projekt.Model;

namespace Wetterstation_Projekt.ViewModel
{
    public class WetterstationViewModel : NotifyPropertyChanged
    {
        #region Felder
        private Wetterstation wetterstation;
        private string uhrzeit;
        private bool istNacht;
        //private string label;
        private int counter = 0;
        #endregion

        #region Konstante
        private const string sehrSchlecht = "Sehr schlecht";
        private const string schlecht = "Schlecht";
        private const string maessig = "Mäßig";
        private const string gut = "Gut";
        private const string sehrGut = "Sehr gut";

        private const string niedrig = "Niedrig";
        private const string hoch = "Hoch";
        private const string sehrHoch = "Sehr hoch";
        private const string extrem = "Extrem";
        #endregion

        #region Eigenschaften Allgemein
        public Wetterstation Wetterstation
        {
            get => wetterstation;
            set
            {
                if (wetterstation != value)
                {
                    wetterstation = value;
                    RaisePropertyChanged(nameof(Wetterstation));
                }
            }
        }

        public string Sonnenaufgang
        {
            get
            {
                DateTime datetime = DateTime.Parse(Wetterstation.Wetter.Sonnenaufgang);
                return datetime.ToString("HH:mm");
            }
        }

        public string Sonnenuntergang
        {
            get
            {
                DateTime datetime = DateTime.Parse(Wetterstation.Wetter.Sonnenuntergang);
                return datetime.ToString("HH:mm");
            }
        }

        public ObservableCollection<WettervorhersageViewModel> WettervorhersageList => DatenInListeHinzufuegen();

        public string Uhrzeit
        {
            get => uhrzeit;
            private set
            {
                if (uhrzeit != value)
                {
                    uhrzeit = value;
                    RaisePropertyChanged(nameof(Uhrzeit));
                }
            }
        }

        public string Datum => DateTime.Now.ToString("dddd, dd. MMMM");
        #endregion

        #region Eigenschaft Outside

        public int Temperatur => (int)Math.Round(Wetterstation.Wetter.Temperatur);

        public int TemperaturGefuehlt => (int)Math.Round(Wetterstation.Wetter.TemperaturGefuehlt);

        public string Status => Wetterstation.Wetter.Status;

        public string Ort => Wetterstation.Wetter.Ort;

        public double Uv => Wetterstation.Wetter.Uv;

        public string DisplayUvText => $"UV-Wert liegt bei {Wetterstation.Wetter.Uv}";

        public double Windstatus => Wetterstation.Wetter.Windstatus;

        public string Windrichtung => Wetterstation.Wetter.Windrichtung;

        public double Luftfeuchtigkeit => Math.Round(Wetterstation.Wetter.Luftfeuchtigkeit, 2);

        public double Luftqualitaet => Wetterstation.Wetter.Luftqualitaet;

        public double Luftdruck => Math.Round(Wetterstation.Wetter.Luftdruck, 1);

        public string LuftfeuchtigkeitStatusBild
        {
            get
            {
                StringBuilder path = new StringBuilder("/Bilder/Status/");
                if (Luftfeuchtigkeit >= 40 && Luftfeuchtigkeit <= 60)
                {
                    path.Append("good.png");
                }
                else
                {
                    path.Append("bad.png");
                }

                return path.ToString();
            }
        }

        public string NO2Status
        {
            get
            {
                string status = string.Empty;
                if (Luftqualitaet > 200)
                {
                    status = sehrSchlecht;
                }
                else if (Luftqualitaet > 100 && Luftqualitaet <= 200)
                {
                    status = schlecht;
                }
                else if (Luftqualitaet > 40 && Luftqualitaet <= 100)
                {
                    status = maessig;
                }
                else if (Luftqualitaet > 20 && Luftqualitaet <= 40)
                {
                    status = gut;
                }
                else if (Luftqualitaet >= 0 && Luftqualitaet <= 20)
                {
                    status = sehrGut;
                }
                return status;
            }
        }

        public string NO2StatusBild
        {
            get
            {
                StringBuilder path = new StringBuilder("/Bilder/Status/");
                if (NO2Status == sehrSchlecht || NO2Status == schlecht)
                {
                    path.Append("bad.png");
                }
                else if (NO2Status == maessig)
                {
                    path.Append("average.png");
                }
                else if (NO2Status == gut || NO2Status == sehrGut)
                {
                    path.Append("good.png");
                }

                return path.ToString();
            }
        }

        public string UvStatus
        {
            get
            {
                string status = string.Empty;
                if (Uv >= 0 && Uv <= 2)
                {
                    status = niedrig;
                }
                else if (Uv > 2 && Uv <= 5)
                {
                    status = maessig;
                }
                else if (Uv > 5 || Uv <= 7)
                {
                    status = hoch;
                }
                else if (Uv > 7 && Uv <= 10)
                {
                    status = sehrHoch;
                }
                else if (Uv > 10)
                {
                    status = extrem;
                }

                return status;
            }
        }

        public string UvStatusBild
        {
            get
            {
                StringBuilder path = new StringBuilder("/Bilder/Status/");

                if (UvStatus == niedrig)
                {
                    path.Append("good.png");
                }
                else if (UvStatus == maessig)
                {
                    path.Append("average.png");
                }
                else if (UvStatus == hoch || UvStatus == sehrHoch)
                {
                    path.Append("bad.png");
                }
                else if (UvStatus == extrem)
                {
                    path.Append("warning.png");
                }

                return path.ToString() ?? "";
            }
        }

        public double SliderLuftfeuchtigkeit => this.Luftfeuchtigkeit / 10;

        public double SLiderLuftqualitaet => Math.Round(this.Luftqualitaet / 10, 2);

        public string Bild
        {
            get
            {
                return WetterStatus.GetWetterStatus(istNacht, Wetterstation.Wetter.StatusCode);
            }
        }

        public bool IstNacht
        {
            get => istNacht;
            set
            {
                if (istNacht != value)
                {
                    istNacht = value;
                    RaisePropertyChanged(nameof(IstNacht));
                    RaisePropertyChanged(nameof(Bild));
                }
            }
        }
        #endregion

        #region Eigenschaft Inside
        public double Innentemperatur => Wetterstation.IndoorStation.Temperatur;

        public string InnenTemperaturStatus
        {
            get
            {
                string status = "";
                if (this.Innentemperatur >= 22 && this.Innentemperatur <= 24)
                {
                    status = gut;
                }
                else
                {
                    status = schlecht;
                }

                return status;
            }
        }

        public string InnenTemperaturStatusBild
        {
            get
            {
                StringBuilder path = new StringBuilder("/Bilder/Status/");
                if (this.InnenTemperaturStatus == gut)
                {
                    path.Append("good.png");
                }
                else if (this.InnenTemperaturStatus == schlecht)
                {
                    path.Append("bad.png");
                }

                return path.ToString();
            }
        }

        public double InnenLuftfeuchtigkeit => Wetterstation.IndoorStation.Luftfeuchtigkeit;

        public double SliderInnenLuftfeuchtigkeit => this.InnenLuftfeuchtigkeit / 10;

        public string InnenLuftfeuchtigkeitStatus
        {
            get
            {
                string status = "";

                if(this.InnenLuftfeuchtigkeit >= 30 && this.InnenLuftfeuchtigkeit <= 45)
                {
                    status = gut;
                }
                else
                {
                    status = schlecht;
                }
                return status;
            }
        }

        public string InnenLuftfeuchtigkeitStatusBild
        {
            get
            {
                StringBuilder path = new StringBuilder("/Bilder/Status/");
                if (this.InnenLuftfeuchtigkeitStatus  == gut)
                {
                    path.Append("good.png");
                }
                else if (this.InnenLuftfeuchtigkeitStatus == schlecht)
                {
                    path.Append("bad.png");
                }

                return path.ToString();
            }
        }

        public double InnenLuftqualitaet => Wetterstation.IndoorStation.Luftqualitaet;
        public double SliderInnenLuftqualitaet => Math.Round(this.InnenLuftqualitaet / 10, 2);

        #endregion

        public WetterstationViewModel()
        {
            Wetterstation = new Wetterstation();
            //Label = $"Has been updated for the {counter}. time";
            DispatcherTimerSetup();
        }

        #region Methoden
        private void DispatcherTimerSetup()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(AktuelleUhrzeitUndWetterdaten);
            dispatcherTimer.Start();
        }

        private void AktuelleUhrzeitUndWetterdaten(object sender, EventArgs e)
        {
            Uhrzeit = DateTime.Now.ToString("HH:mm");

            counter++;
            counter %= 60;
            if (counter == 0)
            {
                this.Wetterstation = new Wetterstation();
            }

            if (DateTime.TryParseExact(Sonnenuntergang, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datetime))
            {
                if (DateTime.Now.TimeOfDay > datetime.TimeOfDay)
                {
                    this.IstNacht = true;
                }
                else
                {
                    this.IstNacht = false;
                }
            }
        }

        public ObservableCollection<WettervorhersageViewModel> DatenInListeHinzufuegen()
        {
            ObservableCollection<WettervorhersageViewModel> wettervorhersageVMList = new ObservableCollection<WettervorhersageViewModel>();
            foreach (Wettervorhersage wettervorhersage in this.Wetterstation.Wetter.WettervorhersageList)
            {
                wettervorhersageVMList.Add(new WettervorhersageViewModel(wettervorhersage));
            }
            return wettervorhersageVMList;
        }
        #endregion
    }

    public enum WetterStatusEnum
    {
        KlarerHimmel = 1000,
        LeichtBewoelkt = 1003,
        Bewoelkt = 1006,
        Bedeckt = 1009,
        LeichterNebel = 1030,
        StellenweiseRegelfall = 1063,
        StellenweiseSchneefall = 1066,
        StellenweiseEisregen = 1069,
        StellenweiseGefrorenderNieselregen = 1072,
        GewittrigeNiederschläge = 1087,
        Schneeverwehungen = 1114,
        Schneesturm = 1117,
        Nebel = 1135,
        Eisnebel = 1147,
        StellenweiseNieselregen = 1150,
        Nieselregen = 1153,
        GefrorenderNieselregen = 1168,
        StarkerNieseslregen = 1171,
        StellenweiseLeichterRegenfall = 1180,
        LeichterRegenfall = 1183,
        ZuweilenMaessigerRegenfall = 1186,
        MaessigerRegenfall = 1189,
        ZuweilenStarkerRegenfall = 1192,
        StarkerRegenfall = 1195,
        LeichterEisregen = 1198,
        MaessigerStarkerEisregen = 1201,
        LeichterGraupelschauer = 1204,
        MaessigerSchwererGraupelschauer = 1207,
        StellenweiseLeichterSchneefall = 1210,
        LeichterSchneefall = 1213,
        StellenweiseMaessigerSchneefall = 1216,
        MaessigerSchneefall = 1219,
        StellenweiseStarkerSchneefall = 1222,
        StarkerSchneefall = 1225,
        Hagel = 1237,
        LeichterRegenschauer = 1240,
        MaessigerStarkerRegenschauer = 1243,
        LeichterSchneegestoeber = 1255,
        MaessigerStarkeSchneegestoeber = 1258,
        LeichterHagelschauer = 1261,
        MaessigerStarkerHagelschauer = 1264,
        StellenweiseLeichterRegenfallGewitter = 1273,
        MaessigerStarkerRegenfallGewitter = 1276,
        StellenweiseLeichterSchneefallGewitter = 1279,
        MaessigerStarkerSchneefallGewitter = 1282
    }
}