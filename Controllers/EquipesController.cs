using CadastroEquipesConsumidor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace CadastroEquipesConsumidor.Controllers
{
    public class EquipesController : Controller
    {
        private readonly HttpClient _httpClient;

        // Aqui deve ser atualizado o endereço da API
        string apiEquipes = "https://localhost:7088/api/Equipes";

        public EquipesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Exibe a lista de equipes
        public async Task<IActionResult> Index()
        {
            try
            {
                var equipes = await _httpClient.GetFromJsonAsync<List<EquipesModel>>(apiEquipes);
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

        public IActionResult Create()
        {
            return View();
        }

        // Recebe os dados do formulário e faz o POST para a API
        [HttpPost]
        public async Task<IActionResult> Create(EquipesModel equipe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Envia a nova pessoa para a API
                    var response = await _httpClient.PostAsJsonAsync(apiEquipes, equipe);

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
            return View(equipe);
        }
        // Método para atualizar uma pessoa
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EquipesModel equipes)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync(apiEquipes, equipes);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index"); // Redireciona para a lista de pessoas após sucesso
                    }
                    else
                    {
                        // Extrai a mensagem de erro da resposta da API
                        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                        string errorMessage = errorResponse?.Message ?? "Erro ao atualizar pessoa. Tente novamente.";
                        return View("Error", new ErrorViewModel { Message = errorMessage });
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
            return View(equipes);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string cpf)

        {
            if (string.IsNullOrEmpty(cpf))
            {
                return BadRequest("CPF não fornecido.");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"{apiEquipes}/{cpf}");
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

        // Classe para mapear a resposta de erro da API
        public class ErrorResponse
        {
            public string Message { get; set; }
        }

    }



}

