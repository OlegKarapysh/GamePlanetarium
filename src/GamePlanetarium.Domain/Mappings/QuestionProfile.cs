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
            .ForMember(d => d.HasSingleAnswer, s => s.MapFrom(f => f is SingleAnswerQuestion))!
            .ForMember(d => d.Answers, s => s.MapFrom(f => f.Answers))!
            .ForMember(d => d.QuestionImage, s => s.MapFrom(f => f.QuestionImage));
        CreateMap<QuestionEntity, SingleAnswerQuestion>()!
            .ForMember(d => d.Text, s => s.MapFrom(f => f.QuestionText))!
            .ForMember(d => d.Answers, s => s.MapFrom(f => f.Answers.ToArray()))!
            .ForMember(d => d.QuestionImage, s => s.MapFrom(f => f.QuestionImage));
    }
}