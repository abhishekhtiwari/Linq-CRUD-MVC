using Linq_CRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linq_CRUD_MVC.ViewModel
{
    public class UserRepository : IUserRepository
    {
        private DBDataContext db;
        public UserRepository()
        {
            db = new DBDataContext();
        }
        public IEnumerable<UserModel> GetUsers()
        {
            IList<UserModel> userList = new List<UserModel>();
            var query = from user in db.Users
                        where user.Deleted == 0
                        select user;
            var users = query.ToList();
            foreach (var userData in users)
            {
                userList.Add(new UserModel()
                {
                    Id = userData.Id,
                    Name = userData.Name,
                    Email = userData.Email,
                    Age = userData.Age
                });
            }
            return userList;
        }
        public UserModel GetUserById(int userId)
        {
            var query = from u in db.Users
                        where u.Id == userId
                        select u;
            var user = query.FirstOrDefault();
            var model = new UserModel()
            {
                Id = userId,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age
            };
            return model;
        }
        public void InsertUser(UserModel user)
        {
            var userData = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
                Deleted = 0,
                CreateOn = DateTime.Now
            };
            db.Users.InsertOnSubmit(userData);
            db.SubmitChanges();
        }

        public void DeleteUser(int userId)
        {
            User userData = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            userData.Deleted = 1;          
            db.SubmitChanges();           
        }

        public void UpdateUser(UserModel user)
        {
            User userData = db.Users.Where(u => u.Id == user.Id).SingleOrDefault();
            userData.Name = user.Name;
            userData.Email = user.Email;
            userData.Age = user.Age;
            db.SubmitChanges();
        }
    }
}