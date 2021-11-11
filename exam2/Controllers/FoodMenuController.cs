using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace exam2.Controllers
{
    public class FoodMenuController : Controller
    {
        // GET: First
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FoodMenu(string searchCoin)
        {
            // подготвяме променливи
            string desc;
            string name;
            
            string price;
            double volume24h;
            double percentChange1h;
            double percentChange24h;
            double percentChange7d;

            string descrip;
            string pict;


            var responseString = "";
            string api_key = "8c35bdc4-ae37-44f8-8279-3a33255a0ca6";
            var responseString2 = "";

            // адрес към който изпращаме заявка
            if (searchCoin == "" || searchCoin == null) searchCoin = "BTC";
            string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol="+searchCoin+"&convert=EUR&CMC_PRO_API_KEY="+ api_key;

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            responseString = webClient.DownloadString(url);

            JObject obj = JObject.Parse(responseString);
            Console.WriteLine(obj.ToString());
            responseString = obj.ToString();
            name = (string)obj["data"][searchCoin]["name"];
            desc = (string)obj["data"][searchCoin]["date_added"];
            
            price= (string)obj["data"][searchCoin]["quote"]["EUR"]["price"];
            
            
            
            
            
            
            volume24h = (double)obj["data"][searchCoin]["quote"]["EUR"]["volume_change_24h"];
            volume24h = Math.Round(volume24h, 2);
            percentChange1h=(double)obj["data"][searchCoin]["quote"]["EUR"]["percent_change_1h"];
            percentChange1h = Math.Round(percentChange1h, 2);
            percentChange24h=(double)obj["data"][searchCoin]["quote"]["EUR"]["percent_change_24h"];
            percentChange24h = Math.Round(percentChange24h);
            percentChange7d=(double)obj["data"][searchCoin]["quote"]["EUR"]["percent_change_7d"];
            percentChange7d = Math.Round(percentChange7d, 2);


            string url2 = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/info?symbol="+searchCoin+"&CMC_PRO_API_KEY=8c35bdc4-ae37-44f8-8279-3a33255a0ca6";

            var webClient2 = new WebClient();
            webClient2.Encoding = Encoding.UTF8;
            responseString2 = webClient2.DownloadString(url2);

            JObject obj2 = JObject.Parse(responseString2);
            Console.WriteLine(obj2.ToString());
            responseString2 = obj2.ToString();


            descrip = (string)obj2["data"][searchCoin]["description"];
            pict = (string)obj2["data"][searchCoin]["logo"];







            ViewBag.name = name;
            ViewBag.descrip = desc;
           
            ViewBag.price = price;
            ViewBag.per1h = percentChange1h;
            ViewBag.per24h = percentChange24h;
            ViewBag.per7d = percentChange7d;

            ViewBag.ddd = descrip;
            ViewBag.pict = pict;

            return View();
        }

    }
}