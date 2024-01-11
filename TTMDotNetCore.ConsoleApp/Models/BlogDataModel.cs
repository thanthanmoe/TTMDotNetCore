using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTMDotNetCore.ConsoleApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    { 
    public int Blog_Id { get; set; }
    public string Blog_Title { get; set; }
    public string Blog_Author { get; set; }
    public string Blog_Content { get; set; }
}
}
