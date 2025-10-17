using System.ComponentModel.DataAnnotations;

namespace GestaoPessoas.Models
{
    public class Setor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do setor é obrigatorio")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A capacidade máxima de pessoas é obrigatória.")]
        [Display(Name = "Capacidade Máxima de Pessoas")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um valor maior que zero.")]
        public int CapacidadeMaximaPessoas  { get; set; }

        
        public ICollection<Funcionario>? Funcionarios { get; set; }
        public ICollection<Meta>? Metas { get; set; }

    }
}
