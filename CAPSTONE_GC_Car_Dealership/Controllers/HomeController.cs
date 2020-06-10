using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CAPSTONE_GC_Car_Dealership.Models;

namespace CAPSTONE_GC_Car_Dealership.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarDAL _DAL;

        public HomeController()
        {
            _DAL = new CarDAL();
        }

        public IActionResult Index()
        {
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> GetCars(int? SearchId, string? SearchMake, string? SearchModel, int? SearchYear, string? SearchColor)
        {
            List<Car> cars = null;
            try
            {
                cars = await _DAL.GetCars(SearchId);
                if (cars != null && SearchMake != null && SearchMake != "")
                {
                    cars = cars.FindAll(x => x.Make == SearchMake);
                }

                if (cars != null && SearchModel != null && SearchModel != "")
                {
                    cars = cars.FindAll(x => x.Model == SearchModel);
                }

                if (cars != null && SearchYear != null)
                {
                    cars = cars.FindAll(x => x.Year == SearchYear);
                }

                if (cars != null && SearchColor != null && SearchColor != "")
                {
                    cars = cars.FindAll(x => x.Color == SearchColor);
                }
            }
            catch (Exception e)
            {
                ViewBag.Result = e.Message;
            }
            return View("Index", cars);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(string? CarMake, string? CarModel, int? CarYear, string? CarColor)
        {
            Car newCar = new Car();
            try
            {
                newCar.Make = CarMake;
                newCar.Model = CarModel;
                newCar.Year = (int)CarYear;
                newCar.Color = CarColor;
            }
            catch
            {
            }
            List<Car> cars = new List<Car>();
            cars.Add(newCar);
            return View("Index", cars);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
