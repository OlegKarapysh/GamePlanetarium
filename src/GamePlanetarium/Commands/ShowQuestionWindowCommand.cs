using System;
using System.Windows.Controls;
using System.Windows.Input;
using GamePlanetarium.Components;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium.Commands;

public class ShowQuestionWindowCommand : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
    
    private readonly MainWindowViewModel _mainWindow;
    
    public ShowQuestionWindowCommand(MainWindowViewModel mainWindow) => _mainWindow = mainWindow;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        var image = (Image)parameter!;
        var imageNumber = (byte)image.Tag!;
        new QuestionWindow(_mainWindow.Game, imageNumber, _mainWindow.IsUkrLocalization).ShowDialog();
        var bitmapImages = _mainWindow.IsUkrLocalization ? _mainWindow.BitmapImagesUkr : _mainWindow.BitmapImagesEng;
        _mainWindow.QuestionImages[imageNumber].ImageSource = bitmapImages[imageNumber].colored;
        _mainWindow.QuestionImages[imageNumber].IsEnabled = false;
    }
}