using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GamePlanetarium.Domain.Statistics;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium.Commands;

public class RestartGameCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly MainWindowViewModel _mainWindow;
    
    public RestartGameCommand(MainWindowViewModel mainWindow) => _mainWindow = mainWindow;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (_mainWindow.IsCommandExecuting)
        {
            return;
        }

        _mainWindow.IsCommandExecuting = true;
        Debug.Write("work-");
        var sender = (Image)parameter!;
        sender.IsEnabled = false;
        // TODO: write statistics to database.
        _mainWindow.Game = _mainWindow.GameFactory.GetGameByLocal(_mainWindow.IsUkrLocalization);
        _mainWindow.GameStatistics = new GameStatisticsDataCollector(_mainWindow.Game);
        if (_mainWindow.IsUkrLocalization)
        {
            for (int i = 0; i < _mainWindow.Game.Questions.Length; i++)
            {
                _mainWindow.QuestionImagesUkr[i].Source = _mainWindow.BitmapImagesUkr[i].blackWhite;
                _mainWindow.QuestionImagesUkr[i].IsEnabled = true;
            }
        }

        sender.IsEnabled = true;
        _mainWindow.IsCommandExecuting = false;
    }
}