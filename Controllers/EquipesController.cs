using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class EquipesController : Controller
    {
        public IActionResult Index()
        {
            return View("Equipes"); // Retorna a view Equipes.cshtml
        }
    }
}

