using System;
using System.Collections.Generic;

namespace WebAppAzureBookDb.Models
{
    public partial class BookDb
    {
        public int Bid { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? Category { get; set; }
        public double? Price { get; set; }
    }
}
