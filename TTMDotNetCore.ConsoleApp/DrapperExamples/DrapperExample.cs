using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
using TTMDotNetCore.ConsoleApp.Models;

namespace TTMDotNetCore.ConsoleApp.DrapperExamples
{
    class DrapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TestDb",
            UserID = "sa",
            Password = "sasa"
        };
        public void Run()
        {
            Read();
            Create("NextTitle", "NextAuthor", "NextContent");
            Edit(4);
            Update(12,"NextUpdateTitle", "NextUpdateAuthor", "NextUpdateContent");
            Read();
            Delete(12);
            Read();
        }
        private void Read()
        {
            string query = "Select * from Tbl_Blog order by Blog_Id desc";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            List<BlogDataModel> list = db.Query<BlogDataModel>(query).ToList();
            foreach(var items in list)
            {
                Console.WriteLine(items.Blog_Id);
                Console.WriteLine(items.Blog_Author);
                Console.WriteLine(items.Blog_Title);
                Console.WriteLine(items.Blog_Content);
            }
        }
        private void Create(String Title,String Author,String Content)
        {
            string query = $@"Insert Into Tbl_Blog
                                (Blog_Title,Blog_Author,Blog_Content) 
                          Values(@Blog_Title,@Blog_Author,@Blog_Content)";
            BlogDataModel model = new BlogDataModel()
            {
                Blog_Title = Title,
                Blog_Author = Author,
                Blog_Content = Content
            };
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, model);
            var message = result > 0 ? "Save Successfully" : "Save Fail";
            Console.WriteLine(message);
        }
        private void Edit(int id)
        {
            string query = $@"Select * from Tbl_Blog Where Blog_Id=@Blog_Id";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
             };
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel model = db.Query<BlogDataModel>(query,blog).FirstOrDefault();
            if (model == null)
            {
                Console.WriteLine("This Blog is not found");
                return;
            }
            Console.WriteLine(model.Blog_Author);
        }
        private void Update(int id,String Title, String Author, String Content)
        {
            string query = @"Update [dbo].[Tbl_Blog] Set
           [Blog_Title]=@Blog_Title
           ,[Blog_Author]=@Blog_Author
           ,[Blog_Content]=@Blog_Content
            Where Blog_Id = @Blog_Id";
            BlogDataModel model=new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = Title,
                Blog_Author = Author,
                Blog_Content = Content
            };
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, model);
            var message = result > 0 ? "Update Successfully" : "Update Fail";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string checkQuery = "Select * from Tbl_Blog Where Blog_Id = @Blog_Id";
            string query = $@"Delete from Tbl_Blog Where Blog_Id = @Blog_Id";
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
            };
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel checkBlog = db.Query<BlogDataModel>(checkQuery,blog).FirstOrDefault();

            if (checkBlog == null)
            {
                Console.WriteLine("This BlogId is not found");
                return;
            }
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Delete Successful." : "Delete Failed.";
            Console.WriteLine(message);
        }
    }
}
