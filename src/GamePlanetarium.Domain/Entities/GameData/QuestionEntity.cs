namespace GamePlanetarium.Domain.Entities.GameData;

public class QuestionEntity
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public bool IsSingleAnswer { get; set; }
    
    public ICollection<AnswerEntity> Answers { get; set; }
    public QuestionImageEntity BlackWhiteImage { get; set; }
    public QuestionImageEntity ColoredImage { get; set; }
}