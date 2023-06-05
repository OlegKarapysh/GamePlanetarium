using System.Diagnostics.CodeAnalysis;

namespace GamePlanetarium.Domain.Question;

public class SingleAnswerQuestion : Question
{
    public override required Answer.Answer[] Answers
    {
        get => _answers;
        init
        {
            ArgumentNullException.ThrowIfNull(value);
            var correctAnswer = value.FirstOrDefault(a => a.IsCorrect);
            ThrowIfNoCorrectAnswers(correctAnswer);
            _answers = value;
            CorrectAnswer = correctAnswer;
        }
    }
    public Answer.Answer CorrectAnswer { get; init; }

    private readonly Answer.Answer[] _answers = null!;

    public SingleAnswerQuestion()
    {
    }
    [SetsRequiredMembers]
    public SingleAnswerQuestion(string text, Answer.Answer[] answers, QuestionImage image)
        : base(text, answers, image)
    {
        ArgumentNullException.ThrowIfNull(image);
    }

    public override bool CheckIfCorrect(params Answer.Answer[] answers) => answers is [{ IsCorrect: true }];
}