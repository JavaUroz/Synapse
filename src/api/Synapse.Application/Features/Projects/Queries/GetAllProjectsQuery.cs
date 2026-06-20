using MediatR;
using Synapse.Application.Common.Interfaces;

namespace Synapse.Application.Features.Projects.Queries;

public class GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllProjectsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _unitOfWork.Projects.GetAllAsync();

        return projects.Select(p => new ProjectDto(p.Id, p.Name, p.RepositoryUrl, p.CreatedAt));
    }    
}
