using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public Brand GetById(int brandId)
        {
            return _brandDal.Get(c=>c.BrandId == brandId);
        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }
    }
}
