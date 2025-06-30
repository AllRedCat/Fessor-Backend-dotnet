using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace FessorApi.Models
{
    public enum UserRole
    {
        Admin,
        User,
        Demo
    }

    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        
        public string? ProfilePicture { get; set; }
        public int? SchoolId { get; set; }
        public School? School { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Report>? Reports { get; set; }
    }
} 