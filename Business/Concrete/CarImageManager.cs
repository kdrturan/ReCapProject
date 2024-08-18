using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;


        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImageDal)
        {
            var result = BusinessRules.Run(CheckCarImagesLimit(carImageDal));
            if(result == null)
            {
                carImageDal.Date = DateTime.Now;
                _carImageDal.Add(carImageDal);
                return new SuccessResult(Messages.CarImageAdded);
            }
            return result;
        }

        public IResult Delete(CarImage carImageDal)
        {
             _carImageDal.Delete(carImageDal);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(result);
        }

        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            var result = BusinessRules.Run(CheckCarImageExixst(id));
            if(result != null)
            {
                return new ErrorDataResult<List<CarImage>>(GetDefaultImage(id).Data);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == id));
        }

        public IResult Update(CarImage carImageDal)
        {
            _carImageDal.Update(carImageDal);
            return new SuccessResult(Messages.CarImageUpdated);
        }



        //Rules
        private IResult CheckCarImagesLimit(CarImage carImage)
        {
            var result = GetByCarId(carImage.CarId).Data.Count;
            if(result <= 5)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageLimitExceeded);
        }

        private IResult CheckCarImageExixst(int  id)
        {
            var result = _carImageDal.GetAll(p => p.CarId == id).Count;
            if (result == 0)
            {
                return new ErrorResult(Messages.CarImageNotExist);
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> GetDefaultImage(int id)
        {
            List<CarImage> carImage = new List<CarImage> { new CarImage {CarId = id, ImagePath = "C:\\Users\\atura\\OneDrive\\Masaüstü\\DefaultImage.jpg" } };
            return new SuccessDataResult<List<CarImage>>(carImage);
        }

    }
}
