using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibrary
{
    internal class ReaderBook
    {
        public int Id { get; set; }
        public Reader? Reader { get; set; }
        [ForeignKey(nameof(Reader))]
        public int ReaderId { get; set; }

        public Books? Book { get; set; }
        [ForeignKey(nameof(Books))]
        public int BookId { get; set; }
    }
}
