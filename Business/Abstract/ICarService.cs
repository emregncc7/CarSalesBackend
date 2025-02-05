using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
   public interface ICarService
    {
        Task<IDataResult<List<Car>>> GetAllAsync(int? brandId = null, int? colorId = null);
        Task<IDataResult<Car>> GetByIdAsync(int carId);
        Task<IResult> AddAsync(Car car);
        Task<IResult> UpdateAsync(Car car);
        Task<IResult> DeleteAsync(int id);

        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetAllByBrandId(int id);
        IDataResult<List<Car>> GetAllByColorId(int id);
        IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int id);
        IDataResult<Car> GetById(int carId);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult AddTransactionalTest(Car car);
    }
}
