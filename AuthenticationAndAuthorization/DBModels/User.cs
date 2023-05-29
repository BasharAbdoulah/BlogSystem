using System.ComponentModel.DataAnnotations;

namespace BlogSystem.DBModels
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; }

        public bool? Admin { get; set; } 

        public DateTime? DOB { get; set; }
        public string? ProfileImg { get; set; }
    }
}
