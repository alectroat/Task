using API_Project.Abstract;
using API_Project.DAL;
using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        void IRegistrationRepository.NewRegistration(dynamic _User)
        {
            _Context.Users.Add(new User()
            {
                UserId = Guid.NewGuid(),
                FullName = _User.FullName,
                Email = _User.Email,
                Password = _User.Password,
                Image = _User.Image,
                DOB = _User.DOB == "" ?null: _User.DOB
            });
            _Context.SaveChanges();
        }
        public void Dispose()
        {
            _Context.Dispose();
            _Context = null;
        }
    }
}