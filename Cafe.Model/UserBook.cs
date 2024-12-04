using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Model
{
    public enum Options
    {
        Bought,
        Brought,
        Taken_Away,
        Sold_Out
    }

    public class UserBook
    {
        public int Id { get; set; }
        public UserLibrary? Reader { get; set; }
        [ForeignKey(nameof(UserLibrary))]
        public int ReaderId { get; set; }

        public Books? Book { get; set; }
        [ForeignKey(nameof(Books))]
        public int BookId { get; set; }
        public Options Status { get; set; }

        public DateTime Data {  get; set; }

    }
}
