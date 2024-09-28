using CadastroEquipesConsumidor.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class EquipesPessoasController : Controller
    {

        private readonly HttpClient _httpClient;

        // Aqui deve ser atualizado o endereço da API
        string apiEquipes = "https://localhost:7088/api/EquipesPessoas";

        public EquipesPessoasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Exibe a lista de equipes com pessoas
        public async Task<IActionResult> Index()
        {
            try
            {
                var equipes = await _httpClient.GetFromJsonAsync<List<EquipesPessoasModel>>(apiEquipes);
                if (equipes == null)
                {
                    return View("Error", "Não foi possível obter as pessoas.");
                }

                return View(equipes); // Passa a lista de equipes para a view
            }
            catch (HttpRequestException e)
            {
                return View("Error", $"Erro ao acessar a API: {e.Message}");

            }
            catch (Exception e)
            {
                return View("Error", $"Ocorreu um erro inesperado: {e.Message}");
            }
        }
    }
}
