using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetTrainingBatch5.Database.Models;

namespace DotnetTrainingBatch5.Domains.Features.Account;

public class MoneyWithdrawService
{
    private readonly AppDbContext _db = new AppDbContext();

    public object CreateWithdraw(string mobileNo, float balance)
    {
        var mobile = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).FirstOrDefault(x => x.MobileNo == mobileNo);
        if (mobile != null)
        {
            if (balance > 0)
            {
                if (10000 >= mobile.Balance - balance)
                {
                    var error = new ErrorResponse
                    {
                        errorMessage = "Insufficient Balance.\n You cannot withdraw!\n"
                    };
                    return error;
                }
                else
                {
                    mobile.Balance -= balance;
                }
            }
            else
            {
                var error = new ErrorResponse
                {
                    errorMessage = "Invalid Balance.\n"
                };
                return error;
            }
            var result = _db.TblAccounts.Update(mobile);
            _db.SaveChanges();
        }
        else
        {
            var error = new ErrorResponse
            {
                errorMessage = "Error!!!\n Mobile phone number doesn't exist."
            };
            return error;
        }
        return mobile;
    }
}
