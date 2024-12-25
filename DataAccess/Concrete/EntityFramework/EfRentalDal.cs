using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,CarDatabaseContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetail()
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.BrandId
                             join b in context.Brands on r.CarId equals b.BrandId
                             join u in context.Users on r.CustomerId equals u.Id
                             select new RentalDetailDto
                             {
                                 Id = r.Id,
                                 CarId = c.CarId,
                                 BrandName = b.BrandName,
                                 CustomerFirstName = u.FirstName ,
                                 CustomerLastName = u.LastName ,
                                 CustomerId = u.Id,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };
                return result.ToList();

            }
        }
    }
}
