using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TTMDotNetCore.WebAPI.AppDB;
using TTMDotNetCore.WebAPI.Models;

namespace TTMDotNetCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read()
        {
           AppDbContext db = new AppDbContext();
            var list = db.Blogs.ToList();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = list
            };
            return Ok(model);
        }
       
        [HttpPost]
        public IActionResult Create([FromBody] BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result=db.SaveChanges();
            string message = result > 0 ? "Saving Successfully" : "Saving Fail";
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result >0,
                Message = message,
                Data = blog
            };
            return Ok(model);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogResponseModel model = new BlogResponseModel();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            
            if (item is null)
            {

                model.IsSuccess = false;
                model.Message = "This Id is not found";
                model.Data = item;
                return NotFound(model);
               
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;
            
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            BlogResponseModel model = new BlogResponseModel();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {

                model.IsSuccess = false;
                model.Message = "This Id is not found";
                model.Data = item;
                return NotFound(model);

            }
            
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Update Successfully" : "Update Fail";
            model.IsSuccess = true;
            model.Message = message;
            model.Data = blog;
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogUpdate(int id, [FromBody] BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            BlogResponseModel model = new BlogResponseModel();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {

                model.IsSuccess = false;
                model.Message = "This Id is not found";
                model.Data = item;
                return NotFound(model);

            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Update Successfully" : "Update Fail";

            model.IsSuccess = true;
            model.Message = message;
            model.Data = blog;

            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogResponseModel model = new BlogResponseModel();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {

                model.IsSuccess = false;
                model.Message = "This Id is not found";
                model.Data = item;
                return NotFound(model);

            }
            db.Blogs.Remove(item);

            var result = db.SaveChanges();
            string message = result > 0 ? "Delete Successfully" : "Delete Fail";

            model.IsSuccess = true;
            model.Message = message;
            
            return Ok(model);
        }
       
    }
}
