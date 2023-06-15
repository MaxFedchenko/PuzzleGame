using System.ComponentModel.DataAnnotations;

namespace PuzzleGame.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Score> Scores { get; set; } = new List<Score>();
    }
}
