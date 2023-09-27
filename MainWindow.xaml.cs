using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WeatherApp;

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
        UpdateUI("Hanau");

    }

    public void UpdateUI(string city)
    {

        WeatherMapResponse result = GetWeatherData(city);
        string finalImage = "sun.png";
        _ = result.weather[0].main.ToLower();

        if (result.weather[0].main.ToLower().Contains("cloud"))
        {
            finalImage = "cloud.png";
        }
        else if (result.weather[0].main.ToLower().Contains("rain"))
        {
            finalImage = "rain.png";
        }
        else if (result.weather[0].main.ToLower().Contains("snow"))
        {
            finalImage = "snow.png";
        }
        else if (result.weather[0].main.ToLower().Contains("sun"))
        {
            finalImage = "sun.png";
        }




        backgroundImage.ImageSource = new BitmapImage(new Uri("images/" + finalImage, UriKind.Relative));

        labelTemp.Content = result.main.temp.ToString("F1") + "°C";
        labeInfo.Content = result.weather[0].description;
    }

    public WeatherMapResponse GetWeatherData(string city)


    {

        HttpClient httpClient = new();
        string finaluri = requestUri + "?q=" + city + "&appid=" + apikey + "&units=metric";
        HttpResponseMessage httpresponse = httpClient.GetAsync(finaluri).Result;
        string response = httpresponse.Content.ReadAsStringAsync().Result;
        WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);
        return weatherMapResponse;
    }

    private void buttonSearch_Click(object sender, RoutedEventArgs e)
    {
        string query = textBoxCity.Text;
        UpdateUI(query);
    }

    private void textBoxCity_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}


