using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibrary
{
    internal class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<ReaderBook> ReaderBooks { get; set; }
    }
}
