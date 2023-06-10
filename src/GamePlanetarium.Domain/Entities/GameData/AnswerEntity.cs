using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.GameData;

public class AnswerEntity
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Відповідь повинна мати текст!"),
     StringLength(1024, MinimumLength = 2, ErrorMessage = "Довжина відповіді не має перевищувати 1024 символів!")]
    public string AnswerText { get; set; } = null!;
    [Required]
    public bool IsCorrect { get; set; }
    [Required]
    public int AnswerOrder { get; set; }

    public QuestionEntity Question { get; set; } = null!;
}