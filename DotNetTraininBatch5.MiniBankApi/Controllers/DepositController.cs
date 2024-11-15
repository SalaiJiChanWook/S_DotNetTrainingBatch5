using DotnetTrainingBatch5.Domains.Features.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraininBatch5.MiniBankApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepositController : ControllerBase
{
    private readonly DepositService _serviceDeposit = new DepositService();


    [HttpPost]
    public IActionResult Deposit(string mobileNo, float balance)
    {
        var result = _serviceDeposit.CreateDeposit(mobileNo, balance);

        return Ok(result);
    }
}
