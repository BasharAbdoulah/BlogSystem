using System.ComponentModel.DataAnnotations;

namespace BlogSystem.DBModels
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PostContent { get; set; } = null!;
        [Required]
        public string PostTitle { get; set; } = null!;
        public string? Tags { get; set; }

        public int UserId { get; set; }

        public DateTime CreationDate { get; set; }
        public int? Likes { get; set; }
        public string? PostImg { get; set; }
    }
}
