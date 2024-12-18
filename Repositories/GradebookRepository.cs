using Npgsql;

using TestWebApiClient.Services.GetGradebook.Contracts;

namespace TestWebApiClient.Repositories;

internal sealed class GradebookRepository : IGradebookRepository
{
    private const string ConnectionString = "Host=postgres;" +
        "Port=5432;" +
        "Username=postgres;" +
        "Password=postgres;" +
        "Database=postgres";

    private const string GetGradebookSql = @"
            SELECT
                *
            FROM Gradebook;
        ";

    public async Task<GradebookItem[]> GetGradebook()
    {
        var gradebook = new List<GradebookItem>();

        await using (var connection = new NpgsqlConnection(ConnectionString))
        {
            try
            {
                await connection.OpenAsync();

                await using (var cmd = new NpgsqlCommand(GetGradebookSql, connection))
                {
                    await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var subject = reader["subject"] as string;
                            var grade = reader["grade"] as int?;
                            var gradebookItem = new GradebookItem(subject, grade);
                            gradebook.Add(gradebookItem);
                        }
                    }
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

        }

        return gradebook.ToArray();
    }
}