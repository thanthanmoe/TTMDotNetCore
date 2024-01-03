using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TTMDotNetCore.ATMMvcApp.Models
{
	[Table("Tbl_Admin")]
	public class AdminModel
	{
		[Key]
		[Column("AdminId")]

		public int AdminId { get; set; }
		public string FullName { get; set; }
		public string NRCNo { get; set; }
		public string PhoneNo { get; set; }
		public string Address { get; set; }
		public string Password { get; set; }
		public string StaffId { get; set; }
		public string Email { get; set; }
		public bool Active { get; set; }

	}
	public class AdminReqModel
	{
		public int AdminId { get; set; }
		public string FullName { get; set; }
		public string NRCNo { get; set; }
		public string PhoneNo { get; set; }
		public string Address { get; set; }
		public string Password { get; set; }
		public string StaffId { get; set; }
		public string Email { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }


	}
	public class AdminDataResponseModel
	{
		public PageSettingModel PageSetting { get; set; }
		public List<AdminModel> Admins { get; set; }
		public List<AdminReqModel> AdminReq { get; set; }
	}
}
