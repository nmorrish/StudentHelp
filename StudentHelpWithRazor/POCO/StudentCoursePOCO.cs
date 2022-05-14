using System;

namespace StudentHelpRazor.POCO
{
    public class StudentCoursePOCO
    {
        public Guid StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
        public int? Grade { get; set; }
    }
}
