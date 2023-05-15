using System.Windows;
using AutoMapper;
using GamePlanetarium.Domain.Mappings;

namespace GamePlanetarium;

public partial class App
{
    private Mapper _mapper;

    protected override void OnStartup(StartupEventArgs e)
    {
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<QuestionStatisticsProfile>();
            cfg.AddProfile<GameStatisticsProfile>();
            cfg.AddProfile<QuestionImageProfile>();
            cfg.AddProfile<AnswerProfile>();
            cfg.AddProfile<QuestionProfile>();
        }));
        
        MainWindow = new MainWindow(_mapper);
        MainWindow.Show();
        
        base.OnStartup(e);
    }
}
