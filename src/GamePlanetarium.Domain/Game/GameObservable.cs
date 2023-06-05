using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public sealed class GameObservable : Game, IGameObservable
{
    public const int QuestionsCount = 15;
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
        // TODO: make game with the fixed number of questions.
        // if (questions.Length != QuestionsCount)
        // {
        //     throw new ArgumentException($"Number of questions in game must be equal to {QuestionsCount}!");
        // }
    }

    public override bool TryAnswerQuestion(byte questionNumber, params Answers[] answerNumbers)
    {
        var isAnswerCorrect = base.TryAnswerQuestion(questionNumber, answerNumbers);
        var firstAnswer = Questions[questionNumber].Answers[(int)answerNumbers.First()];
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