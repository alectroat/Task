using API_Project.Abstract;
using API_Project.DAL;
using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.Concrete
{
    public class LoginRepository : ILoginRepository, IDisposable
    {
        private APPContext _Context;
        public LoginRepository()
        {
            _Context = new APPContext();
        }
        public IQueryable<User> Users
        {
            get { return _Context.Users; }
        }
        User ILoginRepository.AuthenticUser(User user)
        {
            return _Context.Users.Where(p => p.Email == user.Email && p.Password == user.Password).FirstOrDefault<User>();            
        }
        public void Dispose()
        {
            _Context.Dispose();
            _Context = null;
        }
    }
}