using DevelopersBuddyProject.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        void DeleteUser(int userId);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string email, string password);
        List<User> GetUsersByEmail(string email);
        List<User> GetUsersByUserId(int userId);
        int GetLatestUserId();
    }

    public class UsersRepository : IUsersRepository
    {
        readonly DevelopersBuddyDatabaseDbContext db;
        public UsersRepository()
        {
            db = new DevelopersBuddyDatabaseDbContext();
        }
        public void DeleteUser(int userId)
        {
            User existingUser = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (existingUser != null)
            {
                db.Users.Remove(existingUser);
                db.SaveChanges();
            }
        }

        public int GetLatestUserId()
        {
            int userId = db.Users.Select(x => x.UserId).Max();
            return userId;
        }

        public List<User> GetUsers()
        {
            List<User> users = db.Users.Where(x => !x.IsAdmin).OrderBy(x => x.Name).ToList();
            return users;
        }

        public List<User> GetUsersByEmail(string email)
        {
            List<User> users = db.Users.Where(x => x.Email == email).ToList();
            return users;
        }

        public List<User> GetUsersByEmailAndPassword(string email, string password)
        {
            List<User> users = db.Users.Where(x => x.Email==email && x.PasswordHash==password).ToList();
            return users;
        }

        public List<User> GetUsersByUserId(int userId)
        {
            List<User> users = db.Users.Where(x => x.UserId==userId).ToList();
            return users;
        }

        public void InsertUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User existingUser = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Mobile = user.Mobile;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User user)
        {
            User existingUser = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.PasswordHash = user.PasswordHash;
                db.SaveChanges();
            }
        }
    }
}
