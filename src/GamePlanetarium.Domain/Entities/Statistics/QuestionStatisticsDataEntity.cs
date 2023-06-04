using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.Statistics;

public class QuestionStatisticsDataEntity
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(1024, ErrorMessage = "Довжина відповіді не має перевищувати 1024 символів")]
    public string FirstAnswerText { get; set; } = null!;
    [Required, Range(0, byte.MaxValue)]
    public byte QuestionOrder { get; set; }
    [Required, Range(0, byte.MaxValue)]
    public byte IncorrectAnswersCount { get; set; }
    
    public GameStatisticsDataEntity Game { get; set; } = null!;
}