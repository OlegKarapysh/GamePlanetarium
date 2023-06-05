using System.Diagnostics.CodeAnalysis;

namespace GamePlanetarium.Domain.GameSeeds;

public class QuestionTextSeed
{
    public required QuestionTextData[] Data { get; init; }

    [SetsRequiredMembers]
    public QuestionTextSeed(QuestionTextData[] data) => Data = data;
    public QuestionTextSeed()
    {
    }
}