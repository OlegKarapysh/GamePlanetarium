using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AutoMapper;
using GamePlanetarium.Commands;
using GamePlanetarium.Components;
using GamePlanetarium.Converters;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public GameObservable Game { get; set; }
    public GameStatisticsDataCollector GameStatistics { get; set; }
    public IGameFactory GameFactoryUkr { get; }
    public IGameFactory GameFactoryEng { get; }
    public Mapper Mapper { get; }
    public List<(BitmapImage BlackWhite, BitmapImage Colored)> BitmapImagesUkr { get; }
    public List<(BitmapImage BlackWhite, BitmapImage Colored)> BitmapImagesEng { get; }
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
    // TODO: Fix title string position during localization change.
    private const string GameTitleUkr = "ТЕСТ НА УВАЖНІСТЬ";
    private const string GameTitleEng = "ATTENTION TEST";
    private bool _isUkrLocalization = true;
    private string _gameTitle = GameTitleUkr;
    private readonly QuestionImageViewModel.QuestionImagePosition[] _questionImagePositions;

    public MainWindowViewModel(IGameFactory gameFactoryUkr, IGameFactory gameFactoryEng, Mapper mapper)
    {
        GameFactoryEng = gameFactoryEng;
        GameFactoryUkr = gameFactoryUkr;
        GameFactoryEng.OnGameEnded = (_, _) => ShowVictoryWindow();
        GameFactoryUkr.OnGameEnded = (_, _) => ShowVictoryWindow();
        Mapper = mapper;
        Game = gameFactoryUkr.GetGameBySeed();
        GameStatistics = new GameStatisticsDataCollector(Game);
        // TODO: Fix images' positions and size to fit 10 question game.
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
        BitmapImagesUkr = InitBitmapImages(gameFactoryUkr.QuestionImages);
        BitmapImagesEng = InitBitmapImages(gameFactoryEng.QuestionImages);
        QuestionImages = InitQuestionImageViewModels(
            BitmapImagesUkr.Select(i => i.BlackWhite).ToArray(), _questionImagePositions);
        
        ShowQuestionImageCommand = new ShowQuestionWindowCommand(this);
        RestartGameCommand = new RestartGameCommand(this);
        ChangeLocalizationCommand = new ChangeLocalizationCommand(this);
    }

    public void ChangeLocal()
    {
        IsUkrLocalization = !IsUkrLocalization;
    }

    private List<(BitmapImage BlackWhite, BitmapImage Colored)> InitBitmapImages(QuestionImage[] imageSeed)
    {
        var result = new List<(BitmapImage BlackWhite, BitmapImage Colored)>();
        foreach (var questionImage in imageSeed)
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

    private void ShowVictoryWindow() => new InfoWindow(new InfoWindowViewModel(
        InfoWindowViewModel.MessageType.GameVictory, IsUkrLocalization)).ShowDialog();
}