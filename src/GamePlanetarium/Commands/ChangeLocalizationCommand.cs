using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium.Commands;

public class ChangeLocalizationCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly MainWindowViewModel _mainWindow;
    
    public ChangeLocalizationCommand(MainWindowViewModel mainWindow) => _mainWindow = mainWindow;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        var progressBar = (ProgressBar)parameter!;
        progressBar.Visibility = Visibility.Visible;
        var backgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
        backgroundWorker.DoWork += (sender, _) =>
        {
            var currentProgressPercentage = 0;
            var progressStep = (int)Math.Ceiling(100m / _mainWindow.QuestionImages.Count);
            var worker = (sender as BackgroundWorker)!;
            worker.ReportProgress(currentProgressPercentage);

            QuestionsSeed questionsSeed;
            List<(BitmapImage blackWhite, BitmapImage colored)> bitmapImages;
            if (_mainWindow.IsUkrLocalization)
            {
                questionsSeed = _mainWindow.GameFactory.EngSeed;
                bitmapImages = _mainWindow.BitmapImagesEng;
            }
            else
            {
                questionsSeed = _mainWindow.GameFactory.UkrSeed;
                bitmapImages = _mainWindow.BitmapImagesUkr;
            }
            _mainWindow.Game.ChangeQuestionsTextBySeed(questionsSeed);
            _mainWindow.ChangeLocal();
            for (int i = 0; i < _mainWindow.QuestionImages.Count; i++)
            {
                _mainWindow.QuestionImages[i].ImageSource =
                    _mainWindow.QuestionImages[i].IsEnabled ? bitmapImages[i].blackWhite : bitmapImages[i].colored;
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