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
    public class EfCarDal : EfEntityRepositoryBase<Car, CarListsContext>, ICarDal
    {
        //public List<CarDetailDto> GetCarDetails()
        //{
        //    using(CarListsContext context = new CarListsContext())
        //    {
        //        var result = from c in context.Cars
        //                     join co in context.Colors
        //                     on c.ColorId equals co.ColorId
        //                     join b in context.Brands
        //                     on c.BrandId equals b.BrandId
                            
                           
        //                     select new CarDetailDto
        //                     {
        //                         CarId = c.CarId,
        //                         BrandId = b.BrandId,
        //                         BrandName = b.BrandName,
        //                         ColorId = c.ColorId,
        //                         ColorName = co.ColorName,
        //                         CarName = c.CarName,
        //                         DailyPrice = c.DailyPrice,                         
        //                     };
        //        return result.ToList();
        //    }
        //}

        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarListsContext context = new CarListsContext())
            {
                var result = from d in context.Cars
                             join b in context.Brands
                             on d.CarId equals b.BrandId
                             join c in context.Colors
                             on d.ColorId equals c.ColorId
                             join i in context.CarImages
                             on d.CarId equals i.CarId

                             select new CarDetailDto
                             {
                                 CarId = d.CarId,
                                 Description = d.Description,
                                 BrandName = b.BrandName,
                                 ColorName = c.ColorName,
                                 ModelYear = d.ModelYear,
                                 DailyPrice = d.DailyPrice,
                                 BrandId = b.BrandId,
                                 ColorId = c.ColorId,
                                 CarName = d.CarName,
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
