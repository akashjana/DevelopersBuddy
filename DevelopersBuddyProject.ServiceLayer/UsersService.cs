using AutoMapper;
using DevelopersBuddyProject.DomainModel;
using DevelopersBuddyProject.Repositories;
using DevelopersBuddyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.ServiceLayer
{
    public interface IUsersService
    {
        int InsertUser(RegisterViewModel regViewModel);
        void UpdateUserDetails(EditUserDetailsViewModel edtUsrViewModel);
        void UpdateUserPassword(EditUserPasswordViewModel edtUsrPswdViewModel);
        void DeleteUser(int userId);
        List<UserViewModel> GetUsers();
        UserViewModel GetUsersByEmailAndPassword(string email, string password);
        UserViewModel GetUsersByEmail(string email);
        UserViewModel GetUsersByUserId(int userId);
    }
    public class UsersService : IUsersService
    {
        readonly IUsersRepository usersRepository;
        public UsersService()
        {
            usersRepository = new UsersRepository();
        }

        public void DeleteUser(int userId)
        {
            usersRepository.DeleteUser(userId);
        }

        public List<UserViewModel> GetUsers()
        {
            List<User> users = usersRepository.GetUsers();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> userViewModels = mapper.Map<List<User>, List<UserViewModel>>(users);
            return userViewModels;

        }

        public UserViewModel GetUsersByEmail(string email)
        {
            User user = usersRepository
                .GetUsersByEmail(email)
                .FirstOrDefault();
            UserViewModel userViewModels = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                userViewModels = mapper.Map<User, UserViewModel>(user);
            }
            return userViewModels;
        }

        public UserViewModel GetUsersByEmailAndPassword(string email, string password)
        {
            User user = usersRepository
                .GetUsersByEmailAndPassword(email,SHA256HashGenerator.GenerateHash(password))
                .FirstOrDefault();
            UserViewModel userViewModels = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                userViewModels = mapper.Map<User, UserViewModel>(user);
            }
            return userViewModels;
        }

        public UserViewModel GetUsersByUserId(int userId)
        {
            User user = usersRepository
                .GetUsersByUserId(userId)
                .FirstOrDefault();
            UserViewModel userViewModels = null;
            if (user != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                userViewModels = mapper.Map<User, UserViewModel>(user);
            }
            return userViewModels;
        }

        public int InsertUser(RegisterViewModel regViewModel)
        {
            var config = new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<RegisterViewModel, User>(); 
                cfg.IgnoreUnmapped(); 
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<RegisterViewModel, User>(regViewModel);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(regViewModel.Password);
            usersRepository.InsertUser(user);
            int userId = usersRepository.GetLatestUserId();
            return userId;
        }

        public void UpdateUserDetails(EditUserDetailsViewModel edtUsrViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserDetailsViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserDetailsViewModel, User>(edtUsrViewModel);
            usersRepository.UpdateUserDetails(user);

        }

        public void UpdateUserPassword(EditUserPasswordViewModel edtUsrPswdViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditUserPasswordViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            User user = mapper.Map<EditUserPasswordViewModel, User>(edtUsrPswdViewModel);
            user.PasswordHash = SHA256HashGenerator.GenerateHash(edtUsrPswdViewModel.Password);
            usersRepository.UpdateUserPassword(user);
        }
    }
}
