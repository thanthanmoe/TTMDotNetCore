using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.MvcApp.AppDB;
using TTMDotNetCore.MvcApp.Models;

namespace TTMDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
       private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public async Task<IActionResult> BlogIndex(int pageNo = 1, int pageSize = 10)
        {
            List<BlogDataModel> lst = await _context.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.Blog_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int pageRowCount = await _context.Blogs.CountAsync();
            int pageCount = pageRowCount / pageSize;
            if (pageRowCount % pageSize > 0)
                pageCount++;

            //string a = "hello world";
            //ViewData["Title2"] = a;
            //ViewData["Number"] = 2;
            //ViewBag.Number2 = 3;

            //TempData["Title2"] = a;
            //TempData["Number"] = 2;
            //TempData["Number2"] = 3;
            //return Redirect("/Home");

            BlogListResponseModel model = new BlogListResponseModel
            {
                BlogList = lst,
                PageCount = pageCount,
                PageNo = pageNo,
                PageRowCount = pageRowCount,
                PageSize = pageSize
            };

            return View("BlogIndex", model);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            //string a = "hello world";
            //ViewData["Title2"] = a;
            //ViewData["Number"] = 2;
            //ViewBag.Number2 = 3;

            //TempData["Title2"] = a;
            //TempData["Number"] = 2;
            //TempData["Number2"] = 3;
            //return Redirect("/Home");
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel blog)
        {
            await _context.AddAsync(blog);
            var result = await _context.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Redirect("/Blog");
        }

        public async Task<IActionResult> Generate()
        {
            for (int i = 1; i <= 1000; i++)
            {
                await _context.AddAsync(new BlogDataModel
                {
                    Blog_Title = i.ToString(),
                    Blog_Author = i.ToString(),
                    Blog_Content = i.ToString(),
                });

                var result = await _context.SaveChangesAsync();
            }
            return Redirect("/Blog");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }
            return View("BlogEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModel blog)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = await _context.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Redirect("/Blog");
        }
    }
}