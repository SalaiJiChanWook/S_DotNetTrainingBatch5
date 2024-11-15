using DotnetTrainingBatch5.Domains.Features.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraininBatch5.MiniBankApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoneyTransferController : ControllerBase
{
    private readonly MoneyTransferService _transferService = new MoneyTransferService();

    [HttpPost]
    public IActionResult Transfer(string fromMobile, string toMobile, float amount,string OTP, string pin, string notes)
    {
        var result = _transferService.CreateTransfer(fromMobile, toMobile, amount,OTP, pin, notes);

        return Ok(result);
    }
}
