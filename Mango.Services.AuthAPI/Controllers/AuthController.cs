using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
		
	{
		private readonly IAuthService _authService;
		protected ResponseDto _responseDto;

		//add something
		public AuthController(IAuthService authService, ResponseDto responseDto)
		{
			_authService = authService;
			_responseDto = responseDto;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
		{
			var errormsg=await _authService.Register(model);
			if(!string.IsNullOrEmpty(errormsg))
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message= errormsg;
				return BadRequest(_responseDto);
			}
			return Ok(_responseDto);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var loginresponse=await _authService.Login(model);
			if(loginresponse.user==null)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = "Incorrect credentials";
				return BadRequest(_responseDto);
			}
			_responseDto.Result = loginresponse;
			return Ok(_responseDto);
		}

		[HttpPost("AssignRole")]
		public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model)
		{
			var assignrole = await _authService.AssignRole(model.Email,model.Role.ToUpper());
			if (!assignrole)
			{
				_responseDto.IsSuccess = false;
				_responseDto.Message = "Incorrect credentials";
				return BadRequest(_responseDto);
			}
			_responseDto.Result = assignrole;
			return Ok(_responseDto);
		}
	}
}
