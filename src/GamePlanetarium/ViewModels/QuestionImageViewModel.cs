using System.Windows.Media.Imaging;

namespace GamePlanetarium.ViewModels;

public class QuestionImageViewModel : ViewModelBase
{
    public readonly struct QuestionImagePosition
    {
        public readonly int CanvasTop;
        public readonly int CanvasLeft;
        public readonly int RotateAngle;

        public QuestionImagePosition(int canvasTop, int canvasLeft, int rotateAngle)
        {
            CanvasTop = canvasTop;
            CanvasLeft = canvasLeft;
            RotateAngle = rotateAngle;
        }
    }
    public BitmapImage ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            OnPropertyChanged();
        }
    }
    public byte QuestionImageTagIndex { get; }
    public int CanvasTop { get; }
    public int CanvasLeft { get; }
    public int RotateAngle { get; }

    private BitmapImage _imageSource;
    private bool _isEnabled;

    public QuestionImageViewModel(byte questionImageTagIndex, BitmapImage imageSource, QuestionImagePosition position)
    {
        QuestionImageTagIndex = questionImageTagIndex;
        _imageSource = imageSource;
        IsEnabled = true;
        CanvasTop = position.CanvasTop;
        CanvasLeft = position.CanvasLeft;
        RotateAngle = position.RotateAngle;
    }
}