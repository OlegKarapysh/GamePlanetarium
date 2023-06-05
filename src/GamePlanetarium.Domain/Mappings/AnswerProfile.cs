using AutoMapper;
using GamePlanetarium.Domain.Entities.GameData;

namespace GamePlanetarium.Domain.Mappings;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<Answer.Answer, AnswerEntity>()!
            .ForMember(d => d.AnswerText, s => s.MapFrom(f => f.Text))!
            .ForMember(d => d.IsCorrect, s => s.MapFrom(f => f.IsCorrect))!
            .ForMember(d => d.AnswerOrder, s => s.MapFrom(f => (int)f.Number))!
            .ReverseMap();
    }
}