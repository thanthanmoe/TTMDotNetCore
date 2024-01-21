using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Text.Json;
using TTMDotNetCore.RestAPI.AppDB;
using TTMDotNetCore.RestAPI.Models;

namespace TTMDotNetCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BlogController> _logger;
        public BlogController(AppDbContext context, ILogger<BlogController> logger = null)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                List<BlogDataModel> lst = _context.Blogs.ToList();
                _logger.LogInformation("GetBlogs - Retrieved blog data: {BlogData}", JsonSerializer.Serialize(lst));

                BlogResponseModels model = new BlogResponseModels()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Data = lst
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetBlogs");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogs(int pageNo = 1, int pageSize = 10)
        {
            try
            {
                List<BlogDataModel> lst = _context
                    .Blogs
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                _logger.LogInformation("GetBlogs - Retrieved paginated blog data: {BlogData}", JsonSerializer.Serialize(lst));

                BlogResponseModels model = new BlogResponseModels()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Data = lst
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetBlogs with pagination");
                return StatusCode(500, "Internal Server Error");
            }
        }

       
            [HttpGet("{id}")]
            public IActionResult GetBlog(int id)
            {
                try
                {
                    BlogResponseModel model = new BlogResponseModel();

                    BlogDataModel item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
                    if (item is null)
                    {
                        model.IsSuccess = false;
                        model.Message = "No data found.";
                        _logger.LogWarning($"GetBlog - No data found for ID: {id}");
                        return NotFound(model);
                    }

                    model.IsSuccess = true;
                    model.Message = "Success";
                    model.Data = item;
                    _logger.LogInformation($"GetBlog - Retrieved blog data for ID: {id}");
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error in GetBlog with ID: {id}");
                    return StatusCode(500, "Internal Server Error");
                }
            }

            [HttpPost]
            public IActionResult CreateBlog([FromBody] BlogDataModel blog)
            {
                try
                {
                    _context.Blogs.Add(blog);
                    var result = _context.SaveChanges();
                    string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                    BlogResponseModel model = new BlogResponseModel()
                    {
                        IsSuccess = result > 0,
                        Message = message,
                    };
                    _logger.LogInformation($"CreateBlog - {message}");
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in CreateBlog");
                    return StatusCode(500, "Internal Server Error");
                }
            }

            [HttpPut("{id}")]
            public IActionResult UpdateBlog(int id, [FromBody] BlogDataModel blog)
            {
                try
                {
                    BlogResponseModel model = new BlogResponseModel();

                    BlogDataModel item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
                    if (item == null)
                    {
                        model.IsSuccess = false;
                        model.Message = "No data found.";
                        _logger.LogWarning($"UpdateBlog - No data found for ID: {id}");
                        return NotFound(model);
                    }

                    item.Blog_Title = blog.Blog_Title;
                    item.Blog_Author = blog.Blog_Author;
                    item.Blog_Content = blog.Blog_Content;

                    var result = _context.SaveChanges();
                    string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                    model = new BlogResponseModel()
                    {
                        IsSuccess = result > 0,
                        Message = message,
                    };
                    _logger.LogInformation($"UpdateBlog - {message}");
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error in UpdateBlog with ID: {id}");
                    return StatusCode(500, "Internal Server Error");
                }
            }

            // ... (similar logging for other methods)

            [HttpPatch("{id}")]
            public IActionResult PatchBlog(int id, [FromBody] BlogDataModel blog)
            {
                try
                {
                    BlogResponseModel model = new BlogResponseModel();

                    BlogDataModel item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
                    if (item == null)
                    {
                        model.IsSuccess = false;
                        model.Message = "No data found.";
                        _logger.LogWarning($"PatchBlog - No data found for ID: {id}");
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

                    var result = _context.SaveChanges();
                    string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                    model = new BlogResponseModel()
                    {
                        Data = item,
                        IsSuccess = result > 0,
                        Message = message,
                    };
                    _logger.LogInformation($"PatchBlog - {message}");
                    return Ok(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error in PatchBlog with ID: {id}");
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }
    }
