using System.Windows;
using System.Windows.Input;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
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
        _mainWindowViewModel = mainWindowViewModel;
        InitializeComponent();
        DataContext = _mainWindowViewModel;
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
}
