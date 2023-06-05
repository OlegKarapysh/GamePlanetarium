using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GamePlanetarium.Components;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.GameSeeds;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly Mapper _mapper;
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public MainWindow(Mapper mapper)
    {
        _mapper = mapper;
        var gameFactoryUkr = new GameFactory((_, _) => ShowVictoryWindow(),
            new QuestionTextSeed(new QuestionsSeedUkr().QuestionsText),
            new ImageSeedUkr().QuestionImages, new[]
            {
                Answers.First, Answers.First, Answers.First, Answers.First, Answers.First,
                Answers.First, Answers.First, Answers.First, Answers.First, Answers.First,
                Answers.First, Answers.First, Answers.First, Answers.First, Answers.First
            });
        var gameFactoryEng = new GameFactory((_, _) => ShowVictoryWindow(),
            new QuestionTextSeed(new QuestionsSeedEng().QuestionsText),
            new ImageSeedEng().QuestionImages, new[]
            {
                Answers.First, Answers.First, Answers.First, Answers.First, Answers.First,
                Answers.First, Answers.First, Answers.First, Answers.First, Answers.First,
                Answers.First, Answers.First, Answers.First, Answers.First, Answers.First
            });
        // TODO: get startGame from db.
        // var engGame = gameFactory.GetGameByLocal(false);
        // using (var db = new GameDb(
        //            @"Server=(localdb)\MSSQLLocalDB;Database=GamePlanetarium;Trusted_Connection=True;"))
        // {
        //     foreach (var question in engGame.Questions)
        //     {
        //         db.Questions.Add(_mapper.Map<QuestionEntity>((SingleAnswerQuestion)question)!);
        //     }
        //     db.SaveChanges();
        // }
        InitializeComponent();
        DataContext = _mainWindowViewModel = new MainWindowViewModel(gameFactoryUkr, gameFactoryEng);
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
