using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.GameData;

public class AnswerEntity
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(1024, ErrorMessage = "Довжина відповіді не має перевищувати 1024 символів")]
    public string AnswerText { get; set; }
    [Required]
    public bool IsCorrect { get; set; }
    [Required]
    public int AnswerOrder { get; set; }
    
    public QuestionEntity Question { get; set; }
}