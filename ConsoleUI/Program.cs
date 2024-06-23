using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Concrete.InMemory;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car {ColorId = 3, BrandId = 1, DailyPrice = 1500, ModelYear = 2022, Description = "Sahibinden" };
            EfCarDal carManager = new EfCarDal();
            carManager.Add(car);
        }
    }
}
