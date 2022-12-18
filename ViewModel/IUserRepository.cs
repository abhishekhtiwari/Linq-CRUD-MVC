using Linq_CRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_CRUD_MVC.ViewModel
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(int userId);
        void InsertUser(UserModel user);
        void DeleteUser(int userId);
        void UpdateUser(UserModel user);
    }
}
