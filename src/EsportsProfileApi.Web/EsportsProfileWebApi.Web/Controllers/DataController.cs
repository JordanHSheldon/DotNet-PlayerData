namespace EsportsProfileWebApi.Web.Controllers;

using EsportsProfileWebApi.CROSSCUTTING.Requests.Data;
using EsportsProfileWebApi.CROSSCUTTING.Responses.Data;
using EsportsProfileWebApi.Web.Orchestrators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DataController : Controller
{
    private readonly IDataOrchestrator _dataOrchestrator;

    public DataController(IDataOrchestrator dataOrchestrator)
    {
        _dataOrchestrator = dataOrchestrator ?? throw new NotImplementedException();
    }

    [HttpPost]
    [Route("GetDataByName")]
    public async Task<GetDataResponse> GetDataByUserName(GetDataRequest getDataRequestDto)
    {
        var result = await _dataOrchestrator.GetUserDataByAlias(getDataRequestDto);
        return result;
    }

    [Authorize]
    [HttpPost]
    [Route("GetAllUserData")]
    public async Task<List<GetDataResponse>> GetAllDataAsync()
    {
        var result = await _dataOrchestrator.GetAllDataAsync();
        return result;
    }

    [Authorize]
    [HttpPost]
    [Route("GetDataById")]
    public async Task<GetDataResponse> GetDataById(GetDataRequest getDataRequestDto)
    {
        var result = await _dataOrchestrator.GetUserDataByAlias(getDataRequestDto);
        return result;
    }

    [Authorize]
    [HttpPost]
    [Route("UpdateDataByAlias")]
    public ActionResult UpdateDataByAlias(UpdateDataRequest updateDataRequestDto)
    {
        // need to make sure only the user can update their own data
        //https://stackoverflow.com/questions/11037213/asp-net-mvc-attribute-to-only-let-user-edit-his-her-own-content
        return new JsonResult(_dataOrchestrator.UpdateDataByAlias(updateDataRequestDto));
    }
}