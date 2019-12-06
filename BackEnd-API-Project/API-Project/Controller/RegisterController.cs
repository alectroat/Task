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
            repository = new RegistrationRepository();
            _Response = new Response();
        }
        public RegisterController(IRegistrationRepository registrationRepository)
        {
            repository = registrationRepository;
        }
        [HttpPost]
        public dynamic NewRegistration(dynamic _User)
        {
            try
            {
                repository.NewRegistration(_User);
                _Response.Data = "";
                _Response.Message = "Success";
                _Response.Status = 0;
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