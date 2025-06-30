using System;

namespace FessorApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SchoolId { get; set; }
        public School School { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 