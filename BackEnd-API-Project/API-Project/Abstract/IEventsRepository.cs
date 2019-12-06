using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.Abstract
{
    public interface IEventRepository
    {
        IQueryable<Event> Events { get; }
        dynamic AddEvent(dynamic _Event);
        dynamic EditEvent(dynamic _Event);
        dynamic DeleteEvent(dynamic _res);
        dynamic GetEventById(dynamic _res);
        dynamic UserEvents(dynamic _UserId);
    }
}