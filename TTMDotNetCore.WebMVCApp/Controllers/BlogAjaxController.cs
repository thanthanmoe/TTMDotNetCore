using TTMDotNetCore.WebMVCApp.AppDB;
using TTMDotNetCore.WebMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace TTMDotNetCore.WebMVCApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDbContext _context;

        public BlogAjaxController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("List")]
        public async Task<IActionResult> BlogList(int pageNo = 1, int pageSize = 10)
        {
            BlogDataResponseModel model = new BlogDataResponseModel();
            List<BlogDataModel> lst = _context.Blogs.AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
                .ToList();

            int rowCount = await _context.Blogs.CountAsync();
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
                pageCount++;

            model.Blogs = lst;
            model.PageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/blog/list");

            return View("BlogList", model);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel reqModel)
        {
            await _context.Blogs.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            MessageModel model = new MessageModel(result > 0, message);
            return Json(model);
        }

       
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blogajax/list");
            }
            return View("BlogEdit", blog);
        }
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(BlogDataModel reqModel)
        {

            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == reqModel.Blog_Id);
            if (!isExist)
            {
                string message = "No data found.";
                TempData["IsSuccess"] = false;
                TempData["Message"] = message;

                MessageModel model = new MessageModel(!isExist, message);
                return Json(model);
            }

            var item = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == reqModel.Blog_Id);
            if (item == null)
            {
                string message = "No data found.";
                TempData["IsSuccess"] = false;
                TempData["Message"] = message;
                MessageModel model = new MessageModel(false, message);
                return Json(model);
            }
            else
            {
                item.Blog_Id = reqModel.Blog_Id;
                item.Blog_Title = reqModel.Blog_Title;
                item.Blog_Author = reqModel.Blog_Author;
                item.Blog_Content = reqModel.Blog_Content;
                _context.Blogs.Update(item);
                var result = await _context.SaveChangesAsync();

                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                TempData["Message"] = message;
                TempData["IsSuccess"] = result > 0;

                MessageModel model = new MessageModel(result > 0, message);
                return Json(model);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(BlogDataModel reqModel)
        {
            BlogDataModel? blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == reqModel.Blog_Id);

            if (blog is null)
            {
				return Json(new MessageModel(false, "No data found."));
			}

            _context.Blogs.Remove(blog);
			var result = await _context.SaveChangesAsync();
			string message = result > 0 ? "Your blog has been deleted." : "Deleting Failed.";
			TempData["Message"] = message;
			TempData["IsSuccess"] = result > 0;

			MessageModel model = new MessageModel(result > 0, message);
			return Json(model);
		}
    }
}
