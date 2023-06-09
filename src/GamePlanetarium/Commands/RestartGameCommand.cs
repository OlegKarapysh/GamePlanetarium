using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.Statistics;
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
        if (_mainWindow.Game.Questions.All(q => !q.IsAnswered))
        {
            return;
        }
        // TODO: Get connection string from file.
        using (var db = new GameDb(
                   @"Server=(localdb)\MSSQLLocalDB;Database=GamePlanetarium;Trusted_Connection=True;"))
        {
            db.GameStatistics.Add(_mainWindow.Mapper.Map<GameStatisticsDataEntity>(_mainWindow.GameStatistics)!);
            db.SaveChanges();
        }
        var progressBar = (ProgressBar)parameter!;
        progressBar.Visibility = Visibility.Visible;
        var backgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
        backgroundWorker.DoWork += (sender, _) =>
        {
            var currentProgressPercentage = 0;
            var progressStep = (int)Math.Ceiling(100m / _mainWindow.QuestionImages.Count);
            var worker = (sender as BackgroundWorker)!;
            worker.ReportProgress(currentProgressPercentage);
            var bitmapImages = _mainWindow.IsUkrLocalization
                ? _mainWindow.BitmapImagesUkr
                : _mainWindow.BitmapImagesEng;
            _mainWindow.Game = _mainWindow.IsUkrLocalization
                ? _mainWindow.GameFactoryUkr.GetGameBySeed()
                : _mainWindow.GameFactoryEng.GetGameBySeed();
            _mainWindow.GameStatistics = new GameStatisticsDataCollector(_mainWindow.Game);
            for (int i = 0; i < _mainWindow.QuestionImages.Count; i++)
            {
                _mainWindow.QuestionImages[i].ImageSource = bitmapImages[i].blackWhite;
                _mainWindow.QuestionImages[i].IsEnabled = true;
                worker.ReportProgress(currentProgressPercentage += progressStep);
                Thread.Sleep(1);
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