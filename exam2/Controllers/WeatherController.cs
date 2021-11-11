using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace exam2.Controllers
{
    public class WeatherController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: Weather
        public ActionResult Weather(string citySearch)
        {
            double tempC;
            var responseString = "";
            string apiKey = "ea870c5eccbb1247c9d398bc7cac6644";
            var response2String = "";
            string api2key = "609c4e78a4fe474993205658edd7e2e189c4acf693e4d2a2790cba1d4a8fd716";

            if (citySearch == "" || citySearch == null) citySearch = "Sofia";
            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + citySearch + "&appid=" + apiKey;

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            responseString = webClient.DownloadString(url);

            JObject obj = JObject.Parse(responseString);
            Console.WriteLine(obj.ToString());
            responseString = obj.ToString();
            tempC = (double)obj["main"]["temp"];
            tempC = tempC - 273.15;
            tempC = Math.Round(tempC, 2);

            string url2 = "https://serpapi.com/search.json?q=" + obj["weather"][0]["description"] + "%20weather&tbm=isch&ijn=0&api_key=" + api2key;

            var webClient2 = new WebClient();
            webClient2.Encoding = Encoding.UTF8;
            response2String = webClient2.DownloadString(url2);

            // трансформация на JSON в обект
            JObject obj2 = JObject.Parse(response2String);
            Console.WriteLine(obj2.ToString());
            response2String = obj2.ToString();


            ViewBag.temperature = tempC;
            ViewBag.cityname = obj["name"];
            ViewBag.image = obj2["images_results"][0]["thumbnail"];

            return View();
        }
    }
}