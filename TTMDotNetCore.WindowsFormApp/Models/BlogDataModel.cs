﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMDotNetCore.WindowsFormApp.Models
{
    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
    }
}
