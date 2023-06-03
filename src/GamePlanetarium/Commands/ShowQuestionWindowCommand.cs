using System;
using System.Windows.Controls;
using System.Windows.Input;
using GamePlanetarium.Components;
using GamePlanetarium.Converters;
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
        var imageNumber = _mainWindow.ImageNumbers[image.Name!] - 1;
        new QuestionWindow(_mainWindow.Game, (byte)imageNumber, _mainWindow.IsUkrLocalization).ShowDialog();
        image.IsEnabled = false;
        image.Source = BitmapImageConverter.ConvertBytesToImage(
            _mainWindow.Game.Questions[imageNumber].QuestionImage.ColoredImageSource);
    }
}