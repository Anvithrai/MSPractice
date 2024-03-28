using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Mango.Services.AuthAPI.Services
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _appDbContext;
		private readonly UserManager<ApplicationUser> _usermanager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly JwtTokenGenerator _jwtTokenGenerator;
		public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> roleManager, JwtTokenGenerator jwtTokenGenerator)
		{
			_appDbContext = appDbContext;
			_usermanager = usermanager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}
		public async Task<bool> AssignRole(string email, string roleName)
		{
			var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
			if (user != null)
			{
				if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
				{
					_roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();

				}
				await _usermanager.AddToRoleAsync(user,roleName);
				return true;
			}
			return false;

			}		
		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{

			//LoginResponseDTO loginResponseDTO;
			var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName);
			bool isValid = await _usermanager.CheckPasswordAsync(user, loginRequestDTO.Password);
			if (user == null || isValid == false)
			{
				return new LoginResponseDTO()
				{
					user = null,
					Token = ""
				};
			}
				//if user found generate jwt token
				var token = _jwtTokenGenerator.GenerateToken(user);

				UserDTO userDTO = new()
				{
					Email = user.Email,
					Id = user.Id,
					Name = user.Name,
					PhoneNumber = user.PhoneNumber
				};
				LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
				{
					user = userDTO,
					Token = token
				};

				return loginResponseDTO;
			}
		public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
		{
			ApplicationUser user = new()
			{
				UserName = registrationRequestDTO.Email,
				Email = registrationRequestDTO.Email,
				NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
				Name = registrationRequestDTO.Name,
				PhoneNumber = registrationRequestDTO.PhoneNumber
			};
			try
			{
				var result = await _usermanager.CreateAsync(user, registrationRequestDTO.Password);
				if (result.Succeeded)
				{
					var userToReturn = _appDbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDTO.Email);
					UserDTO userdto = new()
					{
						Email = userToReturn.Email,
						Id = userToReturn.Id,
						Name = userToReturn.Name,
						PhoneNumber = userToReturn.PhoneNumber
					};
					return "";
				}
				else
				{
					return result.Errors.FirstOrDefault().Description;
				}
			}
			catch (Exception ex)
			{

			}
			return "Error Encountered";
		}
	}
}
