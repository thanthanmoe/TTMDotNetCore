using Microsoft.AspNetCore.Mvc;
using TTMDotNetCore.ATMWebApp.Models;
using TTMDotNetCore.ATMWebApp.AppDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TTMDotNetCore.ATMWebApp.Controllers
{
	public class LoginController : Controller
	{
		private readonly AppDbContext _context;

		public LoginController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			HttpContext.Session.Remove("LoginName");
			HttpContext.Session.Remove("UserId");
			HttpContext.Session.Clear();
            return View();
		}
		[HttpPost]
		[ActionName("UserLogin")]
		public async Task<IActionResult> UserLogin(UserModel reqModel)
		
		{
		
				bool isExist = await _context.Users.AsNoTracking().AnyAsync(x => x.CardCode == reqModel.CardCode && x.Active==true);
				if (!isExist)
				{

					TempData["IsSuccess"] = false;
					TempData["Message"] = "This Account Is Not Found";

					return Json(new MessageModel(false, "This Account Is Not Found"));
				}
				if (isExist)
				{
					var item = await _context.Users.FirstOrDefaultAsync(x => x.CardCode == reqModel.CardCode && x.Active==true);
					if (string.Equals(item.Password, reqModel.Password))
					{
                    HttpContext.Session.SetString("LoginName", item.FullName);
                    HttpContext.Session.SetInt32("UserId", item.UserId);
                    TempData["Message"] = "Login Successful.";
						TempData["IsSuccess"] = true;
						TempData["LoginName"] = item.FullName;
						TempData["UserId"] = item.UserId;
			
					return Json(new MessageModel(true, "Login Sussfully"));
					}
					else
					{
						TempData["Message"] = "Password Is Incorrect";
						TempData["IsSuccess"] = false;

						return Json(new MessageModel(false, "Password Is Incorrect"));
					}

				}
				else
				{
					string message = "Login Fail.";
					TempData["IsSuccess"] = false;
					TempData["Message"] = message;

					return Json(new MessageModel(false, "Login Fail."));
				}
			
		}
		public async Task<IActionResult> UserView()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
			ViewBag.CurrentBalance = user.CurrentBalance;
			return View();
		}
		public IActionResult AdLogin()
		{
			HttpContext.Session.Remove("LoginName");
			HttpContext.Session.Remove("AdminId");
			HttpContext.Session.Clear();
			return View();
		}
		[HttpPost]
		[ActionName("AdminLogin")]
		public async Task<IActionResult> AdminLogin(AdminModel reqModel)

		{

			bool isExist = await _context.Admins.AsNoTracking().AnyAsync(x => x.StaffId == reqModel.StaffId && x.Active == true);
			if (!isExist)
			{

				TempData["IsSuccess"] = false;
				TempData["Message"] = "This Account Is Not Found";

				return Json(new MessageModel(false, "This Account Is Not Found"));
			}
			if (isExist)
			{
				var item = await _context.Admins.FirstOrDefaultAsync(x => x.StaffId == reqModel.StaffId && x.Active == true);
				if (string.Equals(item.Password, reqModel.Password))
				{
					HttpContext.Session.SetString("LoginName", item.FullName);
					HttpContext.Session.SetInt32("AdminId", item.AdminId);
					TempData["Message"] = "Login Successful.";
					TempData["IsSuccess"] = true;
					TempData["LoginName"] = item.FullName;
					TempData["AdminId"] = item.AdminId;

					return Json(new MessageModel(true, "Login Sussfully"));
				}
			
				else
				{
					TempData["Message"] = "Password Is Incorrect";
					TempData["IsSuccess"] = false;

					return Json(new MessageModel(false, "Password Is Incorrect"));
				}

			}
			else
			{
				string message = "Login Fail.";
				TempData["IsSuccess"] = false;
				TempData["Message"] = message;

				return Json(new MessageModel(false, "Login Fail."));
			}

		}
		
	}
}
