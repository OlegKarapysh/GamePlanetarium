using AutoMapper;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Question;

namespace GamePlanetarium.Domain.Mappings;

public class QuestionImageProfile : Profile
{
    public QuestionImageProfile()
    {
        CreateMap<QuestionImage, QuestionImageEntity>()!
            .ForMember(d => d.ImageName, s => s.MapFrom(f => f.ImageName))!
            .ForMember(d => d.HashCode, s => s.MapFrom(f => f.GetHashCode()))!
            .ForMember(d => d.BlackWhiteImageSource, s => s.MapFrom(f => f.BlackWhiteImageSource))!
            .ForMember(d => d.ColoredImageSource, s => s.MapFrom(f => f.ColoredImageSource));
    }
}