using API_Project.Abstract;
using API_Project.Concrete;
using API_Project.DAL;
using API_Project.Infrastructure;
using API_Project.Models;
using API_Project.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API_Project.Controller
{
    public class EventController:ApiController
    {
        private IEventRepository repository;
        private Response _Response;
        public EventController()
        {
            repository = new EventRepository();
            _Response = new Response();
        }
        public EventController(IEventRepository eventRepository)
        {
            repository = eventRepository;
        }

        [HttpPost]
        public dynamic DeleteEventById(dynamic EventId)
        {
            try
            {
                _Response.Data = repository.DeleteEvent(EventId);
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

        [HttpPost]
        public dynamic GetEventById(dynamic EventId)
        {
            try
            {
                _Response.Data = repository.GetEventById(EventId);
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

        [HttpPost]
        public dynamic UserEvents(dynamic UserId)
        {
            try
            {
                _Response.Data = repository.UserEvents(UserId);
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

        [HttpPost]
        public dynamic CreateNewEvent(dynamic res)
        {
            string EventId = res.EventId.ToString();
            try
            {
                if (res.EventId.ToString() == "")
                {
                    _Response.Data = repository.AddEvent(res);
                }
                else
                {
                    _Response.Data = repository.EditEvent(res);
                }
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