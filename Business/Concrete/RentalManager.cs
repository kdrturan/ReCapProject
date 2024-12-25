using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal  _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(IsRented(rental));

            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.SuccessfullyRented);
        }

        public IResult Delete(Rental rental)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetail());
        }

        public IResult Update(Rental rental)
        {
            throw new NotImplementedException();
        }



        //Rules

        public IResult IsRented(Rental rental)
        {
            using (CarDatabaseContext context = new CarDatabaseContext())
            {
                var rentInfo = context.Rentals
                                .Where(r => r.CarId == rental.CarId)
                                .Select(r => new { r.RentDate, r.ReturnDate }).ToList();

                foreach (var r in rentInfo)
                {

                    if (rental.RentDate < r.RentDate && rental.ReturnDate > r.ReturnDate)
                    {
                        return new ErrorResult(r.RentDate.ToString() + " tarihinden" + r.ReturnDate.ToString() + " tarihine kadar araba kiralanmış durumda.");
                    }
                    else if (rental.RentDate < r.RentDate && rental.ReturnDate < r.ReturnDate && rental.ReturnDate > r.RentDate)
                    {
                        return new ErrorResult(r.RentDate.ToString() + " tarihinden" + r.ReturnDate.ToString() + " tarihine kadar araba kiralanmış durumda.");
                    }
                    else if (r.RentDate == rental.RentDate ||r.RentDate == rental.ReturnDate || r.RentDate == rental.ReturnDate)
                    {
                        return new ErrorResult(r.RentDate.ToString() + " tarihinden" + r.ReturnDate.ToString() + " tarihine kadar araba kiralanmış durumda.");
                    }
                    else
                    {
                        continue;
                    }    

                }
                return new SuccessResult(Messages.Rentable);
            }

        }


    }
}
