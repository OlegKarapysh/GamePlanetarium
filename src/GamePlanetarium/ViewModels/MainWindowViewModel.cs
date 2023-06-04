using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GamePlanetarium.Commands;
using GamePlanetarium.Converters;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public GameObservable Game { get; set; }
    public GameStatisticsDataCollector GameStatistics { get; set; }
    public IGameFactory GameFactory { get; }
    public List<(BitmapImage blackWhite, BitmapImage colored)> BitmapImagesUkr { get; }
    public List<(BitmapImage blackWhite, BitmapImage colored)> BitmapImagesEng { get; }
    public ICommand ShowQuestionImageCommand { get; }
    public ICommand RestartGameCommand { get; }
    public ICommand ChangeLocalizationCommand { get; }
    public ObservableCollection<QuestionImageViewModel> QuestionImages { get; }
    public bool IsUkrLocalization
    {
        get => _isUkrLocalization;
        set
        {
            _isUkrLocalization = value;
            GameTitle = value ? GameTitleUkr : GameTitleEng;
            OnPropertyChanged();
        }
    }
    public string GameTitle
    {
        get => _gameTitle;
        set
        {
            _gameTitle = value;
            OnPropertyChanged();
        }
    }

    private const string GameTitleUkr = "ТЕСТ НА УВАЖНІСТЬ";
    private const string GameTitleEng = "ATTENTION TEST";
    private bool _isUkrLocalization = true;
    private string _gameTitle = GameTitleUkr;
    private readonly QuestionImageViewModel.QuestionImagePosition[] _questionImagePositions;

    public MainWindowViewModel(GameObservable startGame, IGameFactory gameFactory)
    {
        GameFactory = gameFactory;
        Game = startGame;
        GameStatistics = new GameStatisticsDataCollector(Game);
        _questionImagePositions = new QuestionImageViewModel.QuestionImagePosition[]
        {
            new(15, 530, 11),
            new(0, 850, -5),
            new(10, 1150, 21),
            new(15, 1480, 25),
            new(230, 360, -29),
            new(230, 650, -15),
            new(320, 1020, -2),
            new(305, 1270, -15),
            new(330, 1530, 12),
            new(590, 1420, 13),
            new(580, 1150, 3),
            new(580, 900, -4),
            new(580, 600, 15),
            new(550, 320, -9),
            new(470, 40, 18)
        };
        BitmapImagesUkr = InitBitmapImages(GameFactory.UkrImg);
        BitmapImagesEng = InitBitmapImages(GameFactory.EngImg);
        QuestionImages = InitQuestionImageViewModels(
            BitmapImagesUkr.Select(i => i.blackWhite).ToArray(), _questionImagePositions);
        
        ShowQuestionImageCommand = new ShowQuestionWindowCommand(this);
        RestartGameCommand = new RestartGameCommand(this);
        ChangeLocalizationCommand = new ChangeLocalizationCommand(this);
    }

    public void ChangeLocal()
    {
        IsUkrLocalization = !IsUkrLocalization;
    }

    private List<(BitmapImage blackWhite, BitmapImage colored)> InitBitmapImages(ImageSeed imageSeed)
    {
        var result = new List<(BitmapImage blackWhite, BitmapImage colored)>();
        foreach (var questionImage in imageSeed.QuestionImages)
        {
            var blackWhiteImage =
                BitmapImageConverter.ConvertBytesToImage(questionImage.BlackWhiteImageSource);
            var coloredImage =
                BitmapImageConverter.ConvertBytesToImage(questionImage.ColoredImageSource);
            result.Add((blackWhiteImage, coloredImage));
        }

        return result;
    }

    private ObservableCollection<QuestionImageViewModel> InitQuestionImageViewModels(
        BitmapImage[] images, QuestionImageViewModel.QuestionImagePosition[] positions)
    {
        var result = new ObservableCollection<QuestionImageViewModel>();
        for (int i = 0; i < images.Length; i++)
        {
            result.Add(new QuestionImageViewModel((byte)i, images[i], positions[i]));
        }

        return result;
    }
}