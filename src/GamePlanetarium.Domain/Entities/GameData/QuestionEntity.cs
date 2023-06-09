using System.ComponentModel.DataAnnotations;
using GamePlanetarium.Domain.Game;

namespace GamePlanetarium.Domain.Entities.GameData;

public class QuestionEntity
{
    [Key]
    public int Id { get; set; }
    [Required, Range(1, GameObservable.QuestionsCount)]
    public int QuestionNumber { get; set; }

    [Required, MaxLength(1024, ErrorMessage = "Довжина питання не має перевищувати 1024 символів")]
    public string QuestionText { get; set; } = null!;
    [Required]
    public bool IsUkr { get; set; }

    public ICollection<AnswerEntity> Answers { get; set; } = null!;
    public QuestionImageEntity QuestionImage { get; set; } = null!;
}