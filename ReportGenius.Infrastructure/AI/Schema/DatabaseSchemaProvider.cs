using Dapper;
using ReportGenius.Application.Interfaces;
using ReportGenius.Application.Models.Schema;
using ReportGenius.Infrastructure.Database.Connection;
using System.Text;

namespace ReportGenius.Infrastructure.AI.Schema
{
    public sealed class DatabaseSchemaProvider : IDatabaseSchemaProvider
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DatabaseSchemaProvider(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<DatabaseSchema> GetSchemaAsync(CancellationToken cancellationToken = default)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = @"
            SELECT
                table_name       AS TableName,
                column_name      AS ColumnName,
                data_type        AS DataType,
                is_nullable      AS IsNullable,
                ordinal_position AS OrdinalPosition
            FROM information_schema.columns
            WHERE table_schema = 'public'
            ORDER BY table_name, ordinal_position;";

            var rows = await connection.QueryAsync<SchemaRow>(sql);

            foreach (var row in rows.Take(5))
            {
                Console.WriteLine($"Table={row.TableName}, Column={row.ColumnName}, Type={row.DataType}");
            }

            Console.WriteLine("========== DATABASE SCHEMA ==========");

            foreach (var row in rows.Take(10))
            {
                Console.WriteLine(
                    $"Table=[{row.TableName}]  Column=[{row.ColumnName}]  Type=[{row.DataType}]");
            }

            Console.WriteLine("=====================================");

            var schema = new DatabaseSchema();

            foreach (var tableGroup in rows.GroupBy(x => x.TableName))
            {
                var table = new TableSchema
                {
                    TableName = tableGroup.Key
                };

                foreach (var column in tableGroup)
                {
                    table.Columns.Add(new ColumnSchema
                    {
                        ColumnName = column.ColumnName,
                        DataType = column.DataType,
                        IsNullable = column.IsNullable == "YES",
                        OrdinalPosition = column.OrdinalPosition
                    });
                }

                schema.Tables.Add(table);
            }

            return schema;
        }

        private sealed class SchemaRow
        {
            public string TableName { get; set; } = string.Empty;

            public string ColumnName { get; set; } = string.Empty;

            public string DataType { get; set; } = string.Empty;

            public string IsNullable { get; set; } = string.Empty;

            public int OrdinalPosition { get; set; }
        }
    }
}
