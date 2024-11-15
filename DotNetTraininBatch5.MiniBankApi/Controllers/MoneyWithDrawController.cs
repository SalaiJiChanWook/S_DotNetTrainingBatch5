using DotnetTrainingBatch5.Domains.Features.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraininBatch5.MiniBankApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoneyWithDrawController : ControllerBase
{
    private readonly MoneyWithdrawService _withdrawService = new MoneyWithdrawService();

    [HttpPost]
    public IActionResult Withdraw(string mobileNo, float balance)
    {
        var result = _withdrawService.CreateWithdraw(mobileNo, balance);

        return Ok(result);
    }
}
