using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetTrainingBatch5.Database.Models;
using DotnetTrainingBatch5.Domains.Features.Account;

namespace DotNetTraininBatch5.MiniBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountService _accountService;

        public BankAccountController()
        {
            _accountService = new BankAccountService();
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            var lst = _accountService.GetAccounts();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var item = _accountService.GetAccount(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateAccount(TblAccounts account)
        {
            var result = _accountService.CreateAccount(account);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, TblAccounts account)
        {
            var item = _accountService.UpdateAccount(id, account);
            if (item is null)
            {
                return NotFound("Your input balance is Incorrect or Your id is not found!");
            }
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchAccount(int id, TblAccounts account)
        {
            var item = _accountService.PatchAccount(id, account);
            if (item is null)
            {
                return NotFound("Your input balance is Incorrect or Your id is not found!");
            }
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var item = _accountService.DeleteAccount(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok("Account is successfully deleted!");
        }
    }
}
