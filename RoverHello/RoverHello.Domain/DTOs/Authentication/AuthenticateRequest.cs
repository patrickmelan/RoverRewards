﻿using System.ComponentModel.DataAnnotations;

namespace RoverHello.Domain.DTOs.Authentication;

public class AuthenticateRequest
{
    [Required] public string Username { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}