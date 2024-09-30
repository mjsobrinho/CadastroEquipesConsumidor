using CadastroEquipesConsumidor.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class EquipesPessoasController : Controller
    {
        private readonly HttpClient _httpClient;

        // Endereços das APIs
        string apiEquipesPessoas = "https://localhost:7088/api/EquipesPessoas";
        string apiPessoas = "https://localhost:7088/api/Pessoas";
        string apiEquipes = "https://localhost:7088/api/Equipes";

        public EquipesPessoasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtém a lista de EquipesPessoas da API
                var equipesPessoas = await _httpClient.GetFromJsonAsync<List<EquipesPessoasModel>>(apiEquipesPessoas);
                if (equipesPessoas == null)
                {
                    return View("Error", "Não foi possível obter as pessoas.");
                }

                // Carrega as equipes para o combo de seleção
                var responseEquipes = await _httpClient.GetAsync(apiEquipes);
                List<EquipeModel> equipesDropdown = new List<EquipeModel>();

                if (responseEquipes.IsSuccessStatusCode)
                {
                    var equipes = await responseEquipes.Content.ReadFromJsonAsync<List<EquipesModel>>();

                    // Mapeia a lista para a model 'EquipeModel' com Id e Nome da equipe
                    equipesDropdown = equipes.Select(e => new EquipeModel
                    {
                        Id_Equipe = e.Id_Equipe,
                        Nm_Equipe = e.Nm_Equipe
                    }).ToList();
                }
                else
                {
                    var errorResponse = await responseEquipes.Content.ReadFromJsonAsync<ErrorResponse>();
                    string errorMessage = errorResponse?.Message ?? "Não foi possível carregar as equipes.";
                    return View("Error", errorMessage);
                }

                // Cria um ViewModel para passar as duas listas para a view
                var viewModel = new IndexViewModel
                {
                    EquipesPessoas = equipesPessoas,
                    EquipesDropdown = equipesDropdown
                };

                return View(viewModel); // Passa o ViewModel para a view
            }
            catch (HttpRequestException e)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Erro ao acessar a API: {e.Message}" };
                return View("Error", errorViewModel);
            }

            catch (Exception e)
            {
                var errorViewModel = new ErrorViewModel { Message = $"Ocorreu um erro inesperado:  {e.Message}" };
                return View("Error", errorViewModel);
            }
        }



        public IActionResult Create()
        {
            return View();
        }

        // Recebe os dados do formulário e faz o POST para a API
        [HttpPost]
        public async Task<IActionResult> Create(EquipesPessoasModel equipePessoas)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Envia a nova pessoa para a API
                    var response = await _httpClient.PostAsJsonAsync(apiEquipesPessoas, equipePessoas);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index"); // Redireciona para a lista de pessoas após sucesso
                    }
                    else
                    {
                        // Extrai a mensagem de erro da resposta da API
                        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                        string errorMessage = errorResponse?.Message ?? "Erro ao cadastrar pessoa. Tente novamente.";
                        return View("Error", new ErrorViewModel { Message = errorMessage }); // Redireciona para a página de erro

                    }
                }
                catch (HttpRequestException e)
                {
                    return View("Error", new ErrorViewModel { Message = $"Erro ao acessar a API: {e.Message}" });
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorViewModel { Message = $"Ocorreu um erro inesperado: {e.Message}" });
                }
            }

            // Caso ocorra um erro, retorna a view com os dados preenchidos
            return View(equipePessoas);
        }

        // Carrega as equipes no ViewBag para exibição no combo

        // Obtém informações de uma pessoa pelo CPF
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


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id_Equipe, string cpf)

        {
            if (string.IsNullOrEmpty(cpf))
            {
                return BadRequest("CPF não fornecido.");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{apiEquipesPessoas}/{Id_Equipe}/{cpf}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Redireciona para a lista de pessoas após sucesso
                }
                else
                {
                    // Extrai a mensagem de erro da resposta da API
                    var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    string errorMessage = errorResponse?.Message ?? "Erro ao excluir pessoa. Tente novamente.";
                    return View("Error", new ErrorViewModel { Message = errorMessage }); // Redireciona para a página de erro
                }
            }
            catch (HttpRequestException e)
            {
                return View("Error", new ErrorViewModel { Message = $"Erro ao acessar a API: {e.Message}" });
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel { Message = $"Ocorreu um erro inesperado: {e.Message}" });
            }
        }

        // Método para retornar as equipes em formato JSON (caso precise para chamadas Ajax)

        public async Task<IActionResult> GetEquipes()
        {
            try
            {
                var response = await _httpClient.GetAsync(apiEquipes);
                if (response.IsSuccessStatusCode)
                {
                    // Obtenha a lista de equipes da API
                    var equipes = await response.Content.ReadFromJsonAsync<List<EquipesModel>>();

                    // Mapeia a lista para a model 'EquipeModel'
                    var equipesDropdown = equipes.Select(e => new EquipeModel
                    {
                        Id_Equipe = e.Id_Equipe,
                        Nm_Equipe = e.Nm_Equipe
                    }).ToList();

                    return Json(equipesDropdown); // Retorna a lista de equipes em formato JSON para o combo
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    string errorMessage = errorResponse?.Message ?? "Equipe não encontrada.";
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
