using System;
using System.ComponentModel.DataAnnotations;

namespace StudentHelperWebApi.POCO
{
    public class CoursePOCO
    {
        public Guid CourseId { get; set; }

        [StringLength(30, ErrorMessage = "cannot exceed 30 characters")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }

        [StringLength(2000, ErrorMessage = "cannot exceed 2000 characters")]
        [Required(ErrorMessage = "is required")]
        public string Description { get; set; }
    }
}
