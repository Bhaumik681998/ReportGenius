using ReportGenius.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenius.Application.Interfaces
{
    public interface ITableMetadataProvider
    {
        Task<IReadOnlyList<TableMetadataDto>> GetTablesAsync(CancellationToken cancellationToken = default);
    }
}
