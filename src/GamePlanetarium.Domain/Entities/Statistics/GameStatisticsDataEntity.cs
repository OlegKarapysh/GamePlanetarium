using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.Statistics;

public class GameStatisticsDataEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public bool IsGameEnded { get; set; }
    [Required, Range(0, byte.MaxValue)]
    public byte QuestionsAnsweredCount { get; set; }
    [Required]
    public DateOnly DateStamp { get; set; }

    public ICollection<QuestionStatisticsDataEntity> QuestionsStatistics { get; set; }
}