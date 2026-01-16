using
System.ComponentModel.DataAnnotations;

namespace BattlegroundsHubHS.Core.Models
{
    public class Hero
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя героя обязательно")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(30)]
        public required string Title { get; set; } = string.Empty; // Например: "Пират", "Демон"

        public HeroTier Tier { get; set; } = HeroTier.B; // По умолчнию средний

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(200)]
        public string ImageUrl { get; set; } = string.Empty;
    }
}