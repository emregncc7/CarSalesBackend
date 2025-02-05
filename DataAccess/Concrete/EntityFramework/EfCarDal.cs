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

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarSalesContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarSalesContext context = new CarSalesContext())
            {
                var result = from c in context.Cars
                            join b in context.Brands
                            on c.BrandId equals b.Id
                            join co in context.Colors
                            on c.ColorId equals co.Id
                            select new CarDetailDto
                            {
                                CarId = c.Id,
                                BrandName = b.Name,
                                ColorName = co.Name,
                                DailyPrice = c.DailyPrice,
                                Description = c.Description,
                                ModelYear = c.ModelYear.ToString()
                            };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarSalesContext context = new CarSalesContext())
            {
                var result = from d in context.Cars
                             join b in context.Brands
                             on d.BrandId equals b.Id
                             join c in context.Colors
                             on d.ColorId equals c.Id
                             join i in context.CarImages
                             on d.Id equals i.CarId
                             select new CarDetailDto
                             {
                                 CarId = d.Id,
                                 Description = d.Description,
                                 BrandName = b.Name,
                                 ColorName = c.Name,
                                 ModelYear = d.ModelYear.ToString(),
                                 DailyPrice = d.DailyPrice,
                                 BrandId = b.Id,
                                 ColorId = c.Id,
                                 CarName = d.Name,
                                 ImagePath = i.ImagePath,
                                 Email = d.Email,
                                 Telephone = d.Telephone,      
                                 Instagram = d.Instagram
                             };

                if (filter == null)
                {
                    return result.ToList();
                }
                else
                {
                    return result.Where(filter).ToList();
                }
            }
        }
    }
}
