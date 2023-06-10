using System.ComponentModel.DataAnnotations;
using GamePlanetarium.Domain.Game;

namespace GamePlanetarium.Domain.Entities.GameData;

public class QuestionEntity
{
    [Key]
    public int Id { get; set; }
    [Required, Range(1, GameObservable.QuestionsCount)]
    public int QuestionNumber { get; set; }

    [Required(ErrorMessage = "Питання повинно мати текст!"),
     StringLength(1024, MinimumLength = 2, ErrorMessage = "Довжина питання повинна бути від 2 до 1024 символів!")]
    public string QuestionText { get; set; } = null!;
    [Required]
    public bool IsUkr { get; set; }

    public ICollection<AnswerEntity> Answers { get; set; } = null!;
    public QuestionImageEntity QuestionImage { get; set; } = null!;
}