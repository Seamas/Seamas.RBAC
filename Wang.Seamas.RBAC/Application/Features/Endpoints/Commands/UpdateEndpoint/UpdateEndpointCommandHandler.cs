using AutoMapper;
using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.UpdateEndpoint;

public class UpdateEndpointCommandHandler: IRequestHandler<UpdateEndpointCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IApiEndpointService _apiEndpointService;

    public UpdateEndpointCommandHandler(IMapper mapper, IApiEndpointService apiEndpointService)
    {
        _mapper = mapper;
        _apiEndpointService = apiEndpointService;
    }

    public async Task<bool> Handle(UpdateEndpointCommand request, CancellationToken cancellationToken)
    {
        var endpoint = _mapper.Map<ApiEndpoint>(request);
        return await _apiEndpointService.UpdateApiEndpointAsync(endpoint);
    }
}