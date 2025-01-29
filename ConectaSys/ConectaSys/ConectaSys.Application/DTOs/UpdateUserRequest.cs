namespace ConectaSys.ConectaSys.Application.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace ConectaSys.ConectaSys.Application.DTOs.Users
    {
        public class UpdateUserRequest
        {
            [Required]
            public Guid UserId { get; set; }

            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public string? Role { get; set; }
            public string? Phone { get; set; }

            [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
            public string? Password { get; set; }
        }
    }

}
