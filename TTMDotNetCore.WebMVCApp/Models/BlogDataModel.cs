﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TTMDotNetCore.WebMVCApp.Models
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
    public class BlogDataResponseModel
    {
        public PageSettingModel PageSetting { get; set; }
        public List<BlogDataModel> Blogs { get; set; }
    }

    public class PageSettingModel
    {
        public PageSettingModel()
        {
        }

        public PageSettingModel(int pageNo, int pageSize, int pageCount, string pageUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageUrl = pageUrl;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string PageUrl { get; set; }
    }

    public class MessageModel
    {
        public MessageModel() { }
        public MessageModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}