using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.CreateMenu;

public class CreateMenuCommand : IRequest<bool>
{
    public required string Name { get; set; } 
    public string? Code { get; set; } 
    public string? Path { get; set; } 
    public int? ParentId { get; set; } 
    public int? SortOrder { get; set; }
}