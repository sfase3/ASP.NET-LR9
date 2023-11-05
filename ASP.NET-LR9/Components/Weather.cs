using ASP.NET_LR9.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ASP.NET_LR9.Components
{
    public class Weather : ViewComponent
    {
        public IViewComponentResult Invoke(string cityName)
        {

            string appId = "76faaf73138031718f4fc236148368d6";
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                RootObject weatherInfo = JsonConvert.DeserializeObject<RootObject>(json);

                ResultViewModel rslt = new ResultViewModel();

                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = $"http://openweathermap.org/img/w/" + weatherInfo.weather[0].icon + ".png";

                return new HtmlContentViewComponentResult(
                new HtmlString(
                    "<div class=\"weather-forecast\">" +
                    "<h2>Weather Forecast</h2>" +
                    "<div class=\"weather-icon\">" +
                    $"<img id=\"imgWeatherIconUrl\" src=\"{rslt.WeatherIcon}\" title=\"Weather Icon\" />" +
                    "</div>" +
                    "<div class=\"weather-details\">" +
                    $"<strong>City:</strong> <span id=\"lblCity\">{rslt.City}</span>, <span id=\"lblCountry\">{rslt.Country}</span><br />" +
                    $"<strong>Latitude:</strong> <label id=\"lblLat\">{rslt.Lat}</label><br />" +
                    $"<strong>Longitude:</strong> <label id=\"lblLon\">{rslt.Lon}</label><br />" +
                    $"<strong>Temperature:</strong> <label id=\"lblTemp\">{rslt.Temp}°F</label><br />" +
                    $"<strong>Temperature (Min):</strong> <label id=\"lblTempMin\">{rslt.TempMin}°F</label><br />" +
                    $"<strong>Temperature (Max):</strong> <label id=\"lblTempMax\">{rslt.TempMax}°F</label><br />" +
                    $"<strong>Description:</strong> <label id=\"lblDescription\">{rslt.Description}</label><br />" +
                    $"<strong>Humidity:</strong> <label id=\"lblHumidity\">{rslt.Humidity}%</label><br />" +
                    $"<strong>Temperature (Feels Like):</strong> <label id=\"lblTempFeelsLike\">{rslt.TempFeelsLike}°F</label><br />" +
                    "</div>" +
                    "</div>"
                )
            );

            }
        }
    }
}