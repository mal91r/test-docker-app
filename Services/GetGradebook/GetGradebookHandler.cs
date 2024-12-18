using MediatR;

using TestWebApiClient.Repositories;
using TestWebApiClient.Services.GetGradebook.Contracts;

namespace TestWebApiClient.Services.GetGradebook;

internal sealed class GetGradebookHandler : IRequestHandler<GetGradebookRequest, GetGradebookResponse>
{
    private readonly IGradebookRepository _repository;

    public GetGradebookHandler(IGradebookRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetGradebookResponse> Handle(GetGradebookRequest request, CancellationToken cancellationToken)
    {
        GradebookItem[] gradebookItems = await _repository.GetGradebook();

        return new GetGradebookResponse(gradebookItems);
    }
}