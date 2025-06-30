using System;
using System.Collections.Generic;

namespace FessorApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int SchoolId { get; set; }
        public School School { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Report>? Reports { get; set; }
    }
} 