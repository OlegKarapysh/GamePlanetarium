using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using AutoMapper;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Entities.Statistics;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly Mapper _mapper;
    private bool _isUpperImageVisible;
    
    public MainWindow(Mapper mapper)
    {
        _mapper = mapper;
        InitializeComponent();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        const string colorImagePath = @"C:\Users\sebas\Pictures\Saved Pictures\ash flowers.jpg";
        const string blackWhiteImagePath = @"C:\Users\sebas\Pictures\Saved Pictures\ds mh.jpg";
        var colorImageData = ConvertImageToBytes(colorImagePath);
        var blackWhiteImageData = ConvertImageToBytes(blackWhiteImagePath);
        ImageFromBytes!.Source = ConvertBytesToImage(colorImageData);
        ImageFromBytesUpper!.Source = ConvertBytesToImage(blackWhiteImageData);
        
        var questionStats = new QuestionStatisticsData
        {
            FirstAnswerText = "FirstAnswerText...",
            QuestionOrder = 1
        };
        var questionStatsEntity = _mapper.Map<QuestionStatisticsDataEntity>(questionStats);
        TextBlockMain!.Text = questionStatsEntity!.FirstAnswerText + questionStatsEntity.QuestionOrder;

        var question = new SingleAnswerQuestion("questionText", new Answer[]
        {
            new("answer1", Answers.First, true),
            new("answer2", Answers.Second, false),
            new("answer3", Answers.Third, false),
        },
            new QuestionImage
        {
            ImageName = "blackwhite1",
            BlackWhiteImageSource = blackWhiteImageData,
            ColoredImageSource = colorImageData
        });
        var game = new GameObservable(new IQuestion[] { question });
        var gameStats = new GameStatisticsDataCollector(game);
        game.TryAnswerQuestion(0, Answers.First);
        var gameStatsEntity = _mapper.Map<GameStatisticsDataEntity>(gameStats);
        TextBlockGameStats!.Text = gameStatsEntity!.IsGameEnded + "-" +
                                   gameStatsEntity.DateStamp + "-" +
                                   gameStatsEntity.QuestionsAnsweredCount + "-" +
                                   gameStatsEntity.QuestionsStatistics.First().FirstAnswerText;
        var questionEntity = _mapper.Map<QuestionEntity>(question)!;
        QuestionTextBlock!.Text = questionEntity.QuestionText + ": is singleAns: " +
                                  questionEntity.HasSingleAnswer + " firstAns:" +
                                  questionEntity.Answers.First().AnswerText;

        using var db = new GameDb(
            @"Server=(localdb)\MSSQLLocalDB;Database=GamePlanetarium;Trusted_Connection=True;");
        db.Questions.Add(questionEntity);
        db.GameStatistics.Add(gameStatsEntity);
        db.SaveChanges();
    }

    private byte[] ConvertImageToBytes(string imagePath)
    {
        byte[] bytes;
        using var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        using var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        bytes = memoryStream.ToArray();
        return bytes;
    }

    private BitmapImage ConvertBytesToImage(byte[] bytes)
    {
        var imageSource = new BitmapImage();
        using var memoryStream = new MemoryStream(bytes);
        imageSource.BeginInit();
        imageSource.StreamSource = memoryStream;
        imageSource.CacheOption = BitmapCacheOption.OnLoad;
        imageSource.EndInit();
        return imageSource;
    }

    private void ChangeImageButton_OnClick(object sender, RoutedEventArgs e)
    {
        _isUpperImageVisible = !_isUpperImageVisible;
        ImageFromBytesUpper!.Visibility = _isUpperImageVisible ? Visibility.Visible : Visibility.Collapsed;
    }
}
