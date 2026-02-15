using AutoMapper;
using Wang.Seamas.RBAC.Application.DTOs.Menus;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.CreateMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.UpdateMenu;
using Wang.Seamas.RBAC.Domain.Entities;

namespace Wang.Seamas.RBAC.Application.Features.Menus;

public class MenuProfile : Profile
{
    public  MenuProfile()
    {
        CreateMap<CreateMenuCommand, MenuDto>();
        CreateMap<UpdateMenuCommand, MenuDto>();
        
        
        CreateMap<MenuDto, Menu>();
    }
}