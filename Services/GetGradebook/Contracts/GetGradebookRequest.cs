using MediatR;

namespace TestWebApiClient.Services.GetGradebook.Contracts;

public record GetGradebookRequest() : IRequest<GetGradebookResponse>;