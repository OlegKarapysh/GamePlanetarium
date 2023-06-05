using System.Diagnostics.CodeAnalysis;

namespace GamePlanetarium.Domain.Question;

public abstract class Question : IQuestion
{
    public const byte AnswersCount = 3;
            
    public required string Text { get; set; }
    public virtual required Answer.Answer[] Answers { get; init; }
    public required QuestionImage QuestionImage { get; init; }
    public bool IsAnswered { get; protected set; }


    protected Question()
    {
    }
    [SetsRequiredMembers]
    protected Question(string text, Answer.Answer[] answers, QuestionImage questionImage)
    {
        ArgumentNullException.ThrowIfNull(questionImage);
        ThrowIfIncorrectAnswersCount(answers);
        Text = text;
        Answers = answers;
        QuestionImage = questionImage;
    }

    public abstract bool CheckIfCorrect(params Answer.Answer[] answers);

    public virtual bool TryAnswer(params Answer.Answer[] answers)
    {
        var isAnswerCorrect = CheckIfCorrect(answers);
        IsAnswered = IsAnswered || isAnswerCorrect;
        return isAnswerCorrect;
    }

    protected void ThrowIfNoCorrectAnswers(params Answer.Answer[]? answers)
    {
        if (answers is null || answers.Length == 0 || answers.FirstOrDefault() is null)
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