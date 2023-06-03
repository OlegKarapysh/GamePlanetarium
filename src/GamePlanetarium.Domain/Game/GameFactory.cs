using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public class GameFactory : IGameFactory
{
    private readonly EventHandler _onGameEnded;
    private readonly QuestionsSeed _ukrSeed;
    private readonly QuestionsSeed _engSeed;
    private readonly ImageSeed _ukrImg;
    private readonly ImageSeed _engImg;
    
    public GameFactory(QuestionsSeed ukrSeed, QuestionsSeed engSeed, ImageSeed ukrImg, ImageSeed engImg,
        EventHandler onGameEnded)
    {
        _ukrSeed = ukrSeed;
        _engSeed = engSeed;
        _ukrImg = ukrImg;
        _engImg = engImg;
        _onGameEnded = onGameEnded;
    }

    public GameObservable GetGameByLocal(bool isUkrLocal) => 
        isUkrLocal ? GetGameBySeed(_ukrSeed, _ukrImg) : GetGameBySeed(_engSeed, _engImg);

    public GameObservable GetGameBySeed(QuestionsSeed questionSeed, ImageSeed imageSeed)
    {
        var game = new GameObservable(new IQuestion[]
        {
            new SingleAnswerQuestion(questionSeed.QuestionsText[0].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[0].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[0].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(questionSeed.QuestionsText[0].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[0]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[1].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[1].AnswersText[0], Answers.First, true),
                    new Answer.Answer(questionSeed.QuestionsText[1].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[1].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[1]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[2].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[2].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[2].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[2].AnswersText[2], Answers.Third, true)
                }, imageSeed.QuestionImages[2]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[3].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[3].AnswersText[0], Answers.First, true),
                    new Answer.Answer(questionSeed.QuestionsText[3].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[3].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[3]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[4].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[4].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[4].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[4].AnswersText[2], Answers.Third, true)
                }, imageSeed.QuestionImages[4]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[5].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[5].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[5].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(questionSeed.QuestionsText[5].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[5]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[6].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[6].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[6].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[6].AnswersText[2], Answers.Third, true)
                }, imageSeed.QuestionImages[6]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[7].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[7].AnswersText[0], Answers.First, true),
                    new Answer.Answer(questionSeed.QuestionsText[7].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[7].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[7]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[8].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[8].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[8].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(questionSeed.QuestionsText[8].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[8]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[9].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[9].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[9].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[9].AnswersText[2], Answers.Third, true)
                }, imageSeed.QuestionImages[9]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[10].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[10].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[10].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[10].AnswersText[2], Answers.Third, true)
                }, imageSeed.QuestionImages[10]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[11].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[11].AnswersText[0], Answers.First, true),
                    new Answer.Answer(questionSeed.QuestionsText[11].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(questionSeed.QuestionsText[11].AnswersText[2], Answers.Third, true)
                }, imageSeed.QuestionImages[11]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[12].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[12].AnswersText[0], Answers.First, true),
                    new Answer.Answer(questionSeed.QuestionsText[12].AnswersText[1], Answers.Second, false),
                    new Answer.Answer(questionSeed.QuestionsText[12].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[12]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[13].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[13].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[13].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(questionSeed.QuestionsText[13].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[13]),
            new SingleAnswerQuestion(questionSeed.QuestionsText[14].QuestionText,
                new[]
                {
                    new Answer.Answer(questionSeed.QuestionsText[14].AnswersText[0], Answers.First, false),
                    new Answer.Answer(questionSeed.QuestionsText[14].AnswersText[1], Answers.Second, true),
                    new Answer.Answer(questionSeed.QuestionsText[14].AnswersText[2], Answers.Third, false)
                }, imageSeed.QuestionImages[14]),
        });
        game.GameEnded += _onGameEnded;
        return game;
    }
}