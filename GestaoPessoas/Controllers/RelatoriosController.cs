using GestaoPessoas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoPessoas.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly GestaoPessoasContext _context;

        public RelatoriosController(GestaoPessoasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var desempenhoPorSetor = await _context.Setores
                .Select(s => new
                {
                    Setor = s.Nome,
                    QuantidadeFuncionarios = s.Funcionarios.Count(),
                    Media = s.Funcionarios
                        .SelectMany(f => f.Desempenhos)
                        .Average(d => (double?)d.PontuacaoObtida) ?? 0
                })
                .ToListAsync();

            
            var melhorFuncionario = await _context.Funcionarios
                .Select(f => new
                {
                    f.Nome,
                    Media = f.Desempenhos.Average(d => (double?)d.PontuacaoObtida) ?? 0
                })
                .OrderByDescending(f => f.Media)
                .FirstOrDefaultAsync();

            
            var melhorSetor = desempenhoPorSetor
                .OrderByDescending(s => s.Media)
                .FirstOrDefault();

           
            ViewBag.Labels = desempenhoPorSetor.Select(s => s.Setor).ToArray();
            ViewBag.Valores = desempenhoPorSetor.Select(s => s.Media).ToArray();

            
            ViewBag.DadosTabela = desempenhoPorSetor;

            ViewBag.MelhorFuncionario = melhorFuncionario;
            ViewBag.MelhorSetor = melhorSetor;

            return View();
        }
    }
}
