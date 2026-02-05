using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/menus")]
public class MenusController(IMenuService menuService) : ControllerBase
{
    [HttpPost("all")]
    public async Task<List<Menu>> GetAllMenus()
        => await menuService.GetAllMenusAsync();

    [HttpPost("active")]
    public async Task<List<Menu>> GetActiveMenus()
        => await menuService.GetActiveMenusAsync();

    [HttpPost("create")]
    public async Task<int> CreateMenu(CreateMenuRequest request)
    {
        var id = await menuService.CreateMenuAsync(
            request.Name,
            request.Code,
            request.Path,
            request.ParentId,
            request.SortOrder ?? 0,
            request.IsEnabled ?? true
        );
        return id;
    }

    [HttpPost("get")]
    public async Task<Menu?> GetMenu(GetMenuRequest request)
    {
        var menu = await menuService.GetMenuByIdAsync(request.Id);
        return menu;
    }

    [HttpPost("update")]
    public async Task<bool> UpdateMenu(UpdateMenuRequest request)
    {
        var success = await menuService.UpdateMenuAsync(
            request.Id,
            request.Name,
            request.Code,
            request.Path,
            request.ParentId,
            request.SortOrder,
            request.IsEnabled
        );
        Assert.IsTrue(success, "Menu not found");
        return success;
    }
}