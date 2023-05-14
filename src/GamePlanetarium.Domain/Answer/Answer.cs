namespace GamePlanetarium.Domain.Answer;

public class Answer
{
    public string Text { get; set; }
    public Answers Number { get; }
    public bool IsCorrect { get; }
        
    public Answer(string text, Answers number, bool isCorrect)
    {
        Text = text;
        Number = number;
        IsCorrect = isCorrect;
    }
}