using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.Statistics;

public class QuestionStatisticsDataEntity
{
    public int Id { get; set; }
    
    [Required, MaxLength(1024, ErrorMessage = "Довжина питання не має перевищувати 1024 символів")]
    public string FirstAnswerText { get; set; }
    
    [Required, Range(0, int.MaxValue)]
    public int QuestionOrder { get; set; }
    
    [Required, Range(0, int.MaxValue)]
    public int IncorrectAnswersCount { get; set; }
    
    public virtual GameStatisticsDataEntity Game { get; set; }
}