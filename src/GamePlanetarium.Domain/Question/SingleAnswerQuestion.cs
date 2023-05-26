namespace GamePlanetarium.Domain.Question;

public class SingleAnswerQuestion : Question
{
    public Answer.Answer CorrectAnswer { get; }
        
    public SingleAnswerQuestion(string text, Answer.Answer[] answers, QuestionImage image)
        : base(text, answers, image)
    {
        var correctAnswer = answers.FirstOrDefault(a => a.IsCorrect);
        ThrowIfNoCorrectAnswers(correctAnswer);
        CorrectAnswer = correctAnswer;
    }

    public override bool CheckIfCorrect(params Answer.Answer[] answers) => answers is [{ IsCorrect: true }];
}