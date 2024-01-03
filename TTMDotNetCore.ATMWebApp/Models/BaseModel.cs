namespace TTMDotNetCore.ATMMvcApp.Models
{
	public class BaseModel
	{
		public DateTime CreatedDate { get; set; }
		public string CreatedUserId { get; set; }
		public DateTime UpdatedDate { get; set; }
		public string UpdatedUserId { get; set; }
	}
}
