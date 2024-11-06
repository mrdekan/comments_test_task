using CommentsAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace CommentsAPI.Services
{
    public class AuthenticationService
    {
        private readonly UserManager<UserEntity> _userManager;

        public AuthenticationService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserEntity> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            return isPasswordValid ? user : null;
        }

        public async Task<IdentityResult> RegisterUserAsync(string username, string password)
        {
            var user = new UserEntity
            {
                UserName = username,
                AvatarURL = ""
            };

            var result = await _userManager.CreateAsync(user, password);

            return result;
        }
    }
}
