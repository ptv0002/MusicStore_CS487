using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MusicStore_Context: DbContext
    {
        public MusicStore_Context(DbContextOptions<MusicStore_Context> options) : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Instrument> Instruments { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=TCMS_DB;Integrated Security=True;MultipleActiveResultSets=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Instrument>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Instrument>()
                .Property(e => e.InstrumentType)
                .IsUnicode(false);

            modelBuilder.Entity<Instrument>()
                .Property(e => e.Model)
                .IsUnicode(false);
        }
    }
}
