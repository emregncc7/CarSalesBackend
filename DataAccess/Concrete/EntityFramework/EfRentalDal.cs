using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarSalesContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarSalesContext context = new CarSalesContext())
            {
                var result = from r in context.Rentals
                            join c in context.Cars
                            on r.CarId equals c.Id
                            join cu in context.Customers
                            on r.CustomerId equals cu.Id
                            join u in context.Users
                            on cu.UserId equals u.Id
                            select new RentalDetailDto
                            {
                                Id = r.Id,
                                CarId = c.Id,
                                CustomerId = cu.Id,
                                RentDate = r.RentDate,
                                ReturnDate = r.ReturnDate,
                                CustomerName = u.FirstName + " " + u.LastName
                            };
                return result.ToList();
            }
        }
    }
} 