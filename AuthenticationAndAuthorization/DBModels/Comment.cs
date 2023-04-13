using System.ComponentModel.DataAnnotations;

namespace BlogSystem.DBModels
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
