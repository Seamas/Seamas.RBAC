using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.CreateEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DeleteEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.DisableEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.EnableEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.InitEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Commands.UpdateEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.CheckUrl;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetAllEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpoint;
using Wang.Seamas.RBAC.Application.Features.Endpoints.Queries.GetEndpointPage;
using Wang.Seamas.RBAC.Domain.Entities;
using Wang.Seamas.Web.Attributes;
using Wang.Seamas.Web.Common.DTOs;
namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/endpoints")]
[ControllerTag("接口管理")]
public class EndpointController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    [ActionTag("查询")]
    public async Task<ResultPage<ApiEndpoint>> QueryApis(GetEndpointPageQuery request)
    {
        return await mediator.Send(request);
    }

    [HttpPost("get")]
    [ActionTag("获取单个接口信息")]
    public async Task<ApiEndpoint?> GetApiEndpoint(GetEndpointQuery request) => await mediator.Send(request);


    [HttpPost("check-url")]
    [ActionTag("检查接口地址")]
    public async Task<bool> CheckApiEndpointUrl(CheckUrlQuery request) => await mediator.Send(request);

    [HttpPost("create")]
    [ActionTag("创建接口信息")]
    public async Task<bool> CreateApiEndpoint(CreateEndpointCommand request) => await mediator.Send(request);


    [HttpPost("update")]
    [ActionTag("更新接口信息")]
    public async Task<bool> UpdateApiEndpoint(UpdateEndpointCommand request) => await mediator.Send(request);

    [HttpPost("enable")]
    [ActionTag("启用接口")]
    public async Task<bool> EnableApiEndpoint(EnableEndpointCommand request) => await mediator.Send(request);
    
    
    [HttpPost("disable")]
    [ActionTag("禁用接口")]
    public async Task<bool> DisableApiEndpoint(DisableEndpointCommand request) => await mediator.Send(request);

    [HttpPost("delete")]
    [ActionTag("删除接口")]
    public async Task<bool> DeleteApiEndpoint(DeleteEndpointCommand request) => await mediator.Send(request);


    [HttpPost("init")]
    [ActionTag("系统初始化接口信息")]
    public async Task<bool> InitApiEndpoints()
    {
        var command = new InitEndpointCommand();
        return await mediator.Send(command);
    }

    [HttpGet("get-all")]
    [ActionTag("获取所有接口信息")]
    public async Task<IEnumerable<ApiEndpoint>> GetApiEndpoints()
    {
        var request = new GetAllEndpointQuery();
        return await mediator.Send(request);
    }



    
    
}