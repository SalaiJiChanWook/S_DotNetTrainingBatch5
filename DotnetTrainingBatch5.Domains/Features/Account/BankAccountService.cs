using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetTrainingBatch5.Database.Models;

namespace DotnetTrainingBatch5.Domains.Features.Account
{
    public class BankAccountService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<TblAccounts> GetAccounts()
        {
            var result = _db.TblAccounts.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return result;
        }

        public TblAccounts GetAccount(int id)
        {
            var result = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                return null;
            }
            return result;
        }

        public object CreateAccount(TblAccounts account )
        {
            var phone = _db.TblAccounts.FirstOrDefault(x => x.MobileNo == account.MobileNo);
            var gmail = _db.TblAccounts.FirstOrDefault(x => x.Gmail == account.Gmail);
            Console.WriteLine(phone);
            Console.WriteLine(gmail);
            if (phone == null && gmail == null)
            {
                _db.TblAccounts.Add(account);
                _db.SaveChanges();
            }
            else
            {
                //Console.WriteLine("no Mobile phone number is already taken.");
                var error = new ErrorResponse
                {
                    errorMessage = "Mobile phone no or gmail is already taken."
                };
                return error;
            }
            
            return account;
        }

        public TblAccounts UpdateAccount(int id, TblAccounts account)
        {
            var item = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return null;
            }

            item.FullName = account.FullName;
            item.Gmail = account.Gmail;
            item.MobileNo = account.MobileNo;
            if (account.Balance > 0)
            {
                item.Balance = account.Balance;
            }
            else
            {
                return null;
            }
            item.pin = account.pin;


            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public TblAccounts PatchAccount(int id, TblAccounts account)
        {
            var result = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(account.FullName))  // when test show mobileNo and pin is required
            {
                result.FullName = account.FullName;
            }
            if (!string.IsNullOrEmpty(account.Gmail))
            {
                result.Gmail = account.Gmail;
            }
            if (!string.IsNullOrEmpty(account.MobileNo))
            {
                result.MobileNo = account.MobileNo;
            }
            if (account.Balance != null && account.Balance > 0)
            {
                result.Balance = account.Balance;
            }
            if (!string.IsNullOrEmpty(account.pin))
            {
                result.pin = account.pin;
            }
            if (!string.IsNullOrEmpty(account.OTP))
            {
                result.OTP = account.OTP;
            }

            _db.Entry(result).State = EntityState.Modified;
            _db.SaveChanges();

            return result;
        }

        public bool? DeleteAccount(int id)
        {
            var item = _db.TblAccounts.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null) return null;

            item.DeleteFlag = true;

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }
    }
}
