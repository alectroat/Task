using API_Project.Abstract;
using API_Project.DAL;
using API_Project.Models;
using API_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

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
        Event IEventRepository.GetEventById(Guid EventId)
        {
            return _Context.Events.Where(p => p.EventId == EventId).FirstOrDefault<Event>();
        }
        List<EventViewModel> IEventRepository.UserEvents(Guid UserId)
        {
            List<EventViewModel> eventList = new List<EventViewModel>();
            List<Event> events = new List<Event>();

            events = _Context.Events.Where(p => p.UserId == UserId)
                                            .OrderByDescending(p => p.Start)
                                            .OrderByDescending(p => p.Date)
                                            .ToList<Event>();
            foreach (Event e in events)
            {
                groupByDate(eventList, e);
            }
            return eventList;
        }
        Event IEventRepository.SaveEvent(Event _event)
        {
            Event _record = new Event();            

            if (_event.EventId == Guid.Empty)
            {
                _record.EventId = Guid.NewGuid();
                _record.UserId = _event.UserId;
            }
            else
            {
                _record = _Context.Events.Find(_event.EventId);
            }

            _record.Title = _event.Title;
            _record.Description = _event.Description;
            _record.Date = _event.Date;
            _record.Start = _event.Start;
            _record.End = _event.End;
            _record.Location = _event.Location;
            _record.NotifyBefore = _event.NotifyBefore;
            _record.NotificationMedium = _event.NotificationMedium;

            if (_event.EventId == Guid.Empty)
            {
                _Context.Events.Add(_record);
            }
            _Context.SaveChanges();

            return _event;
        }        
        List<EventViewModel> IEventRepository.DeleteEvent(Event res)
        {
            Guid EventId = res.EventId;
            Guid UserId = res.UserId;

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
            return eventList;
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