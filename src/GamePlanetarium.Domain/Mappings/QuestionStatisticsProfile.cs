using AutoMapper;
using GamePlanetarium.Domain.Entities.Statistics;
using GamePlanetarium.Domain.Statistics;

namespace GamePlanetarium.Domain.Mappings;

public class QuestionStatisticsProfile : Profile
{
    public QuestionStatisticsProfile()
    {
        CreateMap<QuestionStatisticsData, QuestionStatisticsDataEntity>()!
            .ForMember(d => d.QuestionOrder, s => s.MapFrom(f => f.QuestionOrder))!
            .ForMember(d => d.IncorrectAnswersCount, s => s.MapFrom(f => f.IncorrectAnswersCount))!
            .ForMember(d => d.FirstAnswerText, s => s.MapFrom(f => f.FirstAnswerText))!
            .ReverseMap();
    }
}