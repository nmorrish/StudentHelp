using System;
using System.ComponentModel.DataAnnotations;

namespace StudentHelp.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
