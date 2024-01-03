using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTMDotNetCore.ATMWebApp.Models
{
	[Table("Tbl_User")]
	public class UserModel
	{

		[Key]
		[Column("UserId")]
		public int UserId { get; set; }
		public string FullName { get; set; }
		public string  NRCNo{ get; set; }
		public string PhoneNo { get; set; }
		public string Address { get; set; }
		public string Password { get; set; }
		public string CardCode { get; set; }
		public string Email { get; set; }
		public string CardType { get; set; }
		public decimal CurrentBalance { get; set; }
		public bool Active { get; set; }
	}
    public class UserReqModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string NRCNo { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string CardCode { get; set; }
        public string Email { get; set; }
        public string CardType { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal WithdrawBalance { get; set; }
        public decimal DepositBalance { get; set; }
		public decimal TransferBalance { get; set; }
		public string ToTransferAccount { get; set; }

	}
    public class UserDataResponseModel
	{
		public PageSettingModel PageSetting { get; set; }
		public List<UserModel> Users { get; set; }
		public List<UserReqModel> UserReq { get; set; }
	}
	public class UserListResponseModel
	{
		public List<UserModel> UserList { get; set; }
		public int PageNo { get; set; }
		public int PageSize { get; set; }
		public int PageCount { get; set; }
		public int PageRowCount { get; set; }
	}
	/*public class PageSettingModel
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
	}*/
}	
