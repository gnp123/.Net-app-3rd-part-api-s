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
    public class CocktailsController : Controller
    {
        // GET: Cocktails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cocktails(string searchName)
        {
            
            var responseString = "";
            var response2String = "";
            string api2key = "609c4e78a4fe474993205658edd7e2e189c4acf693e4d2a2790cba1d4a8fd716";

            
            if (searchName == "" || searchName == null) searchName = "margarita";
            string url = "https://www.thecocktaildb.com/api/json/v1/1/search.php?s=" + searchName;

            
            var webClient = new WebClient();
            responseString = webClient.DownloadString(url);


            
            JObject obj = JObject.Parse(responseString);
            if (obj["drinks"].ToString() != null)
            {
                responseString = obj.ToString();
                string drinkName = obj["drinks"][0]["strDrink"].ToString();
                string strInstructions = obj["drinks"][0]["strInstructions"].ToString();
                ViewBag.drinkName = drinkName;
                ViewBag.strInstructions = strInstructions;
            }
                     



            string url2 = "https://serpapi.com/search.json?q=" + searchName + "%20cocktail&tbm=isch&ijn=0&api_key=" + api2key;

            var webClient2 = new WebClient();
            webClient2.Encoding = Encoding.UTF8;
            response2String = webClient2.DownloadString(url2);

            
            JObject obj2 = JObject.Parse(response2String);
            Console.WriteLine(obj2.ToString());
            response2String = obj2.ToString();

            ViewBag.searchName = searchName;
            ViewBag.response = responseString;
            ViewBag.image = obj2["images_results"][0]["thumbnail"];





            return View();
        }
    }
}