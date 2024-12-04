using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Model
{
    public  class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }

        public static User Admin = new User { Id = 1, Name = Cafe.Model.Role.Admin.Name, Password = "12345" };
    }
}
