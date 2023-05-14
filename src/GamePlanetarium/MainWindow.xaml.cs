using System.Linq;
using System.Windows;
using AutoMapper;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities.Statistics;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly Mapper _mapper;
    
    public MainWindow(Mapper mapper)
    {
        _mapper = mapper;
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        var questionStats = new QuestionStatisticsData
        {
            FirstAnswerText = "FirstAnswerText...",
            QuestionOrder = 1
        };
        var questionStatsEntity = _mapper.Map<QuestionStatisticsDataEntity>(questionStats);
        TextBlockMain!.Text = questionStatsEntity!.FirstAnswerText + questionStatsEntity.QuestionOrder;
        
        var game = new GameObservable(new IQuestion[]
        {
            new SingleAnswerQuestion("questionText", new Answer[]
            {
                new("answer1", Answers.First, true),
                new("answer2", Answers.Second, false),
                new("answer3", Answers.Third, false),
            })
        });
        var gameStats = new GameStatisticsDataCollector(game);
        game.TryAnswerQuestion(0, Answers.First);
        var gameStatsEntity = _mapper.Map<GameStatisticsDataEntity>(gameStats);
        TextBlockGameStats!.Text = gameStatsEntity!.IsGameEnded + "-" +
                                   gameStatsEntity.DateStamp + "-" +
                                   gameStatsEntity.QuestionsAnsweredCount + "-" +
                                   gameStatsEntity.QuestionsStatistics.First().FirstAnswerText;
    }
}
