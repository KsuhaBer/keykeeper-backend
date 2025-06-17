using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class CreateUserRequest
    {

        [Required, MaxLength(100)]
        public string FirstName { get; init; } = default!;

        [Required, MaxLength(100)]
        public string LastName { get; init; } = default!;

        [Required, EmailAddress]
        public string Email { get; init; } = default!;

        [Required, MinLength(8)]
        public string Password { get; init; } = default!;
    }
}
