using System;
using System.Collections.Generic;

namespace FessorApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ProfilePicture { get; set; }
        public int? SchoolId { get; set; }
        public School? School { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Report> Reports { get; set; }
    }
} 