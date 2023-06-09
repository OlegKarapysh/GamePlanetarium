using System.Diagnostics;
using GamePlanetarium.Domain.Entities;
using GamePlanetarium.Domain.Entities.GameData;
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
        return Content(id.ToString());
    }
    
    [HttpPost]
    public async Task<IActionResult> EditQuestion(QuestionEntity questionEntity)
    {
        // _db.Questions.Update(questionEntity);
        // await _db.SaveChangesAsync();
        return Content(questionEntity.ToString());
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
}