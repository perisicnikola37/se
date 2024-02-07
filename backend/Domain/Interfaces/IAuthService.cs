using Contracts.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces;

public interface IAuthService
{
    Task<UserDto?> LogInUserAsync(LogInUserDto user);
    Task<ActionResult<UserDto>> RegisterUserAsync(User userRegistration);
    Task<bool> ForgotPasswordAsync(string userEmail);
    Task<bool> ResetPasswordAsync(string userEmail, string token, string newPassword);
}