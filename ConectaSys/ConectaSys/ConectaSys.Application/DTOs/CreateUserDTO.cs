using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CreateUserDTO
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public required string Password { get; set; }
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public required string Email { get; set; }
    public string? Role { get; set; }
    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    public string? Phone { get; set; }

  

}
