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
        
        // Assert.
        mappedEntity.Should()!.BeOfType<QuestionStatisticsDataEntity>();
        mappedEntity.QuestionOrder.Should()!.Be(questionOrder);
        mappedEntity.IncorrectAnswersCount.Should()!.Be(incorrectAnswerCount);
        mappedEntity.FirstAnswerText.Should()!.Be(firstAnswerText);
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
        var mapping = _mapper.Map<GameStatisticsDataEntity>(gameStatistics)!;
        
        // Assert.
        mapping.Should()!.BeOfType<GameStatisticsDataEntity>();
        mapping.DateStamp.Should()!.Be(DateOnly.FromDateTime(DateTime.Today));
        mapping.IsGameEnded.Should()!.BeFalse();
        mapping.QuestionsStatistics.Should()!.HaveCount(1);
        mapping.QuestionsAnsweredCount.Should()!.Be(1);
    }

    [Fact]
    public void QuestionImageProfile_ShouldMapQuestionImage_ToQuestionImageEntity()
    {
        // Assign.
        var questionImage = new QuestionImage("QuestionImage1", new byte[] { 1, 2 }, new byte[] { 2, 1 });
        
        // Act.
        var mapping = _mapper.Map<QuestionImageEntity>(questionImage)!;
        
        // Assert.
        mapping.Should()!.BeOfType<QuestionImageEntity>();
        mapping.ImageName.Should()!.Be(questionImage.ImageName);
        mapping.HashCode.Should()!.Be(questionImage.GetHashCode());
        mapping.BlackWhiteImageSource.Should()!.Equal(questionImage.BlackWhiteImageSource);
        mapping.ColoredImageSource.Should()!.Equal(questionImage.ColoredImageSource);
    }

    [Fact]
    public void AnswerProfile_ShouldMapAnswer_ToAnswerEntity()
    {
        // Assign.
        var answer = new Answer.Answer("answer1", Answers.First, true);
        
        // Act.
        var mapping = _mapper.Map<AnswerEntity>(answer)!;
        
        // Assert.
        mapping.Should()!.BeOfType<AnswerEntity>();
        mapping.AnswerText.Should()!.Be(answer.Text);
        mapping.IsCorrect.Should()!.Be(answer.IsCorrect);
        mapping.AnswerOrder.Should()!.Be((int)answer.Number);
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
        var mapping = _mapper.Map<QuestionEntity>(question)!;
        
        // Assert.
        mapping.Should()!.BeOfType<QuestionEntity>();
        mapping.QuestionText.Should()!.Be(question.Text);
        mapping.HasSingleAnswer.Should()!.BeTrue();
        mapping.Answers.Should()!.HaveCount(answers1.Length);
        mapping.QuestionImage.Should()!.NotBeNull()!.And.BeOfType<QuestionImageEntity>();
    }
}