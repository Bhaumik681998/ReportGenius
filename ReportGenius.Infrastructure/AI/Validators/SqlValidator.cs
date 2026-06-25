using ReportGenius.Common.Constants;
using ReportGenius.Common.Exceptions;

namespace ReportGenius.Infrastructure.AI.Validators
{
    public static class SqlValidator
    {
        public static void Validate(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new InvalidSqlException("AI returned an empty SQL statement.");
            }

            var normalizedSql = sql.Trim().ToUpperInvariant();

            foreach (var keyword in SqlKeywords.DangerousKeywords)
            {
                if (normalizedSql.Contains(keyword))
                {
                    throw new InvalidSqlException($"Dangerous SQL detected: {keyword}");
                }
            }

            if (normalizedSql.StartsWith("DELETE") && !normalizedSql.Contains("WHERE"))
            {
                throw new InvalidSqlException("DELETE without WHERE is not allowed.");
            }
        }
    }
}
