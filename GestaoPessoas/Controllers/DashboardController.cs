using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoPessoas.Data;
using System.Linq;

namespace GestaoPessoas.Controllers
{
    public class DashboardController : Controller
    {
        private readonly GestaoPessoasContext _context;

        public DashboardController(GestaoPessoasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            ViewBag.TotalFuncionarios = _context.Funcionarios.Count();

            
            var pontuacoesFuncionarios = _context.Funcionarios
                .Include(f => f.Setor)
                .Include(f => f.Desempenhos)
                .Select(f => new
                {
                    Funcionario = f.Nome,
                    Setor = f.Setor != null ? f.Setor.Nome : "Sem setor",
                    PontosTotais = f.Desempenhos.Sum(d => (int?)d.PontuacaoObtida) ?? 0
                })
                .OrderByDescending(f => f.PontosTotais)
                .ToList();

            
            ViewBag.MelhorFuncionario = pontuacoesFuncionarios.FirstOrDefault()?.Funcionario ?? "N/A";

            
            var pontuacoesSetores = _context.Setores
                .Include(s => s.Funcionarios)
                .ThenInclude(f => f.Desempenhos)
                .Select(s => new
                {
                    Setor = s.Nome,
                    PontosTotais = s.Funcionarios
                        .SelectMany(f => f.Desempenhos)
                        .Sum(d => (int?)d.PontuacaoObtida) ?? 0
                })
                .OrderByDescending(s => s.PontosTotais)
                .ToList();


            
            ViewBag.MelhorSetor = pontuacoesSetores.FirstOrDefault()?.Setor ?? "N/A";

            
            ViewBag.DadosGrafico = pontuacoesSetores;

            
            ViewBag.FuncionariosPontuacao = pontuacoesFuncionarios;

            return View();
        }
    }
}
