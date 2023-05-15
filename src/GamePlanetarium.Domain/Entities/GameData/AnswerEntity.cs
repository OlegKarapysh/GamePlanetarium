namespace GamePlanetarium.Domain.Entities.GameData;

public class AnswerEntity
{
    public int Id { get; set; }
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public int AnswerOrder { get; set; }
    
    public Question.Question Question { get; set; }
}