
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TTMDotNetCore.WebAPI.AppDB;
using TTMDotNetCore.WebAPI.Models;

namespace TTMDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        #region EF
        /* [HttpGet]
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
         }*/
        #endregion

        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogDataModel> lst = _db.Blogs.ToList();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                //Data = lst.Where(x => x.Blog_Title == "").OrderByDescending(x => x.Blog_Id).ToList()
                Data = lst
            };
            return Ok(model);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogs(int pageNo, int pageSize)
        {
            //pageNo = 1;
            //pageSize = 10;
            //int endRowNo = pageNo * pageSize;
            //int startRowNo = endRowNo - pageSize + 1;
            //// 1,  1 - 10 // 0
            //// 2, 11 - 20 // 10
            //// 3, 21 - 30 // 20, 21 - 30
            List<BlogDataModel> lst = _db
                .Blogs
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                //Data = lst.Where(x => x.Blog_Title == "").OrderByDescending(x => x.Blog_Id).ToList()
                Data = lst
            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();

            BlogDataModel item = _db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] BlogDataModel blog)
        {
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }


    }
}
