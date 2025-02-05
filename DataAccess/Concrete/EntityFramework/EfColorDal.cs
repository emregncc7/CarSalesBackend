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
    public class EfColorDal : EfEntityRepositoryBase<Color, CarSalesContext>, IColorDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarSalesContext context = new CarSalesContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 BrandId = b.Id,
                                 BrandName = b.Name,
                                 ColorId = c.ColorId,
                                 ColorName = co.Name,
                                 CarName = c.Name,
                                 DailyPrice = c.DailyPrice,
                             };
                return result.ToList();
            }
        }
    }
}
