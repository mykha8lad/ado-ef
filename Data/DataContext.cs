using ADO_EF_P12.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_P12.Data;

public class DataContext : DbContext
{
    public DbSet<Entity.Department> Departments { get; set; }
    public DbSet<Entity.Manager> Managers { get; set; }

    public DataContext() : base()
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder         
            .UseSqlServer(     
                @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ado-ef-p12;Integrated Security=True"
            );                 
    }
    // Microsoft SQL Server (SqlClient)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder                          
            .Entity<Manager>()                
            .HasOne(m => m.MainDep)           
            .WithMany(d => d.MainManagers)    
            .HasForeignKey(m => m.IdMainDep)  
            .HasPrincipalKey(d => d.Id);      
                                                                                            

        modelBuilder
            .Entity<Manager>()
            .HasOne(m => m.SecDep)
            .WithMany()
            .HasForeignKey(m => m.IdSecDep)
            .HasPrincipalKey(d => d.Id);
    }
}
