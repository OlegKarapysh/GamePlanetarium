namespace GamePlanetarium.Domain.Game;

public interface IGameFactory
{
    QuestionsSeed UkrSeed { get; }
    QuestionsSeed EngSeed { get; }
    ImageSeed UkrImg { get; }
    ImageSeed EngImg { get; }

    GameObservable GetGameBySeed(QuestionsSeed questionSeed, ImageSeed imageSeed);
    GameObservable GetGameByLocal(bool isUkrLocal);
}