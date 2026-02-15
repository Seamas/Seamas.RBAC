using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Common.Utils;
using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/permissions")]
public class PermissionsController(IMenuService menuService, ICurrentUserService currentUserService)
    : ControllerBase
{
    
    [HttpPost("user-menus")]
    public async Task<List<Menu>> GetUserVisibleMenus()
    {
        var userId = currentUserService.UserId;
        Assert.State(userId > 0, "Unauthorized");
        
        return await menuService.GetUserVisibleMenusAsync(userId);
    }
    
    
}