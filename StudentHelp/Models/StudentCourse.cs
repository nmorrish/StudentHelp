﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace StudentHelp.Models
{
    public partial class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}