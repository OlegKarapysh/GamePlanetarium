using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public sealed class GameObservable : Game, IGameObservable
{
    public event EventHandler? GameEnded;
    public event EventHandler<AnsweredQuestionInfo>? TriedAnsweringQuestion;

    public override bool IsGameEnded
    {
        get => _isGameEnded;
        protected set
        {
            _isGameEnded = value;
            if (value)
            {
                OnGameEnded();
            }
        }
    }

    private bool _isGameEnded;

    public GameObservable(IQuestion[] questions) : base(questions)
    {
    }

    public override bool TryAnswerQuestion(byte questionNumber, params Answers[] answerNumbers)
    {
        var question = Questions[questionNumber];
        var isAnswerCorrect = question.TryAnswer(
            answerNumbers
                .Select(answerNumber => question.Answers[(int)answerNumber])
                .ToArray());
        if (isAnswerCorrect)
        {
            IsGameEnded = Questions.All(q => q.IsAnswered);
        }
        var firstAnswer = question.Answers[(int)answerNumbers.First()];
        OnQuestionAnswered(questionNumber, firstAnswer, isAnswerCorrect);

        return isAnswerCorrect;
    }

    private void OnGameEnded() => GameEnded?.Invoke(this, EventArgs.Empty);

    private void OnQuestionAnswered(byte questionNumber, Answer.Answer answer, bool isCorrect)
    {
        TriedAnsweringQuestion?.Invoke(this, new AnsweredQuestionInfo
        {
            QuestionNumber = questionNumber,
            FirstAnswer = answer,
            IsAnsweredCorrectly = isCorrect
        });
    }
}