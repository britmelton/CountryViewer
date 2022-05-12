using CountryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CountryApp.Controllers
{
    public class CountryController : Controller
    {
        HttpClient client = new HttpClient();
        public IActionResult Index()
        {
            var endpoint = "https://restcountries.com/v3.1/all";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result);
            IEnumerable<Country> countries = json.Select(p => new Country
            {
                Id = (string?)p["ccn3"],
                Name = (string?)p["name"]["common"],
                Region = (string?)p["subregion"],
                Population = (int)p["population"],
                //Capital = (string)p["capital"][0].ToString()
            });
            return View(countries);
        }
        public IActionResult ViewCountry(string id)
        {
            var endpoint = $"https://restcountries.com/v3.1/alpha/{id}";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result);
            ViewBag.ID = (string?)json[0]["ccn3"];
            ViewBag.Name = (string?)json[0]["name"]["common"];
            ViewBag.Region = (string?)json[0]["subregion"];
            Country countryView = new Country()
            {
                Id = ViewBag.Id,
                Name = ViewBag.Name,
                Region = ViewBag.Region,
                Population = (int)json[0]["population"],
                Flag = (string)json[0]["flags"]["png"]
            };
            
            return View(countryView);
        }
    }
}
