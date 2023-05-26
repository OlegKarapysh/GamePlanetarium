using System.ComponentModel.DataAnnotations;

namespace GamePlanetarium.Domain.Entities.GameData;

public class QuestionEntity
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(1024, ErrorMessage = "Довжина питання не має перевищувати 1024 символів")]
    public string QuestionText { get; set; }
    [Required]
    public bool HasSingleAnswer { get; set; }
    
    public ICollection<AnswerEntity> Answers { get; set; }
    public QuestionImageEntity QuestionImage { get; set; }
}