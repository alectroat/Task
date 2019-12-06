using API_Project.Abstract;
using API_Project.Concrete;
using API_Project.DAL;
using API_Project.Infrastructure;
using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_Project.Controller
{
    public class LoginController : ApiController
    {
        private ILoginRepository repository;
        private Response _Response;
        public LoginController()
        {
            repository = new LoginRepository();
            _Response = new Response();
        }
        public LoginController(ILoginRepository loginRepository)
        {
            repository = loginRepository;
        }
        [HttpPost]
        public dynamic AuthenticUser(dynamic _User)
        {            
            try
            {
                _Response.Data = repository.AuthenticUser(_User);
                _Response.Message = "Success";
                _Response.Status = 1;
            }
            catch (Exception ex)
            {
                _Response.Data = null;
                _Response.Message = ex.Message.ToString();
                _Response.Status = 0;
            }
            return _Response;
        }
    }
}