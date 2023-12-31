using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GamingRoom.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<BusinessDetails> BusinessDetails { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<EventTitles> EventTitles { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Venues> Venues { get; set; }
        public virtual DbSet<Cart> Carts { get; set; } 
        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<UserCustomer> UserCustomers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<EventRequest> EventRequests { get; set; }

        public DbSet<TeamRequest> TeamRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tickets>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.BusinessDetails)
                .WithRequired(e => e.Users);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Events)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Players)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Teams)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.CreatedBy);
        }
    }
}

