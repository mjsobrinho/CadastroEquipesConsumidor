using Microsoft.AspNetCore.Mvc;
using CadastroEquipesConsumidor.Models;

namespace CadastroEquipesConsumidor.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ErrorPage()
    {
        var errorViewModel = new ErrorViewModel
        {
            Message = "Erro ao acessar a API"
        };

        return View("Error", errorViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorModel = new ErrorViewModel
        {
            RequestId = HttpContext.TraceIdentifier
        };
        return View(errorModel); // Certifique-se de passar o modelo correto
    }
}
