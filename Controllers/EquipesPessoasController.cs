using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class EquipesPessoasController : Controller
    {
        public IActionResult Index()
        {
            return View("EquipesPessoas"); // Retorna a view EquipePessoas.cshtml
        }
    }
}
