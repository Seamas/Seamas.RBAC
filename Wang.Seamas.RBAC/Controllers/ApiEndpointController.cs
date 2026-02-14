using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Wang.Seamas.RBAC.Dtos.ApiEndpoint;
using Wang.Seamas.RBAC.Models;
using Wang.Seamas.RBAC.Requests.ApiEndpoint;
using Wang.Seamas.RBAC.Services;
using Wang.Seamas.Web.Common.Dtos;

namespace Wang.Seamas.RBAC.Controllers;

[ApiController]
[Route("rbac/endpoints")]
public class ApiEndpointController(IApiEndpointService apiEndpointService, IMapper mapper, IApiDescriptionGroupCollectionProvider apiProvider) : ControllerBase
{
    [HttpPost("search")]
    public async Task<PagedResult<ApiEndpoint>> QueryApis(SearchApiRequest request)
    {
        var dto = mapper.Map<SearchApiDto>(request);
        var (items, total) = await apiEndpointService.SearchApiAsync(dto);
        return new PagedResult<ApiEndpoint>(items, total, dto.PageIndex, dto.PageSize);
    }


    [HttpPost("get")]
    public async Task<ApiEndpoint?> GetApiEndpoint(ApiIdRequest request) => await apiEndpointService.GetApiEndpointByIdAsync(request.Id);


    [HttpPost("check-url")]
    public async Task<bool> CheckApiEndpointUrl(ApiUrlRequest request) => await apiEndpointService.CheckApiUrlAsync(request.Id, request.Url);

    [HttpPost("create")]
    public async Task<bool> CreateApiEndpoint(CreateApiRequest request)
    {
        var dto = mapper.Map<ApiEndpoint>(request);
        return await apiEndpointService.CreateApiEndpointAsync(dto);
    }

    [HttpPost("update")]
    public async Task<bool> UpdateApiEndpoint(UpdateApiRequest request)
    {
        var dto = mapper.Map<ApiEndpoint>(request);
        return await apiEndpointService.UpdateApiEndpointAsync(dto);
    }

    [HttpPost("enable")]
    public async Task<bool> EnableApiEndpoint(EnableApiRequest request) => await apiEndpointService.EnableApiEndpointAsync(request.Id, request.Enabled);

    [HttpPost("delete")]
    public async Task<bool> DeleteApiEndpoint(ApiIdRequest request) => await apiEndpointService.DeleteApiEndpointAsync(request.Id);


    [HttpPost("init")]
    public async Task<bool> InitApiEndpoints()
    {
        var endpoints = apiProvider.ApiDescriptionGroups.Items
            .SelectMany(group => group.Items)
            .Select(apiDescription =>  "/" + apiDescription.RelativePath)
            .ToList();
        
        return await apiEndpointService.InitApiEndpointsAsync(endpoints);
    }
}