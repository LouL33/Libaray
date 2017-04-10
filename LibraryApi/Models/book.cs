using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set;}
        public string Author { get; set;}
        public int YearPublished { get; set; }
        public string Genre { get; set; }
        public bool IsCheckedOut { get; set; }
        public DateTime? LastCheckedOut { get; set; }
        public DateTime? DueBackDate { get; set; }

    }
}