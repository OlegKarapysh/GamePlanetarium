namespace GamePlanetarium.Domain.Game;

public interface IGameFactory
{
    GameObservable GetGameBySeed(QuestionsSeed questionSeed, ImageSeed imageSeed);
    GameObservable GetGameByLocal(bool isUkrLocal);
}