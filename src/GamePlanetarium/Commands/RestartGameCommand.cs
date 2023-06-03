using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        // TODO: write statistics to database.
        if (_mainWindow.Game.Questions.All(q => !q.IsAnswered))
        {
            return;
        }
        var progressBar = (ProgressBar)parameter!;
        progressBar.Visibility = Visibility.Visible;
        var backgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
        backgroundWorker.DoWork += (sender, _) =>
        {
            _mainWindow.Game = _mainWindow.GameFactory.GetGameByLocal(_mainWindow.IsUkrLocalization);
            _mainWindow.GameStatistics = new GameStatisticsDataCollector(_mainWindow.Game);
            if (_mainWindow.IsUkrLocalization)
            {
                for (int i = 0; i < _mainWindow.Game.Questions.Length; i++)
                {
                    (sender as BackgroundWorker)!.ReportProgress(i * (100 / _mainWindow.Game.Questions.Length + 2));
                    _mainWindow.FirstImage = _mainWindow.BitmapImagesUkr[i].colored;

                    //_mainWindow.QuestionImagesUkr[i].Source = _mainWindow.BitmapImagesUkr[i].blackWhite;
                    //_mainWindow.QuestionImagesUkr[i].IsEnabled = true;
                }
            }
        };
        backgroundWorker.ProgressChanged += (_, args) =>
        {
            progressBar.Value = args.ProgressPercentage;
            if (args.ProgressPercentage >= 100)
            {
                progressBar.Visibility = Visibility.Hidden;
            }
        };
        backgroundWorker.RunWorkerAsync();
    }
}