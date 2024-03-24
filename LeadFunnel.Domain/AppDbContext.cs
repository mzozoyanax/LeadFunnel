using LeadFunnel.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=LeadFunnelDB.mssql.somee.com;packet size=4096;user id=Xhanti123_SQLLogin_1;pwd=skgctu7oo6;data source=LeadFunnelDB.mssql.somee.com;persist security info=False;initial catalog=LeadFunnelDB;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contacts> Contacts { get; set; }

        public DbSet<Survey> Survey { get; set; }

        public DbSet<TwilioCredential> TwilioCredentials { get; set; }
    }
}
