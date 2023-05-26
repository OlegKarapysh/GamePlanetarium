using GamePlanetarium.Domain.Answer;
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

    public void ChangeQuestionsTextBySeed(QuestionsSeed seed)
    {
        ArgumentNullException.ThrowIfNull(seed);
            
        var questionsToChange = Math.Min(seed.QuestionsText.Length, Questions.Length);
        for (int i = 0; i < questionsToChange; i++)
        {
            Questions[i].Text = seed.QuestionsText[i].QuestionText;
            var answersToChange = Math.Min(seed.QuestionsText[i].AnswersText.Length,
                Questions[i].Answers.Length);
            for (int j = 0; j < answersToChange; j++)
            {
                Questions[i].Answers[j].Text = seed.QuestionsText[i].AnswersText[j];
            }
        }
    }
}