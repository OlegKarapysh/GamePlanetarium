using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.GameData;

public class QuestionImageEntity
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(128, ErrorMessage = "Довжина назви картинки не має перевищувати 128 символів")]
    public string ImageName { get; set; }
    [Required]
    public int HashCode { get; set; }
    [Required]
    public byte[] BlackWhiteImageSource { get; set; }
    [Required]
    public byte[] ColoredImageSource { get; set; }
    
    public QuestionEntity Question { get; set; }
}