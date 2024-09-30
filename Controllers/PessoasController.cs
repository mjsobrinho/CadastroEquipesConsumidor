using CadastroEquipesConsumidor.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEquipesConsumidor.Controllers
{
    public class PessoasController : Controller
    {
        private readonly HttpClient _httpClient;

        // Aqui deve ser atualizado o endereço da API
        string apiPessoas = "https://localhost:7088/api/Pessoas";

        public PessoasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Exibe a lista de pessoas
        public async Task<IActionResult> Index()
        {
            try
            {
                var pessoas = await _httpClient.GetFromJsonAsync<List<PessoasModel>>(apiPessoas);
                if (pessoas == null)
                {
                    return View("Error", "Não foi possível obter as pessoas.");
                }

                return View(pessoas); // Passa a lista de pessoas para a view
            }
             catch (HttpRequestException e)
            {
                var errorViewModel = new ErrorViewModel{ Message = $"Erro ao acessar a API: {e.Message}"};
                return View("Error", errorViewModel); 
            }

            catch (Exception e)
            {
                var errorViewModel = new ErrorViewModel{Message = $"Ocorreu um erro inesperado:  {e.Message}"};
                return View("Error", errorViewModel);
            }
        }

        
    

        public IActionResult Create()
        {
            return View();
        }

        // Recebe os dados do formulário e faz o POST para a API
        [HttpPost]
        public async Task<IActionResult> Create(PessoasModel pessoa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Envia a nova pessoa para a API
                    var response = await _httpClient.PostAsJsonAsync(apiPessoas, pessoa);

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
            return View(pessoa);
        }



        // Método para atualizar uma pessoa
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]PessoasModel pessoa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync(apiPessoas, pessoa);
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
            return View(pessoa);
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
                var response = await _httpClient.DeleteAsync($"{apiPessoas}/{cpf}");
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

    }


    // Classe para mapear a resposta de erro da API
    public class ErrorResponse
    {
        public string Message { get; set; }
    }
}
