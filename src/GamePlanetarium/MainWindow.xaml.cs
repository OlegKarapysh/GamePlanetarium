using System.Windows;
using System.Windows.Input;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium;

public partial class MainWindow
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
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
        _mainWindowViewModel.RestartGameCommand.Execute(WorkProgressBar);
    }
}
