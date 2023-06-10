using System.Diagnostics;
using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Game;
using GamePlanetarium.Domain.Question;
using Microsoft.AspNetCore.Mvc;
using GamePlanetarium.WebUI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePlanetarium.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly GameDb _db;
    private readonly ILogger<HomeController> _logger;

    public HomeController(GameDb db, ILogger<HomeController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _db.Answers.ToArrayAsync());
    }
    
    [HttpGet]
    public async Task<IActionResult> QuestionsUkr()
    {
        ViewData["CurrentQuestionsLocalIsUkr"] = true;
        return View("Questions", await _db.Questions
                                          .Where(q => q.IsUkr)
                                          .ToArrayAsync());
    }
    
    [HttpGet]
    public async Task<IActionResult> QuestionsEng()
    {
        ViewData["CurrentQuestionsLocalIsUkr"] = false;
        return View("Questions", await _db.Questions
                                          .Where(q => !q.IsUkr)
                                          .ToArrayAsync());
    }

    [HttpGet]
    public async Task<IActionResult> EditQuestion(int id)
    {
        return View(await _db.Questions
                             .Include(q => q.Answers).Include(q => q.QuestionImage)
                             .FirstAsync(q => q.Id == id));
    }
    
    [HttpPost]
    public async Task<IActionResult> EditQuestion()
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("QuestionsUkr");
        }

        var questionEntity = await GetQuestionEntityFromForm();
         _db.Questions.Update(questionEntity);
         await _db.SaveChangesAsync();
         return RedirectToAction(questionEntity.IsUkr ? "QuestionsUkr" : "QuestionsEng");
    }
    
    [HttpGet]
    public async Task<IActionResult> DetailsQuestion(int id)
    {
        var requestedQuestion = await _db.Questions
                                         .Include(q => q.QuestionImage).Include(q => q.Answers)
                                         .FirstAsync(q => q.Id == id);
        return View(requestedQuestion);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteGameStatistics(int id)
    {
        return Content(id.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [NonAction]
    private async Task<QuestionEntity> GetQuestionEntityFromForm()
    {
        var form = HttpContext.Request.Form;
        var blackWhiteImage = form.Files["blackWhiteImage"];
        var coloredImage = form.Files["coloredImage"];
        byte[] blackWhiteImageBytes = null!, coloredImageBytes = null!;
        if (coloredImage is not null && blackWhiteImage is not null &&
            coloredImage.Length > 0 && blackWhiteImage.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await blackWhiteImage.CopyToAsync(memoryStream);
                blackWhiteImageBytes = memoryStream.ToArray();
            }
            using (var memoryStream = new MemoryStream())
            {
                await coloredImage.CopyToAsync(memoryStream);
                coloredImageBytes = memoryStream.ToArray();
            }
        }
        var answers = new AnswerEntity[Enum.GetValues(typeof(Answers)).Length];
        var correctAnswerNumber = Convert.ToInt32(form["CorrectAnswer"]);
        foreach (var answerNumber in Enum.GetValues(typeof(Answers)))
        {
            var i = (int)answerNumber;
            answers[i] = new AnswerEntity
            {
                Id = Convert.ToInt32(form[$"Answers[{i}].Id"]),
                AnswerOrder = Convert.ToInt32(form[$"Answers[{i}].AnswerOrder"]),
                AnswerText = form[$"Answers[{i}].AnswerText"]!,
                IsCorrect = i == correctAnswerNumber
            };
        }

        //var questionImage = new QuestionImage(imageName, blackWhiteImageBytes, coloredImageBytes);
        
        var questionEntity = new QuestionEntity
        {
            Id = Convert.ToInt32(form["Id"]),
            IsUkr = Convert.ToBoolean(form["IsUkr"]),
            QuestionNumber = Convert.ToInt32(form["QuestionNumber"]),
            QuestionText = form["QuestionText"]!,
            Answers = answers,
            QuestionImage = new QuestionImageEntity
            {
                Id = Convert.ToInt32(form["QuestionImageId"]),
                ImageName = form["QuestionImageName"]!,
                BlackWhiteImageSource = blackWhiteImageBytes,
                ColoredImageSource = coloredImageBytes,
                HashCode = coloredImageBytes.GetHashCode()
            }
        };
        // foreach (var answer in questionEntity.Answers)
        // {
        //     answer.Question = questionEntity;
        // }
        return questionEntity;
    }
}