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
            DataSource = "DESKTOP-F40FPLH",
            InitialCatalog = "AHMTZDotNetCore",
            UserID = "sa",
            Password = "sasa"
        };

        public void Run()
        {
            Create("title", "author", "content");
            Read();
            Edit("update_title", "update_author", "update_content");
            Read();
        }

        private void Read()
        {
            string query = "select * from tbl_blog";
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
        private void Edit(string title, string author, string content)
        {

            string query = $@"Update [dbo].[Tbl_Blog] Set
           [Blog_Title]=@Blog_Title
           ,[Blog_Author]=@Blog_Author
           ,[Blog_Content]=@Blog_Content
     Where Blog_Id = 1";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            connection.Close();

            Console.WriteLine(message);
        }
    }
}
