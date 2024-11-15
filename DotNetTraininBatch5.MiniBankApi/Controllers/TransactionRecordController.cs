using DotnetTrainingBatch5.Domains.Features.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraininBatch5.MiniBankApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionRecordController : ControllerBase
{
    TransactionRecordService _serviceHistory = new TransactionRecordService();

    [HttpGet]
    public IActionResult GetAllHistories(string phoneNo)
    {
        var result = _serviceHistory.GetHistories(phoneNo);
        return Ok(result);
    }

    [HttpGet("{phoneNo}")]
    public IActionResult GetLastHistory(string phoneNo)
    {
        var result = _serviceHistory.GetHistory(phoneNo);
        return Ok(result);
    }
}
