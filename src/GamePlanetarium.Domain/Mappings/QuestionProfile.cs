using AutoMapper;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Mappings;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question.Question, QuestionEntity>()!
            .ForMember(d => d.QuestionText, s => s.MapFrom(f => f.Text))!
            .ForMember(d => d.IsSingleAnswer, s => s.MapFrom(f => f is SingleAnswerQuestion))!
            .ForMember(d => d.Answers, s => s.MapFrom(f => f.Answers))!
            .ForMember(d => d.BlackWhiteImage, s => s.MapFrom(f => f.BlackWhiteImage))!
            .ForMember(d => d.ColoredImage, s => s.MapFrom(f => f.ColoredImage));
    }
}