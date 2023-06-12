using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GamePlanetarium.WebUI.Models;

namespace GamePlanetarium.WebUI.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}