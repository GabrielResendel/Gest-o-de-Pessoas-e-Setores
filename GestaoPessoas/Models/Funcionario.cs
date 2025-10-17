using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPessoas.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do funcionário é obrigatorio")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Cargo do funcionário é obrigatorio")]
        [StringLength(100)]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O salario e obrigatorio")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "A data de admissão é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Admissão")]
        public DateTime DataAdmissao { get; set; }

        
        [Display(Name = "Setor")]
        public int SetorId { get; set; }

        [ForeignKey("SetorId")]
        public Setor? Setor { get; set; }

        
        public ICollection<Desempenho>? Desempenhos { get; set; }

    }
}
