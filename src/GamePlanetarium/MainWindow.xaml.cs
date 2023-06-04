using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GamePlanetarium.Components;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly Mapper _mapper;
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public MainWindow(Mapper mapper)
    {
        _mapper = mapper;
        var questionsSeedUkr = new QuestionsSeedUkr();
        var imageSeed = new ImageSeedUkr();
        var gameFactory = new GameFactory(questionsSeedUkr, new QuestionsSeedEng(), imageSeed, new ImageSeedEng(), 
            (_, _) => ShowVictoryWindow());
        // TODO: get startGame from db.
        var startGame = gameFactory.GetGameByLocal(isUkrLocal: true);
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
        DataContext = _mainWindowViewModel = new MainWindowViewModel(startGame, gameFactory);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        // TODO: something on load.
    }

    private void OnQuestionImageActivated(object sender, MouseButtonEventArgs e)
    {
        _mainWindowViewModel.ShowQuestionImageCommand.Execute(sender);
    }

    private void ChangeLocalizationImage_OnActivated(object sender, MouseButtonEventArgs e)
    {
        _mainWindowViewModel.ChangeLocalizationCommand.Execute(WorkProgressBar);
    }

    private void RestartImage_OnActivated(object sender, MouseButtonEventArgs e)
    {
        // TODO: create new game, save and upload game statistics.
        _mainWindowViewModel.RestartGameCommand.Execute(WorkProgressBar);
    }

    private void ShowVictoryWindow() => new GameVictoriousWindow(_mainWindowViewModel.IsUkrLocalization).ShowDialog();
}
