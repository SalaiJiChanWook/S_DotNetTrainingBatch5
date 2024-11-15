using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Database.Models;

   public partial class TblAccounts
    {
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Gmail { get; set; } = null;

    public string MobileNo { get; set; } = null!;

    public float Balance { get; set; }

    public string OTP { get; set; } = null!;

    public string pin { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}

