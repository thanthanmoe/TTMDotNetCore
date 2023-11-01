using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TTMDotNetCore.RestAPI.AppDB;
using TTMDotNetCore.RestAPI.Models;

namespace TTMDotNetCore.RestAPI.Controllers
{
    public class BlogDapperController : Controller
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        // IConfiguration can read appsetting.json
        public BlogDapperController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbConnection");
            string connectionString2 = configuration.GetSection("SqlDbConnection").Value;
            string connectionString3 = configuration.GetSection("MyDbConnections:MyDb:DbConnection").Value;
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "SELECT * FROM Tbl_Blgo order by Blog_Id desc";
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();

            BlogListResponseModel blogList = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Ok(blogList);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            string query = "SELECT * FROM [Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";

            BlogDataModel student = new BlogDataModel()
            {
                Blog_Id = id,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel item = db.Query<BlogDataModel>(query, student).FirstOrDefault();

            BlogResponseModel model = new BlogResponseModel();
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found!!";
                model.Data = student;
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
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            if (!string.IsNullOrWhiteSpace(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrWhiteSpace(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrWhiteSpace(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            AppDbContext db = new AppDbContext();
            var blog = db.Blogs.FirstOrDefault(b => b.Blog_Id == id);

            BlogResponseModel data = new BlogResponseModel();
            if (blog is null)
            {
                data.IsSuccess = false;
                data.Message = "No data found.";
                return NotFound(data);
            }

            db.Blogs.Remove(blog);
            db.SaveChanges();
            data.IsSuccess = true;
            data.Message = "Success";
            return Ok(data);
        }
    }
}

