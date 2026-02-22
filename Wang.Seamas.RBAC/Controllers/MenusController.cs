using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.CreateMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.DeleteMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.DisableMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.EnableMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Commands.UpdateMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.FirstLevelMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetAllMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenu;
using Wang.Seamas.RBAC.Application.Features.Menus.Queries.GetMenuPage;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Attributes;
using Wang.Seamas.Web.Common.DTOs;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/menus")]
[ControllerTag("菜单管理")]
public class MenusController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    [ActionTag("查询")]
    public async Task<ResultPage<Menu>> QueryMenus(GetMenuPageQuery request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("get")]
    [ActionTag("获取单个菜单信息")]
    public async Task<Menu?> GetMenu(GetMenuQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpGet("first-level")]
    [ActionTag("获取一级菜单")]
    public async Task<IEnumerable<Menu>> GetFirstLevelMenusAsync()
    {
        var query = new FirstLevelMenuQuery();
        return await mediator.Send(query);
    }
        
    

    [HttpPost("create")]
    [ActionTag("创建菜单")]
    public async Task<bool> CreateMenu(CreateMenuCommand request)
    {
        return await mediator.Send(request);
    }
    
    [HttpPost("update")]
    [ActionTag("更新菜单")]
    public async Task<bool> UpdateMenu(UpdateMenuCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("delete")]
    [ActionTag("删除菜单")]
    public async Task<bool> DeleteMenu(DeleteMenuCommand request)
    {
        return await mediator.Send(request);
    }


    [HttpPost("enable")]
    [ActionTag("启用菜单")]
    public async Task<bool> EnableMenu(EnableMenuCommand request)
    {
        return await mediator.Send(request);
    }
    
    
    [HttpPost("disable")]
    [ActionTag("禁用菜单")]
    public async Task<bool> DisableMenu(DisableMenuCommand request)
    {
        return await mediator.Send(request);
    }

    [HttpGet("get-all")]
    [ActionTag("获取所有菜单")]
    public async Task<IEnumerable<Menu>> GetAllMenus()
    {
        var request = new GetAllMenuQuery();
        return await mediator.Send(request);
    }
    
}