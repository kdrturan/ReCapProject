using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarDatabaseContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join cl in context.Colors on c.ColorId equals cl.ColorId
                             join im in context.CarImages on c.CarId equals im.CarId into carImagesGroup
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = carImagesGroup.Select(im => im.ImagePath).ToList()
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsById(int id)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join cl in context.Colors on c.ColorId equals cl.ColorId
                             join im in context.CarImages on c.CarId equals im.CarId into carImagesGroup
                             where c.CarId == id
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = carImagesGroup.Select(im => im.ImagePath).ToList()
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarsByBrandIdDto(int id)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join cl in context.Colors on c.ColorId equals cl.ColorId
                             where id == b.BrandId
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarsByColorIdDto(int id)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join cl in context.Colors on c.ColorId equals cl.ColorId
                             where cl.ColorId == id  
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 DailyPrice = c.DailyPrice
                             };
                return result.ToList();
            }
        }
    }
}
