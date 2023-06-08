using System.Windows;
using System.Windows.Input;

namespace GamePlanetarium.Components
{
    public partial class IncorrectAnswerWindow
    {
        private readonly bool _isUkrLocal;
        
        public IncorrectAnswerWindow(bool isUkrLocal)
        {
            _isUkrLocal = isUkrLocal;
            InitializeComponent();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e) => Close();

        private void CloseButton_OnTouchUp(object sender, TouchEventArgs e) => Close();

        private void IncorrectAnswerWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!_isUkrLocal)
            {
                MessageTextBlock!.Text = "The option is interesting, but can we try again?";
            }
        }
    }
}