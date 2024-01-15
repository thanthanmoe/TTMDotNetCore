
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMDotNetCore.MinimalApi.AppDB;
using TTMDotNetCore.MinimalApi.Models;

namespace TTMDotNetCore.MinimalApi.Features.Blog
{

    public static class BlogService
    {
        public static void AddBlogService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blog/{pageNo}/{pageSize}", async ([FromServices] AppDbContext db, int pageNo, int pageSize) =>
            {
                return await db.Blogs
                    .AsNoTracking()
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapPost("/blog", async ([FromServices] AppDbContext db, BlogDataModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                return Results.Ok(new BlogResponseModel
                {
                    Data = blog,
                    IsSuccess = result > 0,
                    Message = message
                });
            })
           .WithName("CreateBlog")
           .WithOpenApi();

            app.MapPut("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel reqBlog) =>
            {
                var model = await db.Blogs.FindAsync(id);

                if (model == null)
                {
                    return Results.NotFound($"Blog with ID {id} not found.");
                }


                model.Blog_Title = reqBlog.Blog_Title;
                model.Blog_Author = reqBlog.Blog_Author;
                model.Blog_Content = reqBlog.Blog_Content;

                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Update Successful." : "Update Failed.";
                return Results.Ok(new BlogResponseModel
                {
                    Data = model,
                    IsSuccess = result > 0,
                    Message = message
                });
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            app.MapPatch("/blog/{id}", async ([FromServices] AppDbContext db, int id, BlogDataModel reqBlog) =>
            {
                var model = await db.Blogs.FindAsync(id);

                if (model == null)
                {
                    return Results.NotFound(new BlogResponseModel
                    {
                        
                        IsSuccess = false,
                        Message = "This Blog Is Not Found!"
                    });
                }
                if (!string.IsNullOrWhiteSpace(reqBlog.Blog_Title))
                {
                    model.Blog_Title = reqBlog.Blog_Title;
                }
                if (!string.IsNullOrWhiteSpace(reqBlog.Blog_Author))
                {
                    model.Blog_Author = reqBlog.Blog_Author;
                }
                if (!string.IsNullOrWhiteSpace(reqBlog.Blog_Content))
                {
                    model.Blog_Content = reqBlog.Blog_Content;
                }
                int result = await db.SaveChangesAsync();
                return Results.Ok(new BlogResponseModel
                {
                    Data = model,
                    IsSuccess = result > 0,
                    Message = "Successfully.."
                });
            })
            .WithName("PatchBlog")
            .WithOpenApi();

            app.MapDelete("/blog/{id}", async ([FromServices] AppDbContext db, int id) =>
            {
                var blogToDelete = await db.Blogs.FindAsync(id);

                if (blogToDelete == null)
                {
                    return Results.NotFound($"Blog with ID {id} not found.");
                }

                db.Blogs.Remove(blogToDelete);
                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Deletion Successful." : "Deletion Failed.";
                return Results.Ok(new BlogResponseModel
                {
                    Data = null,
                    IsSuccess = result > 0,
                    Message = message
                });
            })
            .WithName("DeleteBlog")
            .WithOpenApi();

        }
    }
}