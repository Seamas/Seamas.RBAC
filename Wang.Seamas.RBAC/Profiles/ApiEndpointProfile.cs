using AutoMapper;
using Wang.Seamas.RBAC.Dtos.ApiEndpoint;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests.ApiEndpoint;

namespace Wang.Seamas.RBAC.Profiles;

public class ApiEndpointProfile : Profile
{
    
    public ApiEndpointProfile()
    {
        CreateMap<SearchApiRequest, SearchApiDto>();
        CreateMap<CreateApiRequest, ApiEndpoint>();
        CreateMap<UpdateApiRequest, ApiEndpoint>();
    }
}