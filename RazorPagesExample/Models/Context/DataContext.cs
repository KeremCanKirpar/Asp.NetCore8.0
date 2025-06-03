using System;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesExample.Models.Context;

public class DataContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = localhost, 1441; Database =RazorExamplesDb; User = sa; Password = YourStrong@Passw0rd; TrustServerCertificate = true");
    }
}
