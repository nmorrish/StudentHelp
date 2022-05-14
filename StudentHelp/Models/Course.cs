﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentHelp.Models
{
    public partial class Course
    {
        public Course()
        {
            Assignment = new HashSet<Assignment>();
            StudentCourse = new HashSet<StudentCourse>();
        }

        public Guid CourseId { get; set; }

        [StringLength(30, ErrorMessage = "cannot exceed 30 characters")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }

        [StringLength(2000, ErrorMessage = "cannot exceed 2000 characters")]
        [Required(ErrorMessage = "is required")]
        public string Description { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
    }
}