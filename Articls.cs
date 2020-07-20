using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jornal.Models
{
    public class Articls
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Describtion { get; set; }
        public string AuthorName { get; set; }

    }
}