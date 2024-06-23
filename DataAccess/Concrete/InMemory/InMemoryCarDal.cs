using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> { 
                new Car() {BrandId = 1 , CarId = 1  , ColorId = 1 , DailyPrice = 100 , Description = "New Car" , ModelYear = 2020},
                new Car() {BrandId = 2 , CarId = 2  , ColorId = 2 , DailyPrice = 200 , Description = "New Car" , ModelYear = 2021},
                new Car() {BrandId = 3 , CarId = 3  , ColorId = 3 , DailyPrice = 300 , Description = "New Car" , ModelYear = 2022},
                new Car() {BrandId = 4 , CarId = 4  , ColorId = 4 , DailyPrice = 400 , Description = "New Car" , ModelYear = 2023}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = null;
            foreach (var c  in _cars)
            {
                if (car.CarId == c.CarId)
                { 
                    carToDelete = c;
                    break; 
                }
            }
            carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllByBrand(int BrandId)
        {
            return _cars.Where(c => c.BrandId == BrandId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.BrandId = car.BrandId;

        }
    }
}
