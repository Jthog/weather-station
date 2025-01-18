using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wetterstation_Projekt.API;
using Wetterstation_Projekt.Assist;

namespace Wetterstation_Projekt.Model
{
    public class IndoorStation : NotifyPropertyChanged
    {
        private double temperatur;
        private double luftfeuchtigkeit;
        private double luftqualitaet;

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

        public IndoorStation()
        {
            OwnWeatherInsideAPIResponse ownWeatherInsideAPIResponse = GetResponseVonEigenerAPIInside();
            WerteVonEigenerAPIInsideZuweisen(ownWeatherInsideAPIResponse);
        }

        public static OwnWeatherInsideAPIResponse GetResponseVonEigenerAPIInside()
        {
            HttpClient httpClient = new HttpClient();
            string requestURI = $"http://localhost/weatherAPI.php?inCurrent=main_entrance";
            HttpResponseMessage httpResponse = httpClient.GetAsync(requestURI).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            OwnWeatherInsideAPIResponse ownWeatherAPIInsideResponse = JsonConvert.DeserializeObject<OwnWeatherInsideAPIResponse>(response);
            return ownWeatherAPIInsideResponse;
        }

        private void WerteVonEigenerAPIInsideZuweisen(OwnWeatherInsideAPIResponse ownWeatherInsideAPIResponse)
        {
            if (ownWeatherInsideAPIResponse != null)
            {
                this.Temperatur = ownWeatherInsideAPIResponse.temperature;
                this.Luftfeuchtigkeit = ownWeatherInsideAPIResponse.humidity;
                this.Luftqualitaet = ownWeatherInsideAPIResponse.co2;
            }
        }
    }
}
