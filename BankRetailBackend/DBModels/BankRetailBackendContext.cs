using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankRetailBackend.DBModels
{
    public partial class BankRetailBackendContext : DbContext
    {
        public BankRetailBackendContext()
        {
        }

        public BankRetailBackendContext(DbContextOptions<BankRetailBackendContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountStatus> AccountStatus { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatus { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Userstore> Userstore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:brb-server.database.windows.net,1433;Initial Catalog=BRBackend;Persist Security Info=False;User ID=brbadmin;Password=#Back-end!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__accountS__F267253E81432EA0");

                entity.ToTable("accountStatus");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.AccountType)
                    .HasColumnName("accountType")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("lastUpdated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.AccountStatus)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__accountSt__custo__6477ECF3");
            });

            modelBuilder.Entity<CustomerStatus>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__B611CB9D86E88BE7");

                entity.ToTable("customerStatus");

                entity.HasIndex(e => e.Ssn)
                    .HasName("SSN_idx");

                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("addressLine1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerAge).HasColumnName("customerAge");

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customerName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("lastUpdated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ssn).HasColumnName("SSN");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .HasColumnName("roleName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__transact__9B57CF527EE9BECB");

                entity.ToTable("transactions");

                entity.HasIndex(e => e.AccountId)
                    .HasName("transactions_accountID_idx");

                entity.Property(e => e.TransactionId).HasColumnName("transactionID");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.TransactionAmount).HasColumnName("transactionAmount");

                entity.Property(e => e.TransactionDate)
                    .HasColumnName("transactionDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TransactionType)
                    .HasColumnName("transactionType")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__transacti__accou__7C4F7684");
            });

            modelBuilder.Entity<Userstore>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__userstor__CB9A1CDF290743EE");

                entity.ToTable("userstore");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("lastLogin")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoginId)
                    .HasColumnName("loginID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pw)
                    .HasColumnName("pw")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("roleID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
