using System.Windows;
using System.Windows.Input;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium.Components;

public partial class InfoWindow : Window
{
    public InfoWindow(InfoWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void CloseButton_OnTouchUp(object? sender, TouchEventArgs e) => OnCloseButtonActivated();
    private void CloseButton_OnClick(object sender, RoutedEventArgs e) => OnCloseButtonActivated();
    private void OnCloseButtonActivated() => Close();
}