using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using StackOverflow.DomainModels;
using StackOverflow.Repositories;
using StackOverflow.ViewModel;

namespace StackOverflow.Servicelayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel uvm);
        void UpdateUserDetails(EditUserDetailsViewModel uvm);
        void UpdateUserPassword(EditUserPasswordViewModel uvm);
        void DeleteUser(int id);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string email, string password);
        UserViewModel GetUsersByEmail(string email);
        UserViewModel GetUsersByUserId(int userId);
    }

    public class UsersService : IUsersService
    {
        IUsersRepository ur;

        public UsersService()
        {
            ur = new UserRepository();
        }

        public int InsertUser(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>();cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(uvm);
            u.Password = SHA256HashGenerator.GenerateHash(uvm.Password);

            ur.InsertUser(u);
            int uid = ur.GetLatestUserId();
            return uid;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();

            User u = mapper.Map<EditUserDetailsViewModel, User>(uvm);
            ur.UpdateUserDetails(u);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();

            User u = mapper.Map<EditUserPasswordViewModel, User>(uvm);
            u.Password = SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.UpdateUserPassword(u);
        }

        public void DeleteUser(int uid)
        {
            ur.DeleteUser(uid);
        }

        public List<UserViewModel> GetUsers()
        {
            List <User> u = ur.GetUsers();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User,UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> uvm = mapper.Map<List<User>, List<UserViewModel>>(u);
            return uvm;
        }

        public UserViewModel GetUsersByEmailAndPassword(string email, string password)
        {
            User u = ur.GetUsersByEmailAndPAssword(email, SHA256HashGenerator.GenerateHash(password)).FirstOrDefault();
            UserViewModel uvm = null;
            if(u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);

            }

            return uvm;
        }

        public UserViewModel GetUsersByEmail(string email)
        {
            User u = ur.GetUsersByEmail(email).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);

            }

            return uvm;
        }

        public UserViewModel GetUsersByUserId(int userId)
        {
            User u = ur.GetUsersByUserId(userId).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);

            }
            return uvm;
        }


    }
}
