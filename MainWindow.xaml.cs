using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly string apikey = "08f723768229d2538d67a045446ed88a";
        private readonly string requestUri = "https://api.openweathermap.org/data/2.5/weather";

        public MainWindow()
        {
            InitializeComponent();

            HttpClient httpClient = new HttpClient();

            var city = "Hanau";
            var finaluri = requestUri + "?q=" + city + "&appid=" + apikey + "&units=metric";
            HttpResponseMessage httpresponse = httpClient.GetAsync(finaluri).Result;
            string response = httpresponse.Content.ReadAsStringAsync().Result;
            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);
            Console.WriteLine(weatherMapResponse);
        }
    }
}

