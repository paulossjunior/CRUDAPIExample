using System.ComponentModel.DataAnnotations;

namespace APIUsuarioEndereco.DTO;

public class UsuarioDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
    
}