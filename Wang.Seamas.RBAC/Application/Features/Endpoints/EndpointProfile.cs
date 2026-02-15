using AutoMapper;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.CreateEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.UpdateEndpoint;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints;

public class EndpointProfile: Profile
{

    public EndpointProfile()
    {
        CreateMap<CreateEndpointCommand, ApiEndpoint>();
        CreateMap<UpdateEndpointCommand, ApiEndpoint>();
    }
}