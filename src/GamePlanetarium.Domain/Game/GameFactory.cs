using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.GameSeeds;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public class GameFactory : IGameFactory
{
    public QuestionTextSeed QuestionTextSeed { get; }
    public QuestionImage[] QuestionImages { get; }
    public Answers[] CorrectAnswers { get; }
    private readonly EventHandler _onGameEnded;
    
    public GameFactory(EventHandler onGameEnded, QuestionTextSeed questionTextSeed,
        QuestionImage[] questionImages, Answers[] correctAnswers)
    {
        if (questionTextSeed.Data.Length != GameObservable.QuestionsCount ||
            questionImages.Length != GameObservable.QuestionsCount ||
            correctAnswers.Length != GameObservable.QuestionsCount)
        {
            throw new ArgumentException("Number of seed data must correspond to QuestionsCount of game!");
        }
        _onGameEnded = onGameEnded;
        QuestionTextSeed = questionTextSeed;
        QuestionImages = questionImages;
        CorrectAnswers = correctAnswers;
    }

    public GameObservable GetGameBySeed()
    {
        var questions = new IQuestion[GameObservable.QuestionsCount];
        for (int i = 0; i < GameObservable.QuestionsCount; i++)
        {
            questions[i] = new SingleAnswerQuestion(QuestionTextSeed.Data[i].QuestionText,
                new Answer.Answer[]
                {
                    new(QuestionTextSeed.Data[i].AnswersText[0], Answers.First,
                        CorrectAnswers[i] == Answers.First),
                    new(QuestionTextSeed.Data[i].AnswersText[1], Answers.Second,
                        CorrectAnswers[i] == Answers.Second),
                    new(QuestionTextSeed.Data[i].AnswersText[2], Answers.Third,
                        CorrectAnswers[i] == Answers.Third),
                }, QuestionImages[i]);
        }

        var game = new GameObservable(questions);
        game.GameEnded += _onGameEnded;
        return game;
    }
}