using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GamePlanetarium.Commands;
using GamePlanetarium.Converters;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public Dictionary<string, int> ImageNumbers { get; }
    public GameObservable Game { get; set; }
    public GameStatisticsDataCollector GameStatistics { get; set; }
    public IGameFactory GameFactory { get; }
    public List<Image> QuestionImagesUkr { get; }
    public List<(BitmapImage blackWhite, BitmapImage colored)> BitmapImagesUkr;
    public List<(BitmapImage blackWhite, BitmapImage colored)> BitmapImagesEng;
    public List<BitmapImage> Exper { get; }
    public ICommand ShowQuestionImageCommand { get; }
    public ICommand RestartGameCommand { get; }
    public bool IsUkrLocalization
    {
        get => _isUkrLocalization;
        set
        {
            _isUkrLocalization = value;
            OnPropertyChanged();
        }
    }
    public BitmapImage FirstImage
    {
        get => _firstImage;
        set
        {
            _firstImage = value;
            OnPropertyChanged();
        }
    }
    
    private bool _isUkrLocalization;
    private BitmapImage _firstImage;

    public MainWindowViewModel(GameObservable startGame, IGameFactory gameFactory, List<Image> questionImagesUkr)
    {
        ImageNumbers = new Dictionary<string, int>
        {
            { "FirstImage", 1 },
            { "SecondImage", 2 },
            { "ThirdImage", 3 },
            { "FourthImage", 4 },
            { "FifthImage", 5 },
            { "SixthImage", 6 },
            { "SeventhImage", 7 },
            { "EighthImage", 8 },
            { "NinthImage", 9 },
            { "TenthImage", 10 },
            { "EleventhImage", 11 },
            { "TwelfthImage", 12 },
            { "ThirteenthImage", 13 },
            { "FourteenthImage", 14 },
            { "FifteenthImage", 15 }
        };
        QuestionImagesUkr = questionImagesUkr;
        _isUkrLocalization = true;
        GameFactory = gameFactory;
        Game = startGame;
        GameStatistics = new GameStatisticsDataCollector(Game);
        BitmapImagesUkr = InitBitmapImages(Game);
        ShowQuestionImageCommand = new ShowQuestionWindowCommand(this);
        RestartGameCommand = new RestartGameCommand(this);
        _firstImage = BitmapImageConverter.ConvertBytesToImage(Game.Questions[0].QuestionImage.BlackWhiteImageSource);
        Exper = new List<BitmapImage>() { FirstImage };
    }

    public void ChangeLocal()
    {
        IsUkrLocalization = !IsUkrLocalization;
    }

    private List<(BitmapImage blackWhite, BitmapImage colored)> InitBitmapImages(GameObservable game)
    {
        var images = new List<(BitmapImage blackWhite, BitmapImage colored)>();
        for (int i = 0; i < game.Questions.Length; i++)
        {
            var blackWhiteImage =
                BitmapImageConverter.ConvertBytesToImage(game.Questions[i].QuestionImage.BlackWhiteImageSource);
            var coloredImage =
                BitmapImageConverter.ConvertBytesToImage(game.Questions[i].QuestionImage.ColoredImageSource);
            images.Add((blackWhiteImage, coloredImage));
        }

        return images;
    }
}