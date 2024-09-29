using CadastroEquipesConsumidor.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class EquipesPessoasController : Controller
    {
        private readonly HttpClient _httpClient;

        // Aqui deve ser atualizado o endereço da API
        string apiEquipes = "https://localhost:7088/api/EquipesPessoas";
        string apiPessoas = "https://localhost:7088/api/Pessoas";

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



        [HttpGet]
        public async Task<IActionResult> GetPessoa(string cpf)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{apiPessoas}/{cpf}");
                if (response.IsSuccessStatusCode)
                {
                    var pessoa = await response.Content.ReadFromJsonAsync<PessoasModel>();
                    return Json(pessoa); // Retorna os dados da pessoa em formato JSON
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    string errorMessage = errorResponse?.Message ?? "Pessoa não encontrada. Confirme o cadastro.";
                    return Json(new { success = false, message = errorMessage });
                }
            }
            catch (HttpRequestException e)
            {
                return Json(new { success = false, message = $"Erro ao acessar a API: {e.Message}" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = $"Ocorreu um erro inesperado: {e.Message}" });
            }
        }

    }


}
