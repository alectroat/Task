using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Project.Infrastructure
{
    public class Response
    {
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}