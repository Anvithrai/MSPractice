﻿namespace Mango.Services.AuthAPI.Models.Dto
{
	public class LoginResponseDTO
	{
		public UserDTO user { get; set; }
		public string Token { get; set; }
	}
}
