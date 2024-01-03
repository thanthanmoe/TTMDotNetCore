using Microsoft.AspNetCore.Mvc;
using TTMDotNetCore.ATMMvcApp.Models;
using TTMDotNetCore.ATMMvcApp.AppDB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace TTMDotNetCore.ATMMvcApp.Controllers
{
	public class UserController : Controller
	{
		private readonly AppDbContext _context;

		public UserController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		
		public async Task<IActionResult> UserList(int pageNo = 1, int pageSize = 5)
		{
			UserDataResponseModel model = new UserDataResponseModel();
			List<UserModel> lst = _context.Users.AsNoTracking()
				 .Where(user => user.Active)
				.Skip((pageNo - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			int rowCount = await _context.Users.CountAsync();
			int pageCount = rowCount / pageSize;
			if (rowCount % pageSize > 0)
				pageCount++;

			model.Users = lst;
			model.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/User/userlist");

			return View("UserList", model);
		}
		/*[ActionName("UserCreate")]*/
		public IActionResult UserCreate()
		{
			return View("UserCreate");
		}
		[HttpPost]
		[ActionName("Save")]
		public async Task<IActionResult> Save(UserModel reqModel)
		{
			bool isExist = await _context.Users.AnyAsync(x => x.CardCode == reqModel.CardCode);
			if (!isExist)
			{
				await _context.Users.AddAsync(reqModel);
				int result = await _context.SaveChangesAsync();
				string message = result > 0 ? "Saving Successful." : "Saving Failed.";
				TempData["Message"] = message;
				TempData["IsSuccess"] = result > 0;

				MessageModel model = new MessageModel(result > 0, message);
				return Json(model);
			}
			else
			{
				return Json(new MessageModel(false,"This account is already create"));
			}
		}
		[HttpPost]
		[ActionName("Delete")]
		public async Task<IActionResult> UserDelete(UserModel reqModel)
		{
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId && x.Active==true);

			if (user is null)
			{
				return Json(new MessageModel(false, "No data found."));
			}
			user.Active = false;
			_context.Users.Update(user);
			var result = await _context.SaveChangesAsync();
			
			string message = result > 0 ? "User Account has been deleted." : "Deleting Failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;

			return Json(new MessageModel(true, message));
		}

		
		public async Task<IActionResult> Withdraw()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
			ViewBag.CurrentBalance = user.CurrentBalance;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> WithdrawMoney(UserReqModel reqModel)
		{
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId && x.Active==true);
			TempData["LoginName"] = user.FullName;
			if (user == null)
			{
				TempData["Message"] = "User not found.";
				TempData["IsSuccess"] = false;

				return Json(new MessageModel(false, "User not found."));
			}

			if (string.Equals(user.Password, reqModel.Password))
			{
				if (user.CurrentBalance >= reqModel.WithdrawBalance)
				{
					try
					{
						using (var transaction = _context.Database.BeginTransaction())
						{
							try
							{
								decimal newBalance = user.CurrentBalance - reqModel.WithdrawBalance;
								user.CurrentBalance = newBalance;
								_context.Users.Update(user);

								int result = await _context.SaveChangesAsync();

								transaction.Commit();

								string message = result > 0 ? "Successfully Withdraw." : "Withdraw failed.";
								TempData["Message"] = message;
								TempData["IsSuccess"] = result > 0;

								return Json(new MessageModel(result > 0, message));
							}
							catch (Exception)
							{
								transaction.Rollback();
								throw; // Rethrow the exception after rolling back the transaction
							}
						}
					}
					catch (Exception ex)
					{
						TempData["Message"] = "An error occurred during the withdrawal.";
						TempData["IsSuccess"] = false;

						return Json(new MessageModel(false, "An error occurred during the withdrawal. " + ex.Message));
					}
				}
				else
				{
					TempData["Message"] = "Your CurrentBalance is not enough.";
					TempData["IsSuccess"] = false;

					return Json(new MessageModel(false, "Your CurrentBalance is not enough."));
				}
			}
			else
			{
				TempData["Message"] = "Your Password is incorrect.";
				TempData["IsSuccess"] = false;

				return Json(new MessageModel(false, "Your Password is incorrect."));
			}
		}
		public async Task<IActionResult> Deposit()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
			ViewBag.CurrentBalance = user.CurrentBalance;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> DepositMoney(UserReqModel reqModel)
		{
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId && x.Active==true);
			TempData["LoginName"] = user.FullName;
			if (user == null)
			{
				TempData["Message"] = "User not found.";
				TempData["IsSuccess"] = false;

				return Json(new MessageModel(false, "User not found."));
			}

			if (string.Equals(user.Password, reqModel.Password))
			{
					try
					{
						using (var transaction = _context.Database.BeginTransaction())
						{
							try
							{
								decimal newBalance = user.CurrentBalance + reqModel.DepositBalance;
								user.CurrentBalance = newBalance;
								_context.Users.Update(user);

								int result = await _context.SaveChangesAsync();

								transaction.Commit();

								string message = result > 0 ? "Successfully Deposit." : "Deposit failed.";
								TempData["Message"] = message;
								TempData["IsSuccess"] = result > 0;

								return Json(new MessageModel(result > 0, message));
							}
							catch (Exception)
							{
								transaction.Rollback();
								throw; // Rethrow the exception after rolling back the transaction
							}
						}
					}
					catch (Exception ex)
					{
						TempData["Message"] = "An error occurred during the withdrawal.";
						TempData["IsSuccess"] = false;

						return Json(new MessageModel(false, "An error occurred during the withdrawal. " + ex.Message));
					}
			
			}
			else
			{
				TempData["Message"] = "Your Password is incorrect.";
				TempData["IsSuccess"] = false;

				return Json(new MessageModel(false, "Your Password is incorrect."));
			}
		}
		public async Task<IActionResult> Transfer()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
			ViewBag.CurrentBalance = user.CurrentBalance;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> TransferMoney(UserReqModel reqModel)
		{
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId && x.Active==true);
			bool isExist = await _context.Users.AsNoTracking().AnyAsync(x => x.CardCode == reqModel.ToTransferAccount);
			if (!isExist)
			{

				TempData["IsSuccess"] = false;
				TempData["Message"] = "This Account Is Not Found";

				return Json(new MessageModel(false, "This Account Is Not Found"));
			}


			var item = await _context.Users.FirstOrDefaultAsync(x => x.CardCode == reqModel.ToTransferAccount && x.Active==true);
			
			if (string.Equals(user.Password, reqModel.Password))

			{
				if (user.CardCode == reqModel.ToTransferAccount)
				{
					TempData["Message"] = "Transfer Account Error";
					TempData["IsSuccess"] = false;

					return Json(new MessageModel(false, "Transfer Account Error."));
				}
				else if (user.CurrentBalance > reqModel.TransferBalance)
				{
					try
					{
						using (var transaction = _context.Database.BeginTransaction())
						{
							try
							{
								decimal newBalance = user.CurrentBalance - reqModel.TransferBalance;
								user.CurrentBalance = newBalance;
								_context.Users.Update(user);

								decimal balance = item.CurrentBalance + reqModel.TransferBalance;
								item.CurrentBalance = balance;
								_context.Users.Update(item);

								int result = await _context.SaveChangesAsync();

								transaction.Commit();

								string message = result > 0 ? "Successfully Transfer." : "Transfer failed.";
								TempData["Message"] = message;
								TempData["IsSuccess"] = result > 0;

								return Json(new MessageModel(result > 0, message));
							}
							catch (Exception)
							{
								transaction.Rollback();
								throw; // Rethrow the exception after rolling back the transaction
							}
						}
					}
					catch (Exception ex)
					{
						TempData["Message"] = "An error occurred during the withdrawal.";
						TempData["IsSuccess"] = false;

						return Json(new MessageModel(false, "An error occurred during the withdrawal. " + ex.Message));
					}
				}
				else
				{
					TempData["Message"] = "Your CurrentBalance is not enough.";
					TempData["IsSuccess"] = false;

					return Json(new MessageModel(false, "Your CurrentBalance is not enough."));
				}
			}
			else
			{
				TempData["Message"] = "Your Password is incorrect.";
				TempData["IsSuccess"] = false;

				return Json(new MessageModel(false, "Your Password is incorrect."));
			}
		}

		public async Task<IActionResult> Edit()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
			ViewBag.CurrentBalance = user.CurrentBalance;
			return View("Edit",user);
		}

		[HttpPost]
		[ActionName("Update")]
		public async Task<IActionResult> UserUpdate(UserModel reqModel)
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			bool isExist = await _context.Users.AnyAsync(x => x.UserId == userId && x.Active==true);
			if (!isExist)
			{
				string message = "No User found.";
				TempData["IsSuccess"] = false;
				TempData["Message"] = message;

				MessageModel model = new MessageModel(!isExist, message);
				return Json(model);
			}
			else {

				
				UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

				user.UserId = (int)userId;
				user.FullName = reqModel.FullName;
				/*user.NRCNo = reqModel.NRCNo;*/
				user.PhoneNo = reqModel.PhoneNo;
				user.Address = reqModel.Address;
				user.Password = reqModel.Password;
				user.Email = reqModel.Email;
				user.CurrentBalance = reqModel.CurrentBalance;
				_context.Users.Update(user);
				var result = await _context.SaveChangesAsync();

				string message = result > 0 ? "Updating Successful." : "Updating Failed.";
				TempData["Message"] = message;
				TempData["IsSuccess"] = result > 0;

				MessageModel model = new MessageModel(result > 0, message);
				return Json(model);

			}

		}
		public async Task<IActionResult> Change()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
			ViewBag.CurrentBalance = user.CurrentBalance;
			return View();
		}
		[HttpPost]
		[ActionName("userchange")]
		public async Task<IActionResult> UserChange(AdminReqModel reqModel)
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			bool isExist = await _context.Users.AnyAsync(x => x.UserId == userId && x.Active == true);

			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId && x.Active == true);
			if (!isExist)
			{
				string message = "No Account found.";
				TempData["IsSuccess"] = false;
				TempData["Message"] = message;

				MessageModel model = new MessageModel(!isExist, message);
				return Json(model);
			}
			if (user != null && string.Equals(user.Password, reqModel.OldPassword))
			{
				user.Password = reqModel.NewPassword;
				_context.Users.Update(user);
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
		public async Task<IActionResult> UserEdit(int id)
		{

			UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

			return View("UserEdit", user);
		}

		[HttpPost]
		[ActionName("EditUser")]
		public async Task<IActionResult> EditUser(UserModel reqModel)
		{
			
			bool isExist = await _context.Users.AnyAsync(x => x.UserId == reqModel.UserId && x.Active == true);
			if (!isExist)
			{
				string message = "No User found.";
				TempData["IsSuccess"] = false;
				TempData["Message"] = message;

				MessageModel model = new MessageModel(!isExist, message);
				return Json(model);
			}
			else
			{


				UserModel? user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId);

				user.FullName = reqModel.FullName;
				/*user.NRCNo = reqModel.NRCNo;*/
				user.PhoneNo = reqModel.PhoneNo;
				user.Address = reqModel.Address;
				user.Password = reqModel.Password;
				user.Email = reqModel.Email;
				/*user.CurrentBalance = reqModel.CurrentBalance;*/
				_context.Users.Update(user);
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
