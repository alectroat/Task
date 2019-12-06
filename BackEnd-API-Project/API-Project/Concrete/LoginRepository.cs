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
        dynamic ILoginRepository.AuthenticUser(dynamic res)
        {
            string Email = res.Email.ToString();
            string Password = res.Password.ToString();
            User result = _Context.Users.Where(p => p.Email == Email && p.Password == Password).FirstOrDefault<User>();
            return result;
        }
        public void Dispose()
        {
            _Context.Dispose();
            _Context = null;
        }
    }
}