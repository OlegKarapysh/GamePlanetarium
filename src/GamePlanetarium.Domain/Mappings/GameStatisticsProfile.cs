using AutoMapper;
using GamePlanetarium.Domain.Entities.Statistics;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium.Domain.Mappings;

public class GameStatisticsProfile : Profile
{
    public GameStatisticsProfile()
    {
        CreateMap<GameStatisticsDataCollector, GameStatisticsDataEntity>()!
            .ForMember(d => d.QuestionsAnsweredCount, s => s.MapFrom(f => f.QuestionsAnsweredCount))!
            .ForMember(d => d.IsGameEnded, s => s.MapFrom(f => f.IsGameEnded))!
            .ForMember(d => d.DateStamp, s => s.MapFrom(f => f.DateStamp))!
            .ForMember(d => d.QuestionsStatistics, s => s.MapFrom(f => f.QuestionsStatistics.Values));
    }
}