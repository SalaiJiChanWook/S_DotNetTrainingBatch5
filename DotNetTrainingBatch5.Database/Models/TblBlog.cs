using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetTrainingBatch5.Database.Models;

public partial class TblBlog
{
    //[Key]
    //public int Id { get; set; } // This should be your primary key
    //public string BlogTitle { get; set; }
    //public string BlogAuthor { get; set; }
    //public string BlogContent { get; set; }
    //public bool DeleteFlag { get; set; }
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
