using API_Project.Models;
using API_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.Abstract
{
    public interface IEventRepository
    {
        IQueryable<Event> Events { get; }
        Event SaveEvent(Event Event);
        List<EventViewModel> DeleteEvent(Event res);
        Event GetEventById(Guid EventId);
        List<EventViewModel> UserEvents(Guid UserId);
    }
}