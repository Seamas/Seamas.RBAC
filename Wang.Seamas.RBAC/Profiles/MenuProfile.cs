using AutoMapper;
using Wang.Seamas.RBAC.Dtos.Menu;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.RBAC.Requests.Menu;

namespace Wang.Seamas.RBAC.Profiles;

public class MenuProfile : Profile
{
    
    public MenuProfile()
    {
        CreateMap<SearchMenuRequest, SearchMenuDto>();
        CreateMap<CreateMenuRequest, MenuDto>();
        CreateMap<UpdateMenuRequest, MenuDto>();
        
        CreateMap<MenuDto, Menu>();
    }
}