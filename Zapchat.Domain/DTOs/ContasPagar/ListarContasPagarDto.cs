using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapchat.Domain.DTOs.ContasPagar
{
    public class ListarContasPagarDto
    {
        public int Pagina { get; set; }
        public int TotalDePaginas { get; set; }
        public int Registros { get; set; }
        public int TotalDeRegistros { get; set; }
        public List<ContaPagarCadastroDto>? ContaPagarCadastro { get; set; }
    }
}
