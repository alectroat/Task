using API_Project.Abstract;
using API_Project.DAL;
using API_Project.Models;
using System;
using System.Linq;

namespace API_Project.Concrete
{
    public class RegistrationRepository : IRegistrationRepository, IDisposable
    {
        private APPContext _Context;
        public RegistrationRepository()
        {
            _Context = new APPContext();
        }
        public IQueryable<User> Users
        {
            get { return _Context.Users; }
        }
        User IRegistrationRepository.NewRegistration(User user)
        {
            _Context.Users.Add
            (
                new User()
                {
                    UserId = Guid.NewGuid(),
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                    Image = user.Image,
                    DOB = user.DOB
                }
            );
            _Context.SaveChanges();
            return user;
        }
        public void Dispose()
        {
            _Context.Dispose();
            _Context = null;
        }
    }
}