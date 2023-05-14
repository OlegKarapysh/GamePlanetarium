using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.Statistics;

public class GameStatisticsDataEntity
{
    public int Id { get; set; }
    
    [Required]
    public bool IsGameEnded { get; set; }
    
    [Required, Range(0, int.MaxValue)]
    public int QuestionsAnsweredCount { get; set; }
    
    [Required]
    public DateOnly DateStamp { get; set; }

    public ICollection<QuestionStatisticsDataEntity> QuestionsStatistics { get; set; }
}