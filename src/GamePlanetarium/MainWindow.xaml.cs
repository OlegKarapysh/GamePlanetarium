using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AutoMapper;
using GamePlanetarium.Components;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly Mapper _mapper;
    private readonly GameFactory _gameFactory;
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly List<Image> _questionImagesUkr;
    private bool _isUkrLocal;
    private bool _isUpperImageVisible;
    
    public MainWindow(Mapper mapper)
    {
        _mapper = mapper;
        _isUkrLocal = true;
        var questionsSeedUkr = new QuestionsSeedUkr();
        var imageSeed = new ImageSeedUkr();
        _gameFactory = new GameFactory(questionsSeedUkr, null, imageSeed, null, (_, _) => ShowVictoryWindow());
        // TODO: get startGame from db.
        var startGame = _gameFactory.GetGameByLocal(isUkrLocal: true);
        // using (var db = new GameDb(
        //            @"Server=(localdb)\MSSQLLocalDB;Database=GamePlanetarium;Trusted_Connection=True;"))
        // {
        //     foreach (var question in _game.Questions)
        //     {
        //         db.Questions.Add(_mapper.Map<QuestionEntity>((SingleAnswerQuestion)question)!);
        //     }
        //     db.SaveChanges();
        // }
        InitializeComponent();
        _questionImagesUkr = new List<Image>
        {
            FirstImage!, SecondImage!, ThirdImage!, FourthImage!, FifthImage!, SixthImage!, 
            SeventhImage!, EighthImage!, NinthImage!, TenthImage!, EleventhImage!, 
            TwelfthImage!, ThirteenthImage!, FourteenthImage!, FifteenthImage!
        };
        DataContext = _mainWindowViewModel = new MainWindowViewModel(startGame, _gameFactory, _questionImagesUkr);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        // TODO: something on load.
        // FirstImage!.Source = ConvertBytesToImage(_game.Questions[2].QuestionImage.ColoredImageSource);
    }

    private void OnQuestionImageActivated(object sender, MouseButtonEventArgs e)
    {
        _mainWindowViewModel.ShowQuestionImageCommand.Execute(sender);
        // TODO: Show question window dialog, show colored image, hide black-white image and disable it.
    }

    private void ChangeLocalizationImage_OnActivated(object sender, MouseButtonEventArgs e)
    {
        // TODO: change the text in the game and images.
    }

    private void RestartImage_OnActivated(object sender, MouseButtonEventArgs e)
    {
        // TODO: create new game, save and upload game statistics.
        _mainWindowViewModel.RestartGameCommand.Execute(WorkProgressBar);
    }
    
    private void ChangeImageButton_OnClick(object sender, RoutedEventArgs e)
    {
        _isUpperImageVisible = !_isUpperImageVisible;
        //ImageFromBytesUpper!.Visibility = _isUpperImageVisible ? Visibility.Visible : Visibility.Collapsed;
    }
    
    private void ShowVictoryWindow() => new GameVictoriousWindow(_isUkrLocal).ShowDialog();
}
