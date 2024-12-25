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
    public class EfCustomerDal:EfEntityRepositoryBase<Customer,CarDatabaseContext>,ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetail ()
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var result = from c in context.Customers
                             join u in context.Users on c.UserId equals u.Id
                             select new CustomerDetailDto
                             {
                                 Id = c.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = c.CompanyName
                             };
                return result.ToList();
            }
        }
    }
}
