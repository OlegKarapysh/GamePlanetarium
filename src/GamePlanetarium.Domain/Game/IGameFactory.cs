namespace GamePlanetarium.Domain.Game;

public interface IGameFactory
{
    Game GetGameWithLocalization(bool isUkrLocal);
}