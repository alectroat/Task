using API_Project.Abstract;
using API_Project.DAL;
using API_Project.Models;
using API_Project.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.Concrete
{
    public class EventRepository: IEventRepository, IDisposable
    {
        private APPContext _Context;
        public EventRepository()
        {
            _Context = new APPContext();
        }
        public IQueryable<Event> Events
        {
            get { return _Context.Events; }
        }
        dynamic IEventRepository.GetEventById(dynamic EventId)
        {
            Guid _EventId = new Guid(EventId.EventId.ToString());
            Event _Event = _Context.Events.Where(p => p.EventId == _EventId)
                                            .FirstOrDefault<Event>();
            return JsonConvert.SerializeObject(_Event);
        }
        dynamic IEventRepository.UserEvents(dynamic UserId)
        {
            Guid _UserId = new Guid(UserId.UserId.ToString());
            List<EventViewModel> eventList = new List<EventViewModel>();
            List<Event> events = new List<Event>();

            events = _Context.Events.Where(p => p.UserId == _UserId)
                                            .OrderByDescending(p => p.Start)
                                            .OrderByDescending(p => p.Date)
                                            .ToList<Event>();
            foreach (Event e in events)
            {
                groupByDate(eventList, e);
            }
            return JsonConvert.SerializeObject(eventList);
        }
        dynamic IEventRepository.AddEvent(dynamic _Event)
        {
            TimeSpan Start = new TimeSpan(0, 0, 0);
            TimeSpan End = new TimeSpan(0, 0, 0);
            DateTime Date = DateTime.Now;

            try
            {
                Start = new TimeSpan(Int32.Parse(_Event.Start.hour.ToString()), 
                    Int32.Parse(_Event.Start.minute.ToString()), 0);
                End = new TimeSpan(Int32.Parse(_Event.End.hour.ToString()), 
                    Int32.Parse(_Event.End.minute.ToString()), 0);
                Date = Convert.ToDateTime(_Event.Date.ToString());
            }
            catch (Exception) { }

            _Context.Events.Add(new Event()
            {
                EventId = Guid.NewGuid(),
                UserId = new Guid(_Event.UserId.ToString()),
                Title = _Event.Title,
                Description = _Event.Description,
                Date = Date.Date,
                Start = Start,
                End = End,
                Location = _Event.Location,
                NotifyBefore = _Event.NotifyBefore,
                NotificationMedium = _Event.NotificationMedium
            });
            _Context.SaveChanges();

            return null;
        }
        dynamic IEventRepository.EditEvent(dynamic _Event)
        {
            Guid EventId = new Guid(_Event.EventId.ToString());

            TimeSpan Start = new TimeSpan(0, 0, 0);
            TimeSpan End = new TimeSpan(0, 0, 0);
            DateTime Date = DateTime.Now;

            try
            {
                Start = new TimeSpan(Int32.Parse(_Event.Start.hour.ToString()), 
                    Int32.Parse(_Event.Start.minute.ToString()), 0);
                End = new TimeSpan(Int32.Parse(_Event.End.hour.ToString()), 
                    Int32.Parse(_Event.End.minute.ToString()), 0);
                Date = Convert.ToDateTime(_Event.Date.ToString());
            }
            catch (Exception) { }

            Event _record = _Context.Events.Find(EventId);

            _record.Title = _Event.Title;
            _record.Description = _Event.Description;
            _record.Date = Date.Date;
            _record.Start = Start;
            _record.End = End;
            _record.Location = _Event.Location;
            _record.NotifyBefore = _Event.NotifyBefore;
            _record.NotificationMedium = _Event.NotificationMedium;

            _Context.SaveChanges();

            return null;
        }
        dynamic IEventRepository.DeleteEvent(dynamic res)
        {
            Guid EventId = new Guid(res.EventId.ToString());
            Guid UserId = new Guid(res.UserId.ToString());

            List<EventViewModel> eventList = new List<EventViewModel>();
            List<Event> events = new List<Event>();

            Event _event = _Context.Events.Find(EventId);
            if (_event != null)
            {
                _Context.Events.Remove(_event);
                _Context.SaveChanges();
            }
            events = _Context.Events.Where(p => p.UserId == UserId)
                                        .OrderByDescending(p => p.Start)
                                        .OrderByDescending(p => p.Date)
                                        .ToList<Event>();

            foreach (Event e in events)
            {
                groupByDate(eventList, e);
            }
            return JsonConvert.SerializeObject(eventList);
        }
        public void groupByDate(List<EventViewModel> eventList, Event eventObj)
        {
            EventViewModel record = eventList.Where(p => p.Date == eventObj.Date).FirstOrDefault<EventViewModel>();
            if (record == null)
            {
                eventList.Add(new EventViewModel()
                {
                    Date = eventObj.Date,
                    DateStringFormat = eventObj.Date.ToString("dddd,  MMMM dd"),
                    Events = new List<Event>() { eventObj }
                });
            }
            else
            {
                record.Events.Add(eventObj);
            }
        }
        public void Dispose()
        {
            _Context.Dispose();
            _Context = null;
        }
    }
}