using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linq_CRUD_MVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Deleted { get; set; }
        public int Age { get; set; }
    }
}