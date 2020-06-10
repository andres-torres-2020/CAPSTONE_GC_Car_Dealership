using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CAPSTONE_GC_Car_Dealership.Models
{
    public class CarDAL
    {
        public CarDAL()
        {

        }
        private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325");
            return client;
        }
        public async Task<List<Car>> GetCars(int? SearchId)
        {
            List<Car> cars = null;
            try
            {
                string resource = "/api/Car/";
                if (SearchId != null)
                {
                    resource += SearchId.ToString();
                }
                var client = GetClient();
                var response = await client.GetAsync(resource);
                var carJSON = await response.Content.ReadAsStringAsync();
                if (SearchId != null)
                {
                    Class1 car = JsonSerializer.Deserialize<Class1>(carJSON);
                    cars = new List<Car>();
                    Car copyCar = new Car();
                    copyCar.Make = car.make;
                    copyCar.Model = car.model;
                    copyCar.Year = car.year;
                    copyCar.Color = car.color;
                    cars.Add(copyCar);
                }
                else
                {
                    //CarRoot carRoot = JsonSerializer.Deserialize<CarRoot>(carJSON);
                    Class1[] carRoot = JsonSerializer.Deserialize<Class1[]>(carJSON);
                    if (carRoot != null && carRoot.Length > 0)
                    {
                        cars = new List<Car>();
                        for (int i = 0; i < carRoot.Length; i++)
                        {
                            Car copyCar = new Car();
                            copyCar.Make = carRoot[i].make;
                            copyCar.Model = carRoot[i].model;
                            copyCar.Year = carRoot[i].year;
                            copyCar.Color = carRoot[i].color;
                            cars.Add(copyCar);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                cars = new List<Car>();
                Car car = new Car();
                car.Make = e.Message;
                cars.Add(car);
            }
            return cars;
        }
    }
}
