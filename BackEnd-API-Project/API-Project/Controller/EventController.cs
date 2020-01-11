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
        }
        public EventController(IEventRepository eventRepository)
        {
            repository = eventRepository;
            _Response = new Response();
        }

        [HttpPost]
        public IHttpActionResult DeleteEventById(Event Event)
        {
            try
            {
                _Response.Data = repository.DeleteEvent(Event);
            }
            catch (Exception ex)
            {
                _Response.Message = ex.Message.ToString();
                _Response.Status = 0;
            }
            return Ok(_Response);
        }

        [HttpPost]
        public IHttpActionResult GetEventById(Event Event)
        {
            try
            {
                _Response.Data = repository.GetEventById(Event.EventId);
            }
            catch (Exception ex)
            {
                _Response.Message = ex.Message.ToString();
                _Response.Status = 0;
            }
            return Ok(_Response);
        }

        [HttpPost]
        public IHttpActionResult UserEvents(Event Event)
        {
            try
            {
                _Response.Data = repository.UserEvents(Event.UserId);
            }
            catch (Exception ex)
            {
                _Response.Message = ex.Message.ToString();
                _Response.Status = 0;
            }
            return Ok(_Response);
        }

        [HttpPost]
        public IHttpActionResult SaveEvent(Event res)
        {
            try
            {
                _Response.Data = repository.SaveEvent(res);
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