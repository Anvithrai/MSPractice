using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Service
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;
		public AuthService(IBaseService baseservice)
		{
			_baseService = baseservice;
		}
		public async Task<ResponseDto> LoginAsync(LoginRequestDTO loginRequestDTO)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
                ApiType = SD.ApiType.POST,
				Data = loginRequestDTO,
				Url = SD.AuthApIBase + "/api/auth/login"
			});
		}

		public async Task<ResponseDto> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = registrationRequestDTO,
				Url = SD.AuthApIBase + "/api/auth/register"
			});
		}

		public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = registrationRequestDTO,
				Url = SD.AuthApIBase + "/api/auth/AssignRole"
			});

		}
	}
	
}
