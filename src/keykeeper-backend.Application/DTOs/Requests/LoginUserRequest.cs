using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs.Requests
{
    public class LoginUserRequest
    {
        [Required, EmailAddress]
        public string Email { get; init; } = default!;

        [Required, MinLength(8)]
        public string Password { get; init; } = default!;
    }
}
