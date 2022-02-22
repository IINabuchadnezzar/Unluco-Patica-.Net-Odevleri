using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Common;

namespace WebApi.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required] public string Email { get; set; }
        public UserEnum UserRole { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}