using System;
using System.ComponentModel.DataAnnotations;

namespace StudentHelp.Entities
{
    public class Students
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
