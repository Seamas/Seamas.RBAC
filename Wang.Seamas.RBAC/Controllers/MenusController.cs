using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.CreateMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.DeleteMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.DisableMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.EnableMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.UpdateMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.FirstLevelMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenuPage;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/menus")]
public class MenusController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    public async Task<ResultPage<Menu>> QueryMenus(GetMenuPageQuery request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("get")]
    public async Task<Menu?> GetMenu(GetMenuQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpGet("first-level")]
    public async Task<IEnumerable<Menu>> GetFirstLevelMenusAsync()
    {
        var query = new FirstLevelMenuQuery();
        return await mediator.Send(query);
    }
        
    

    [HttpPost("create")]
    public async Task<bool> CreateMenu(CreateMenuCommand request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("update")]
    public async Task<bool> UpdateMenu(UpdateMenuCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("delete")]
    public async Task<bool> DeleteMenu(DeleteMenuCommand request)
    {
        return await mediator.Send(request);
    }


    [HttpPost("enable")]
    public async Task<bool> EnableMenu(EnableMenuCommand request)
    {
        return await mediator.Send(request);
    }
    
    
    [HttpPost("disable")]
    public async Task<bool> DisableMenu(DisableMenuCommand request)
    {
        return await mediator.Send(request);
    }
}