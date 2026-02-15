using AutoMapper;
using MediatR;
using Wang.Seamas.RBAC.Application.DTOs.Menus;
using Wang.Seamas.RBAC.Domain.Interfaces;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, bool>
{
    private readonly IMenuService _menuService;
    private readonly IMapper _mapper;

    public CreateMenuCommandHandler(IMenuService menuService, IMapper mapper)
    {
        _menuService = menuService;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        var menuDto = _mapper.Map<MenuDto>(request);
        return await _menuService.CreateMenuAsync(menuDto) > 0;
    }
}