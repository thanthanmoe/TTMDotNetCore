using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TTMDotNetCore.ConsoleApp.AdoDotNetCoreExamples
{
    public class AdoDotNetExample
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
            Create("title", "author", "content");
            Read();
            Edit(3);
            Update(3,"update_title1", "update_author1", "update_content1");
            Read();
            Delete(2);
            Read();
        }

        private void Read()
        {
            string query = "select * from Tbl_Blog";
           
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_Id"].ToString());
                Console.WriteLine(dr["Blog_Title"].ToString());
                Console.WriteLine(dr["Blog_Author"].ToString());
                Console.WriteLine(dr["Blog_Content"].ToString());
            }
        }

        private void Create(string title, string author, string content)
        {
            //       string query = $@"INSERT INTO [dbo].[Tbl_Blog]
            //      ([Blog_Title]
            //      ,[Blog_Author]
            //      ,[Blog_Content])
            //VALUES
            //      ('{title}'
            //      ,'{author}'
            //      ,'{content}')";

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            connection.Close();

            Console.WriteLine(message);
        }
        private void Edit(int id)
        {
            string query = "select * from Tbl_Blog where Blog_Id = @BlogId;";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["Blog_Id"].ToString());
            Console.WriteLine(dr["Blog_Title"].ToString());
            Console.WriteLine(dr["Blog_Author"].ToString());
            Console.WriteLine(dr["Blog_Content"].ToString());
        }
        private void Update(int id, string title, string author, string content)
        {

            string query = $@"Update [dbo].[Tbl_Blog] Set
           [Blog_Title]=@Blog_Title
           ,[Blog_Author]=@Blog_Author
           ,[Blog_Content]=@Blog_Content
     Where Blog_Id = @Blog_Id";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            connection.Close();

            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            string checkQuery = "select * from Tbl_Blog where Blog_Id = @BlogId;";
            string query = $@"Delete from [dbo].[Tbl_Blog] Where Blog_Id = @Blog_Id";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(checkQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }
            SqlCommand cm = new SqlCommand(query, connection);
            int result = cm.ExecuteNonQuery();
            string message = result > 0 ? "Delete Successful." : "Delete Failed.";

            connection.Close();

            Console.WriteLine(message);
        }
    }
}
