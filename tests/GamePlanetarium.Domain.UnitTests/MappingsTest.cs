using AutoMapper;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Entities.Statistics;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Mappings;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium.Domain.UnitTests;

public class MappingsTest
{
    private readonly Mapper _mapper;

    public MappingsTest()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<QuestionStatisticsProfile>();
            cfg.AddProfile<GameStatisticsProfile>();
            cfg.AddProfile<QuestionImageProfile>();
            cfg.AddProfile<AnswerProfile>();
            cfg.AddProfile<QuestionProfile>();
        }));
    }

    [Fact]
    public void QuestionStatisticsProfile_ShouldMapQuestionStatisticsData_ToQuestionStatisticsDataEntity()
    {
        // Assign.
        const byte questionOrder = 1;
        const byte incorrectAnswerCount = 0;
        const string firstAnswerText = "firstAnswer";
        var questionStatistics = new QuestionStatisticsData
        {
            QuestionOrder = questionOrder,
            FirstAnswerText = firstAnswerText
        };
        
        // Act.
        var mappedEntity = _mapper.Map<QuestionStatisticsDataEntity>(questionStatistics)!;
        var reverseMapping = _mapper.Map<QuestionStatisticsData>(mappedEntity)!;
        
        // Assert.
        mappedEntity.Should()!.BeOfType<QuestionStatisticsDataEntity>();
        mappedEntity.QuestionOrder.Should()!.Be(questionOrder);
        mappedEntity.IncorrectAnswersCount.Should()!.Be(incorrectAnswerCount);
        mappedEntity.FirstAnswerText.Should()!.Be(firstAnswerText);
        reverseMapping.Should()!.BeOfType<QuestionStatisticsData>();
        reverseMapping.QuestionOrder.Should()!.Be(questionOrder);
        reverseMapping.IncorrectAnswersCount.Should()!.Be(incorrectAnswerCount);
        reverseMapping.FirstAnswerText.Should()!.Be(firstAnswerText);
    }

    [Fact]
    public void GameStatisticsProfile_ShouldMapGameStatisticsDataCollector_ToGameStatisticsDataEntity()
    {
        // Assign.
        var questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
        var answers1 = new Answer.Answer[]
        {
            new("answer1", Answers.First, false),
            new("answer2", Answers.Second, true),
            new("answer3", Answers.Third, false)
        };
        var answers2 = new Answer.Answer[]
        {
            new("answer4", Answers.First, true),
            new("answer5", Answers.Second, false),
            new("answer6", Answers.Third, false)
        };
        var questions = new IQuestion[]
        {
            new SingleAnswerQuestion("question1", answers1, questionImage),
            new SingleAnswerQuestion("question1", answers2, questionImage)
        };
        var game = new GameObservable(questions);
        var gameStatistics = new GameStatisticsDataCollector(game);
        game.TryAnswerQuestion(0, Answers.Second);
        
        // Act.
        var mappedEntity = _mapper.Map<GameStatisticsDataEntity>(gameStatistics)!;
        
        // Assert.
        mappedEntity.Should()!.BeOfType<GameStatisticsDataEntity>();
        mappedEntity.DateStamp.Should()!.Be(DateOnly.FromDateTime(DateTime.Today));
        mappedEntity.IsGameEnded.Should()!.BeFalse();
        mappedEntity.QuestionsStatistics.Should()!.HaveCount(1);
        mappedEntity.QuestionsAnsweredCount.Should()!.Be(1);
    }

    [Fact]
    public void QuestionImageProfile_ShouldMapQuestionImage_ToQuestionImageEntity()
    {
        // Assign.
        var questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
        
        // Act.
        var mappedEntity = _mapper.Map<QuestionImageEntity>(questionImage)!;
        var reverseMapping = _mapper.Map<QuestionImage>(mappedEntity)!;
        
        // Assert.
        mappedEntity.Should()!.BeOfType<QuestionImageEntity>();
        mappedEntity.ImageName.Should()!.Be(questionImage.ImageName);
        mappedEntity.HashCode.Should()!.Be(questionImage.GetHashCode());
        mappedEntity.BlackWhiteImageSource.Should()!.Equal(questionImage.BlackWhiteImageSource);
        mappedEntity.ColoredImageSource.Should()!.Equal(questionImage.ColoredImageSource);
        reverseMapping.Should()!.BeOfType<QuestionImage>();
        reverseMapping.ImageName.Should()!.Be(questionImage.ImageName);
        reverseMapping.GetHashCode().Should()!.Be(questionImage.GetHashCode());
        reverseMapping.BlackWhiteImageSource.Should()!.Equal(questionImage.BlackWhiteImageSource);
        reverseMapping.ColoredImageSource.Should()!.Equal(questionImage.ColoredImageSource);
    }

    [Theory]
    [InlineData("a", 0, false)]
    [InlineData("ab", 1, false)]
    [InlineData("Abc1", 2, true)]
    public void AnswerProfile_ShouldMapAnswer_ToAnswerEntity(string text, int order, bool isCorrect)
    {
        // Assign.
        var answer = new Answer.Answer(text, (Answers)order, isCorrect);
        
        // Act.
        var mappedEntity = _mapper.Map<AnswerEntity>(answer)!;
        var reverseMapping = _mapper.Map<Answer.Answer>(mappedEntity)!;
        
        // Assert.
        mappedEntity.Should()!.BeOfType<AnswerEntity>();
        mappedEntity.AnswerText.Should()!.Be(answer.Text);
        mappedEntity.IsCorrect.Should()!.Be(answer.IsCorrect);
        mappedEntity.AnswerOrder.Should()!.Be((int)answer.Number);
        reverseMapping.Should()!.BeOfType<Answer.Answer>();
        reverseMapping.Text.Should()!.Be(answer.Text);
        reverseMapping.IsCorrect.Should()!.Be(answer.IsCorrect);
        reverseMapping.Number.Should()!.Be(answer.Number);
    }

    [Fact]
    public void QuestionProfile_ShouldMapQuestion_ToQuestionEntity()
    {
        // Assign.
        var questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
        var answers1 = new Answer.Answer[]
        {
            new("answer1", Answers.First, false),
            new("answer2", Answers.Second, true),
            new("answer3", Answers.Third, false)
        };
        var question = new SingleAnswerQuestion("question1", answers1, questionImage);
        
        // Act.
        var mappedEntity = _mapper.Map<QuestionEntity>(question)!;
        var reverseMapping = _mapper.Map<SingleAnswerQuestion>(mappedEntity)!;
        
        // Assert.
        mappedEntity.Should()!.BeOfType<QuestionEntity>();
        mappedEntity.QuestionText.Should()!.Be(question.Text);
        mappedEntity.Answers.Should()!.HaveCount(answers1.Length);
        mappedEntity.QuestionImage.Should()!.NotBeNull()!.And.BeOfType<QuestionImageEntity>();
        reverseMapping.Should()!.BeOfType<SingleAnswerQuestion>();
        reverseMapping.Text.Should()!.Be(question.Text);
        reverseMapping.Answers.Should()!.HaveCount(answers1.Length);
        reverseMapping.QuestionImage.Should()!.NotBeNull()!.And.BeOfType<QuestionImage>();
    }
}