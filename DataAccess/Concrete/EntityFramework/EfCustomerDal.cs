using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarSalesContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (CarSalesContext context = new CarSalesContext())
            {
                var result = from c in context.Customers
                            join u in context.Users
                            on c.UserId equals u.Id
                            select new CustomerDetailDto
                            {
                                Id = c.Id,
                                UserId = u.Id,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                CompanyName = c.CompanyName
                            };
                return result.ToList();
            }
        }
    }
}
