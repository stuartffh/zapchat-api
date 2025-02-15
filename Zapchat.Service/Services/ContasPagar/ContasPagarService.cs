using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<string> ListarContasPagarExcel(ListarContasPagarExcelDto listarContasPagarExcelDto)
        {
            var baseUri = "https://app.omie.com.br/api/v1/financas/contapagar/";
            if (string.IsNullOrEmpty(baseUri))
                throw new InvalidOperationException("A URL da API não foi configurada.");

            // Cria o objeto de requisição
            var request = new
            {
                call = "ListarContasPagar",
                app_key = "4268864731131",
                app_secret = "8e656102b1d574351e95a1072a4e7b08",
                param = new[]
                {
                    new
                    {
                        pagina = 1,
                        registros_por_pagina = 10,
                        apenas_importado_api = "N"
                    }
                }
            };

            try
            {
                // Faz a chamada à API
                var response = await ExecuteApiCall<object, ListarContasPagarDto>(HttpMethod.Post, new Uri(baseUri), request);

                // Exporta o resultado para CSV em Base64
                return ExportarContasPagarXlsxBase64(response.ContaPagarCadastro);
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"Erro ao chamar API: {ex.Message}");
            }
        }

        private string ExportarContasPagarXlsxBase64(List<ContaPagarCadastroDto> ListContaPagarCadastroDto)
        {
            // Criar o arquivo Excel em memória
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("ContasPagar");

                // Cabeçalhos
                worksheet.Cell(1, 1).Value = "CodigoLancamentoOmie";
                worksheet.Cell(1, 2).Value = "CodigoClienteFornecedor";
                worksheet.Cell(1, 3).Value = "DataEmissao";
                worksheet.Cell(1, 4).Value = "DataVencimento";
                worksheet.Cell(1, 5).Value = "StatusTitulo";
                worksheet.Cell(1, 6).Value = "ValorDocumento";

                // Preencher os dados
                int row = 2;
                foreach (var conta in ListContaPagarCadastroDto)
                {
                    worksheet.Cell(row, 1).Value = conta.CodigoLancamentoOmie;
                    worksheet.Cell(row, 2).Value = conta.CodigoClienteFornecedor;
                    worksheet.Cell(row, 3).Value = DateTime.Now;//conta.DataEmissao.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    worksheet.Cell(row, 4).Value = DateTime.Now;//conta.DataVencimento.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    worksheet.Cell(row, 5).Value = conta.StatusTitulo;
                    worksheet.Cell(row, 6).Value = conta.ValorDocumento.ToString(CultureInfo.InvariantCulture);
                    row++;
                }

                // Salvar o arquivo em memória
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    var byteArray = memoryStream.ToArray();
                    return Convert.ToBase64String(byteArray);
                }
            }
        }

        private async Task<TResponse> ExecuteApiCall<TRequest, TResponse>(HttpMethod httpMethod, Uri fullUrl, TRequest request)
        {
            var jsonPayload = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Faz a requisição HTTP
            var response = await _httpClient.PostAsync(fullUrl, content);

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
