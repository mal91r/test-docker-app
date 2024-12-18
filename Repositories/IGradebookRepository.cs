using TestWebApiClient.Services.GetGradebook.Contracts;

namespace TestWebApiClient.Repositories;

public interface IGradebookRepository
{
    Task<GradebookItem[]> GetGradebook();
}