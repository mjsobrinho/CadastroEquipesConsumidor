using CadastroEquipesConsumidor.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorPage()
        {
            var errorViewModel = new ErrorViewModel
            {
                Message = "Erro ao acessar a API"
            };

            return View("Error", errorViewModel);
        }
    }

}
