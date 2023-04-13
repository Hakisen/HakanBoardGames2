using System.ComponentModel.DataAnnotations;

namespace HakanBoardGames.Shared.BoardGameModels
{
    public class BGBoardGame
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? BoardGameId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Releasd { get; set; }
        public string? Tagline { get; set; }
        public int Age { get; set; }
        public List<BGPlayer>? Players { get; set; } = new List<BGPlayer>();
        public List<BGCreator>? Creators { get; set; } = new List<BGCreator>();
        public List<BGCategory>? Categories { get; set; } = new List<BGCategory>();

        [Required]
        public DateTime SavedToDatabaseDate { get; set; }
    }
}
