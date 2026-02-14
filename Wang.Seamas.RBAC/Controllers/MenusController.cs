using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Dtos.Menu;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests.Menu;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Dtos;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/menus")]
public class MenusController(IMenuService menuService, IMapper mapper) : ControllerBase
{
    [HttpPost("search")]
    public async Task<PagedResult<Menu>> QueryMenus(SearchMenuRequest request)
    {
        var dto = mapper.Map<SearchMenuDto>(request);
        var (list, total) = await menuService.QueryMenusAsync(dto);
        return new PagedResult<Menu>(list, total, dto.PageIndex, dto.PageSize);
    }
    
    [HttpPost("get")]
    public async Task<Menu?> GetMenu(MenuIdRequest idRequest)
    {
        var menu = await menuService.GetMenuByIdAsync(idRequest.Id);
        return menu;
    }

    [HttpGet("first-level")]
    public async Task<List<Menu>> GetFirstLevelMenusAsync()
        => await menuService.GetFirstLevelMenusAsync();
    

    [HttpPost("create")]
    public async Task<int> CreateMenu(CreateMenuRequest request)
    {
        var dto = mapper.Map<MenuDto>(request);
        var id = await menuService.CreateMenuAsync(dto);
        return id;
    }
    
    [HttpPost("update")]
    public async Task<bool> UpdateMenu(UpdateMenuRequest request)
    {
        var dto = mapper.Map<MenuDto>(request);
        var success = await menuService.UpdateMenuAsync(dto);
        Assert.IsTrue(success, $"找不到对应的菜单{request.Id}");
        return success;
    }

    [HttpPost("delete")]
    public async Task<bool> DeleteMenu(MenuIdRequest request)
    {
        return await menuService.DeleteMenuAsync(request.Id);
    }


    [HttpPost("enable")]
    public async Task<bool> EnableMenu(EnableMenuRequest request)
    {
        return await menuService.EnableMenuAsync(request.Id, request.Enabled);
    }
}