using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibrarys
{
    public enum Option
    {
        Bought,
        Brought,
        Taken_Away,
        Sold_Out
    }

    public class ReaderBook
    {
        public int Id { get; set; }
        public Reader? Readers { get; set; }
        [ForeignKey(nameof(Reader))]
        public int ReaderId { get; set; }

        public Book? Book { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Option Status { get; set; }

        public DateTime Data { get; set; }
    }
}
