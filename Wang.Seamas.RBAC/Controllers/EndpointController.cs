using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.CreateEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DeleteEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DisableEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.EnableEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.InitEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.UpdateEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.CheckUrl;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpointPage;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Common.DTOs;
namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/endpoints")]
public class EndpointController(IMediator mediator, IApiDescriptionGroupCollectionProvider apiProvider) : ControllerBase
{
    [HttpPost("search")]
    public async Task<ResultPage<ApiEndpoint>> QueryApis(GetEndpointPageQuery request)
    {
        return await mediator.Send(request);
    }


    [HttpPost("get")]
    public async Task<ApiEndpoint?> GetApiEndpoint(GetEndpointQuery request) => await mediator.Send(request);


    [HttpPost("check-url")]
    public async Task<bool> CheckApiEndpointUrl(CheckUrlQuery request) => await mediator.Send(request);

    [HttpPost("create")]
    public async Task<bool> CreateApiEndpoint(CreateEndpointCommand request) => await mediator.Send(request);


    [HttpPost("update")]
    public async Task<bool> UpdateApiEndpoint(UpdateEndpointCommand request) => await mediator.Send(request);

    [HttpPost("enable")]
    public async Task<bool> EnableApiEndpoint(EnableEndpointCommand request) => await mediator.Send(request);
    
    
    [HttpPost("disable")]
    public async Task<bool> DisableApiEndpoint(DisableEndpointCommand request) => await mediator.Send(request);

    [HttpPost("delete")]
    public async Task<bool> DeleteApiEndpoint(DeleteEndpointCommand request) => await mediator.Send(request);


    [HttpPost("init")]
    public async Task<bool> InitApiEndpoints()
    {
        var endpoints = apiProvider.ApiDescriptionGroups.Items
            .SelectMany(group => group.Items)
            .Select(apiDescription =>  "/" + apiDescription.RelativePath)
            .ToList();
        var command = new InitEndpointCommand {Endpoints = endpoints};
        return await mediator.Send(command);
    }
}