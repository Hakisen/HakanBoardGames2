using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HakanBoardGames.Shared.BoardGameModels
{
    public class BGCreator
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string? FullName { get; set; }

        // navigation properties
        public Guid BoardGameId { get; set; }
        [JsonIgnore]
        public BGBoardGame? BoardGame { get; set; }
    }
}
