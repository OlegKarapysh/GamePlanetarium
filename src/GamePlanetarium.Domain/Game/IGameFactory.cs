using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.GameSeeds;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public interface IGameFactory
{
    QuestionTextSeed QuestionTextSeed { get; }
    public QuestionImage[] QuestionImages { get; }
    public Answers[] CorrectAnswers { get; }

    GameObservable GetGameBySeed();
}