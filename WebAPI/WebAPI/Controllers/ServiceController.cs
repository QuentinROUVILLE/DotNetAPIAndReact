using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController, Route("api/v1/services")]
public class ServiceController : ControllerBase
{
    private static readonly string[] ServiceList = ["S1", "S2", "S3"];
    private static readonly string[] ActionList1 = ["ACTION1.1", "ACTION1.2", "ACTION1.3"];
    private static readonly string[] ActionList2 = ["ACTION2.1", "ACTION2.2", "ACTION2.3"];
    private static readonly string[] ActionList3 = ["ACTION3.1", "ACTION3.2", "ACTION3.3"];

    [HttpGet]
    [Authorize]
    public ActionResult<string[]> GetServiceList()
    {
        return Ok(ServiceList);
    }

    [HttpGet("{serviceName}")]
    [Authorize]
    public ActionResult<string[]> GetServiceActions(string serviceName)
    {
        if (serviceName == ServiceList[0])
        {
            return Ok(ActionList1);
        }
        if (serviceName == ServiceList[1])
        {
            return Ok(ActionList2);
        }
        if (serviceName == ServiceList[2])
        {
            return Ok(ActionList3);
        }

        return NotFound();
    }

    [HttpPost("{serviceName}")]
    [Authorize]
    public IActionResult ExecuteAction(string serviceName, ActionModel model)
    {
        var taskId = TaskHelpers.StartTask(serviceName, model.Action);

        return Ok(new { taskId });
    }
}