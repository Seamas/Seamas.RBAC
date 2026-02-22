using System.Reflection;
using Dm.util;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using SqlSugar;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.RBAC.Domain.Interfaces;
using Wang.Seamas.Web.Attributes;

namespace Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.InitEndpoint;

public class InitEndpointCommandHandler : IRequestHandler<InitEndpointCommand, bool>
{
    private readonly IApiEndpointService _apiEndpointService;
    private readonly IApiDescriptionGroupCollectionProvider _apiProvider;
    private readonly ISqlSugarClient _db;

    public InitEndpointCommandHandler(IApiEndpointService apiEndpointService, IApiDescriptionGroupCollectionProvider apiProvider, ISqlSugarClient db)
    {
        _apiEndpointService = apiEndpointService;
        _apiProvider = apiProvider;
        _db = db;
    }


    public async Task<bool> Handle(InitEndpointCommand request, CancellationToken cancellationToken)
    {
        var apiDescriptions = _apiProvider.ApiDescriptionGroups.Items.SelectMany(group => group.Items);
        var apiEndpoints = new List<ApiEndpoint>();
        foreach (var api in apiDescriptions)
        {
            var url = "/" + api.RelativePath;
            if (api.ActionDescriptor is ControllerActionDescriptor controllerAction)
            {
                var description = controllerAction.MethodInfo.GetCustomAttribute<ActionTagAttribute>()?.Name;
                var groupName = controllerAction.ControllerTypeInfo.GetCustomAttribute<ControllerTagAttribute>()?.Name;
                
                apiEndpoints.add(new ApiEndpoint {ApiGroup = groupName, Description = description, Url = url});
            }
            else
            {
                apiEndpoints.add(new ApiEndpoint(){Url = url});
            }
        }

        var res = await _db.Ado.UseTranAsync(async () =>
        {
            await _apiEndpointService.InitApiEndpointsAsync(apiEndpoints);
        });

        return res.IsSuccess;
    }
}