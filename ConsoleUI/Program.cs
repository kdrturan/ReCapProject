using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.InMemory;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using Business.Abstract;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //manuelAdd();
            CarManager carManager = new CarManager(new EfCarDal(), new BrandManager(new EfBrandDal()));
            var result = carManager.GetCarDetails();
            if (result.IsSuccess == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Owner:" + car.CarName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }

        private static void manuelAdd()
        {
            Car car = new Car { ColorId = 3, BrandId = 1, DailyPrice = 1500, ModelYear = 2022, Description = "Sahibinden" };
            EfCarDal carManager = new EfCarDal();
            carManager.Add(car);
        }
    }
}
