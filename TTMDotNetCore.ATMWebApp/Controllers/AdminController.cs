using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.ATMWebApp.Models;
using TTMDotNetCore.ATMWebApp.AppDB;

namespace TTMDotNetCore.ATMWebApp.Controllers
{
	public class AdminController : Controller
	{
		private readonly AppDbContext _context;

		public AdminController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult AdminCreate()
		{
			return View();
		}
		[HttpPost]
		[ActionName("Save")]
		public async Task<IActionResult> Save(AdminModel reqModel)
		{
			bool isExist = await _context.Admins.AnyAsync(x => x.StaffId == reqModel.StaffId);
			if (!isExist)
			{
				await _context.Admins.AddAsync(reqModel);
				int result = await _context.SaveChangesAsync();
				string message = result > 0 ? "Saving Successful." : "Saving Failed.";
				TempData["Message"] = message;
				TempData["IsSuccess"] = result > 0;

				MessageModel model = new MessageModel(result > 0, message);
				return Json(model);
			}
			else
			{
				return Json(new MessageModel(false,"This Account is already create"));
			}
		}
		public async Task<IActionResult> AdminList(int pageNo = 1, int pageSize = 5)
		{
			AdminDataResponseModel model = new AdminDataResponseModel();
			List<AdminModel> lst = _context.Admins.AsNoTracking()
				.Where(x => x.Active)
				.Skip((pageNo - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			int rowCount = await _context.Admins.CountAsync();
			int pageCount = rowCount / pageSize;
			if (rowCount % pageSize > 0)
				pageCount++;

			model.Admins = lst;
			model.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/admin/adminlist");

			return View("AdminList", model);
		}
		public IActionResult Change()
		{
			return View();
		}

		[HttpPost]
		[ActionName("adminchange")]
		public async Task<IActionResult> AdminChange(AdminReqModel reqModel)
		{
			var adminId = HttpContext.Session.GetInt32("AdminId");

			// Check if the admin exists and is active
			bool isExist = await _context.Admins.AnyAsync(x => x.AdminId == adminId && x.Active == true);

			if (!isExist)
			{
				string message = "No Account found.";
				TempData["IsSuccess"] = false;
				TempData["Message"] = message;

				MessageModel model = new MessageModel(!isExist, message);
				return Json(model);
			}

			// Fetch the admin
			AdminModel? admin = await _context.Admins.FirstOrDefaultAsync(x => x.AdminId == adminId && x.Active == true);

			if (admin != null && string.Equals(admin.Password, reqModel.OldPassword))
			{
				// Update the password
				admin.Password = reqModel.NewPassword;
				_context.Admins.Update(admin);

				// Save changes to the database
				var result = await _context.SaveChangesAsync();

				string message = result > 0 ? "Password Change Successful." : "Failed.";
				TempData["Message"] = message;
				TempData["IsSuccess"] = result > 0;

				MessageModel model = new MessageModel(result > 0, message);
				return Json(model);
			}
			else
			{
				// Incorrect old password
				MessageModel model = new MessageModel(false, "Password is Incorrect");
				return Json(model);
			}
		}

		[HttpPost]
		[ActionName("Delete")]
		public async Task<IActionResult> AdminDelete(AdminModel reqModel)
		{
			AdminModel? admin = await _context.Admins.FirstOrDefaultAsync(x => x.AdminId == reqModel.AdminId && x.Active == true);

			if (admin is null)
			{
				return Json(new MessageModel(false, "No data found."));
			}
			admin.Active = false;
			_context.Admins.Update(admin);
			var result = await _context.SaveChangesAsync();

			string message = result > 0 ? "Admin Account has been deleted." : "Deleting Failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;

			return Json(new MessageModel(true, message));
		}
		public async Task<IActionResult> Edit(int id)
		{

			AdminModel? admin = await _context.Admins.FirstOrDefaultAsync(x => x.AdminId == id);

			return View("Edit", admin);
		}

		[HttpPost]
		[ActionName("EditAdmin")]
		public async Task<IActionResult> EditAdmin(AdminModel reqModel)
		{

			bool isExist = await _context.Admins.AnyAsync(x => x.AdminId == reqModel.AdminId && x.Active == true);
			if (!isExist)
			{
				string message = "No Account found.";
				TempData["IsSuccess"] = false;
				TempData["Message"] = message;

				MessageModel model = new MessageModel(!isExist, message);
				return Json(model);
			}
			else
			{


				AdminModel? admin = await _context.Admins.FirstOrDefaultAsync(x => x.AdminId == reqModel.AdminId);

				admin.FullName = reqModel.FullName;
				admin.NRCNo = reqModel.NRCNo;
				admin.PhoneNo = reqModel.PhoneNo;
				admin.Address = reqModel.Address;
				admin.Password = reqModel.Password;
				admin.Email = reqModel.Email;
				
				_context.Admins.Update(admin);
				var result = await _context.SaveChangesAsync();

				string message = result > 0 ? "Updating Successful." : "Updating Failed.";
				TempData["Message"] = message;
				TempData["IsSuccess"] = result > 0;

				MessageModel model = new MessageModel(result > 0, message);
				return Json(model);

			}

		}
	}
}
