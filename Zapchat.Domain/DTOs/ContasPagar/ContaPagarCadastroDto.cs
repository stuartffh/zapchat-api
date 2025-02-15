using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapchat.Domain.DTOs.ContasPagar
{
    public class ContaPagarCadastroDto
    {
        public string? BaixaBloqueada { get; set; }
        public string? Bloqueado { get; set; }
        public string? CodigoCategoria { get; set; }
        public long CodigoClienteFornecedor { get; set; }
        public string? CodigoLancamentoIntegracao { get; set; }
        public long CodigoLancamentoOmie { get; set; }
        public string? CodigoTipoDocumento { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataPrevisao { get; set; }
        public DateTime DataVencimento { get; set; }
        public List<object> Distribuicao { get; set; }
        public long IdContaCorrente { get; set; }
        public string? IdOrigem { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? NumeroDocumentoFiscal { get; set; }
        public string? NumeroParcela { get; set; }
        public string? RetemCofins { get; set; }
        public string? RetemCsll { get; set; }
        public string? RetemInss { get; set; }
        public string? RetemIr { get; set; }
        public string? RetemIss { get; set; }
        public string? RetemPis { get; set; }
        public string? StatusTitulo { get; set; }
        public decimal ValorDocumento { get; set; }
    }
}
