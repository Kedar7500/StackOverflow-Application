using StackOverflow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Repositories
{
    public interface IUsersRepository 
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int uid);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPAssword(string email, string password);
        List<User> GetUsersByEmail(string email);
        List<User> GetUsersByUserId(int uid);
        int GetLatestUserId();
    }

    public class UserRepository : IUsersRepository
    {
        StackOverflowDB db;

        public UserRepository()
        {
            db = new StackOverflowDB();
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User u)
        {
            User us = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();

            if(us != null)
            {
                us.Name = u.Name;
                us.Mobile = u.Mobile;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User u)
        {
            User us = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();

            if (us != null)
            {
                us.Password= u.Password;
                db.SaveChanges();
            }
        }

        public void DeleteUser(int uid)
        {
            User us = db.Users.Where(temp => temp.UserID == uid).FirstOrDefault();

            if (us != null)
            {
                db.Users.Remove(us);
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> u = db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
            return u;
        }
        public List<User> GetUsersByEmailAndPAssword(string Email, string Password)
        {
            List<User> u = db.Users.Where(temp => temp.Email == Email &&temp.Password == Password ).ToList();
            return u;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> u = db.Users.Where(temp => temp.Email == Email).ToList();
            return u;
        }

        public List<User> GetUsersByUserId(int uid)
        {
            List<User> u = db.Users.Where(temp => temp.UserID == uid).ToList();
            return u;
        }
        public int GetLatestUserId()
        {
            int uid = db.Users.Select(temp => temp.UserID).Max();
            return uid;
        }


    }
}
