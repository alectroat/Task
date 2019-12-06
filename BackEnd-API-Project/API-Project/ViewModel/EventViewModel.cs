using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.ViewModel
{
    public class EventViewModel
    {
        public DateTime Date { get; set; }
        public string DateStringFormat { get; set; }
        public List<Event> Events { get; set; }
    }
}