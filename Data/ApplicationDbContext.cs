using System;
using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Data
{
    public class ApplicationDbContext : DbContext
    {
		public DbSet<SpeakerEval> SpeakerEvaluations { get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
            Database.EnsureCreated();
		}
    }
}
