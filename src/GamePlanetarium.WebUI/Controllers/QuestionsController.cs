using GamePlanetarium.Domain.Answer;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
using GamePlanetarium.Domain.Question;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamePlanetarium.WebUI.Controllers;

public class QuestionsController : Controller
{
    private readonly GameDb _db;
    
    public QuestionsController(GameDb db) => _db = db;

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
    public async Task<IActionResult> EditQuestion(int qid)
    {
        return View(await _db.Questions
                             .Include(q => q.Answers).Include(q => q.QuestionImage)
                             .FirstAsync(q => q.Id == qid));
    }
    
    [HttpPost]
    public async Task<IActionResult> EditQuestion()
    {
        var questionEntity = await GetQuestionEntityFromForm();
        if (questionEntity.QuestionImage is null)
        {
            return Json(new
            {
                success = false,
                errorMessage = "Треба завантажити обидві валідні картинки питання у форматі png!"
            });
        }
        _db.Questions.Update(questionEntity);
        await _db.SaveChangesAsync();
        return Json(new
        {
            success = true,
            redirect = $"/Questions/DetailsQuestion/{questionEntity.Id}"
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> DetailsQuestion(int id)
    {
        var requestedQuestion = await _db.Questions
                                         .Include(q => q.QuestionImage).Include(q => q.Answers)
                                         .FirstAsync(q => q.Id == id);
        return View(requestedQuestion);
    }

    [NonAction]
    private async Task<QuestionEntity> GetQuestionEntityFromForm()
    {
        var form = HttpContext.Request.Form;
        var blackWhiteImage = form.Files["blackWhiteImage"];
        var coloredImage = form.Files["coloredImage"];
        byte[]? blackWhiteImageBytes = null!, coloredImageBytes = null!;
        if (coloredImage is not null && blackWhiteImage is not null &&
            coloredImage.Length > 0 && blackWhiteImage.Length > 0 &&
            Path.GetExtension(coloredImage.FileName) == QuestionImage.ImageExtension &&
            Path.GetExtension(blackWhiteImage.FileName) == QuestionImage.ImageExtension)
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
        var questionEntity = new QuestionEntity
        {
            Id = Convert.ToInt32(form["Id"]),
            IsUkr = Convert.ToBoolean(form["IsUkr"]),
            QuestionNumber = Convert.ToInt32(form["QuestionNumber"]),
            QuestionText = form["QuestionText"]!,
            Answers = answers,
        };
        if (blackWhiteImageBytes is not null && coloredImageBytes is not null)
        {
            questionEntity.QuestionImage = new QuestionImageEntity
            {
                Id = Convert.ToInt32(form["QuestionImageId"]),
                ImageName = form["QuestionImageName"]!,
                BlackWhiteImageSource = blackWhiteImageBytes,
                ColoredImageSource = coloredImageBytes,
                HashCode = coloredImageBytes.GetHashCode()
            };
        }
        return questionEntity;
    }
}