using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.ViewModels;

namespace GamePlanetarium.Components
{
    public partial class QuestionWindow
    {
        private readonly bool _isUkrLocal;
        private readonly GameObservable _game;
        private readonly byte _questionIndex;
        private readonly Dictionary<string, Answers> _answersNumbers;

        public QuestionWindow(GameObservable game, byte questionIndex, bool isUkrLocal)
        {
            _isUkrLocal = isUkrLocal;
            _game = game;
            _questionIndex = questionIndex;
            _answersNumbers = new Dictionary<string, Answers>
            {
                { "Answer1Button", Answers.First },
                { "Answer2Button", Answers.Second },
                { "Answer3Button", Answers.Third }
            };
            InitializeComponent();
        }
        
        private void QuestionWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var currentQuestion = _game.Questions[_questionIndex];
            QuestionTextBlock!.Text = currentQuestion.Text;
            Answer1TextBlock!.Text = currentQuestion.Answers[(int)Answers.First].Text;
            Answer2TextBlock!.Text = currentQuestion.Answers[(int)Answers.Second].Text;
            Answer3TextBlock!.Text = currentQuestion.Answers[(int)Answers.Third].Text;
        }

        private void OnAnswerButtonActivated(object sender, EventArgs e)
        {
            var answer = _answersNumbers[((Button)sender).Name];
            if (_game.TryAnswerQuestion(_questionIndex, answer))
            {
                Close();
            }
            else
            {
                new InfoWindow(
                    new InfoWindowViewModel(InfoWindowViewModel.MessageType.IncorrectAnswer, _isUkrLocal))
                    .ShowDialog();
            }
        }
    }
}