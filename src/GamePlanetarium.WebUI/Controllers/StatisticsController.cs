using GamePlanetarium.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamePlanetarium.WebUI.Controllers;

public class StatisticsController : Controller
{
    private readonly GameDb _db;

    public StatisticsController(GameDb db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GameStatistics()
    {
        return View(await _db.GameStatistics.ToArrayAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GameStatisticsDetails(int id)
    {
        return View(await _db.GameStatistics
                             .Include(g => g.QuestionsStatistics)
                             .FirstAsync(g => g.Id == id));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteGameStatistics(int id)
    {
        var gameStatistics = await _db.GameStatistics.FirstAsync(g => g.Id == id);
        _db.GameStatistics.Remove(gameStatistics);
        await _db.SaveChangesAsync();
        return RedirectToAction("GameStatistics");
    }
}