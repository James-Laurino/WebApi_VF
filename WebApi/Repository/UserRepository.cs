using WebApi.Interfaces;
using WebApi.Data;

namespace WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext Context;
        public UserRepository(DataContext Context)
        {
            this.Context = Context;
        }

        public List<User> Read()
        {
            return this.Context.User.ToList();
        }

        public User Update(User user)
        {
            User u = this.Context.User.Find(user.UserId);
            if(user == null)
            {
                return null;
            }
            else  
            {
                if(user.UserName.Equals("") != true)
                    u.UserName = user.UserName;
                
                if(user.Password.Equals("") != true)
                    u.Password = user.Password;

                this.Context.SaveChanges();
                return u;
            }
        }

        public User Delete(int id)
        {
            User user = this.Context.User.Find(id);
            if(user == null)
            {
                return null;
            }

            this.Context.User.Remove(user);
            this.Context.SaveChanges();
            return user;
        }

        public User Create(User user)
        {
            List<User> ListOfUserInDataBase = this.Context.User.ToList();

            foreach (var item in ListOfUserInDataBase)
            {
                if(user.UserName.ToLower() == item.UserName.ToLower())
                {
                    return null;
                }
            }

            this.Context.User.Add(user);
            this.Context.SaveChanges();
            return user;
        }

        public User ReadOne(int id)
        {
            User user = this.Context.User.Find(id);
            if(user != null)
            {
                return user;
            }

            return null;
        }

        public List<User> GetAllInformation(string info)
        {
            info = info.ToLower();
            List<User> ListOfUserInDataBase = this.Context.User.ToList();
            List<User> listOfUserFind = new List<User>();

            foreach (var item in ListOfUserInDataBase)
            {
                if(item.UserName.ToLower() == info || item.Password.ToLower() == info)
                {
                    listOfUserFind.Add(item);
                }
            }

            if(listOfUserFind.Count == 0)
            {
                return null;
            }
            else 
            {
                return listOfUserFind;
            }
        }
    }
}