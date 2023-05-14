namespace GamePlanetarium.Domain.Question;

public class SingleAnswerQuestion : Question
{
    public Answer.Answer CorrectAnswer { get; }
        
    public SingleAnswerQuestion(string text, Answer.Answer[] answers) : base(text, answers)
    {
        var correctAnswer = answers.FirstOrDefault(a => a.IsCorrect);
        ThrowIfNoCorrectAnswers(correctAnswer);
        CorrectAnswer = correctAnswer;
    }

    public override bool CheckIfCorrect(params Answer.Answer[] answers) => answers is [{ IsCorrect: true }];
}