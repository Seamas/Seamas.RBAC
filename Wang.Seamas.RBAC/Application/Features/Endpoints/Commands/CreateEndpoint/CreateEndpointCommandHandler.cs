using AutoMapper;
using MediatR;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.CreateEndpoint;

public class CreateEndpointCommandHandler: IRequestHandler<CreateEndpointCommand, bool>
{
    private readonly IApiEndpointService _apiEndpointService;
    private readonly IMapper _mapper;

    public CreateEndpointCommandHandler(IApiEndpointService apiEndpointService, IMapper mapper)
    {
        _apiEndpointService = apiEndpointService;
        _mapper = mapper;
    }


    public async Task<bool> Handle(CreateEndpointCommand request, CancellationToken cancellationToken)
    {
        var endpoint = _mapper.Map<ApiEndpoint>(request);
        return await _apiEndpointService.CreateApiEndpointAsync(endpoint);
    }
}