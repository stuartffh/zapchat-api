using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Zapchat.Domain.DTOs.ContasPagar;
using Zapchat.Domain.Interfaces.ContasPagar;

namespace Zapchat.Service.Services.ContasPagar
{
    public class ContasPagarService : IContasPagarService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public ContasPagarService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        //public async Task<string> ListarContasPagarExcel(ListarContasPagarExcelDto listarContasPagarExcelDto)
        //{
        //    //var baseUri = _configuration["BaseUrlOmie"];
        //    var baseUri = "https://app.omie.com.br/api/v1/financas/contapagar/";
        //    if (string.IsNullOrEmpty(baseUri))
        //        throw new InvalidOperationException("A URL da API não foi configurada no appsettings.json.");

        //    //using var client = new HttpClient();
        //    //var content = new StringContent($"{{\"token\":\"{token}\"}}", Encoding.UTF8, "application/json");
        //    //var response = await client.PostAsync(urlAsftpApi, content);

        //    using var httpClient = new HttpClient();

        //    var body = $"{{\"call\":\"ListarContasPagar\",\"app_key\":\"38333295000\",\"app_secret\":\"8e656102b1d574351e95a1072a4e7b08\",\"param\":[{{\"pagina\":1,\"registros_por_pagina\":20,\"apenas_importado_api\":\"N\"}}]}}";
        //    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        //    /*var payload = new
        //    {
        //        call = "ListarContasPagar",
        //        app_key = 4268864731131,
        //        app_secret = "8e656102b1d574351e95a1072a4e7b08",
        //        param = new 
        //        [
        //        {
        //            pagina = 1,
        //            registros_por_pagina = 999,
        //            apenas_importado_api = "N"
        //        }
        //        ]
        //    };*/

        //    /*var jsonPayload = JsonSerializer.Serialize(body, new JsonSerializerOptions
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    });*/

        //    var content = new StringContent(body, Encoding.UTF8, "application/json");

        //    try
        //    {
        //        var response = await httpClient.PostAsync(baseUri, content);

        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine($"Código HTTP: {response.StatusCode}");
        //        Console.WriteLine($"Resposta da API: {responseBody}");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            throw new HttpRequestException($"Erro na API Omie. Código: {response.StatusCode}, Resposta: {responseBody}");
        //        }


        //        var resultado = JsonSerializer.Deserialize<ListarContasPagarDto>(responseBody, new JsonSerializerOptions
        //        {
        //            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //        });

        //        return ExportarContasPagarCsvBase64(resultado.ContaPagarCadastro);

        //        /*
        //         * GovBrRequest? govBr = JsonSerializer.Deserialize<GovBrRequest>(response.Data!, new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true,
        //                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //            });
        //        */

        //    }
        //    catch (Exception ex)
        //    {

        //        return await Task.FromResult($"Erro ao chamar API: {ex.Message}");
        //    }

        //}

        public async Task<string> ListarContasPagarExcel(ListarContasPagarExcelDto listarContasPagarExcelDto)
        {
            var baseUri = "https://app.omie.com.br/api/v1/financas/contapagar/";
            if (string.IsNullOrEmpty(baseUri))
                throw new InvalidOperationException("A URL da API não foi configurada.");

            // Cria o objeto de requisição
            var request = new
            {
                call = "ListarContasPagar",
                app_key = "38333295000",
                app_secret = "8e656102b1d574351e95a1072a4e7b08",
                param = new[]
                {
            new
            {
                pagina = 1,
                registros_por_pagina = 20,
                apenas_importado_api = "N"
            }
        }
            };

            try
            {
                // Faz a chamada à API
                var response = await ExecuteApiCall<object, ListarContasPagarDto>(HttpMethod.Post, new Uri(baseUri), request);

                // Exporta o resultado para CSV em Base64
                return ExportarContasPagarCsvBase64(response.ContaPagarCadastro);
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"Erro ao chamar API: {ex.Message}");
            }
        }

        private string ExportarContasPagarCsvBase64(List<ContaPagarCadastroDto> ListContaPagarCadastroDto)
        {
            var sb = new StringBuilder();
            sb.AppendLine("CodigoLancamentoOmie,CodigoClienteFornecedor,DataEmissao,DataVencimento,StatusTitulo,ValorDocumento");

            foreach (var conta in ListContaPagarCadastroDto)
            {
                sb.AppendLine($"{conta.CodigoLancamentoOmie}," +
                              $"{conta.CodigoClienteFornecedor}," +
                              $"{conta.DataEmissao.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}," +
                              $"{conta.DataVencimento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}," +
                              $"{conta.StatusTitulo}," +
                              $"{conta.ValorDocumento.ToString(CultureInfo.InvariantCulture)}");
            }

            var byteArray = Encoding.UTF8.GetBytes(sb.ToString());

            return Convert.ToBase64String(byteArray);
        }

        private async Task<TResponse> ExecuteApiCall<TRequest, TResponse>(HttpMethod httpMethod, Uri fullUrl, TRequest request)
        {
            using var httpClient = new HttpClient();

            // Serializa o objeto de requisição para JSON
            var jsonPayload = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Cria o conteúdo da requisição
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Faz a requisição HTTP
            var response = await httpClient.PostAsync(fullUrl, content);

            // Verifica se a requisição foi bem-sucedida
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Erro na API Omie. Código: {response.StatusCode}, Resposta: {errorResponse}");
            }

            // Lê e desserializa a resposta
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

    }
}
