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
        }
        public LoginController(ILoginRepository loginRepository)
        {
            repository = loginRepository;
            _Response = new Response();
        }
        [HttpPost]
        public IHttpActionResult AuthenticUser(User user)
        {            
            try
            {
                _Response.Data = repository.AuthenticUser(user);
            }
            catch (Exception ex)
            {
                _Response.Message = ex.Message.ToString();
                _Response.Status = 0;
            }
            return Ok(_Response);
        }
    }
}