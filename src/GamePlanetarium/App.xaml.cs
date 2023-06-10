﻿using System;
using System.Linq;
using System.Windows;
using AutoMapper;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Entities.Statistics;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.GameSeeds;
using GamePlanetarium.Domain.Mappings;
using GamePlanetarium.Domain.Question;
using GamePlanetarium.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GamePlanetarium;

public partial class App
{
    private const string ConnectionString =
        @"Server=(localdb)\MSSQLLocalDB;Database=GamePlanetarium;Trusted_Connection=True;";
    private Mapper? _mapper;
    private MainWindowViewModel? _mainWindowViewModel;

    protected override void OnStartup(StartupEventArgs e)
    {
        _mapper = CreateConfiguredMapper();
        using var db = new GameDb(ConnectionString);
        {
            // var correctAns = new Answers[]
            // {
            //     Answers.Second,
            //     Answers.Third,
            //     Answers.First,
            //     Answers.Third,
            //     Answers.Third,
            //     Answers.First,
            //     Answers.Second,
            //     Answers.Third,
            //     Answers.Third,
            //     Answers.First
            // };
            // var fUkr = new GameFactory(
            //     new QuestionTextSeed(new QuestionsSeedUkr().QuestionsText),
            //     new ImageSeedUkr().QuestionImages, correctAns);
            // var fEng = new GameFactory(
            //     new QuestionTextSeed(new QuestionsSeedEng().QuestionsText),
            //     new ImageSeedEng().QuestionImages, correctAns);
            // db.Database.EnsureCreated();
            // int counter = 1;
            // foreach (var question in fUkr.GetGameBySeed().Questions)
            // {
            //     var questionEntity = _mapper.Map<QuestionEntity>((SingleAnswerQuestion)question)!;
            //     questionEntity.QuestionNumber = counter;
            //     counter++;
            //     questionEntity.IsUkr = true;
            //     db.Questions.Add(questionEntity);
            // }
            //
            // counter = 1;
            // foreach (var question in fEng.GetGameBySeed().Questions)
            // {
            //     var questionEntity = _mapper.Map<QuestionEntity>((SingleAnswerQuestion)question)!;
            //     questionEntity.QuestionNumber = counter;
            //     counter++;
            //     questionEntity.IsUkr = false;
            //     db.Questions.Add(questionEntity);
            // }
            // db.SaveChanges();

            var (questionTextSeedUkr, questionImagesUkr, correctAnswers1) = GetGameSeedsFromDb(
                db, q => q.QuestionNumber <= GameObservable.QuestionsCount && q.IsUkr);
            var (questionTextSeedEng, questionImagesEng, correctAnswers2) = GetGameSeedsFromDb(
                db, q => q.QuestionNumber <= GameObservable.QuestionsCount && !q.IsUkr);
            var gameFactoryUkr = new GameFactory(questionTextSeedUkr, questionImagesUkr, correctAnswers1);
            var gameFactoryEng = new GameFactory(questionTextSeedEng, questionImagesEng, correctAnswers2);
            _mainWindowViewModel = new MainWindowViewModel(gameFactoryUkr, gameFactoryEng, _mapper);
            MainWindow = new MainWindow(_mainWindowViewModel);
        }
        MainWindow.Show();

        base.OnStartup(e);
    }

    private Mapper CreateConfiguredMapper()
    {
        return new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<QuestionStatisticsProfile>();
            cfg.AddProfile<GameStatisticsProfile>();
            cfg.AddProfile<QuestionImageProfile>();
            cfg.AddProfile<AnswerProfile>();
            cfg.AddProfile<QuestionProfile>();
        }));
    }

    private (QuestionTextSeed, QuestionImage[], Answers[]) GetGameSeedsFromDb(
        GameDb db, Func<QuestionEntity, bool> gameQuestionsPredicate)
    {
        var game = new GameObservable(db.Questions
                                        .Include(q => q.Answers).Include(q => q.QuestionImage)
                                        .Where(gameQuestionsPredicate).ToArray()
                                        .Select(q => _mapper!.Map<SingleAnswerQuestion>(q)).ToArray());
        return (game.GetQuestionTextSeed(), game.GetQuestionImages(), game.GetCorrectAnswers());
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        if (_mainWindowViewModel!.Game.Questions.Any(q => q.IsAnswered))
        {
            using var db = new GameDb(ConnectionString);
            db.GameStatistics.Add(_mapper!.Map<GameStatisticsDataEntity>(_mainWindowViewModel!.GameStatistics)!);
            db.SaveChanges();
        }
    }
}
