using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Game;

public interface IGameObservable
{
    event EventHandler? GameEnded;
    event EventHandler<AnsweredQuestionInfo>? TriedAnsweringQuestion;
}