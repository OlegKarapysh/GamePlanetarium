using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.GameSeeds;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public class Game
{
    public virtual bool IsGameEnded { get; protected set; }
    public IQuestion[] Questions { get; }
        
    public Game(IQuestion[] questions)
    {
        Questions = questions ?? throw new ArgumentNullException(nameof(questions));
    }

    public virtual bool TryAnswerQuestion(byte questionNumber, params Answers[] answerNumbers)
    {
        if (questionNumber >= Questions.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(questionNumber));
        }
        if (IsGameEnded)
        {
            throw new InvalidOperationException("Cannot answer the question when the game has ended!");
        }
        
        var question = Questions[questionNumber];
        var isAnswerCorrect = question.TryAnswer(
            answerNumbers
                .Select(answerNumber => question.Answers[(int)answerNumber])
                .ToArray());
        if (isAnswerCorrect)
        {
            IsGameEnded = Questions.All(q => q.IsAnswered);
        }

        return isAnswerCorrect;
    }

    public QuestionTextSeed GetQuestionTextSeed()
    {
        return new QuestionTextSeed(
            Questions
                .Select(q => new QuestionTextData(
                    q.Text, q.Answers.Select(a => a.Text).ToArray())).ToArray());
    }

    public Answers[] GetCorrectAnswers()
    {
        return Questions
            .Select(q => q.Answers.First(a => a.IsCorrect).Number)
            .ToArray();
    }

    public QuestionImage[] GetQuestionImages() => Questions.Select(q => q.QuestionImage).ToArray();

    public void ChangeQuestionsTextBySeed(QuestionTextSeed seed)
    {
        ArgumentNullException.ThrowIfNull(seed);
            
        var questionsToChange = Math.Min(seed.Data.Length, Questions.Length);
        for (int i = 0; i < questionsToChange; i++)
        {
            Questions[i].Text = seed.Data[i].QuestionText;
            var answersToChange = Math.Min(seed.Data[i].AnswersText.Length,
                Questions[i].Answers.Length);
            for (int j = 0; j < answersToChange; j++)
            {
                Questions[i].Answers[j].Text = seed.Data[i].AnswersText[j];
            }
        }
    }
}