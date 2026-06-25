using ReportGenius.Application.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenius.Infrastructure.AI.Clients
{
    public interface IAITableSelector
    {
        Task<IReadOnlyList<string>> SelectTablesAsync(string userPrompt, DatabaseSchema schema, CancellationToken cancellationToken = default);
    }
}
