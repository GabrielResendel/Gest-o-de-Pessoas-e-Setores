using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPessoas.Models
{
    public class Desempenho
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A pontuação obtida é obrigatória.")]
        [Range(0, 1000, ErrorMessage = "Informe uma pontuação válida.")]
        [Display(Name = "Pontuação Obtida")]
        public int PontuacaoObtida { get; set; }

        
        [Required]
        [Display(Name = "Funcionário")]
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public Funcionario? Funcionario { get; set; }

        
        [Required]
        [Display(Name = "Meta")]
        public int? MetaId { get; set; }

        [ForeignKey("MetaId")]
        public Meta? Meta { get; set; }
    }
}
