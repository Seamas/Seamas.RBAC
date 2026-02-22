using MediatR;

namespace Wang.Seamas.RBAC.Application.Features.Menus.Commands.UpdateMenu;

public class UpdateMenuCommand : IRequest<bool>
{
    public int Id { get; set; } 
    public string? Name { get; set; } 
    public string? Code { get; set; } 
    public string? Path { get; set; } 
    public string? Icon { get; set; }
    public int? ParentId { get; set; } 
    public int? SortOrder { get; set; }
}