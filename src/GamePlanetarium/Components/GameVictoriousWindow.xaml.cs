using System.Windows;
using System.Windows.Input;

namespace GamePlanetarium.Components
{
    public partial class GameVictoriousWindow
    {
        private readonly bool _isUkrLocal;
        
        public GameVictoriousWindow(bool isUkrLocal)
        {
            _isUkrLocal = isUkrLocal;
            InitializeComponent();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e) => Close();

        private void CloseButton_OnTouchUp(object sender, TouchEventArgs e) => Close();

        private void GameVictoriousWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!_isUkrLocal)
            {
                MessageTextBlock!.Text = "Oho!\nYou will be friends with aliens :)";
            }
        }
    }
}