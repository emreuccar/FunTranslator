using FunTranslator.Data.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FunTranslator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TranslateLog>? TranslateLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}