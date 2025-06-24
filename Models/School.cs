using System;
using System.Collections.Generic;

namespace FessorApi.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Principal { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Student> Students { get; set; }
        public List<User> Users { get; set; }
        public List<Report> Reports { get; set; }
    }
} 