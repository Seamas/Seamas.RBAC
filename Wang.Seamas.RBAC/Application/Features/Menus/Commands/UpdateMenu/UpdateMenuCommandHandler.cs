using AutoMapper;
using MediatR;
using Wang.Seamas.RBAC.Application.DTOs.Menus;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.UpdateMenu;

public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, bool>
{
    private readonly IMenuService _menuService;
    private readonly IMapper _mapper;

    public UpdateMenuCommandHandler(IMenuService menuService, IMapper mapper)
    {
        _menuService = menuService;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
    {
        var menuDto = _mapper.Map<MenuDto>(request);
        return await _menuService.UpdateMenuAsync(menuDto);
    }
}