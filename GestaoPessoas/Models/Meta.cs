using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPessoas.Models
{
    public class Meta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A pontuação máxima é obrigatória.")]
        [Range(0,1000, ErrorMessage = "Informe uma pontuação válida")]
        public int PontuacaoMaxima { get; set; }

        // Relação com Setor e desempenho
        [Required]
        [Display(Name = "Setor")]
        public int SetorId { get; set; }

        [ForeignKey("SetorId")]
        public Setor? Setor { get; set; }

        public ICollection<Desempenho>? Desempenhos { get; set; }
    }
}
