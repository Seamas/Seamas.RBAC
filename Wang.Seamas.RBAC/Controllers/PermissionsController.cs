using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Utils;
using Wang.Seamas.Web.Utils;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/permissions")]
public class PermissionsController(IMenuService menuService)
    : ControllerBase
{
    [HttpPost("user-menus")]
    public async Task<List<Menu>> GetUserVisibleMenus()
    {
        var userId = HttpContextUtil.GetCurrentUserId(HttpContext);
        Assert.State(userId > 0, "Unauthorized");
        
        return await menuService.GetUserVisibleMenusAsync(userId);
    }
    
    
}