using System;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Income> Income { get; set; }   
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Admission> Admission { get; set; }
    }
}
