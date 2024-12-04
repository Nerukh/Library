using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibrary
{
    internal class Books
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Pages { get; set; }
        public int Price { get; set; }

        public ICollection<ReaderBook> ReaderBooks { get; set; }
    }
}
