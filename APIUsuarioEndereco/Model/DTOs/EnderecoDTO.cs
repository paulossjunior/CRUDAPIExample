using System.ComponentModel.DataAnnotations;

namespace APIUsuarioEndereco.DTO;

public class EnderecoDTO
{
    [Required(ErrorMessage = "O logradouro é obrigatório")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "O número é obrigatório")]
    public string Numero { get; set; }

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O estado é obrigatório")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter 2 caracteres")]
    public string Estado { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório")]
    [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP inválido")]
    public string CEP { get; set; }
}