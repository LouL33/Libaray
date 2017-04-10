using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using LibraryApi.Models;

namespace LibraryApi.Controllers
{
    public class LibraryController : ApiController
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=ThiLibrary;Trusted_Connection=True;";

        public object Title { get; private set; }

        public IHttpActionResult GetAllBooks()
        {
            // We want to query the database for all the books;
            var rv = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {

                var text = @"SELECT * FROM Library";
                var cmd = new SqlCommand(text, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add($"{reader["Title"]}-- {reader["Genre"]}");
                }
                connection.Close();
            }

            return Ok(rv);

        }

        [HttpPut]
        public IHttpActionResult CreateBook([FromBody]Book book)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                var query = @"INSERT INTO Library ([Title],[Author],[YearPublished],[Genre])
                             VALUES (@Title, @Auther, @YearPubLished, @Genre)";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Auther", book.Author);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                // cmd.Parameters.AddWithValue("@IsCheckedOut", book.IsCheckedOut);
                //cmd.Parameters.AddWithValue("@LastCheckOutDate", book.LastCheckedOut);
                //cmd.Parameters.AddWithValue("@DueBackDate", book.DueBackDate);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            return Ok(book);


        }

        [HttpDelete]
        public IHttpActionResult RemoveBook([FromBody]Book book)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                var query = @"DELETE FROM [dbo].[Library] WHERE Title = @Title";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Title", book.Title);
               // cmd.Parameters.AddWithValue("@Auther", book.Author);
               // cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                // cmd.Parameters.AddWithValue("@Genre", book.Genre);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
           

            }
            return Ok(book);
        }


    }

    
}
