﻿using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
