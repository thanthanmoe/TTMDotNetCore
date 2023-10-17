using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTMDotNetCore.WindowsFormApp.Models;

namespace TTMDotNetCore.WindowsFormApp
{
    public partial class FrmBlog : Form
    {
        private readonly AppDbContext _context;
        private readonly SqlConnection _sqlConnection;

        public FrmBlog()
        {
            InitializeComponent();
            AppConfigService appConfigService = new AppConfigService();
            _context = new AppDbContext(appConfigService.GetDbConnection());
            _sqlConnection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello world.");
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Author = txtAuthor.Text,
                Blog_Content = txtContent.Text,
                Blog_Title = txtTitle.Text
            };

            #region EF

            //_context.Blogs.Add(blog);
            //var result = _context.SaveChanges();
            //string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            //MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            #endregion

            #region Dapper

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            using (IDbConnection db = new SqlConnection(_sqlConnection.ConnectionString))
            {
                var result = db.Execute(query, blog);
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
           

          string querys = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

                SqlConnection connection = new SqlConnection(_sqlConnection.ConnectionString);
                connection.Open();

                SqlCommand cmd = new SqlCommand(querys, connection);
                cmd.Parameters.AddWithValue("@Blog_Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Blog_Author", txtAuthor.Text);
                cmd.Parameters.AddWithValue("@Blog_Content", txtContent.Text);
                int results = cmd.ExecuteNonQuery();
                string messages = results > 0 ? "Saving Successful." : "Saving Failed.";

                connection.Close();
                MessageBox.Show(messages, "Alert", MessageBoxButtons.OK, results > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            Console.WriteLine(messages);
            
            #endregion

            txtAuthor.Clear();
            txtContent.Clear();
            txtTitle.Clear();
            txtTitle.Focus();
        }
        


    }
}