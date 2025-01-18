using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Wetterstation_Projekt.API;
using Wetterstation_Projekt.Assist;

namespace Wetterstation_Projekt.Model
{
    public class Wetter : NotifyPropertyChanged
    {
        private double temperatur;
        private double temperaturGefuehlt;
        private string status;
        private int statusCode;
        private string ort;
        private double uv;
        private double windstatus;
        private string windrichtung;
        private string sonnenaufgang;
        private string sonnenuntergang;
        private double luftfeuchtigkeit;
        private double innentemperatur;
        private double innenluftfeuchtigkeit;
        private double innenco2;
        private double luftqualitaet;
        private double luftdruck;
        private List<Wettervorhersage> wettervorhersageList = new List<Wettervorhersage>();

        public double Temperatur
        {
            get => temperatur;
            set
            {
                if (temperatur != value)
                {
                    temperatur = value;
                    RaisePropertyChanged(nameof(Temperatur));
                }
            }
        }

        public double TemperaturGefuehlt
        {
            get => temperaturGefuehlt;
            set
            {
                if (temperaturGefuehlt != value)
                {
                    temperaturGefuehlt = value;
                    RaisePropertyChanged(nameof(TemperaturGefuehlt));
                }
            }
        }

        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged(nameof(Status));
                }
            }
        }

        public int StatusCode
        {
            get => statusCode;
            set
            {
                if(statusCode != value)
                {
                    statusCode = value;
                    RaisePropertyChanged(nameof(StatusCode));
                }
            }
        }

        public string Ort
        {
            get => ort;
            set
            {
                if (ort != value)
                {
                    ort = value;
                    RaisePropertyChanged(nameof(Ort));
                }
            }
        }

        public double Uv
        {
            get => uv;
            set
            {
                if (uv != value)
                {
                    uv = value;
                    RaisePropertyChanged(nameof(Uv));
                }
            }
        }

        public double Windstatus
        {
            get => windstatus;
            set
            {
                if (windstatus != value)
                {
                    windstatus = value;
                    RaisePropertyChanged(nameof(Windstatus));
                }
            }
        }

        public string Windrichtung
        {
            get => windrichtung;
            set
            {
                if (windrichtung != value)
                {
                    windrichtung = value;
                    RaisePropertyChanged(nameof(Windrichtung));
                }
            }
        }

        public string Sonnenaufgang
        {
            get => sonnenaufgang;
            set
            {
                if (sonnenaufgang != value)
                {
                    sonnenaufgang = value;
                    RaisePropertyChanged(nameof(Sonnenaufgang));
                }
            }
        }

        public string Sonnenuntergang
        {
            get => sonnenuntergang;
            set
            {
                if (sonnenuntergang != value)
                {
                    sonnenuntergang = value;
                    RaisePropertyChanged(nameof(Sonnenuntergang));
                }
            }
        }

        public double Luftfeuchtigkeit
        {
            get => luftfeuchtigkeit;
            set
            {
                if (luftfeuchtigkeit != value)
                {
                    luftfeuchtigkeit = value;
                    RaisePropertyChanged(nameof(Luftfeuchtigkeit));
                }
            }
        }

        public double Luftqualitaet
        {
            get => luftqualitaet;
            set
            {
                if (luftqualitaet != value)
                {
                    luftqualitaet = value;
                    RaisePropertyChanged(nameof(Luftqualitaet));
                }
            }
        }

        public double Luftdruck
        {
            get => luftdruck;
            set
            {
                if (luftdruck != value)
                {
                    luftdruck = value;
                    RaisePropertyChanged(nameof(Luftdruck));
                }
            }
        }

        public List<Wettervorhersage> WettervorhersageList
        {
            get => wettervorhersageList;
            set
            {
                if (wettervorhersageList != value)
                {
                    wettervorhersageList = value;
                    RaisePropertyChanged(nameof(WettervorhersageList));
                }
            }
        }

        public Wetter()
        {
            WeatherAppResponse weatherAppResponse = GetResponseVonAPI();
            OwnWeatherAPIResponse ownWeatherAPIResponse = GetResponseVonEigenerAPIOutside();
            WerteVonEigenerAPIZuweisen(ownWeatherAPIResponse);
            WerteVonOpenAPIZuweisen(weatherAppResponse);
            DatenInListeHinzufuegen(weatherAppResponse);
        }

        public static OwnWeatherAPIResponse GetResponseVonEigenerAPIOutside()
        {
            HttpClient httpClient = new HttpClient();
            string requestURI = $"http://localhost/weatherAPI.php?outCurrent=outdoor_area";
            HttpResponseMessage httpResponse = httpClient.GetAsync(requestURI).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            OwnWeatherAPIResponse ownWeatherAPIResponse = JsonConvert.DeserializeObject<OwnWeatherAPIResponse>(response);
            return ownWeatherAPIResponse;
        }

        public static WeatherAppResponse GetResponseVonAPI()
        {
            string apiKey = "9d0c364bbd8746599fe192740251101";
            HttpClient httpClient = new HttpClient();
            string requestURI = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q=Mainz&days=5&aqi=yes&alerts=no&lang=de";
            HttpResponseMessage httpResponse = httpClient.GetAsync(requestURI).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            WeatherAppResponse weatherAppResponse = JsonConvert.DeserializeObject<WeatherAppResponse>(response);
            return weatherAppResponse;
        }

        private void WerteVonEigenerAPIZuweisen(OwnWeatherAPIResponse ownWeatherAPIResponse)
        {
            if (ownWeatherAPIResponse != null)
            {
                this.Temperatur = ownWeatherAPIResponse.temperature;
                this.Luftfeuchtigkeit = ownWeatherAPIResponse.humidity;
                this.Luftdruck = ownWeatherAPIResponse.pressure;
            }
        }

        private void WerteVonOpenAPIZuweisen(WeatherAppResponse weatherAppResponse)
        {
            this.TemperaturGefuehlt = weatherAppResponse.current.feelslike_c;
            this.Status = weatherAppResponse.current.condition.text;
            this.StatusCode = weatherAppResponse.current.condition.code;
            this.Ort = weatherAppResponse.location.name;

            this.Uv = weatherAppResponse.current.uv;
            this.Windstatus = weatherAppResponse.current.wind_kph;
            this.Windrichtung = weatherAppResponse.current.wind_dir;

            this.Sonnenaufgang = weatherAppResponse.forecast.forecastday[0].astro.sunrise;
            this.Sonnenuntergang = weatherAppResponse.forecast.forecastday[0].astro.sunset;

            this.Luftqualitaet = weatherAppResponse.current.air_quality.no2;
        }

        private void DatenInListeHinzufuegen(WeatherAppResponse weatherAppResponse)
        {
            foreach (Forecastday forecastday in weatherAppResponse.forecast.forecastday)
            {
                WettervorhersageList.Add(new Wettervorhersage() { Datum = forecastday.date, MinTemperatur = forecastday.day.mintemp_c, MaxTemperatur = forecastday.day.maxtemp_c, StatusCode = forecastday.day.condition.code });
            }
        }
    }
}
