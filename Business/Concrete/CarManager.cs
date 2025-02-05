using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _CarDal;
        IBrandService _brandService;

        public CarManager(ICarDal carDal,IBrandService brandService)
        {
            _CarDal = carDal;
            _brandService = brandService;
        }

        public async Task<IDataResult<List<Car>>> GetAllAsync(int? brandId = null, int? colorId = null)
        {
            try
            {
                var query = _CarDal.GetAll();

                if (brandId.HasValue)
                    query = query.Where(c => c.BrandId == brandId.Value).ToList();

                if (colorId.HasValue)
                    query = query.Where(c => c.ColorId == colorId.Value).ToList();

                return await Task.FromResult(new SuccessDataResult<List<Car>>(query, Messages.ProductListed));
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<Car>>($"Araçlar listelenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IDataResult<Car>> GetByIdAsync(int carId)
        {
            try
            {
                var car = _CarDal.Get(c => c.Id == carId);
                if (car == null)
                    return new ErrorDataResult<Car>(Messages.CarNotFound);

                return await Task.FromResult(new SuccessDataResult<Car>(car));
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Car>($"Araç getirilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> AddAsync(Car car)
        {
            try
            {
                IResult result = BusinessRules.Run(
                    CheckIfCarNameExists(car.Name),
                    CheckIfProductCountOfCategoryCorrect(car.BrandId),
                    CheckIfBrandLimitExceded()
                );

                if (result != null)
                    return result;

                _CarDal.Add(car);
                return await Task.FromResult(new SuccessResult(Messages.CarAdded));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.InnerException != null 
                    ? $"Araç eklenirken bir hata oluştu: {ex.Message} - İç Hata: {ex.InnerException.Message}"
                    : $"Araç eklenirken bir hata oluştu: {ex.Message}";
                return new ErrorResult(errorMessage);
            }
        }

        public async Task<IResult> UpdateAsync(Car car)
        {
            try
            {
                var existingCar = _CarDal.Get(c => c.Id == car.Id);
                if (existingCar == null)
                    return new ErrorResult(Messages.CarNotFound);

                _CarDal.Update(car);
                return await Task.FromResult(new SuccessResult(Messages.CarUpdated));
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Araç güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            try
            {
                var car = _CarDal.Get(c => c.Id == id);
                if (car == null)
                    return new ErrorResult(Messages.CarNotFound);

                _CarDal.Delete(car);
                return await Task.FromResult(new SuccessResult(Messages.CarDeleted));
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Araç silinirken bir hata oluştu: {ex.Message}");
            }
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            try
            {
                //business kodları
                IResult result = BusinessRules.Run(CheckIfCarNameExists(car.Name),
                     CheckIfProductCountOfCategoryCorrect(car.BrandId), CheckIfBrandLimitExceded());

                if (result != null)
                {
                    return result;
                }
                _CarDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Araç eklenirken bir hata oluştu: {ex.Message}");
            }
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Car>> GetAllByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll(p => p.BrandId == id));
        }
        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll(p => p.ColorId == id));
        }
        [CacheAspect]
        [PerformanceAspect(5)]

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_CarDal.Get(p => p.Id == carId));
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_CarDal.GetAll(p => p.Price >= min && p.Price <= max));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_CarDal.GetCarDetails());
        }
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]

        public IResult Update(Car car)
        {
            var result = _CarDal.GetAll(p => p.BrandId == car.BrandId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.CarCountOfCategoryError);
            }
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int brandId)
        {
            var result = _CarDal.GetAll(p => p.BrandId == brandId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.CarCountOfCategoryError);
            }

            return new SuccessResult();


        }
        private IResult CheckIfCarNameExists(string carName)
        {
            var result = _CarDal.GetAll(p => p.Name == carName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.BrandLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            if (car.Price < 10)
            {
                throw new Exception("");
            }
            Add(car);
            return null;
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_CarDal.GetCarDetails(c => c.CarId == id));
        }
    }
}
