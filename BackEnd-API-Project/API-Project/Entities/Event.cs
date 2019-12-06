using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_Project.Models
{
    public class Event
    {
        [Key]
        public Guid EventId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Location { get; set; }
        public string NotifyBefore { get; set; }
        public string NotificationMedium { get; set; }
        public virtual User User { get; set; }
    }
}