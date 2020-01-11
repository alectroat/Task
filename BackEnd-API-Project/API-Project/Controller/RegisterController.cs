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
using System.Web.Http.Cors;

namespace API_Project.Controller
{
    public class RegisterController: ApiController
    {
        private IRegistrationRepository repository;
        private Response _Response;
        public RegisterController()
        {            
        }
        public RegisterController(IRegistrationRepository registrationRepository)
        {
            repository = registrationRepository;
            _Response = new Response();
        }
        [HttpPost]
        public IHttpActionResult NewRegistration(User _User)
        {            
            try
            {                
                _Response.Data = repository.NewRegistration(_User);                
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