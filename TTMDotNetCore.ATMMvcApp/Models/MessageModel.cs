using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTMDotNetCore.ATMMvcApp.Models
{
  
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

}
