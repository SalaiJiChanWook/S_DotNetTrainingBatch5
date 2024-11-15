﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetTrainingBatch5.Database.Models;


namespace DotnetTrainingBatch5.Domains.Features.Account;

public class TransactionRecordService
{
    AppDbContext _db = new AppDbContext();

    public object GetHistories(string phone)
    {
        var result = _db.TblTransactionHistories
            .AsNoTracking()
            .Where(x => x.DeleteFlag == false)
            .Where(x => x.FromMobileNo == phone)
            .OrderByDescending(x => x.TranDate)
            .ToList();
        if (result.Count == 0)
        {
            return new ErrorResponse { errorMessage = "No History!!" };
        }
        return result;
    }

    public object GetHistory(string phone)
    {
        var result = _db.TblTransactionHistories
            .AsNoTracking()
            .Where(x => x.DeleteFlag == false)
            .OrderByDescending(x => x.TranDate)
            .FirstOrDefault(x => x.FromMobileNo == phone);

        if (result is null)
        {
            return new ErrorResponse { errorMessage = "No History!!" };
        }
        return result;
    }
}
