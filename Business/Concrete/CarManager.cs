using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcers.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal  _carDal;
        IBrandService _brandService;
        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p=>p.CarId == id));
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarListed);
        }
        
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(P => P.BrandId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandIdDto(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsByBrandIdDto(id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorIdDto(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsByColorIdDto(id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id)); 
        }

        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("admin,car.add")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckCarLimit());

            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult("Araba eklendi");
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsById(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsById(id));
        }


        //Rules
        private IResult CheckCarLimit()
        {
            var result= _brandService.GetAll();
            if (result.Data.Count > 20)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            throw new NotImplementedException();
        }


    }
}
