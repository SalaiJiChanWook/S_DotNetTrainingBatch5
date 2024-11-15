using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Database.Models;

public partial class TblTransactionRecord
{
    public int TranId { get; set; }

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public float Amount { get; set; }

    public string Notes { get; set; } = null!;

    public DateTime TranDate { get; set; }

    public bool DeleteFlag { get; set; }
}
