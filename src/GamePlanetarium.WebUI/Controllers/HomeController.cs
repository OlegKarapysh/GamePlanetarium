using System.Diagnostics;
using GamePlanetarium.Domain.Entities;
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}