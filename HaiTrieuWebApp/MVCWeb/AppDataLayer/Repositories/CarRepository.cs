using System.Collections.Generic;
using System.Linq;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.Repositories
{
    public class CarRepository
    {
        public static List<Car> AllCars()
        {
            var db = new DbAppContext();
            var list = db.Cars.ToList();
            return list;
        }
    }
}