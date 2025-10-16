using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GestaoPessoas.Data
{
    public class GestaoPessoasContextFactory : IDesignTimeDbContextFactory<GestaoPessoasContext>
    {
        public GestaoPessoasContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GestaoPessoasContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GestaoPessoasDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new GestaoPessoasContext(optionsBuilder.Options);
        }
    }
}
