using GamePlanetarium.Domain.GameSeeds;

namespace GamePlanetarium.Domain.Game;

public abstract class QuestionsSeed
{
    public abstract QuestionTextData[] QuestionsText { get; }
}