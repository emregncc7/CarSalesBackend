using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
            
            // Kullanıcı eklendikten sonra rol ataması yapılıyor
            using (var context = new DataAccess.Concrete.EntityFramework.CarSalesContext())
            {
                // Admin rolü yoksa oluştur
                var adminRole = context.OperationClaims.FirstOrDefault(x => x.Name == "Admin");
                if (adminRole == null)
                {
                    adminRole = new OperationClaim { Name = "Admin" };
                    context.OperationClaims.Add(adminRole);
                    context.SaveChanges();
                }

                // User rolü yoksa oluştur
                var userRole = context.OperationClaims.FirstOrDefault(x => x.Name == "User");
                if (userRole == null)
                {
                    userRole = new OperationClaim { Name = "User" };
                    context.OperationClaims.Add(userRole);
                    context.SaveChanges();
                }

                // Kullanıcıya rol ata
                var roleToAssign = user.Role == "Admin" ? adminRole : userRole;
                var userOperationClaim = new UserOperationClaim
                {
                    UserId = user.Id,
                    OperationClaimId = roleToAssign.Id
                };
                context.UserOperationClaims.Add(userOperationClaim);
                context.SaveChanges();
            }
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}

