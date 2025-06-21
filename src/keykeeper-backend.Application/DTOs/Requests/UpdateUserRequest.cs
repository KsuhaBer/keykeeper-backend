using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs.Requests
{
    public class UpdateUserPasswordRequest
    {
        [Required]
        public int UserID {  get; init; }
        [Required, MinLength(8)]
        public string Password { get; init; } = default!;
    }
    public class UpdateUserInfoRequest
    {
        [Required]
        public int UserId { get; init; }

        [Required, MaxLength(100)]
        public string FirstName { get; init; } = default!;

        [Required, MaxLength(100)]
        public string LastName { get; init; } = default!;

        [Required, EmailAddress]
        public string Email { get; init; } = default!;

        [Phone]
        public string? PhoneNumber { get; init; }
    }
}
