namespace GamePlanetarium.ViewModels;

public class InfoWindowViewModel : ViewModelBase
{
    public enum MessageType
    {
        IncorrectAnswer = 0,
        GameVictory = 1
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }
    public string InfoImageSource
    {
        get => _infoImageSource;
        set
        {
            _infoImageSource = value;
            OnPropertyChanged();
        }
    }

    private const string IncorrectAnswerMessageUkr = "Цікава відповідь,\nале можливо спробуємо ще раз?";
    private const string IncorrectAnswerMessageEng = "The option is interesting,\nbut can we try again?";
    private const string GameVictoryMessageUkr = "Воу!\nЗ інопланетянами ти круто потоваришуєш :)";
    private const string GameVictoryMessageEng = "Oho!\nYou will be friends with aliens :)";
    private readonly (string Ukr, string Eng)[] _messagesByType =
    {
        (IncorrectAnswerMessageUkr, IncorrectAnswerMessageEng),
        (GameVictoryMessageUkr, GameVictoryMessageEng)
    };
    private readonly string[] _infoImageSourcesByType =
    {
        "pack://application:,,,/GamePlanetarium;component/Images/thinking_emoji.png",
        "pack://application:,,,/GamePlanetarium;component/Images/victory_emoji.png"
    };
    private string _message = null!;
    private string _infoImageSource = null!;

    public InfoWindowViewModel(MessageType type, bool isUkr)
    {
        var typeInt = (int)type;
        Message = isUkr ? _messagesByType[typeInt].Ukr : _messagesByType[typeInt].Eng;
        InfoImageSource = _infoImageSourcesByType[typeInt];
    }
}