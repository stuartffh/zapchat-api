using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zapchat.Domain.DTOs.ContasPagar;

namespace Zapchat.Domain.Interfaces.ContasPagar
{
    public interface IContasPagarService
    {
        Task<string> ListarContasPagarExcel(ListarContasPagarExcelDto listarContasPagarExcelDto);
    }
}
