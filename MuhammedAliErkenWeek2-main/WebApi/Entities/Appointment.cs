using System;

namespace WebApi.Entities
{
    public class Appointment
    {
        // scalar property
        public int Id { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Description { get; set; }

        // reference navigation property
        public User User { get; set; }
    }
}