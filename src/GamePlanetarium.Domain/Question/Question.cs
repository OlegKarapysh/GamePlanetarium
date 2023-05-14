namespace GamePlanetarium.Domain.Question;

public abstract class Question : IQuestion
{
    public const byte AnswersCount = 3;
            
    public string Text { get; set; }
    public Answer.Answer[] Answers { get; set; }
    public bool IsAnswered { get; protected set; }

    protected Question(string text, Answer.Answer[] answers)
    {
        ThrowIfIncorrectAnswersCount(answers);
        Text = text;
        Answers = answers;
    }

    public abstract bool CheckIfCorrect(params Answer.Answer[] answers);

    public virtual bool TryAnswer(params Answer.Answer[] answers) => IsAnswered = CheckIfCorrect(answers);

    protected void ThrowIfNoCorrectAnswers(params Answer.Answer[]? answers)
    {
        if (answers is null || answers.Length == 0)
        {
            throw new ArgumentException("Answers must contain at least one correct answer!");
        }
    }

    protected void ThrowIfIncorrectAnswersCount(params Answer.Answer[]? answers)
    {
        if (answers is null || answers.Length != AnswersCount)
        {
            throw new ArgumentException($"Exactly {AnswersCount} answers must be provided!");
        }
    }
}