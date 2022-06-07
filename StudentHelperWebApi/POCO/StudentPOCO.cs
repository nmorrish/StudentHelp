using System;
using System.ComponentModel.DataAnnotations;

namespace StudentHelperWebApi.POCO
{
    public class StudentPOCO
    {
        public Guid StudentId { get; set; }

        [StringLength(50, ErrorMessage = "first name cannot exceed 50 characters")]
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "last name cannot exceed 50 characters")]
        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

        [StringLength(60, ErrorMessage = "email cannot exceed 60 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "email is required")]
        public string Email { get; set; }

        [DataType(DataType.Date, ErrorMessage = "please enter a date")]
        [Required(ErrorMessage = "Registration date is required")]
        public DateTime RegistrationDate { get; set; }

    }
}
