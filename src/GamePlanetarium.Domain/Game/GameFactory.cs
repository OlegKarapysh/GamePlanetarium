using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public class GameFactory : IGameFactory
{
    private readonly QuestionsSeed _ukrSeed;
    private readonly QuestionsSeed _engSeed;
    private readonly EventHandler _onGameEnded;
        
    public GameFactory(QuestionsSeed ukrSeed, QuestionsSeed engSeed, EventHandler onGameEnded)
    {
        _ukrSeed = ukrSeed;
        _engSeed = engSeed;
        _onGameEnded = onGameEnded;
    }
        
    public Game GetGameWithLocalization(bool isUkrLocal)
    {
        var seed = isUkrLocal ? _ukrSeed : _engSeed;
        var game = new GameObservable(new IQuestion[]
        {
            new SingleAnswerQuestion(seed.QuestionsText[0].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[0].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[0].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(seed.QuestionsText[0].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[1].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[1].AnswersText[0], Answers.First, true),
                    new Answer.Answer(seed.QuestionsText[1].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[1].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[2].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[2].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[2].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[2].AnswersText[2], Answers.Third, true)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[3].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[3].AnswersText[0], Answers.First, true),
                    new Answer.Answer(seed.QuestionsText[3].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[3].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[4].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[4].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[4].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[4].AnswersText[2], Answers.Third, true)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[5].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[5].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[5].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(seed.QuestionsText[5].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[6].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[6].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[6].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[6].AnswersText[2], Answers.Third, true)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[7].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[7].AnswersText[0], Answers.First, true),
                    new Answer.Answer(seed.QuestionsText[7].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[7].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[8].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[8].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[8].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(seed.QuestionsText[8].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[9].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[9].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[9].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[9].AnswersText[2], Answers.Third, true)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[10].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[10].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[10].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[10].AnswersText[2], Answers.Third, true)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[11].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[11].AnswersText[0], Answers.First, true),
                    new Answer.Answer(seed.QuestionsText[11].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(seed.QuestionsText[11].AnswersText[2], Answers.Third, true)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[12].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[12].AnswersText[0], Answers.First, true),
                    new Answer.Answer(seed.QuestionsText[12].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(seed.QuestionsText[12].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[13].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[13].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[13].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(seed.QuestionsText[13].AnswersText[2], Answers.Third, false)
                }),
            new SingleAnswerQuestion(seed.QuestionsText[14].QuestionText,
                new[]
                {
                    new Answer.Answer(seed.QuestionsText[14].AnswersText[0], Answers.First, false),
                    new Answer.Answer(seed.QuestionsText[14].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(seed.QuestionsText[14].AnswersText[2], Answers.Third, false)
                }),
        });
        game.GameEnded += _onGameEnded;
        return game;
    }
}