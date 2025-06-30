using System;
using System.Collections.Generic;

namespace FessorApi.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Principal { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Student>? Students { get; set; }
        public List<User>? Users { get; set; }
        public List<Report>? Reports { get; set; }
    }
} 