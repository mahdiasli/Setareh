﻿
using Setareh.Bussines.Security;
using Setareh.Bussines.Services.Interface;
using Setareh.DAL.Entities.Account;
using Setareh.DAL.Entities.User;
using Setareh.DAL.Repositories.Interface;
using Setareh.DAL.ViewModels.User;

namespace Setareh.Bussines.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserResult> CreateUserAsync(CreateUserModel model)
        {
            User user = new()
            {
                CreateDate = DateTime.Now,
                Email = model.Email.Trim().ToLower(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mobile = model.Mobile,
                Password = model.Password.Trim().EncodePasswordMd5(),
                IsActive = model.IsActive
            };

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveAsync();

            return CreateUserResult.Success;
        }

        public async Task<UserFilterViewModel> FilterAsync(UserFilterViewModel model)
        {
            return await _userRepository.FilterAsync(model);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email.Trim().ToLower());
        }

        public async Task<UserEditModel> GetForEditByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserEditModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                Id = user.Id,
                IsActive = user.IsActive
            };

        }

		public async Task<UserDetailViewModel> GetUserInformationAsync(int userId)
		{
			var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return null;

            return new UserDetailViewModel
            {
                CreateDate = user.CreateDate,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                IsActive = user.IsActive,
                Id = user.Id
            };


		}

		public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {
            model.Email = model.Email.Trim().ToLower();

            var user = await _userRepository.GetByEmailAsync(model.Email);

            if(user == null)
                return LoginResult.UserNotFound;

            string hashPassword = model.Password.Trim().EncodePasswordMd5();

            if (user.Password != hashPassword)
                return LoginResult.UserNotFound;

            return LoginResult.Success;

        }

        public async Task<EditUserResult> UpdateAsync(UserEditModel model)
        {
            var user = await _userRepository.GetByIdAsync(model.Id);

            if (user == null)
                return EditUserResult.UserNotFound;

            if(await _userRepository.DuplicatedEmail(user.Id,model.Email.Trim().ToLower()))
                return EditUserResult.EmailDuplicated;

            if (await _userRepository.DuplicatedMobile(user.Id, model.Mobile))
                return EditUserResult.MobileDuplicated;

            user.IsActive = model.IsActive;
            user.Email = model.Email;   
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Mobile = model.Mobile;
            
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
            return EditUserResult.Success;
        }
    }
}
