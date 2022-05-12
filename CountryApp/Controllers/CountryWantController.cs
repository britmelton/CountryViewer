using CountryApp.Data;
using CountryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CountryApp.Controllers
{
    public class CountryWantController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CountryWantController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CountryWant> objCountryWantList = _db.TravelList;
            return View(objCountryWantList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string Id)
        {
            var client = new HttpClient();
            var endpoint = $"https://restcountries.com/v3.1/alpha/{Id}";
            var result = client.GetStringAsync(endpoint).Result;
            var json = JArray.Parse(result);
            CountryWant country = new CountryWant()
            {
                Id = (string?)json[0]["ccn3"],
                Name = (string?)json[0]["name"]["common"],
                Region = (string?)json[0]["subregion"]
            };
            if (ModelState.IsValid)
            {
                _db.TravelList.Add(country);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Add(CountryWant country)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.TravelList.Add(country);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(country);
        //}

        public IActionResult Edit(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }
            var countryFromDb = _db.TravelList.Find(id);
            if (countryFromDb == null)
            {
                return NotFound();
            }
            return View(countryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CountryWant country)
        {
            if (ModelState.IsValid)
            {
                _db.TravelList.Update(country);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);

        }
        public IActionResult Delete(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }
            var countryFromDb = _db.TravelList.Find(id);
            if (countryFromDb == null)
            {
                return NotFound();
            }
            return View(countryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? id)
        {
            var obj = _db.TravelList.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.TravelList.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
